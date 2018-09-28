using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sprache;

namespace Nexus
{
    public static class NexusParser
    {
        public static readonly Parser<IEnumerable<string>> Whitespaces =
            Parse.LineTerminator.Or(Parse.String(" ").Text()).Many();

        public static Parser<string> Identifier =>
            from start in Parse.Letter.Once().Text()
            from rest in Parse.LetterOrDigit.Or(Parse.Chars("_-")).Many().Text()
            select start + rest;
        
        public static readonly Parser<IExpression> QuotedText =
            from start in Parse.Char('"').Once()
            from text in Parse.AnyChar.Except(Parse.Char('"')).Many().Text()
            from end in Parse.Char('"').Once()
            select new Text {Value = text};

        public static readonly Parser<string> IntegerSuffix =
            from start in Parse.Char('_').Once()
            from sign in Parse.Chars("iu").Once()
            from length in Parse.String("8")
                .Or(Parse.String("16"))
                .Or(Parse.String("32"))
                .Or(Parse.String("64"))
                .Text()
            select start.ToString() + sign + length;

        public static readonly Parser<string> RealSuffix =
            Parse.IgnoreCase('f')
                .Or(Parse.IgnoreCase('d'))
                .Select(i => i.ToString())
                .Or(Parse.Char('_').Then(i => Parse.IgnoreCase('f').Then(j => Parse.Number)));

        public static Parser<string> IntegerPart =>
            from first in Parse.Numeric
            from rest in
                (from comma in Parse.Char(',')
                    from number in Parse.Numeric
                    select number)
                .Or(Parse.Numeric).Many()
            select first + new string(rest.ToArray());

        public static readonly Parser<NumberLiteral> Integer =
            from sign in Parse.Chars("-+").Named("sign").Optional()
            from integerPart in IntegerPart.Named("integer part")
            from suffix in IntegerSuffix.Optional().Named("suffix")
            select NumberLiteral.Parse(sign.GetOrElse('+'), integerPart, null, suffix.GetOrDefault());

        public static readonly Parser<NumberLiteral> Real =
            from sign in Parse.Chars("-+").Named("sign").Optional()
            from integerPart in IntegerPart.Named("integer part")
            from dot in Parse.Char('.').Named("decimal ")
            from decimalPart in IntegerPart.Named("decimal part").Optional()
            from suffix in RealSuffix.Named("suffix").Optional()
            select NumberLiteral.Parse(sign.GetOrElse('+'), integerPart, decimalPart.GetOrElse("0"), suffix.GetOrDefault());

        public static Parser<NumberLiteral> Binary =>
            from start in Parse.IgnoreCase('b')
            from x in Parse.Char('x')
            from bits in Parse.Chars("01").AtLeastOnce().Text()
            select NumberLiteral.ParseBinary(bits);

        public static Parser<NumberLiteral> Number =>
            Real
                .Or(Integer)
                .Or(Binary);

        public static Parser<IExpression> RangeIndex =>
            Number
                .Or(ArrayAccessLiteral)
                .Or(VariableLiteral);

        public static Parser<RangeLiteral> RangeLiteral =>
            //from begin in Parse.Char('[').Shift()
            from start in RangeIndex.Shift()
            from dots in Parse.String("until").Shift()
            from end in RangeIndex.Shift()
            //from stop in Parse.Char(']').Shift()
            select new RangeLiteral
            {
                Start = start,
                End = end
            };

        public static Parser<BooleanLiteral> BooleanLiteral =>
            from value in Parse.String("false").Or(Parse.String("true")).Text()
            select new BooleanLiteral
            {
                Value = value == "false" ? false : true
            };

        public static Parser<IExpression> VarFactor =>
            QuotedText
                .Or(RangeLiteral)
                .Or(BooleanLiteral)
                .Or(Number)
                .Or(ArrayAccessLiteral)
                .Or(VariableLiteral);

        public static Parser<IExpression> Factor =>
                // expression inside parentheses
                (from leftParentheses in Parse.Char('(')
                    from expr in Expression
                    from rightParentheses in Parse.Char(')').Token()
                    select expr)
                // tuple
                .Or(from begin in Parse.Char('(')
                    from values in Expression.Shift().DelimitedBy(Parse.Char(','))
                    from end in Parse.Char(')').Shift()
                    select new Tuple{Values = values.ToList()})
                // array
                .Or(from begin in Parse.Char('[')
                    from values in Expression.Shift().DelimitedBy(Parse.Char(','))
                    from end in Parse.Char(']').Shift()
                    select new ArrayLiteral
                    {
                        Values = values.ToList()
                    })
                .Or(VarFactor);

        public static readonly Parser<TypeDefinition> Type =
            (from type in Identifier.Named("variable type")
                from array in Parse.String("[]").Shift().Many().Optional()
                select new TypeDefinition(type, array.GetOrElse(new List<List<char>>()).Count()))
            .Or(from start in Parse.Char('(').Shift()
                from types in Type.Shift().DelimitedBy(Parse.Char(','))
                from end in Parse.Char(')').Shift()
                from array in Parse.String("[]").Shift().Many().Optional()
                select TypesExtension.CreateTupleTypeDefinition(types, array.GetOrElse(new List<List<char>>()).Count()));

        public static readonly Parser<IExpression> Assignment =
            from equalSign in Parse.Char('=')
            from expression in Expression.Shift()
            select expression;

        public static readonly Parser<IStatement> Variable =
            from type in Type.Shift()
            from name in Identifier.Shift().Named("variable name")
            from setter in Parse.String("set").Shift().Text().Optional()
            from getter in Parse.String("get").Shift().Text().Optional()
            from assignment in Assignment.Shift().Optional().Named("variable initialization")
            select new Variable
            {
                Type = type,
                Name = name,
                Setter = setter.GetOrDefault() != null,
                Getter = getter.GetOrDefault() != null,
                Initialization = assignment
            };

        public static Parser<IStatement> VariableStatement =>
            from type in Type.Shift()
            from name in Identifier.Shift().Named("variable name")
            from assignment in Assignment.Shift().Optional().Named("variable initialization")
            from colon in Parse.Char(';').Shift()
            select new Variable
            {
                Type = type,
                Name = name,
                Initialization = assignment.GetOrDefault(),
                Getter = false,
                Setter = false
            };

        public static Parser<IStatement> AssignmentStatementDefinition =>
            from lvalue in Factor
            from rvalue in Assignment.Shift()
            select new AssignmentStatement{LValue = lvalue, RValue = rvalue};

        public static Parser<IStatement> AssigmnentStatement =>
            from assignment in AssignmentStatementDefinition.Shift()
            from colon in Parse.Char(';').Shift()
            select assignment;

        public static Parser<IStatement> FunctionCallStatementDefinition =>
            from name in Identifier
            from lparen in Parse.Char('(').Shift()
            from parameter in Expression.DelimitedBy(Parse.Char(',')).Optional()
            from rparen in Parse.Char(')').Shift()
            select new FunctionCallStatement
            {
                Name = name,
                Parameter = parameter.GetOrElse(new List<IExpression>()).ToList()
            };

        public static Parser<IStatement> FunctionCallStatement =>
            from call in FunctionCallStatementDefinition.Shift()
            from colon in Parse.Char(';').Shift()
            select call;

        public static Parser<IStatement> ReturnStatement =>
            from keyword in Parse.String("return").Text()
            from value in Expression
            from colon in Parse.Char(';').Shift()
            select new ReturnStatement {Value = value};

        public static Parser<IStatement> WhileStatement =>
            from keyword in Parse.String("while").Text().Shift()
            from lparen in Parse.Char('(').Shift()
            from condition in Comparison.Shift()
            from rparen in Parse.Char(')').Shift()
            from bodyBegin in Parse.Char('{').Shift()
            from body in FunctionStatement.Many().Shift()
            from bodyEnd in Parse.Char('}').Shift()
            select new WhileStatement
            {
                Condition = condition,
                Body = body.ToList()
            };

        public static Parser<IStatement> IfStatement =>
            from keywordIf in Parse.String("if").Shift()
            from lparenIf in Parse.Char('(').Shift()
            from condition in Comparison.Shift()
            from rparenIf in Parse.Char(')').Shift()
            from thenBegin in Parse.Char('{').Shift()
            from thenBody in FunctionStatement.Many()
            from thenEnd in Parse.Char('}').Shift()
            from elseBody in
                (from keywordElse in Parse.String("else").Shift()
                from elseBegin in Parse.Char('{').Shift()
                from elseBody in FunctionStatement.Many()
                from elseEnd in Parse.Char('}').Shift()
                select elseBody.ToList()).Optional()
            select new IfStatement
            {
                Condition = condition,
                Then = thenBody.ToList(),
                Else = elseBody.GetOrDefault()
            };

        public static Parser<IStatement> ForInitialization =>
            VariableStatement
                .Or(AssigmnentStatement)
                .Or(FunctionCallStatement)
                .Or(from colon in Parse.Char(';').Shift()
                    select new EmptyStatement());

        public static Parser<IStatement> ForStep =>
            AssignmentStatementDefinition
                .Or(FunctionCallStatementDefinition);

        public static Parser<IStatement> ForStatement =>
            from keyword in Parse.String("for")
            from lparen in Parse.Char('(').Shift()
            from initalization in ForInitialization.Shift()
            from condition in Comparison.Shift().Optional()
            from colon2 in Parse.Char(';').Shift()
            from step in ForStep.Shift().Optional()
            from rparen in Parse.Char(')').Shift()
            from begin in Parse.Char('{').Shift()
            from body in FunctionStatement.Many()
            from end in Parse.Char('}').Shift()
            select new ForStatement
            {
                Initialization = initalization,
                Condition = condition.GetOrDefault(),
                Step = step.GetOrDefault(),
                Body = body.ToList()
            };

        public static Parser<IStatement> FunctionStatement =>
            Function.Shift()
                .Or(VariableStatement.Shift())
                .Or(AssigmnentStatement.Shift())
                .Or(FunctionCallStatement.Shift())
                .Or(IfStatement.Shift())
                .Or(ForStatement.Shift())
                .Or(WhileStatement.Shift())
                .Or(ReturnStatement.Shift());

        public static Parser<IStatement> FunctionParameter =>
            from type in Type.Shift()
            from name in Identifier.Shift().Named("variable name")
            from assignment in Assignment.Shift().Optional().Named("variable initialization")
            select new Variable
            {
                Type = type,
                Name = name,
                Setter = false,
                Getter = false,
                Initialization = assignment.GetOrDefault()
            };

        public static readonly Parser<IStatement> Function =
            from returnType in Type.Named("function return value")
            from name in Identifier.Shift().Named("function name")
            from parametersBegin in Parse.Char('(').Shift().Named("function parameters begin")
            from parameters in FunctionParameter.Shift().DelimitedBy(Parse.Char(',')).Optional().Named("function parameters")
            from parametersEnd in Parse.Char(')').Shift().Named("function parameters end")
            from bodyBegin in Parse.Char('{').Shift().Named("function body begin")
            from body in FunctionBody.Named("function body").Many()
            from bodyEnd in Parse.Char('}').Shift().Named("function body end")
            select new Function
            {
                Name = name,
                ReturnType = returnType,
                Parameters = parameters.GetOrElse(new List<IStatement>()).Select(i => (Variable) i).ToList(),
                Body = body.ToList()
            };

        public static Parser<IStatement> ClassMembers =>
            Function.Named("class function").Shift()
                .Or(Variable
                    .Then(member => Parse.Char(';')
                        .Named("class member delimiter ';'")
                        .Shift()
                        .Select(_ => member))
                    .Named("class variable"))
                .Shift();

        public static Parser<IEnumerable<IStatement>> ClassDefinition =>
            ClassMembers.Named("class member").Many();

        public static Parser<Class> Class =>
            from begin in Parse.String("class").Named("'class' keyword")
            from name in Identifier.Named("class name").Shift()
            from start in Parse.Char('{').Named("class definition start '{'").Shift()
            from definition in ClassDefinition.Named("class member")
            from end in Parse.Char('}').Named("class definition end '}'").Shift()
            select new Class {Name = name, Members = definition.ToList()};

        public static Parser<IExpression> VariableLiteral =>
            from identifier in Identifier
            select new VariableLiteral
            {
                Name = identifier
            };

        public static Parser<IExpression> ArrayAccessLiteral =>
            from identifier in Identifier
            from lparen in Parse.Char('[').Shift().Named("left parentheses")
            from index in Expression.Shift().Named("index")
            from rparen in Parse.Char(']').Shift()
            select new ArrayAccessLiteral
            {
                Name = identifier,
                Index = index
            };

        public static Parser<IExpression> Term =>
               (from left in Factor.Shift()
                from op in Parse.Char('+').Shift()
                from right in Term.Shift()
                select new Expression { LeftExpression = left, Type = ExpressionType.Add, RightExpression = right })
               .Or(from left in Factor.Shift()
                   from op in Parse.Char('-').Shift()
                   from right in Term.Shift()
                   select new Expression { LeftExpression = left, Type = ExpressionType.Subtract, RightExpression = right })
               .Or(Factor.Shift());

        public static Parser<IExpression> Expression =>
            (from left in Term.Shift()
                from op in Parse.Char('*').Shift()
                from right in Expression.Shift()
                select new Expression
                    {LeftExpression = left, Type = ExpressionType.Multiply, RightExpression = right})
            .Or(from left in Term.Shift()
                from op in Parse.Char('/').Shift()
                from right in Expression.Shift()
                select new Expression
                    {LeftExpression = left, Type = ExpressionType.Divide, RightExpression = right})
            .Or(Term.Shift());

        public static Parser<IExpression> Comparison =>
            from lhs in Expression.Named("comparison left side")
            from op in Parse.Chars("=<>!").Many().Text().Shift()
            from rhs in Expression.Named("comparison right side").Shift()
            select new Comparison(lhs, op, rhs);

        public static Parser<T> Shift<T>(this Parser<T> parser) => Whitespaces.Then(_ => parser).Or(parser);
    }

    internal static class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Input file not set");
                return 1;
            }
            
            var content = File.ReadAllText(args[0]);
            var parsedContent = NexusParser.Class.Parse(content);
            var printer = new Printer(Console.Out);
            var compilationUnit = new CompilationUnit(parsedContent);

            compilationUnit.ToHeader(printer);
            compilationUnit.ToSource(printer);

            return 0;
        }
    }
}

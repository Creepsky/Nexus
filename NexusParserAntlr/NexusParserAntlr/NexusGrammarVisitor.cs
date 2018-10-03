using System;
using System.Linq;
using NexusParserAntlr.ir;

namespace NexusParserAntlr
{
    public class NexusGrammarVisitor : NexusBaseVisitor<object>
    {
        public override object VisitFile(NexusParser.FileContext context)
        {
            var list = context.file_declaration().Select(Visit).ToList();

            return new File
            {
                Classes = list.Where(i => i.GetType() == typeof(Class)).Select(i => (Class) i).ToList(),
                ExtensionFunctions = list.Where(i => i.GetType() == typeof(ExtensionFunction)).Select(i => (ExtensionFunction) i).ToList()
            };
        }

        public override object VisitClass(NexusParser.ClassContext context)
        {
            var member = context.class_member().Select(Visit).ToList();

            return new Class
            {
                Name = context.name.Text,
                Variables = member.Where(i => i.GetType() == typeof(Variable)).Select(i => (Variable) i).ToList(),
                Functions = member.Where(i => i.GetType() == typeof(Function)).Select(i => (Function) i).ToList()
            };
        }

        public override object VisitNamedType(NexusParser.NamedTypeContext context) => new SimpleType
        {
            Name = context.IDENTIFIER().GetText(),
            Array = context.ARRAY_DECLARATION().Length
        };

        public override object VisitTupleType(NexusParser.TupleTypeContext context) => new TupleType
        {
            Types = context.type().Select(i => (IType) Visit(i)).ToList(),
            Array = context.ARRAY_DECLARATION().Length
        };

        public override object VisitMapType(NexusParser.MapTypeContext context) => new MapType
        {
            KeyType = (IType) Visit(context.key),
            ValueType = (IType) Visit(context.value),
            Array = context.ARRAY_DECLARATION().Length
        };

        public override object VisitVariable_declaration(NexusParser.Variable_declarationContext context) => new Variable
        {
            Type = (IType) Visit(context.type()),
            Name = context.IDENTIFIER().GetText(),
            Getter = context.GET() != null,
            Setter = context.SET() != null,
            Initialization = context.expression() == null ? null : (IExpression) Visit(context.expression())
        };

        public override object VisitFunction_parameter(NexusParser.Function_parameterContext context) => new Variable
        {
            Type = (IType) Visit(context.type()),
            Name = context.IDENTIFIER().GetText(),
            Setter = false,
            Getter = false,
            Initialization = context.expression() == null ? null : (IExpression) Visit(context.expression())
        };

        public override object VisitFunction_declaration(NexusParser.Function_declarationContext context) => new Function
        {
            Type = (IType) Visit(context.type()),
            Name = context.IDENTIFIER().GetText(),
            Parameter = context.function_parameter().Select(i => (Variable) Visit(i)).ToList(),
            Statements = context.function_body().function_body_statement().Select(i => (IStatement) Visit(i))
                .ToList()
        };

        public override object VisitAssignment_statement(NexusParser.Assignment_statementContext context) => new AssignmentStatement
        {
            Left = (IExpression) Visit(context.left),
            Right = (IExpression) Visit(context.right)
        };

        public override object VisitReturn_statement(NexusParser.Return_statementContext context) => new ReturnStatement
        {
            Value = (IExpression) Visit(context.expression())
        };

        public override object VisitVariable_statement(NexusParser.Variable_statementContext context) => new Variable
        {
            Type = (IType) Visit(context.type()),
            Name = context.IDENTIFIER().GetText(),
            Setter = false,
            Getter = false,
            Initialization = context.expression() == null ? null : (IExpression) Visit(context.expression())
        };

        public override object VisitTuple_explosion_statement(NexusParser.Tuple_explosion_statementContext context) => new TupleExplosionStatement
        {
            Names = context.IDENTIFIER().Skip(1).Select(i => i.GetText()).ToList(),
            Right = (IExpression) Visit(context.tuple_explosion_expression())
        };

        public override object VisitIf_statement(NexusParser.If_statementContext context) => new IfStatement
        {
            Condition = (ICondition) Visit(context.comparison()),
            Then = context.then.function_body_statement()
                .Select(i => (IStatement) Visit(i))
                .ToList(),
            Else = context.@else.function_body_statement()
                .Select(i => (IStatement) Visit(i))
                .ToList()
        };

        public override object VisitWhile_statement(NexusParser.While_statementContext context) => new WhileStatement
        {
            Condition = (ICondition) Visit(context.comparison()),
            Body = context.function_body().function_body_statement().Select(i => (IStatement) Visit(i)).ToList()
        };

        public override object VisitFor_statement(NexusParser.For_statementContext context) => new ForStatement
        {
            Start = (IStatement) Visit(context.for_init()),
            Stop = (ICondition) Visit(context.comparison()),
            Step = (IExpression) Visit(context.expression()),
            Body = context.function_body().function_body_statement().Select(i => (IStatement) Visit(i)).ToList()
        };

        public override object VisitFunction_call(NexusParser.Function_callContext context) => new FunctionCall
        {
            Name = context.IDENTIFIER().GetText(),
            Parameter = context.expression().Select(i => (IExpression) Visit(i)).ToList()
        };

        public override object VisitFunction_body_statement(NexusParser.Function_body_statementContext context)
        {
            if (context.function_call() != null)
                return new FunctionCallStatement {FunctionCall = (FunctionCall) VisitFunction_call(context.function_call())};

            return base.VisitFunction_body_statement(context);
        }

        public override object VisitDiv(NexusParser.DivContext context) => new BinaryOperation
        {
            Left = (IExpression) Visit(context.expression(0)),
            Type = BinaryOperatorType.Div,
            Right = (IExpression) Visit(context.expression(1)),
        };

        public override object VisitAdd(NexusParser.AddContext context) => new BinaryOperation
        {
            Left = (IExpression) Visit(context.expression(0)),
            Type = BinaryOperatorType.Add,
            Right = (IExpression) Visit(context.expression(1)),
        };

        public override object VisitTuple(NexusParser.TupleContext context)
        {
            return new TupleLiteral
            {
                Values = context.expression().Select(i => (IExpression) Visit(i)).ToList()
            };
        }

        public override object VisitSub(NexusParser.SubContext context) => new BinaryOperation
        {
            Left = (IExpression) Visit(context.expression(0)),
            Type = BinaryOperatorType.Sub,
            Right = (IExpression) Visit(context.expression(1)),
        };

        public override object VisitParen(NexusParser.ParenContext context) => Visit(context.expression());

        public override object VisitArray(NexusParser.ArrayContext context) => new ArrayLiteral
        {
            Values = context.expression().Select(i => (IExpression) Visit(i)).ToList()
        };

        public override object VisitMul(NexusParser.MulContext context) => new BinaryOperation
        {
            Left = (IExpression) Visit(context.expression(0)),
            Type = BinaryOperatorType.Mul,
            Right = (IExpression) Visit(context.expression(1)),
        };

        public override object VisitRange(NexusParser.RangeContext context) => new RangeLiteral
        {
            Start = (IExpression) Visit(context.expression(0)),
            End = (IExpression) Visit(context.expression(1)),
        };

        public override object VisitMap(NexusParser.MapContext context) => new MapLiteral
        {
            Values = context.key_value_pair().ToDictionary(
                i => (IExpression) Visit(i),
                j => (IExpression) Visit(j)
            )
        };

        public override object VisitBoolean(NexusParser.BooleanContext context) => new BooleanLiteral
        {
            Value = context.TRUE() != null
        };

        public override object VisitArray_access(NexusParser.Array_accessContext context) => new ArrayAccess
        {
            Name = context.IDENTIFIER().GetText(),
            Index = (IExpression) Visit(context.expression())
        };

        public override object VisitNumber(NexusParser.NumberContext context)
        {
            if (context.INTEGER() != null)
                return NumberLiteral.Parse(
                    context.INTEGER().GetText(),
                    null,
                    context.INTEGER_SUFFIX()?.GetText()
                );

            if (context.REAL() != null)
            {
                return null;
            }

            return null;
        }

        public override object VisitQuoted_text(NexusParser.Quoted_textContext context) => new Text
        {
            Value = context.text.Text
        };

        public override object VisitFactor(NexusParser.FactorContext context)
        {
            if (context.number() != null)
                return VisitNumber(context.number());

            if (context.boolean() != null)
                return VisitBoolean(context.boolean());

            if (context.BINARY() != null)
                return NumberLiteral.ParseBinary(context.BINARY().GetText().Substring(2));

            if (context.HEX() != null)
                return NumberLiteral.ParseHex(context.HEX().GetText().Substring(2));

            if (context.quoted_text() != null)
                return new Text { Value = context.quoted_text().text == null ? string.Empty : context.quoted_text().text.Text };

            if (context.IDENTIFIER() != null)
                return new VariableLiteral { Name = context.IDENTIFIER().GetText() };

            return base.VisitFactor(context);
        }

        public override object VisitComparison(NexusParser.ComparisonContext context)
        {
            ComparisonType type;

            if (context.EQUAL() != null)
                type = ComparisonType.Equals;
            else if (context.GREATER() != null)
                type = ComparisonType.Greater;
            else if (context.LESS() != null)
                type = ComparisonType.Less;
            else if (context.GREATER_EQUAL() != null)
                type = ComparisonType.GreaterEquals;
            else if (context.LESS_EQUAL() != null)
                type = ComparisonType.LessEquals;
            else
                throw new ArgumentOutOfRangeException("unknown comparison", null as Exception);

            return new Comparison
            {
                Left = (IExpression) Visit(context.expression(0)),
                Type = type,
                Right = (IExpression) Visit(context.expression(1)),
            };
        }

        public override object VisitExtension_function(NexusParser.Extension_functionContext context) => new ExtensionFunction
        {
            ReturnType = (IType) Visit(context.type()),
            Class = context.className.Text,
            Name = context.name.Text,
            Parameter = context.function_parameter().Select(i => (Variable) Visit(i)).ToList(),
            Body = context.function_body().function_body_statement().Select(i => (IStatement) Visit(i)).ToList()
        };
    }
}

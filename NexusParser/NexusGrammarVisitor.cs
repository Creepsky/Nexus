using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Nexus.common;
using Nexus.ir;
using Nexus.ir.expr;
using Nexus.ir.stmt;
using System;
using System.Linq;

namespace Nexus
{
    public class NexusGrammarVisitor : NexusBaseVisitor<object>
    {
        public override object VisitFile(NexusParser.FileContext context)
        {
            var list = context.file_declaration().Select(Visit).ToList();

            return new File
            {
                Classes = list.OfType<Class>().ToList(),
                ExtensionFunctions = list.OfType<ExtensionFunction>().ToList()
            };
        }

        public override object VisitClass_declaration(NexusParser.Class_declarationContext context)
        {
            var member = context.class_member().Select(Visit).ToList();

            return new Class(context.name.Text,
                context.template_list_declaration() == null ? null : (ITemplateList) Visit(context.template_list_declaration()),
                member.OfType<Variable>().ToList(),
                member.OfType<CppBlockStatement>().ToList(),
                member.OfType<Include>().ToList())
            {
                Line = context.Start.Line,
                Column = context.Start.Column
            };
        }

        public override object VisitClass_member(NexusParser.Class_memberContext context)
        {
            if (context.CPP_BLOCK() != null)
            {
                return new CppBlockStatement(ExtractCppBlock(context.CPP_BLOCK().GetText(), context.Start));
            }

            if (context.include() != null)
            {
                return Visit(context.include());
            }

            return Visit(context.variable_declaration());
        }

        public override object VisitFarInclude(NexusParser.FarIncludeContext context)
        {
            return new Include(context.path.Text);
        }

        public override object VisitNearInclude(NexusParser.NearIncludeContext context)
        {
            return new Include(context.path.Text);
        }

        public override object VisitNamedType(NexusParser.NamedTypeContext context) => new SimpleType(
            context.IDENTIFIER().GetText(),
            context.ARRAY_DECLARATION().Length,
            context.Start.Line,
            context.Start.Column);

        public override object VisitTupleType(NexusParser.TupleTypeContext context) => new TupleType
        {
            Types = context.type().Select(i => (IType) Visit(i)).ToList(),
            Array = context.ARRAY_DECLARATION().Length,
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitMapType(NexusParser.MapTypeContext context) => new MapType
        {
            KeyType = (IType) Visit(context.key),
            ValueType = (IType) Visit(context.value),
            Array = context.ARRAY_DECLARATION().Length,
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitVariable_declaration(NexusParser.Variable_declarationContext context) => new Variable
        {
            Type = (IType) Visit(context.type()),
            Name = context.IDENTIFIER().GetText(),
            Initialization = context.expression() == null ? null : (IExpression) Visit(context.expression()),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitFunction_parameter(NexusParser.Function_parameterContext context) => new Variable
        {
            Type = (IType) Visit(context.type()),
            Name = context.IDENTIFIER().GetText(),
            Initialization = context.expression() == null ? null : (IExpression) Visit(context.expression()),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitFunction_declaration(NexusParser.Function_declarationContext context) => new Function
        {
            Type = (IType) Visit(context.type()),
            Name = context.IDENTIFIER().GetText(),
            TemplateList = context.template_list_declaration() != null ?
                (ITemplateList) Visit(context.template_list_declaration()) : null,
            Parameter = context.function_parameter().Select(i => (Variable) Visit(i)).ToList(),
            Statements = context.function_body().function_body_statement().Select(i => (IStatement) Visit(i))
                .ToList(),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitAssignment_statement(NexusParser.Assignment_statementContext context) => new AssignmentStatement
        {
            Left = (IExpression) Visit(context.left),
            Right = (IExpression) Visit(context.right),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitReturn_statement(NexusParser.Return_statementContext context) => new ReturnStatement
        {
            Value = (IExpression) Visit(context.expression()),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitVariable_statement(NexusParser.Variable_statementContext context) => new Variable
        {
            Type = (IType) Visit(context.type()),
            Name = context.IDENTIFIER().GetText(),
            Initialization = context.expression() == null ? null : (IExpression) Visit(context.expression()),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitTuple_explosion_statement(NexusParser.Tuple_explosion_statementContext context) => new TupleExplosionStatement
        {
            Names = context.IDENTIFIER().Skip(1).Select(i => i.GetText()).ToList(),
            Right = (IExpression) Visit(context.expression()),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitIf_statement(NexusParser.If_statementContext context) => new IfStatement
        {
            Condition = (ICondition) Visit(context.comparison()),
            Then = context.then.function_body_statement()
                .Select(i => (IStatement) Visit(i))
                .ToList(),
            Else = context.otherwise.function_body_statement()
                .Select(i => (IStatement) Visit(i))
                .ToList(),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitWhile_statement(NexusParser.While_statementContext context) => new WhileStatement
        {
            Condition = (ICondition) Visit(context.comparison()),
            Body = context.function_body().function_body_statement().Select(i => (IStatement) Visit(i)).ToList(),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitFor_statement(NexusParser.For_statementContext context) => new ForStatement
        {
            Start = (IStatement) Visit(context.for_init()),
            Stop = (ICondition) Visit(context.comparison()),
            Step = (IExpression) Visit(context.expression()),
            Body = context.function_body().function_body_statement().Select(i => (IStatement) Visit(i)).ToList(),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitFunction_call(NexusParser.Function_callContext context) => new FunctionCall
        {
            Name = context.IDENTIFIER().GetText(),
            Parameter = context.expression().Select(i => (IExpression) Visit(i)).ToList(),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitExtension_function_call(NexusParser.Extension_function_callContext context)
        {
            return new ExtensionFunctionCall
            {
                Variable = new VariableLiteral{Name = context.IDENTIFIER().GetText()},
                FunctionCall = (FunctionCall) Visit(context.function_call()),
                Line = context.Start.Line,
                Column = context.Start.Column
            };
        }

        public override object VisitFunction_body_statement(NexusParser.Function_body_statementContext context)
        {
            if (context.function_call() != null)
            {
                return new FunctionCallStatement
                {
                    FunctionCall = (FunctionCall) VisitFunction_call(context.function_call()),
                    Line = context.Start.Line,
                    Column = context.Start.Column
                };
            }

            if (context.CPP_BLOCK() != null)
            {
                return new CppBlockStatement(ExtractCppBlock(context.CPP_BLOCK().GetText(), context.Start));
            }

            return base.VisitFunction_body_statement(context);
        }

        public override object VisitDiv(NexusParser.DivContext context) => new BinaryOperation
        {
            Left = (IExpression) Visit(context.expression(0)),
            Type = BinaryOperatorType.Div,
            Right = (IExpression) Visit(context.expression(1)),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitAdd(NexusParser.AddContext context) => new BinaryOperation
        {
            Left = (IExpression) Visit(context.expression(0)),
            Type = BinaryOperatorType.Add,
            Right = (IExpression) Visit(context.expression(1)),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitTuple(NexusParser.TupleContext context) => new TupleLiteral
        {
            Values = context.expression().Select(i => (IExpression) Visit(i)).ToList(),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitSub(NexusParser.SubContext context) => new BinaryOperation
        {
            Left = (IExpression) Visit(context.expression(0)),
            Type = BinaryOperatorType.Sub,
            Right = (IExpression) Visit(context.expression(1)),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitParen(NexusParser.ParenContext context) => new ParenExpression
        {
            Expression = (IExpression)Visit(context.expression()),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitArray(NexusParser.ArrayContext context) => new ArrayLiteral
        {
            Values = context.expression().Select(i => (IExpression) Visit(i)).ToList(),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitMul(NexusParser.MulContext context) => new BinaryOperation
        {
            Left = (IExpression) Visit(context.expression(0)),
            Type = BinaryOperatorType.Mul,
            Right = (IExpression) Visit(context.expression(1)),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitRange(NexusParser.RangeContext context) => new RangeLiteral
        {
            Start = (IExpression) Visit(context.expression(0)),
            End = (IExpression) Visit(context.expression(1)),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitCppBlock(NexusParser.CppBlockContext context)
        {
            return ExtractCppBlock(context.GetText(), context.Start);
        }

        public override object VisitMap(NexusParser.MapContext context) => new MapLiteral
        {
            Values = context.key_value_pair().ToDictionary(
                i => (IExpression) Visit(i.expression(0)),
                i => (IExpression) Visit(i.expression(1))
            ),
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitTruth_value(NexusParser.Truth_valueContext context) => new BooleanLiteral
        {
            Value = context.TRUE() != null,
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitNumber(NexusParser.NumberContext context)
        {
            if (context.INTEGER() != null)
            {
                return NumberLiteral.Parse(
                    context.INTEGER().GetText(),
                    null,
                    context.INTEGER_SUFFIX()?.GetText(),
                    context.Start.Line,
                    context.Start.Column
                );
            }

            if (context.REAL() != null)
            {
                return null;
            }

            return null;
        }

        public override object VisitQuoted_text(NexusParser.Quoted_textContext context) => new Text
        {
            Value = context.text.Text,
            Line = context.Start.Line,
            Column = context.Start.Column
        };

        public override object VisitFactor(NexusParser.FactorContext context)
        {
            if (context.number() != null)
            {
                return VisitNumber(context.number());
            }

            if (context.truth_value() != null)
            {
                return VisitTruth_value(context.truth_value());
            }

            if (context.BINARY() != null)
            {
                return NumberLiteral.ParseBinary(context.BINARY().GetText().Substring(2), context.Start.Line, context.Start.Column);
            }

            if (context.HEX() != null)
            {
                return NumberLiteral.ParseHex(context.HEX().GetText().Substring(2), context.Start.Line, context.Start.Column);
            }

            if (context.quoted_text() != null)
            {
                return new Text
                {
                    Value = context.quoted_text().text == null ? string.Empty : context.quoted_text().text.Text,
                    Line = context.Start.Line,
                    Column = context.Start.Column
                };
            }

            if (context.IDENTIFIER() != null)
            {
                return new VariableLiteral
                {
                    Name = context.IDENTIFIER().GetText(),
                    Line = context.Start.Line,
                    Column = context.Start.Column
                };
            }

            return base.VisitFactor(context);
        }

        public override object VisitComparison(NexusParser.ComparisonContext context)
        {
            ComparisonType type;

            if (context.EQUAL() != null)
            {
                type = ComparisonType.Equals;
            }
            else if (context.GREATER() != null)
            {
                type = ComparisonType.Greater;
            }
            else if (context.LESS() != null)
            {
                type = ComparisonType.Less;
            }
            else if (context.GREATER_EQUAL() != null)
            {
                type = ComparisonType.GreaterEquals;
            }
            else if (context.LESS_EQUAL() != null)
            {
                type = ComparisonType.LessEquals;
            }
            else
            {
                throw new ArgumentOutOfRangeException("unknown comparison", null as Exception);
            }

            return new Comparison
            {
                Left = (IExpression) Visit(context.expression(0)),
                Type = type,
                Right = (IExpression) Visit(context.expression(1)),
                Line = context.Start.Line,
                Column = context.Start.Column
            };
        }

        public override object VisitExtension_function(NexusParser.Extension_functionContext context)
        {
            var function = context.@operator() != null ? new OperatorFunction() : new ExtensionFunction();

            function.ReturnType = (IType) Visit(context.returnType);
            function.ExtensionBase = (IType) Visit(context.extensionType);
            function.TemplateList = context.template_list_declaration() != null
                ? (ITemplateList) Visit(context.template_list_declaration())
                : null;
            function.Parameter = context.function_parameter().Select(i => (Variable) Visit(i)).ToList();
            function.Body = context.function_body().function_body_statement().Select(i => (IStatement) Visit(i)).ToList();
            function.Line = context.Start.Line;
            function.Column = context.Start.Column;

            function.SetName(
                function.GetType() == typeof(OperatorFunction)
                ? context.@operator().GetText()
                : context.IDENTIFIER().GetText());

            return function;
        }

        public override object VisitTemplate_list_declaration(NexusParser.Template_list_declarationContext context)
        {
            if (context.template_list() != null)
            {
                return Visit(context.template_list());
            }

            if (context.variadic_template() != null)
            {
                return Visit(context.variadic_template());
            }

            return base.VisitTemplate_list_declaration(context);
        }

        public override object VisitTemplate_list_usage(NexusParser.Template_list_usageContext context)
        {
            return Visit(context.template_list());
        }

        public override object VisitTemplate_list(NexusParser.Template_listContext context)
        {
            return new TemplateList(
                context.IDENTIFIER()
                    .Select(i => i.GetText())
                    .ToList())
            {
                Line = context.Start.Line,
                Column = context.Start.Column
            };
        }

        public override object VisitVariadic_template(NexusParser.Variadic_templateContext context)
        {
            return new VariadicTemplateList
            {
                Line = context.Start.Line,
                Column = context.Start.Column
            };
        }

        private static CppBlock ExtractCppBlock(string wholeCppBlock, IToken token)
        {
            var wholeBlockTrimmed = wholeCppBlock.Trim();

            if (!wholeBlockTrimmed.StartsWith("c++") ||
                !wholeBlockTrimmed.EndsWith("}"))
            {
                throw new PositionedException(token.Line, token.Column, "invalid c++ block");
            }

            var cppBlockWithoutCpp = wholeBlockTrimmed.Substring("c++".Length).TrimStart();

            if (!cppBlockWithoutCpp.StartsWith("{"))
            {
                throw new PositionedException(token.Line, token.Column, "invalid c++ block");
            }

            var innerBlockStart = cppBlockWithoutCpp.Substring("{".Length);
            var innerBlockNotTrimmed = innerBlockStart.Substring(0, innerBlockStart.Length - "}".Length);
            var innerBlock = innerBlockNotTrimmed.Trim();

            return new CppBlock(innerBlock, token.Line, token.Column);
        }

        public override object VisitArrayAccess([NotNull] NexusParser.ArrayAccessContext context)
        {
            return new ArrayAccess
            {
                Value = (IExpression) Visit(context.expression(0)),
                Index = (IExpression) Visit(context.expression(1)),
                Line = context.Start.Line,
                Column = context.Start.Column
            };
        }
    }
}

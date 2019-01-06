using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Nexus.ir;
using Nexus.ir.expr;
using Nexus.ir.stmt;
using System.Collections.Generic;
using System.Linq;

namespace Nexus
{
    public class NexusGrammarVisitor : NexusBaseVisitor<object>
    {
        public override object VisitCpp_block(NexusParser.Cpp_blockContext context)
        {
            return new CppBlockStatement(ExtractCppBlock(context.GetText(), context.Start))
            {
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath
            };
        }

        public override object VisitFile(NexusParser.FileContext context)
        {
            var list = context.file_declaration().Select(Visit).ToList();

            return new File
            {
                Classes = list.OfType<Class>().ToList(),
                Functions = list.OfType<Function>().ToList(),
                CppBlocks = list.OfType<CppBlockStatement>().ToList(),
                FilePath = FileParser.CurrentPath
            };
        }

        public override object VisitClass_declaration(NexusParser.Class_declarationContext context)
        {
            var member = context.class_member().Select(Visit).ToList();

            return new Class(context.name.Text,
                context.template_list() == null ? null : (TemplateList) Visit(context.template_list()),
                member.OfType<Variable>().ToList(),
                member.OfType<CppBlockStatement>().ToList(),
                member.OfType<Include>().ToList())
            {
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath
            };
        }

        public override object VisitClass_member(NexusParser.Class_memberContext context)
        {
            if (context.cpp_block() != null)
            {
                return Visit(context.cpp_block());
            }

            if (context.include() != null)
            {
                return Visit(context.include());
            }

            return Visit(context.variable_statement());
        }

        public override object VisitFarInclude(NexusParser.FarIncludeContext context)
        {
            return new Include(context.path.Text)
            {
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath,
                Name = "#include_" + context.path.Text
            };
        }

        public override object VisitNearInclude(NexusParser.NearIncludeContext context)
        {
            return new Include(context.path.Text)
            {
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath,
                Name = "#include_" + context.path.Text
            };
        }

        public override object VisitNamedType(NexusParser.NamedTypeContext context)
        {
            return new SimpleType(
                context.IDENTIFIER().GetText(),
                VisitTemplate_list(context.template_list()) as TemplateList,
                context.ARRAY_DECLARATION().Length)
            {
                FilePath = FileParser.CurrentPath,
                Line = context.Start.Line,
                Column = context.Start.Column
            };
        }

        public override object VisitTupleType(NexusParser.TupleTypeContext context) => new SimpleType(
            "tuple", new TemplateList(context.type().Select(i => (SimpleType) Visit(i)).ToList()), context.ARRAY_DECLARATION().Length)
        {
            Line = context.Start.Line,
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
        };

        public override object VisitMapType(NexusParser.MapTypeContext context) => new SimpleType(
            "map", new TemplateList(new List<SimpleType>{(SimpleType) Visit(context.key), (SimpleType) Visit(context.value)}), context.ARRAY_DECLARATION().Length)
        {
            Line = context.Start.Line,
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
        };

        public override object VisitCppType(NexusParser.CppTypeContext context)
        {
            return new CppType(ExtractCppBlock(context.GetText(), context.Start));
        }

        public override object VisitFunction_parameter(NexusParser.Function_parameterContext context)
        {
            var i = new Variable
            {
                Type = (SimpleType) Visit(context.type()),
                Name = context.IDENTIFIER().GetText(),
                //Initialization = context.expression() == null ? null : (IExpression) Visit(context.expression()),
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath
            };

            i.Type.Constant = context.AMPERSAND() == null;
            i.Type.Reference = true;

            return i;
        }

        public override object VisitReturn_statement(NexusParser.Return_statementContext context) => new ReturnStatement
        {
            Value = (IExpression) Visit(context.expression()),
            Line = context.Start.Line,
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
        };

        public override object VisitVariable_statement(NexusParser.Variable_statementContext context) => new Variable
        {
            Type = (SimpleType)Visit(context.type()),
            Name = context.IDENTIFIER().GetText(),
            Initialization = context.expression() == null ? null : (IExpression)Visit(context.expression()),
            Line = context.Start.Line,
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
        };

        public override object VisitTuple_explosion_statement(NexusParser.Tuple_explosion_statementContext context) => new TupleExplosionStatement
        {
            Names = context.IDENTIFIER().Skip(1).Select(i => i.GetText()).ToList(),
            Right = (IExpression)Visit(context.expression()),
            Line = context.Start.Line,
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
        };

        public override object VisitIf_statement(NexusParser.If_statementContext context) => new IfStatement
        {
            Condition = (IExpression) Visit(context.expression()),
            Then = context.then.function_body_statement()
                .Select(i => (IStatement) Visit(i))
                .ToList(),
            Else = context.otherwise?.function_body_statement()
                .Select(i => (IStatement) Visit(i))
                .ToList(),
            Line = context.Start.Line,
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
        };

        public override object VisitWhile_statement(NexusParser.While_statementContext context) => new WhileStatement
        {
            Condition = (IExpression) Visit(context.expression()),
            Body = context.function_body().function_body_statement().Select(i => (IStatement) Visit(i)).ToList(),
            Line = context.Start.Line,
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
        };

        public override object VisitFor_statement(NexusParser.For_statementContext context)
        {
            return new ForStatement
            {
                Start = (IExpression) Visit(context.expression(0)),
                Stop = (IExpression) Visit(context.expression(1)),
                Step = (IExpression) Visit(context.expression(2)),
                Body = context.function_body().function_body_statement().Select(i => (IStatement) Visit(i)).ToList(),
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath
            };
        }

        public override object VisitFunction_call_expression(NexusParser.Function_call_expressionContext context) => new FunctionCall
        {
            Name = context.IDENTIFIER().GetText(),
            Parameter = context.expression().Select(i => (IExpression)Visit(i)).ToList(),
            Line = context.Start.Line,
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
        };

        public override object VisitInclude_statement(NexusParser.Include_statementContext context)
        {
            return Visit(context.include());
        }

        public override object VisitExpression_statement(NexusParser.Expression_statementContext context) =>
            new ExpressionStatement
            {
                Expression = context.expression() == null ? null : (IExpression) Visit(context.expression()),
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath
            };

        //public override object VisitFunction_body_statement(NexusParser.Function_body_statementContext context)
        //{
        //    //if (context.function_call() != null)
        //    //{
        //    //    return new FunctionCallStatement
        //    //    {
        //    //        FunctionCall = (FunctionCall)VisitFunction_call(context.function_call()),
        //    //        Line = context.Start.Line,
        //    //        Column = context.Start.Column,
        //    //        FilePath = FileParser.CurrentPath
        //    //    };
        //    //}

        //    if (context.cpp_block() != null)
        //    {
        //        return Visit(context.cpp_block());
        //    }

        //    if (context.include_statement() != null)
        //    {
        //        var tmp =  VisitInclude_statement(context.include_statement());
        //        return tmp;
        //    }

        //    return base.VisitFunction_body_statement(context);
        //}

        //public override object VisitCalculation_expression(NexusParser.Calculation_expressionContext context)
        //{
        //    return new BinaryOperation
        //    {
        //        Left = Visit(context.calc),
        //        Line = context.Start.Line,
        //        Column = context.Start.Column,
        //        FilePath = FileParser.CurrentPath
        //    };
        //}

        //public override object VisitTuple_expression(NexusParser.Tuple_expressionContext context)
        //{
        //    return base.VisitTuple_expression(context);
        //}

        //public override object VisitArray_index_expression(NexusParser.Array_index_expressionContext context)
        //{
        //    return base.VisitArray_index_expression(context);
        //}

        //public override object VisitMap_expression(NexusParser.Map_expressionContext context)
        //{
        //    return base.VisitMap_expression(context);
        //}

        //public override object VisitRange_expression(NexusParser.Range_expressionContext context)
        //{
        //    return base.VisitRange_expression(context);
        //}

        public override object VisitNewExpression(NexusParser.NewExpressionContext context)
        {
            var n = new New
            {
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath,
                Type = (SimpleType) Visit(context.type())
            };

            // single member classes can be shorthand initialized
            if (context.IDENTIFIER().Length == 0)
            {
                if (context.expression().Length == 1)
                {
                    n.SingleParameter = (IExpression)Visit(context.expression(0));
                }
            }
            else
            {
                for (var i = 0; i < context.IDENTIFIER().Length; ++i)
                {
                    var member = context.IDENTIFIER(i).GetText();
                    var initialization = (IExpression)Visit(context.expression(i));
                    n.Parameter.Add(member, initialization);
                }
            }

            return n;
        }

        public override object VisitAssignmentExpression(NexusParser.AssignmentExpressionContext context)
        {
            return new Assignment((IExpression) Visit(context.expression(0)),
                (IExpression) Visit(context.expression(1)))
            {
                Name = Function.OperatorToName("="),
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath
            };
        }

        //public override object VisitPrefixOperatorExpression(NexusParser.PrefixOperatorExpressionContext context)
        //{
        //    throw new NotImplementedException();
        //}

        public override object VisitTupleLiteral(NexusParser.TupleLiteralContext context) => new TupleLiteral
        {
            Values = context.expression().Select(i => (IExpression) Visit(i)).ToList(),
            Line = context.Start.Line,
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
        };

        public override object VisitFunctionCallExpression(NexusParser.FunctionCallExpressionContext context) =>
            Visit(context.function_call_expression());

        public override object VisitMemberAccessExpression(NexusParser.MemberAccessExpressionContext context)
        {
            return new MemberAccess((IExpression) Visit(context.element), context.IDENTIFIER().GetText())
            {
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath
            };
        }

        public override object VisitVariableDeclaration(NexusParser.VariableDeclarationContext context)
        {
            return new Variable
            {
                Type = (SimpleType)Visit(context.type()),
                Name = context.IDENTIFIER().GetText(),
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath
            };
        }

        public override object VisitParen(NexusParser.ParenContext context) => new ParenExpression
        {
            Expression = (IExpression)Visit(context.expression()),
            Line = context.Start.Line,
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
        };

        public override object VisitArrayLiteral(NexusParser.ArrayLiteralContext context) => new ArrayLiteral
        {
            Values = context.expression().Select(i => (IExpression) Visit(i)).ToList(),
            Line = context.Start.Line,
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
        };

        public override object VisitExtensionFunctionCallExpression(NexusParser.ExtensionFunctionCallExpressionContext context)
        {
            var functionCall = (FunctionCall)Visit(context.function_call_expression());
            functionCall.Object = (IExpression) Visit(context.expression());
            functionCall.Static = context.DOUBLE_COLON() != null;
            return functionCall;
        }

        public override object VisitMathematicOperation(NexusParser.MathematicOperationContext context)
        {
            return new BinaryOperation
            {
                Left = (IExpression)Visit(context.expression(0)),
                Type = (BinaryOperatorType)Visit(context.math_operator()),
                Right = (IExpression)Visit(context.expression(1)),
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath
            };
        }

        public override object VisitRangeLiteral(NexusParser.RangeLiteralContext context) => new RangeLiteral
        {
            Start = (IExpression) Visit(context.expression(0)),
            End = (IExpression) Visit(context.expression(1)),
            Line = context.Start.Line,
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
        };

        public override object VisitMapLiteral(NexusParser.MapLiteralContext context) => new MapLiteral
        {
            Values = context.key_value_pair().ToDictionary(
                i => (IExpression) Visit(i.expression(0)),
                i => (IExpression) Visit(i.expression(1))
            ),
            Line = context.Start.Line,
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
        };

        public override object VisitComparisonOperation(NexusParser.ComparisonOperationContext context)
        {
            return new BinaryOperation
            {
                Left = (IExpression)Visit(context.expression(0)),
                Type = (BinaryOperatorType)Visit(context.comparison_operator()),
                Right = (IExpression)Visit(context.expression(1)),
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath
            };
        }

        //public override object VisitExtension_function_call_expression(NexusParser.Extension_function_call_expressionContext context)
        //{
        //    return base.VisitExtension_function_call_expression(context);
        //}

        public override object VisitCppBlockExpression(NexusParser.CppBlockExpressionContext context)
        {
            return ExtractCppBlock(context.CPP_BLOCK().GetText(), context.Start);
        }

        public override object VisitLogicalOperation(NexusParser.LogicalOperationContext context)
        {
            return new BinaryOperation
            {
                Left = (IExpression)Visit(context.expression(0)),
                Type = (BinaryOperatorType)Visit(context.logical_operator()),
                Right = (IExpression)Visit(context.expression(1)),
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath
            };
        }

        //public override object VisitMember_access_expression(NexusParser.Member_access_expressionContext context)
        //{
        //    return base.VisitMember_access_expression(context);
        //}

        public override object VisitTruth_value(NexusParser.Truth_valueContext context) => new BooleanLiteral
        {
            Value = context.TRUE() != null,
            Line = context.Start.Line,
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
        };

        public override object VisitNumber(NexusParser.NumberContext context)
        {
            if (context.INTEGER() != null)
            {
                var number = NumberLiteral.Parse(
                    context.INTEGER().GetText(),
                    null,
                    context.INTEGER_SUFFIX()?.GetText(),
                    context.Start.Line,
                    context.Start.Column
                );

                number.FilePath = FileParser.CurrentPath;

                return number;
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
            Column = context.Start.Column,
            FilePath = FileParser.CurrentPath
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
                var number = NumberLiteral.ParseBinary(context.BINARY().GetText().Substring(2), context.Start.Line, context.Start.Column);
                number.FilePath = FileParser.CurrentPath;
                return number;
            }

            if (context.HEX() != null)
            {
                var number = NumberLiteral.ParseHex(context.HEX().GetText().Substring(2), context.Start.Line, context.Start.Column);
                number.FilePath = FileParser.CurrentPath;
                return number;
            }

            if (context.quoted_text() != null)
            {
                return new Text
                {
                    Value = context.quoted_text().text == null ? string.Empty : context.quoted_text().text.Text,
                    Line = context.Start.Line,
                    Column = context.Start.Column,
                    FilePath = FileParser.CurrentPath
                };
            }

            if (context.IDENTIFIER() != null)
            {
                return new VariableLiteral
                {
                    Name = context.IDENTIFIER().GetText(),
                    Line = context.Start.Line,
                    Column = context.Start.Column,
                    FilePath = FileParser.CurrentPath
                };
            }

            return base.VisitFactor(context);
        }

        public override object VisitMath_operator(NexusParser.Math_operatorContext context)
        {
            var equal = context.EQUAL() != null;

            if (context.PLUS() != null)
            {
                return equal ? BinaryOperatorType.AddAndAssign : BinaryOperatorType.Add;
            }

            if (context.MINUS() != null)
            {
                return equal ? BinaryOperatorType.SubAndAssign : BinaryOperatorType.Sub;
            }

            if (context.STAR() != null)
            {
                return equal ? BinaryOperatorType.MulAndAssign : BinaryOperatorType.Mul;
            }

            if (context.SLASH() != null)
            {
                return equal ? BinaryOperatorType.DivAndAssign :BinaryOperatorType.Div;
            }

            throw new ArgumentOutOfRangeException($"Unknown mathematical operator: {context}");
        }

        public override object VisitComparison_operator(NexusParser.Comparison_operatorContext context)
        {
            var equal = context.EQUAL().Length > 0;

            if (context.LESS() != null)
            {
                return equal ? BinaryOperatorType.LessEqual : BinaryOperatorType.Less;
            }

            if (context.GREATER() != null)
            {
                return equal ? BinaryOperatorType.GreaterEqual : BinaryOperatorType.Greater;
            }

            if (context.EQUAL().Length == 2)
            {
                return BinaryOperatorType.Equal;
            }

            throw new ArgumentOutOfRangeException($"Unknown comparison operator: {context}");
        }

        public override object VisitLogical_operator(NexusParser.Logical_operatorContext context)
        {
            if (context.AMPERSAND().Length > 0)
            {
                if (context.AMPERSAND().Length == 2)
                {
                    return BinaryOperatorType.LogicalAnd;
                }

                return BinaryOperatorType.BitwiseAnd;
            }

            if (context.PIPE().Length > 0)
            {
                if (context.PIPE().Length == 2)
                {
                    return BinaryOperatorType.LogicalOr;
                }

                return BinaryOperatorType.BitwiseOr;
            }

            throw new ArgumentOutOfRangeException($"Unknown logical operator: {context}");
        }

        public override object VisitFunction_declaration(NexusParser.Function_declarationContext context)
        {
            var function = new Function
            {
                //Name = context.@operator() != null
                //    ? Function.OperatorToName(context.@operator().GetText())
                //    : context.IDENTIFIER().GetText(),
                Name = context.@operator() != null
                       ? Function.OperatorToName(context.@operator().GetText())
                       : context.IDENTIFIER().GetText(),
                ReturnType = (SimpleType) Visit(context.returnType),
                Parameter = context.function_parameter().Select(i => (Variable) Visit(i)).ToList(),
                Body =
                    context.function_body().function_body_statement().Select(i => (IStatement) Visit(i)).ToList(),
                Operator = context.@operator() != null,
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath,
            };

            if (context.extensionType != null)
            {
                function.ExtensionBase = (SimpleType) Visit(context.extensionType);
                function.ExtensionBase.Reference = true;
                function.ExtensionBase.Constant = context.extensionTypeMutability == null;
            }

            if (context.template_list() != null)
            {
                function.TemplateList = (TemplateList) Visit(context.template_list());
            }

            if (context.DOUBLE_COLON() != null)
            {
                function.Static = true;
            }

            return function;
        }

        public override object VisitTemplate_list_declaration(NexusParser.Template_list_declarationContext context)
        {
            return Visit(context.template_list());
        }

        public override object VisitTemplate_list(NexusParser.Template_listContext context)
        {
            if (context?.IDENTIFIER() != null)
            {
                var types = new List<SimpleType>();

                foreach (var i in context.IDENTIFIER())
                {
                    if (context.DOT_DOT_DOT() != null &&
                        i == context.IDENTIFIER().Last())
                    {
                        types.Add(new SimpleVariadicType(i.GetText()));
                    }
                    else
                    {
                        types.Add(new SimpleType(i.GetText())
                        {
                            IsTemplate = true
                        });
                    }
                }

                return new TemplateList(types)
                {
                    Line = context.Start.Line,
                    Column = context.Start.Column,
                    FilePath = FileParser.CurrentPath
                };
            }

            return null;
        }

        private static CppBlock ExtractCppBlock(string wholeCppBlock, IToken token)
        {
            var wholeBlockTrimmed = wholeCppBlock.Trim();
            var cppBlockWithoutCpp = wholeBlockTrimmed.Substring("c++".Length).TrimStart();
            var innerBlockStart = cppBlockWithoutCpp.Substring("{|".Length);
            var innerBlockNotTrimmed = innerBlockStart.Substring(0, innerBlockStart.Length - "|}".Length);
            var innerBlock = innerBlockNotTrimmed.Trim();

            return new CppBlock(innerBlock, token.Line, token.Column) {FilePath = FileParser.CurrentPath};
        }

        public override object VisitArrayAccessExpression([NotNull] NexusParser.ArrayAccessExpressionContext context)
        {
            return new FunctionCall
            {
                Object = (IExpression) Visit(context.expression(0)),
                Parameter = new List<IExpression>{(IExpression) Visit(context.expression(1))},
                Name = Function.OperatorToName("[]"),
                Line = context.Start.Line,
                Column = context.Start.Column,
                FilePath = FileParser.CurrentPath
            };
        }
    }
}

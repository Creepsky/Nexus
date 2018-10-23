using System;
using System.Collections.Generic;
using System.IO;
using Nexus;
using Nexus.gen;
using Nexus.ir.expr;
using Nexus.ir.stmt;
using Xunit;

namespace NexusParserTest
{
    public static class Expressions
    {
        [Fact]
        public static void ArrayAccessTestArray()
        {
            var context = CreateContextWithPrimitives(1);
            
            foreach (var generationElement in context.GetElements())
            {
                var i = (Variable) generationElement;
                new ArrayAccess {Name = i.Name, Index = new I32(0)}.Check(context);
                new ArrayAccess{Name = i.Name, Index = new VariableLiteral{Name = "a"}}.Check(context);
            }
        }

        private static Context CreateContextWithPrimitives(int array)
        {
            var context = new Context();

            foreach (var i in TypesExtension.Primitives)
            {
                try
                {
                    AddNewPrimitiveVariable(context, $"my{i}", i, array);
                }
                catch (Exception)
                {
                    // skip alias
                }
            }

            return context;
        }

        private static void AddNewPrimitiveVariable(Context context, string variableName, string name, int array)
        {
            context.Add(variableName, new Variable{
                Type = new SimpleType(name, array),
                Name = variableName
            });
        }

        [Fact]
        public static void BinaryOperationTest()
        {
            var context = new Context();

            context.Add("a", new Variable {Name = "a", Type = new SimpleType("i32")});
            context.Add("b", new Variable {Name = "b", Type = new SimpleType("i32", 1)});

            context.Add("c", new Variable
            {
                Name = "c", Type = new MapType
                {
                    KeyType = new SimpleType("string"),
                    ValueType = new SimpleType("float")
                }
            });

            context.Add("MyClass", new Class("MyClass", null, new List<Variable>(), new List<CppBlockStatement>()));

            context.Add("MyClass.func", new ExtensionFunction
            {
                Name = "func",
                ExtensionBase = new SimpleType("MyClass"),
                Parameter = new List<Variable>
                {
                    new Variable
                    {
                        Type = new SimpleType("string"),
                        Name = "param1"
                    },
                    new Variable
                    {
                        Type = new SimpleType("i32"),
                        Name = "param2"
                    },
                },
                ReturnType = new TupleType
                {
                    Types = new List<IType>
                    {
                        new SimpleType("string"),
                        new SimpleType("i32")
                    }
                },
                Body = new List<IStatement>
                {
                    new ReturnStatement
                    {
                        Value = new TupleLiteral
                        {
                            Values = new List<IExpression>
                            {
                                new VariableLiteral{Name = "param1"},
                                new VariableLiteral{Name = "param2"}
                            }
                        }
                    }
                }
            });
            
            context.Add("myClassInstance", new Variable
            {
                Type = new SimpleType("MyClass"),
                Name = "myClassInstance"
            });

            CheckBinaryOperation(new I8(100), new I8(-100), BinaryOperatorType.Add, context, "100 + -100");

            CheckBinaryOperation(new VariableLiteral {Name = "a"}, new ArrayAccess {Value = new VariableLiteral{Name = "b"}, Index = new USize(0)},
                BinaryOperatorType.Sub, context, "a - b[0]");

            CheckBinaryOperation(new ArrayAccess
            {
                Value = new ArrayLiteral
                {
                    Values = new List<IExpression>
                    {
                        new I8(1),
                        new I16(2),
                        new I32(3),
                        new I64(4),
                        new U8(5),
                        new U16(6),
                        new U32(7),
                        new U64(8),
                        new USize(9)
                    }
                },
                Index = new I32(2)
            }, new F32(10), BinaryOperatorType.Mul, context, "{1, 2, 3, 4, 5, 6, 7, 8, 9}[2] * 10");

            CheckBinaryOperation(new Text{Value = "abc"}, new ArrayAccess
            {
                Value = new ExtensionFunctionCall
                {
                    Variable = new VariableLiteral{Name = "myClassInstance" },
                    FunctionCall = new FunctionCall
                    {
                        Name = "func",
                        Parameter = new List<IExpression>
                        {
                            new Text{Value = "text"},
                            new I32(16)
                        }
                    }
                },
                Index = new USize(1)
            }, BinaryOperatorType.Div, context, "\"abc\" / myClassInstance.func(\"text\", 16)[1]");
        }

        private static void CheckBinaryOperation(IExpression left, IExpression right, BinaryOperatorType type,
            Context context, string expectedOutput)
        {
            var bin = new BinaryOperation
            {
                Left = left,
                Right = right,
                Type = type
            };

            GenerateAndCheck(bin, context);
            bin.GetResultType(context);
            Assert.Equal(expectedOutput, Print(bin));
        }

        [Theory]
        [InlineData("(1 + 1) * 5")]
        [InlineData("a * b + (1 - 0) / -2")]
        [InlineData("[1, 2, 3][1] * a[0] / 1", "{1, 2, 3}[1] * a[0] / 1")]
        public static void ParenTest(string expression, string expected = null)
        {
            var i = FileParser.ParseFile($"class test {{}} int test.func() {{ return {expression}; }}");
            Assert.Equal(1, i.ExtensionFunctions.Count);
            Assert.Equal(1, i.ExtensionFunctions[0].Body.Count);
            Assert.IsType<ReturnStatement>(i.ExtensionFunctions[0].Body[0]);
            var rs = (ReturnStatement)i.ExtensionFunctions[0].Body[0];
            GenerateAndCheck(i, new Context());
            Assert.Equal(expected ?? expression, Print(rs.Value));
        }

        [Fact]
        public static void FunctionTest()
        {
            var i = FileParser.ParseFile("class test{} " +
                                         "int test.func(){ " +
                                         "int max(int a, int b) { if (a > b) { return a; } else { return b; } } " +
                                         "int my_a = 1; int my_b = 2; " +
                                         "auto my_max = max_of(my_a, my_b); " +
                                         " }");
            GenerateAndCheck(i, new Context());
            var element = i.ExtensionFunctions[0].Body[0];
            Assert.IsType<Function>(element);
            var function = (Function)element;

            var expectedLines = new[]
            {
                "auto max = [](int32_t a, int32_t b)",
                "{",
                "    if (a > b)",
                "    {",
                "        return a;",
                "    }",
                "    else",
                "    {",
                "        return b;",
                "    }",
                "};",
                ""
            };

            Assert.Equal(string.Join(Environment.NewLine, expectedLines), Print(function, PrintType.Source));
        }

        private static void GenerateAndCheck(IGenerationElement element, Context context)
        {
            element.Generate(context, GenerationPhase.ForwardDeclaration);
            element.Generate(context, GenerationPhase.Declaration);
            element.Generate(context, GenerationPhase.Definition);
            element.Check(context);
            
        }

        private static string Print(IPrintable element, PrintType type = PrintType.Header)
        {
            var stringWriter = new StringWriter();
            var printer = new Printer(stringWriter);
            element.Print(type, printer);
            return stringWriter.ToString();
        }
    }
}
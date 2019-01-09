using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nexus;
using Nexus.gen;
using Nexus.ir.expr;
using Nexus.ir.stmt;
using Xunit;

namespace NexusParserTest
{
    public class Expressions : IClassFixture<StandardLibFixture>
    {
        private readonly StandardLibFixture _fixture;

        public Expressions(StandardLibFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ArrayAccessTestArray()
        {
            var context = CreateContextWithPrimitives(1);
            
            foreach (var generationElement in context.GetElements())
            {
                var i = (Variable) generationElement;

                new ArrayAccess
                {
                    Value = new VariableLiteral{Name = i.Name},
                    Index = new I32(0)
                }.Check(context);
            }
        }

        private Context CreateContextWithPrimitives(int array)
        {
            var context = _fixture.Context.StackNewContext(null);

            foreach (var i in TypesExtension.Primitives)
            {
                try
                {
                    var name = $"my{i}";

                    context.Add(name, new Variable
                    {
                        Type = new SimpleType(i, array),
                        Name = name
                    });
                }
                catch (Exception)
                {
                    // skip alias
                }
            }

            _fixture.Generate();

            return context;
        }

        [Fact]
        public void BinaryOperationTest()
        {
            var context = _fixture.Context.StackNewContext(null);

            context.Add("a", new Variable {Name = "a", Type = new SimpleType("i32")});
            context.Add("b", new Variable {Name = "b", Type = new SimpleType("i32", 1)});

            context.Add("c", new Variable
            {
                Name = "c", Type = new SimpleType("map", new TemplateList(new List<SimpleType>
                {
                    new SimpleType("string"),
                    new SimpleType("float")
                }), 0)
            });

            context.Add("MyClass",
                new Class("MyClass", null, new List<Variable>(), new List<CppBlockStatement>(), new List<Include>()));

            context.Add("func", new Function
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
                ReturnType = new SimpleType("tuple", new TemplateList(new List<SimpleType>
                {
                    new SimpleType("string"),
                    new SimpleType("i32")
                }), 0),
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

            _fixture.Generate();

            CheckBinaryOperation(new I8(100), new I8(-100), BinaryOperatorType.Add, context, "i8{100} + i8{-100}");

            CheckBinaryOperation(new VariableLiteral {Name = "a"}, new ArrayAccess {Value = new VariableLiteral{Name = "b"}, Index = new USize(0)},
                BinaryOperatorType.Sub, context, "a - at(b, usize{0})");

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
            }, new F32(10), BinaryOperatorType.Mul, context, "at({i8{1}, i16{2}, i32{3}, i64{4}, u8{5}, u16{6}, u32{7}, u64{8}, usize{9}}, i32{2}) * f32{10}");

            // TODO: implement tuples and then re-activate this test
            //CheckBinaryOperation(new Text{Value = "abc"}, new ArrayAccess
            //{
            //    Value = new FunctionCall
            //    {
            //        Object = new VariableLiteral{Name = "myClassInstance" },
            //        Name = "func",
            //        Parameter = new List<IExpression>
            //        {
            //            new Text{Value = "text"},
            //            new I32(16)
            //        }
            //    },
            //    Index = new USize(1)
            //}, BinaryOperatorType.Div, context, "\"abc\" / myClassInstance.func(\"text\", i32(16))[usize(1)]");
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
        [InlineData("(1 + 1) * 5", "(i64{1} + i64{1}) * i64{5}", "i64")]
        [InlineData("(1 - 0) / -2", "(i64{1} - i64{0}) / i64{-2}", "f64")]
        [InlineData("([1, 2, 3][1] / 1) * 5", "(at({i64{1}, i64{2}, i64{3}}, i64{1}) / i64{1}) * i64{5}", "f64")]
        public void ParenTest(string expression, string expected, string expectedReturnValue)
        {
            var i = FileParser.ParseFile($"{expectedReturnValue} Main.func() {{ return {expression}; }} int Main.run() {{ return 0_i32; }}");
            // Main.run and Main.func
            Assert.Equal(2, i.Functions.Count);
            Assert.Equal(1, i.Functions[0].Body.Count);
            Assert.IsType<ReturnStatement>(i.Functions[0].Body[0]);
            var rs = (ReturnStatement)i.Functions[0].Body[0];
            foreach (var j in i.Functions)
            {
                _fixture.Context.AddGlobal(j.Name, j);
            }
            _fixture.Generate();
            GenerateAndCheck(i, _fixture.Context);
            Assert.Equal(expected ?? expression, Print(rs.Value));
        }

        private static void GenerateAndCheck(IGenerationElement element, Context context)
        {
            element.ForwardDeclare(context);
            element.Declare();
            element.Define();
            element.Check(context);
        }

        private static string Print(IPrintable element, PrintType type = PrintType.Header)
        {
            var stringWriter = new StringWriter();
            var printer = new Printer(stringWriter);
            element.Print(type, printer);
            return stringWriter.ToString();
        }

        [Fact]
        public void CheckMapLiterals()
        {
            var i = FileParser.ParseFile("class Test{ [string -> int] values; } " +
                                         "int Test.get(string key) { " +
                                         "  return this.values[key]; " +
                                         "} " +
                                         "int Main.run() { return 0_i32; }");
            foreach (var j in i.Classes.Select(c => c as IGenerationElement)
                                       .Concat(i.Functions.Select(c => c as IGenerationElement)))
            {
                _fixture.Context.AddGlobal(j.Name, j);
            }
            _fixture.Generate();
            GenerateAndCheck(i, _fixture.Context.StackNewContext(null));
        }

        [Fact]
        public void TestAssignment()
        {
            var context = _fixture.Context;
            
            // int a = 0;
            context.Add("a", new Variable
            {
                Type = new SimpleType(TypesExtension.Int, 0),
                Name = "a",
                //Initialization = new I32(0)
            });
            
            // a = 1;
            var i = new Assignment(new VariableLiteral {Name = "a"}, new I32(1), AssignmentType.Copy);
            
            _fixture.Generate();
            i.Check(context);
            i.ForwardDeclare(context);
            i.Declare();
            i.Define();
        }
    }
}
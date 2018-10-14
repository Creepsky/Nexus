using System.Collections.Generic;
using Nexus.gen;
using Nexus.ir.expr;
using Nexus.ir.stmt;
using Xunit;

namespace NexusParserTest
{
    public static class ResultTypeTest
    {
        [Fact]
        public static void ResultTypeFunctionCheck()
        {
            var context = new Context();

            var returnType = new TupleType
            {
                Types = new List<IType>
                {
                    new SimpleType{Name = "i32"},
                    new SimpleType{Name = "usize"}
                }
            };
            
            var parameter1 = new Variable{
                Type = new SimpleType
                {
                    Name = "i32",
                },
                Name = "param1"
            };
            
            var parameter2 = new Variable{
                Type = new SimpleType
                {
                    Name = "string",
                    Array = 1
                },
                Name = "param2"
            };
            
            var function = new Function
            {
                Name = "myFunction",
                Parameter = new List<Variable>{parameter1, parameter2},
                Statements = new List<IStatement>(),
                Type = returnType
            };
            
            context.Add(function.Name, function);

            Assert.Equal(parameter1.Type, parameter1.GetResultType(context));
            Assert.Equal(parameter2.Type, parameter2.GetResultType(context));
            Assert.Equal(returnType, function.GetResultType(context));
        }
    }
}
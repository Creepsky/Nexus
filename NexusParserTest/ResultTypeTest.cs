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
                    new SimpleType(TypesExtension.I32, 0),
                    new SimpleType(TypesExtension.USize, 0)
                }
            };
            
            var parameter1 = new Variable{
                Type = new SimpleType(TypesExtension.I32, 0),
                Name = "param1"
            };
            
            var parameter2 = new Variable{
                Type = new SimpleType(TypesExtension.String, 1),
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
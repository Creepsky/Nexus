using System;
using Nexus.gen;
using Nexus.ir.expr;
using Nexus.ir.stmt;
using Xunit;

namespace NexusParserTest
{
    public static class Expressions
    {
        [Fact]
        public static void ArrayAccessTestPrimitives()
        {
//            var context = CreateContextWithPrimitives(0);
//            
//            foreach (PrimitiveType i in Enum.GetValues(typeof(PrimitiveType)))
//            {
//                // the only primitive type that can be indexed is String
//                if (i == PrimitiveType.String)
//                {
//                    new ArrayAccess{Name = $"my{i.ToString()}", Index = new I32(0)}.Check(context);
//                    new ArrayAccess{Name = $"my{i}", Index = new VariableLiteral {Name = "a"}}.Check(context);
//                }
//                // all others fail
//                else
//                {
//                    Assert.ThrowsAny<Exception>(() => new ArrayAccess{
//                        Name = $"my{i.ToString()}",
//                        Index = new I32(0)
//                        
//                    }.Check(context));
//                    Assert.ThrowsAny<Exception>(() => new ArrayAccess{
//                        Name = $"my{i}",
//                        Index = new VariableLiteral{Name = "a"}
//                    }.Check(context));
//                }
//            }
        }
        
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
    }
}
using Nexus.gen;
using Nexus.ir.expr;
using Xunit;
using Nexus.ir.stmt;

namespace NexusParserTest
{
    public static class StatementUnitTests
    {
        [Fact]
        public static void TestAssignment()
        {
            var context = new Context();
            
            // int a = 0;
            context.Add("a", new Variable
            {
                Type = new SimpleType{Name = "int"},
                Name = "a",
                Initialization = new I32(0)
            });
            
            // a = 1;
            var i = new AssignmentStatement
            {
                Left = new VariableLiteral
                {
                    Name = "a"
                },
                Right = new I32(1)
            };
            
            i.Check(context);
            i.Generate(context, GenerationPhase.ForwardDeclaration);
            i.Generate(context, GenerationPhase.Declaration);
            i.Generate(context, GenerationPhase.Definition);
        }
    }
}
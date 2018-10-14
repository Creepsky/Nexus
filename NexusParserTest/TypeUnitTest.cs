using System.Collections.Generic;
using Nexus.ir.expr;
using Xunit;

namespace NexusParserTest
{
    public static class TypeUnitTest
    {
        [Fact]
        public static void TypeEquality()
        {
            foreach (var i in TypesExtension.Aliases)
            {
                var lhs = new SimpleType(i.Key, 0);
                var rhs = new SimpleType(i.Value, 0);
                Assert.Equal(lhs, rhs);
            }

            var i32Type = new SimpleType(TypesExtension.I32, 0);
            var stringType = new SimpleType(TypesExtension.String, 0);
            var byteArray = new SimpleType(TypesExtension.Byte, 1);

            var i32TypeAlias = new SimpleType(TypesExtension.Int, 0);
            var byteArrayAlias = new SimpleType(TypesExtension.U8, 1);

            var mapType = new MapType
            {
                KeyType = i32Type,
                ValueType = stringType
            };

            var mapTypeAlias = new MapType
            {
                KeyType = i32TypeAlias,
                ValueType = new SimpleType(TypesExtension.String, 0)
            };

            var tupleType = new TupleType
            {
                Types = new List<IType>
                {
                    i32Type,
                    stringType,
                    byteArray
                }
            };

            var tupleTypeAlias = new TupleType
            {
                Types = new List<IType>
                {
                    i32TypeAlias,
                    new SimpleType(TypesExtension.String, 0),
                    byteArrayAlias
                }
            };

            Assert.Equal(mapType, mapTypeAlias);
            Assert.Equal(tupleType, tupleTypeAlias);
        }
    }
}
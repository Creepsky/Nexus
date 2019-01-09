using System.Collections.Generic;
using System.Linq;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class TupleType : SimpleType
    {
        private Class _class;

        public TupleType(IList<SimpleType> types, int array)
            : base($"tuple{types.Count}", new TemplateList(types), array)
        {
        }

        public override void ForwardDeclare(Context upperContext)
        {
            _class = upperContext.Get(Name) as Class;

            if (_class != null)
            {
                return;
            }

            var templateTypes = new List<SimpleType>();
            var variables = new List<Variable>();

            for (var i = 0; i < TemplateList.Types.Count; ++i)
            {
                templateTypes.Add(new SimpleType($"T{i}")
                {
                    IsTemplate = true
                });

                variables.Add(new Variable
                {
                    Type = new SimpleType($"T{i}")
                    {
                        IsTemplate = true
                    },
                    Name = $"_{i}"
                });
            }

            _class = new Class(Name, new TemplateList(templateTypes), variables,
                new List<CppBlockStatement>(), new List<Include>()); 

            // make it public
            _class.ForwardDeclare(upperContext);
        }

        public override void Check(Context context)
        {
            _class = _class.GetVariant(this, context);
        }

        //    //var element = context.Get(Name);
        //    //bool needCreation;

        //    //if (element is Class c)
        //    //{
        //    //    needCreation = c.GetVariant(this, context) == null;
        //    //}
        //    //// the class does not exist yet, we need to create the template for it
        //    //else
        //    //{
        //    //    needCreation = true;
        //    //}

        //    //if (needCreation)
        //    //{
        //    //    var templateTypes = new List<SimpleType>();
        //    //    var variables = new List<Variable>();

        //    //    for (var i = 0; i < TemplateList.Types.Count; ++i)
        //    //    {
        //    //        templateTypes.Add(new SimpleType($"T{i}")
        //    //        {
        //    //            IsTemplate = true
        //    //        });

        //    //        variables.Add(new Variable
        //    //        {
        //    //            Type = new SimpleType($"T{i}")
        //    //            {
        //    //                IsTemplate = true
        //    //            },
        //    //            Name = $"_{i}"
        //    //        });
        //    //    }

        //    //    var tupleClass = new Class(Name, new TemplateList(templateTypes), variables,
        //    //        new List<CppBlockStatement>(), new List<Include>()); 

        //    //    // make it public
        //    //    tupleClass.ForwardDeclare(context);

        //    //    // create a template from the new tuple class
        //    //    _class = tupleClass.GetVariant(this, context);
        //    //}

        //    base.Check(context);
        //}

        public override object Clone()
        {
            return new TupleType(TemplateList.Types.Select(i => (SimpleType)i.CloneDeep()).ToList(), Array);
        }
    }
}
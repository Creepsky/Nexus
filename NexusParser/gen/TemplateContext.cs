using System;
using System.Collections.Generic;
using Nexus.common;
using Nexus.ir.expr;
using NLog;

namespace Nexus.gen
{
    public class TemplateContext : Context
    {
        /// <summary>
        /// The template variable names from the template list of the template function and their types that are bound to it.
        /// Initially there are no bindings for a template variable name, which is marked as a null as value.
        /// </summary>
        public IDictionary<string, SimpleType> TemplateVariables { get; } = new Dictionary<string, SimpleType>();

        /// <summary>
        /// The context of the caller, that initially started the templating process for the template function.
        /// Use this to retrieve the original symbols from the perspective of the calling function.
        /// For example the keyword this has different meanings in both the caller context and the template function context.
        /// </summary>
        public Context CallerContext { get; }

        /// <summary>
        /// The logger instance.
        /// </summary>
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private IGenerationElementGenerator _generator;

        /// <summary>
        /// Creates a template context instance.
        /// This will initialize the context properly and also create a concrete implementation of the template function.
        /// However, after creation, <see cref="Generate"/> has to be called to fill the freshly created function with life.
        /// </summary>
        /// <param name="templateContext">The context of the template element.</param>
        /// <param name="callerContext">The context of the caller of the template function.</param>
        /// <param name="template">The called template element.</param>
        public TemplateContext(Context templateContext, Context callerContext, IGenerationElementGenerator template)
            : base(templateContext.UpperContext, template.Generate(), new Dictionary<string, IGenerationElement>())
        {
            CallerContext = callerContext;
            _generator = template;

            foreach (var i in template.TemplateList.Types)
            {
                TemplateVariables.Add(i.Name, null);
            }
        }

        /// <summary>
        /// Tries to generate the template function.
        /// </summary>
        /// <param name="concreteElement">The function call, that initially created the </param>
        /// <returns>true, if the template was properly created and also valid in the given context, false otherwise.</returns>
        public bool Generate(IGenerationElement concreteElement)
        {
            try
            {
                Element.Template(this, concreteElement);
                Element.Declare();
                Element.Define();
                Element.Check(this);
                AddGlobal(Element.Name, Element);
                _logger.Debug($"Template {Element} generated from {_generator}");
                return true;
            }
            catch (Exception e)
            {
                _logger.Debug($"Template {Element} failed for {concreteElement}");
                _logger.Debug(e);
                Element.Remove();
                return false;
            }
        }

        //public SimpleType LookupTemplateType(SimpleType templateType) => Get(templateType.Name).GetResultType(this) as SimpleType;
        public SimpleType LookupTemplateType(SimpleType templateType) =>
            TemplateVariables.ContainsKey(templateType.Name)
                ? TemplateVariables[templateType.Name]
                : templateType;

        public SimpleType RegisterTemplateType(SimpleType templateType, SimpleType concreteType)
        {
            var registeredType = LookupTemplateType(templateType);

            if (registeredType == null || registeredType.IsTemplate)
            {
                //Add(templateType.Name, concreteType);
                TemplateVariables[templateType.Name] = concreteType;
                return concreteType;
            }

            if (!registeredType.Equals(concreteType))
            {
                throw new TemplateGenerationException(concreteType,
                    $"Concrete type {concreteType} does not match the " +
                    $"template type {registeredType}");
            }

            return registeredType;
        }

        //public void DeriveTemplates(SimpleType templateType, SimpleType concreteType)
        //{
        //    if (templateType.TemplateList != null)
        //    {
        //        if (concreteType.TemplateList == null)
        //        {
        //            throw new TemplateGenerationException(concreteType, "Concrete type needs a template list but has none");
        //        }

        //        foreach (var i in templateType.TemplateList.Derive(concreteType.TemplateList))
        //        {
        //            if (_templateVariables.ContainsKey(i.Key))
        //            {
        //                if (!_templateVariables[i.Key].Equals(i.Value))
        //                {
        //                    throw new TemplateGenerationException(i.Value,
        //                        $"Concrete type {i.Value} does not match the already derived type {_templateVariables[i.Key]} for key {i.Key}");
        //                }
        //            }
        //            else
        //            {
        //                _templateVariables.Add(i);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (_templateVariables.ContainsKey(templateType.Name))
        //        {
        //            if (!_templateVariables[templateType.Name].Equals(concreteType))
        //            {
        //                throw new TemplateGenerationException(concreteType,
        //                    $"Concrete type {concreteType} does not match the already derived type {_templateVariables[templateType.Name]} for key {templateType.Name}");
        //            }
        //        }
        //        else
        //        {
        //            _templateVariables.Add(templateType.Name, concreteType);
        //        }
        //    }
        //}

        public SimpleType TemplateTypeToConcrete(string templateTypeName)
        {
            if (TemplateVariables.ContainsKey(templateTypeName))
            {
                return TemplateVariables[templateTypeName];
            }

            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.ComponentModel;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource.Plugins
{
	[Description("A Concept is an autoimplemented interface for generated types.")]
	public class ImplementConcept
	{


		public const string Concept = "Concept";

		public Func<Type[]> ReferencedConcepts;

		public IEnumerable<Type> GetInterfaces(XElement BodyElement, Dictionary<string, Type> ElementTypes)
		{
			// http://msdn.microsoft.com/en-us/library/ms256086.aspx

			var Properties = Enumerable.ToArray(
				from CurrentElement in BodyElement.XPathSelectElements(".//*[@id]")
				let id = CurrentElement.Attribute("id").Value
				let e_Type = ElementTypes.ContainsKey(CurrentElement.Name.LocalName) ? ElementTypes[CurrentElement.Name.LocalName] : typeof(IHTMLElement)
				select new { CurrentElement, id, e_Type }
			);

			return
				from ConceptType in ReferencedConcepts()

				let Missing =
					from IProperty in ConceptType.GetProperties()
					let Property = Properties.FirstOrDefault(k => k.id == IProperty.Name)
					where Property == null
					select IProperty

				where !Missing.Any()

				let UnexpectedType =
					from IProperty in ConceptType.GetProperties()
					let Property = Properties.First(k => k.id == IProperty.Name)
					where Property.e_Type != IProperty.PropertyType
					select new { IProperty, Property }

				where !UnexpectedType.Any()

				select ConceptType;
		}
	}
}

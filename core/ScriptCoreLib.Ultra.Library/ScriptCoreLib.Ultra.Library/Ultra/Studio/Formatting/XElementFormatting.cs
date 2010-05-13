using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio.Formatting
{
	public class XElementFormatting
	{
		public XElementFormatting()
		{
			GetName =
				e =>
				{
					return e.Name.LocalName;
				};

			CanCollapse = 
				e => true;

			WriteXMLAttributeValue =
				(e, a, File) =>
				{
					// is IE returning inherited attributes with null value?
					if (a.Value == null)
						return;

					File.Write(SolutionFileTextFragment.XMLAttributeValue, InternalXMLExtensions.ToXMLString(a.Value));
				};
		}
		public Func<XElement, string> GetName;

		// http://www.featureblend.com/xml-closing-tag.html
		public Func<XElement, bool> CanCollapse;


		public Action<XElement, XAttribute, SolutionFile> WriteXMLAttributeValue;

	}
}

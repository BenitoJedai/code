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
		public Func<XElement, string> GetName = e => e.Name.LocalName;

		// http://www.featureblend.com/xml-closing-tag.html
		public Func<XElement, bool> CanCollapse = e => true;

		public Action<XElement, XAttribute, SolutionFile> WriteXMLAttributeValue =
			(e, a, File) =>
				File.Write(SolutionFileTextFragment.XMLAttributeValue, InternalXMLExtensions.ToXMLString(a.Value));

	}
}

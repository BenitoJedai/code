using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.XML.XLinq
{
	using AS3_XML = global::ScriptCoreLib.ActionScript.XML;
	using AS3_XMLList = global::ScriptCoreLib.ActionScript.XMLList;

	[Script(Implements = typeof(XDocument))]
	internal class __XDocument : __XContainer
	{

		public static __XDocument Parse(string text)
		{
			var InternalValue = new AS3_XML(text);

			return new __XDocument { InternalValue = InternalValue };
		}

		__XElement _Root;

		public XElement Root
		{
			get
			{
				if (_Root == null)
				{
					_Root = new __XElement { InternalValue = InternalValue.elements()[0] };
				}

				return (XElement)(object)_Root;
			}
		}
	}
}

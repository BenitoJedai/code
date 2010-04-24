using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.XML.XLinq
{
	using AS3_XML = global::ScriptCoreLib.ActionScript.XML;
	using AS3_XMLList = global::ScriptCoreLib.ActionScript.XMLList;


	[Script(Implements = typeof(XContainer))]
	internal class __XContainer : __XNode
	{
		public IEnumerable<XElement> Elements()
		{
			var e = this.InternalValue;
			var a = new List<XElement>();

			var elements = e.elements();
			var length = elements.length();

			for (int i = 0; i < length; i++)
			{
				var item = elements[i];
		
				a.Add(
					(XElement)(object)new __XElement { InternalValue = item }
				);

			}

			return a;

		}

		public void Add(object content)
		{
			if (this.InternalValue == null)
			{
				throw new NotImplementedException();
			}

			{
				var e = content as string;

				if (e != null)
				{
					this.InternalValue.appendChild(
						(AS3_XML)(object)e
					);
					return;
				}
			}

			throw new NotImplementedException();

		}
	}
}

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
		public __XName InternalElementName;


        public XElement Element(XName name)
        {
            return Elements(name).FirstOrDefault();
        }

        public IEnumerable<XElement> Elements(XName name)
        {
            return this.Elements().Where(k => k.Name.LocalName == name.LocalName);
        }


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

		public void Add(params object[] content)
		{
			foreach (var item in content)
			{
				this.Add(item);
			}
		}

		public void Add(object content)
		{
			if (this.InternalValue == null)
			{
				this.InternalValue = __createElement();
				this.InternalValue.setLocalName(this.InternalElementName.LocalName);
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

			#region XElement
			{
				var e = (__XElement)(object)(content as XElement);
				if (e != null)
				{
					if (e.InternalValue == null)
					{
						e.InternalValue = __createElement();
						e.InternalValue.setLocalName(e.InternalElementName.LocalName);
					}

					this.InternalValue.appendChild(e.InternalValue);

					return;
				}
			}
			#endregion

			throw new NotImplementedException();

		}

		[Script(OptimizedCode = "return <item />;")]
		internal static AS3_XML __createElement()
		{
			return default(AS3_XML);
		}

	}
}

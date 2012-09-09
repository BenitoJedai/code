using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Xml.Linq
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
			var e = this.InternalElement;
			var a = new List<XElement>();

			var elements = e.elements();
			var length = elements.length();

			for (int i = 0; i < length; i++)
			{
				var item = elements[i];

				a.Add(
					(XElement)(object)new __XElement { InternalElement = item }
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
			if (this.InternalElement == null)
			{
				this.InternalElement = __createElement();
				this.InternalElement.setLocalName(this.InternalElementName.LocalName);
			}

			{
				var e = content as string;

				if (e != null)
				{
					this.InternalElement.appendChild(
						(AS3_XML)(object)e
					);
					return;
				}
			}

            #region XAttribute
            {
                var e = (__XAttribute)(object)(content as XAttribute);
                if (e != null)
                {
                    var CurrentValue = e.Value;

                    e.InternalParentElement = this;
                    e.Value = CurrentValue;
                    return;
                }
            }
            #endregion


			#region XElement
			{
				var e = (__XElement)(object)(content as XElement);
				if (e != null)
				{
					if (e.InternalElement == null)
					{
						e.InternalElement = __createElement();
						e.InternalElement.setLocalName(e.InternalElementName.LocalName);
					}

					this.InternalElement.appendChild(e.InternalElement);

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

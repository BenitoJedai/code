using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml.Linq;

namespace ScriptCoreLibJava.BCLImplementation.System.Xml.Linq
{
	[Script(Implements = typeof(global::System.Xml.Linq.XContainer))]
	internal class __XContainer : __XNode
	{
		public __XName InternalElementName;

		public void Add(object content)
		{
			if (this.InternalValue == null)
			{
				try
				{
					var f = javax.xml.parsers.DocumentBuilderFactory.newInstance();
					var b = f.newDocumentBuilder();

					var doc = b.newDocument();


					this.InternalValue = doc.createElement(this.InternalElementName.LocalName);
				}
				catch
				{
					throw new NotSupportedException();
				}
			}

			{
				var e = content as string;

				if (e != null)
				{
					this.InternalValue.appendChild(
						this.InternalValue.getOwnerDocument().createTextNode(e)
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

					e.InternalElement = this;
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
					if (e.InternalValue == null)
					{
						e.InternalValue = this.InternalValue.getOwnerDocument().createElement(e.InternalElementName.LocalName);
					}
					else
					{
						__adoptNode(e);
					}

					this.InternalValue.appendChild(e.InternalValue);

					return;
				}
			}
			#endregion


			throw new NotImplementedException();
		}



		public org.w3c.dom.Element InternalElement
		{
			get
			{
				return (org.w3c.dom.Element)this.InternalValue;
			}
		}

		private void __adoptNode(__XElement e)
		{
			// adoptNode not available in java 1.4
			// should use importNode?

			// org.w3c.dom.DOMException: WRONG_DOCUMENT_ERR: A node is used 
			// in a different document than the one that created it.


			if (e.InternalValue.getOwnerDocument() != this.InternalValue.getOwnerDocument())
			{
				var ownerDocument = this.InternalValue.getOwnerDocument();

				try
				{
					// IE does not implement adoptNode yet
					e.InternalValue = ownerDocument.importNode(e.InternalValue, true);
				}
				catch
				{

				}
			}
		}
	}
}

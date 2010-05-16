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
			InternalEnsureElement();

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

		private void InternalEnsureElement()
		{
			if (this.InternalValue == null)
			{
				try
				{
					// us thus supposed to work under applet?
					// http://forums.sun.com/thread.jspa?threadID=753378&tstart=3525
					// http://stackoverflow.com/questions/2745365/java-applet-in-firefox


					var f = InternalCreateFactory();

					var b = f.newDocumentBuilder();

					var doc = b.newDocument();

					var name = this.InternalElementName.LocalName;

					this.InternalValue = doc.createElement(name);
				}
				catch (csharp.ThrowableException exc)
				{
					((java.lang.Throwable)(object)exc).printStackTrace();

					throw new NotSupportedException();
				}
			}
		}

		private static javax.xml.parsers.DocumentBuilderFactory InternalCreateFactory()
		{
			var f = default(javax.xml.parsers.DocumentBuilderFactory);
			try
			{
				f = javax.xml.parsers.DocumentBuilderFactory.newInstance();
			}
			catch (csharp.ThrowableException exc)
			{
				((java.lang.Throwable)(object)exc).printStackTrace();

				throw new NotSupportedException();
			}
			return f;
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

		public XElement Element(XName name)
		{
			var e = new __XElement
			{
				InternalValue = this.InternalElement.getElementsByTagName(name.LocalName).item(0)
			};

			return (XElement)(object)e;
		}

		public void RemoveNodes()
		{
			var InternalElement = this.InternalElement;

			var p = InternalElement.getFirstChild();
			while (p != null)
			{
				InternalElement.removeChild(p);
				p = InternalElement.getFirstChild();
			}
		}
	}
}

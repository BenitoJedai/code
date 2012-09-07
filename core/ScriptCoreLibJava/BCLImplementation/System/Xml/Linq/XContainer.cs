using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml.Linq;
using System.Collections;

namespace ScriptCoreLibJava.BCLImplementation.System.Xml.Linq
{
	[Script(Implements = typeof(global::System.Xml.Linq.XContainer))]
	internal class __XContainer : __XNode
	{
		public __XName InternalElementName;

        /// <summary>
        /// This is the list of elements which were added to our node.
        /// 
        /// It may be partial as the actual data is stored in the native dom.
        /// </summary>
        public readonly ArrayList InternalPartialElements = new ArrayList();

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

                    this.InternalPartialElements.Add(e);
					this.InternalValue.appendChild(e.InternalValue);

					return;
				}
			}
			#endregion


			throw new NotImplementedException();
		}

        void InternalNotifyChildren()
        {
            foreach (__XElement item in this.InternalPartialElements)
            {
                item.InternalValue = this.InternalGetElementByTag(item.InternalValue.getLocalName());
                item.InternalNotifyChildren();
            }
        }

		override protected void InternalEnsureElement()
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

                    var root = doc.createElement(name);

                    //var element = root.appendChild(doc.createElement(name));

                    this.InternalValue = root;
                    this.InternalNotifyChildren();
				}
				catch // (csharp.ThrowableException exc)
				{
                    //((java.lang.Throwable)(object)exc).printStackTrace();

                    //throw new NotSupportedException();
                    throw;
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
			catch // (csharp.ThrowableException exc)
			{
                //((java.lang.Throwable)(object)exc).printStackTrace();

                //throw new NotSupportedException();
                throw;
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
                    e.InternalNotifyChildren();
				}
				catch
				{
                    throw;
				}
			}
		}


		public XElement Element(XName name)
		{
            var InternalValue = InternalGetElementByTag(name.LocalName);

            if (InternalValue == null)
                return null;

            // should we see if we already have it?
			var e = new __XElement
			{
                InternalValue = InternalValue
			};

			return (XElement)(object)e;
		}

        private org.w3c.dom.Node InternalGetElementByTag(string name)
        {
            return this.InternalElement.getElementsByTagName(name).item(0);
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

            this.InternalPartialElements.Clear();
		}
	}
}

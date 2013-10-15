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
        public readonly List<__XElement> InternalPartialElements = new List<__XElement>();

        public void Add(object content)
        {
            if (content == null)
                return;

            InternalEnsureElement();

            #region string
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
            #endregion


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
                        e.InternalFixBeforeAdobt();
                        //Console.WriteLine("before adobt, add: " + e.InternalToString());
                        __adoptNode(e);
                    }

                    this.InternalPartialElements.Add(e);
                    this.InternalValue.appendChild(e.InternalValue);

                    //Console.WriteLine("after add: " + this.ToString());
                    return;
                }
            }
            #endregion

            Console.WriteLine("__XContainer.Add " + new { content });
            if (content != null)
                Console.WriteLine("__XContainer.Add Type " + content.GetType());

            throw new NotImplementedException();
        }


        void InternalNotifyChildren()
        {
            //    foreach (__XElement item in this.InternalPartialElements)
            //    {
            //        item.InternalValue = this.InternalGetElementByTag(item.InternalValue.getLocalName());
            //        item.InternalNotifyChildren();
            //    }
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
                catch
                {
                    throw;
                }
            }

            InternalFixTextNode(this.InternalValue);
        }

        private static javax.xml.parsers.DocumentBuilderFactory InternalCreateFactory()
        {
            var f = default(javax.xml.parsers.DocumentBuilderFactory);
            try
            {
                f = javax.xml.parsers.DocumentBuilderFactory.newInstance();
            }
            catch
            {
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

        public void __adoptNode(__XElement e)
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
                    //e.InternalValue = ownerDocument.importNode(e.InternalValue, true);
                    e.InternalValue = ownerDocument.adoptNode(e.InternalValue);
                    e.InternalNotifyChildren();
                }
                catch
                {
                    throw;
                }
            }
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


        public XElement Element(XName name)
        {
            var InternalValue = InternalGetElementByTag(name.LocalName);

            if (InternalValue == null)
                return null;

            // should we see if we already have it?

            return new __XElement { InternalValue = InternalValue };
        }

        public IEnumerable<XElement> Elements(XName name)
        {
            return this.Elements().Where(k => k.Name.LocalName == name.LocalName);
        }


        public IEnumerable<XElement> Elements()
        {
            // http://publib.boulder.ibm.com/infocenter/domhelp/v8r0/index.jsp?topic=%2Fcom.ibm.designer.domino.api.doc%2Fr_wpdr_dom6_domnode_getnodetype_r.html
            var DOMElement = 1;

            var a = new List<XElement>();

            var c = this.InternalElement.getChildNodes();

            for (int i = 0; i < c.getLength(); i++)
            {
                var InternalValue = c.item(i);

                if (InternalValue.getNodeType() == DOMElement)
                    a.Add(new __XElement { InternalValue = InternalValue });
            }



            return a;
        }
    }
}

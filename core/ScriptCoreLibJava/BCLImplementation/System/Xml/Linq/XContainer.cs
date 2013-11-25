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
            // tested by
            // X:\jsc.svn\examples\javascript\forms\FormsNIC\FormsNIC\ApplicationWebService.cs

            //Console.WriteLine("__XContainer Add " + new { content });


            if (content == null)
                return;

            InternalEnsureElement();



            //            0001 02000042 NextPageFromWebService.ApplicationWebService::<module>.SHA1528eebcd6a7f0708c37e60bd499d6140e316862d@1047698586
            //Y:\NextPageFromWebService.ApplicationWebService\staging.java\web\files
            //C:\Program Files (x86)\Java\jdk1.6.0_35\bin\javac.exe  -encoding UTF-8 -cp Y:\NextPageFromWebService.ApplicationWebService\staging.java\web\release;C:\util\appengine-java-sdk-1.8.3\lib\impl\*;C:\util\appengine-java-sdk-1.8.3\lib\shared\* -d "Y:\NextPageFromWebService.ApplicationWebService\staging.java\web\release" @"Y:\NextPageFromWebService.ApplicationWebService\staging.java\web\files"
            //Y:\NextPageFromWebService.ApplicationWebService\staging.java\web\java\ScriptCoreLibJava\BCLImplementation\System\Xml\Linq\__XContainer.java:58: illegal generic type for instanceof
            //        enumerable_10 = ((((Object)content) instanceof  __IEnumerable_1<Object>) ? (__IEnumerable_1<Object>)((Object)content) : (__IEnumerable_1<Object>)null);
            //                                                                       ^

            #region IEnumerable<object>
            var __IEnumerable = content as IEnumerable;
            if (__IEnumerable != null)
            {
                foreach (object item in __IEnumerable)
                {
                    Add(item);
                }

                return;
            }
            #endregion


            //            __XContainer.Add { content =    }
            //__XContainer.Add Type ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XNode
            //java.lang.RuntimeException
            //        at ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XContainer.Add(__XContainer.java:133)

            #region __XText
            {
                var e = content as __XText;

                if (e != null)
                {
                    this.InternalValue.appendChild(
                        this.InternalValue.getOwnerDocument().createTextNode(e.Value)
                    );
                    return;
                }
            }
            #endregion

            #region string
            {
                var e = content as string;

                if (e == null)
                {
                    if (content is int)
                    {
                        e = "" + ((int)content);
                    }
                    else if (content is long)
                    {
                        e = "" + ((long)content);
                    }
                }

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
                var SourceAttribute = (__XAttribute)(object)(content as XAttribute);
                if (SourceAttribute != null)
                {
                    var SourceAttributeValue = SourceAttribute.Value;

                    //Console.WriteLine("__XContainer Add " + new { SourceAttribute.Name, SourceAttributeValue });

                    SourceAttribute.InternalElement = this;
                    SourceAttribute.Value = SourceAttributeValue;
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

            //            { ColumnName = Key, value = 12 }
            //__XContainer.Add { content = 12 }
            //__XContainer.Add Type java.lang.Integer
            //java.lang.RuntimeException
            //        at ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XContainer.Add(__XContainer.java:148)
            //        at ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XElement.<init>(__XElement.java:38)
            //        at ScriptCoreLib.Library.StringConversionsForDataTable.ConvertToString(StringConversionsForDataTable.java:105)


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





        //Implementation not found for type import :
        //type: System.Xml.Linq.XContainer
        //method: System.Collections.Generic.IEnumerable`1[System.Xml.Linq.XNode] Nodes()
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!

        public IEnumerable<XNode> Nodes()
        {
            // http://publib.boulder.ibm.com/infocenter/domhelp/v8r0/index.jsp?topic=%2Fcom.ibm.designer.domino.api.doc%2Fr_wpdr_dom6_domnode_getnodetype_r.html
            var DOMElement = 1;
            var TEXT_NODE = 3;


            var a = new List<XNode>();

            var c = this.InternalElement.getChildNodes();

            for (int i = 0; i < c.getLength(); i++)
            {
                var InternalValue = c.item(i);

                if (InternalValue.getNodeType() == DOMElement)
                    a.Add((__XNode)new __XElement { InternalValue = InternalValue });
                else if (InternalValue.getNodeType() == TEXT_NODE)
                    a.Add((__XNode)new __XText { InternalValue = InternalValue });
                else
                    a.Add(new __XNode { InternalValue = InternalValue });
            }

            return a;
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


        public void ReplaceNodes(object content)
        {
            this.RemoveNodes();
            this.Add(content);

        }
    }
}

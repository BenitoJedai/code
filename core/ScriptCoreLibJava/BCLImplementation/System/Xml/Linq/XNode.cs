using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using javax.xml.transform;
using javax.xml.transform.dom;
using java.io;
using javax.xml.transform.stream;

namespace ScriptCoreLibJava.BCLImplementation.System.Xml.Linq
{
    [Script(Implements = typeof(global::System.Xml.Linq.XNode))]
    internal class __XNode : __XObject
    {
        public org.w3c.dom.Node InternalValue;

        virtual protected void InternalEnsureElement()
        {
        }

        public void InternalFixTextNode(org.w3c.dom.Node n)
        {
            const int TEXT_NODE = 3;

            var nl = n.getChildNodes();
            for (int i = 0; i < nl.getLength(); i++)
            {
                var k = nl.item(i);

                var IsTextNode = k.getNodeType() == TEXT_NODE;
                var IsContentNull = k.getNodeValue() == null;

                if (IsTextNode && IsContentNull)
                {
                    k.setNodeValue("");
                }
                else
                {
                    InternalFixTextNode(k);
                }
            }
        }

        public override string ToString()
        {
            return InternalToString();
        }

        public void InternalFixBeforeAdobt()
        {
            // why does this fix our attribute value problem??
            // <output id="Content"><div><a href="&#10;  ">sdfsdfsdf</a></div></output>
            // X:\jsc.svn\examples\php\PHPWiki\PHPWiki\ApplicationWebService.cs


            try
            {
                this.InternalEnsureElement();

                var s = new DOMSource(this.InternalValue);
                var w = new StringWriter();
                var r = new StreamResult(w);
                var f = TransformerFactory.newInstance();

                f.newTransformer().transform(s, r);
            }
            catch
            {
                throw;
            }

        }

        public string InternalToString()
        {
            // http://faq.javaranch.com/java/DocumentToString
            var value = default(string);

            try
            {
                this.InternalEnsureElement();

                // http://stackoverflow.com/questions/9150403/how-do-you-debug-an-xml-object-that-causes-a-transform-error-when-writing-to-str
                // http://dotcommers.wordpress.com/2008/10/22/javaxxmltransformtransformerexception-javalangnullpointerexception-how-to-solve/


                var s = new DOMSource(this.InternalValue);
                var w = new StringWriter();
                var r = new StreamResult(w);
                var f = TransformerFactory.newInstance();

                f.newTransformer().transform(s, r);

                value = w.getBuffer().toString();

                #region __XDocument
                var IsDocument = (this is __XDocument);
                if (!IsDocument)
                {
                    // hack.

                    {
                        var prefix = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

                        if (value.StartsWith(prefix))
                            value = value.Substring(prefix.Length);
                    }

                    {
                        var prefix = "\r\n";

                        if (value.StartsWith(prefix))
                            value = value.Substring(prefix.Length);
                    }
                }
                #endregion

            }
            catch
            {
                // The input node can not be null for a DOMSource for newTemplates!

                throw;
            }

            return value;
        }

    }
}

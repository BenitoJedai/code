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
                // X:\jsc.svn\examples\javascript\forms\FormsNIC\FormsNIC\ApplicationWebService.cs


                //Caused by: java.lang.NullPointerException
                //       at org.apache.xml.serializer.ToStream.writeAttrString(ToStream.java:2099)
                //       at org.apache.xml.serializer.ToStream.processAttributes(ToStream.java:2079)
                //       at org.apache.xml.serializer.ToStream.closeStartTag(ToStream.java:2623)
                //       at org.apache.xml.serializer.ToStream.characters(ToStream.java:1410)
                //       at org.apache.xalan.transformer.TransformerIdentityImpl.characters(TransformerIdentityImpl.java:1126)
                //       at org.apache.xml.serializer.TreeWalker.dispatachChars(TreeWalker.java:246)
                //       at org.apache.xml.serializer.TreeWalker.startNode(TreeWalker.java:416)
                //       at org.apache.xml.serializer.TreeWalker.traverse(TreeWalker.java:145)
                //       at org.apache.xalan.transformer.TransformerIdentityImpl.transform(TransformerIdentityImpl.java:390)
                //       at ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XNode.InternalFixBeforeAdobt(__XNode.java:77)
                //       ... 25 more

                throw;
            }

        }

        public string InternalToString()
        {
            // http://faq.javaranch.com/java/DocumentToString
            var value = default(string);

            Console.WriteLine("InternalToString " + new { this.InternalValue });
            try
            {


                //I/System.Console(12089): Caused by: java.lang.NullPointerException
                //I/System.Console(12089):        at org.apache.xml.serializer.ToStream.writeAttrString(ToStream.java:2099)
                //I/System.Console(12089):        at org.apache.xml.serializer.ToStream.processAttributes(ToStream.java:2079)
                //I/System.Console(12089):        at org.apache.xml.serializer.ToStream.closeStartTag(ToStream.java:2623)
                //I/System.Console(12089):        at org.apache.xml.serializer.ToStream.startElement(ToStream.java:1927)
                //I/System.Console(12089):        at org.apache.xalan.transformer.TransformerIdentityImpl.startElement(TransformerIdentityImpl.java:1073)
                //I/System.Console(12089):        at org.apache.xml.serializer.TreeWalker.startNode(TreeWalker.java:359)
                //I/System.Console(12089):        at org.apache.xml.serializer.TreeWalker.traverse(TreeWalker.java:145)
                //I/System.Console(12089):        at org.apache.xalan.transformer.TransformerIdentityImpl.transform(TransformerIdentityImpl.java:390)
                //I/System.Console(12089):        at ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XNode.InternalToString(__XNode.java:81)


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

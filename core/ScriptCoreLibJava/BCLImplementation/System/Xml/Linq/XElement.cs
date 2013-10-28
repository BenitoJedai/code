using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml.Linq;

namespace ScriptCoreLibJava.BCLImplementation.System.Xml.Linq
{
    [Script(Implements = typeof(global::System.Xml.Linq.XElement))]
    internal class __XElement : __XContainer
    {

        public __XElement()
            : this(null)
        {
        }

        public __XElement(XName name)
        {
            InternalElementName = (__XName)(object)name;
        }

        public __XElement(XName name, object item)
        {
            InternalElementName = (__XName)(object)name;
            this.Add(item);
        }

        public __XElement(XName name, params object[] c)
        {
            InternalElementName = (__XName)(object)name;
            foreach (var item in c)
            {
                this.Add(item);
            }
        }



        public static XElement Parse(string text)
        {
            return __XDocument.Parse(text).Root;
        }

        public string Value
        {
            get
            {
                var w = new StringBuilder();

                var f = this.InternalElement.getFirstChild();

                if (f == null)
                    return null;

                // http://faq.javaranch.com/java/GetNodeValue
                // http://java.sun.com/j2se/1.4.2/docs/api/constant-values.html#org.w3c.dom.Node.TEXT_NODE
                if (f.getNodeType() == 3)
                {
                    w.Append(f.getNodeValue());
                }

                return w.ToString();
            }
            set
            {
                this.InternalEnsureElement();
                this.RemoveNodes();
                this.Add(value);
            }
        }

        public void ReplaceAll(object content)
        {
            this.RemoveAttributes();
            this.RemoveNodes();
            this.Add(content);
        }



        public void ReplaceAttributes(object content)
        {
            this.RemoveNodes();
            this.Add(content);

        }

        public void RemoveAttributes()
        {



            foreach (var item in Attributes().ToArray())
            {
                this.InternalElement.removeAttribute(
                    item.Name.LocalName
                );

            }

        }

        public IEnumerable<XAttribute> Attributes()
        {
            var z = this.InternalElement.getAttributes();


            return Enumerable.Range(
                0,
                 z.getLength()
            ).Select(
                i =>
                {

                    return (XAttribute)new __XAttribute { InternalElement = this, Name = z.item(i).getNodeName() };

                }
            );



        }

        public XAttribute Attribute(XName name)
        {
            if (this.InternalElement.hasAttribute(name.LocalName))
                return new __XAttribute { InternalElement = this, Name = name };

            return null;
        }




        public static implicit operator XElement(__XElement n)
        {
            return (XElement)(object)n;
        }

        public __XName Name
        {
            get
            {
                // http://bugs.jqueryui.com/ticket/5557
                // http://ejohn.org/blog/nodename-case-sensitivity/
                var nodeName = this.InternalElement.getNodeName();


                return new __XName { InternalValue = nodeName };
            }
            set
            {

                InternalSetName(value);
            }
        }

        private void InternalSetName(__XName value)
        {
            // fcku jxml

            // [Fatal Error] :22:5: The element type "ydob" must be terminated by the matching end-tag "</ydob>".
            var old = "<" + this.Name.LocalName + " ";
            var oldclose = "</" + this.Name.LocalName + ">";

            var str = this.ToString();
            var fake = "<" + value.LocalName + " " + str.Substring(old.Length, str.Length - old.Length - oldclose.Length)
                + "</" + value.LocalName + ">";

            var e = (__XElement)(object)__XElement.Parse(fake);



            this.InternalElementName = value;

            e.InternalFixBeforeAdobt();
            //Console.WriteLine("before adobt, add: " + e.InternalToString());
            __adoptNode(e);

            //this.InternalPartialElements.Add(e);
            this.InternalValue.getParentNode().replaceChild(e.InternalValue, this.InternalValue);

            this.InternalValue = e.InternalValue;
        }



    }
}

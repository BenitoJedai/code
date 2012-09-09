using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.PHP.DOM;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Xml.Linq
{
    [Script(Implements = typeof(global::System.Xml.Linq.XElement))]
    internal class __XElement : __XContainer
    {
        public __XElement()
            : this("item", null, null)
        {
        }

        public __XElement(XName name)
            : this(name, null, null)
        {
        }

        public __XElement(XName name, object i)
            : this(name, i, null)
        {
        }


        public __XElement(XName name, params object[] c)
            : this(name, null, c)
        {
        }

        public __XElement(XName name, object i, params object[] c)
        {

            InternalElementName = (__XName)(object)name;

            if (i != null)
                this.Add(i);

            if (c != null)
                this.Add(c);
        }

        public static XElement Parse(string text)
        {
            var x = __XDocument.Parse(text);

            return x.Root;
        }


        public string Value
        {
            get
            {
                return this.InternalElement.nodeValue;
            }
            set
            {
                this.InternalElement.nodeValue = value;
            }
        }


        #region Attributes
        public XAttribute Attribute(XName name)
        {
            return this.Attributes(name).FirstOrDefault();
        }

        public IEnumerable<XAttribute> Attributes(XName name)
        {
            return this.Attributes().Where(k => k.Name == name);
        }

        public IEnumerable<XAttribute> Attributes()
        {
            var a = this.InternalElement.attributes;

            return Enumerable.Range(0, a.length).Select(
                i =>
                {
                    var attr = (DOMAttr)a.item(i);

                    return (XAttribute)new __XAttribute(
                        XName.Get(attr.name, null),
                        null
                    )
                    {
                        InternalElement = this
                    };
                }
            );
        }
        #endregion
    }
}

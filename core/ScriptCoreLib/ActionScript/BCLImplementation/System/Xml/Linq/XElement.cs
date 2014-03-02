using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Xml.Linq
{
    using AS3_QName = global::ScriptCoreLib.ActionScript.QName;
    using AS3_XML = global::ScriptCoreLib.ActionScript.XML;
    using AS3_XMLList = global::ScriptCoreLib.ActionScript.XMLList;


    [Script(Implements = typeof(XElement))]
    internal class __XElement : __XContainer
    {

        public __XName Name
        {
            get
            {

                AS3_QName InternalQName = (AS3_QName)this.InternalElement.name();


                // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/XML.html#name()
                return new __XName { InternalValue = InternalQName.localName };
            }
            set
            {
                this.InternalElement.setLocalName(value.LocalName);
            }
        }

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

        public static XElement Parse(string e)
        {
            var doc = __XDocument.Parse(e);

            return doc.Root;
        }


        public void RemoveAll()
        {
            if (this.InternalElement == null)
                return;

            var p = this.InternalElement.children();

            while (p.length() > 0)
            {
                __delete(p, 0);
            }
        }


        public string Value
        {
            get
            {
                var w = new StringBuilder();

                var c = this.InternalElement.text();
                var length = c.length();

                for (int i = 0; i < length; i++)
                {
                    w.Append(c[i]);
                }

                return w.ToString();
            }
            set
            {
                RemoveAll();
                Add(value);
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
            var a = this.InternalElement.attributes();

            return Enumerable.Range(0, a.length()).Select(
                i =>
                {
                    return (XAttribute)new __XAttribute(
                        XName.Get("" + a[i].name(), null),
                        null
                    )
                    {
                        InternalParentElement = this
                    };
                }
            );
        }
        #endregion





        public IEnumerable<XElement> DescendantsAndSelf()
        {
            // X:\jsc.svn\examples\actionscript\air\AIRNestedIFrameWithTransform\AIRNestedIFrameWithTransform\ApplicationSprite.cs

            return Enumerable.Concat(
                new[] { (XElement)(object)this },
                this.Elements().SelectMany(k => k.DescendantsAndSelf())
            );
        }
    }
}

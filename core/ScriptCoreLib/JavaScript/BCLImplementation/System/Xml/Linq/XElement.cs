using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{
    // http://referencesource.microsoft.com/#System.Xml.Linq/XLinq.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Xml.XDocument/System/Xml/Linq/XElement.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Xml.XDocument/src/System/Xml/Linq/XElement.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System.Xml.Linq/System.Xml.Linq/XElement.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Xml\Linq\XElement.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Xml\Linq\XElement.cs

    [Script(Implements = typeof(XElement))]
    internal class __XElement : __XContainer
    {
        // what about binary xml?
        // http://en.wikipedia.org/wiki/Binary_XML
        // could aswell use TLV?
        // http://en.wikipedia.org/wiki/Extensible_Binary_Meta_Language
        // suspended memory? serialize to IL?
        // https://msdn.microsoft.com/en-us/library/cc219210.aspx




        // 20150105
        // c# shall have xml literals?
        // https://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/6942534-implement-xml-literals

        // tested by?

        // http://weblogs.asp.net/kennykerr
        // http://msdn.microsoft.com/en-us/magazine/cc163436.aspx



        // "C:\Program Files (x86)\SketchUp\SketchUp 2014\Tools\RubyStdLib\rexml\element.rb"
        public __XName Name
        {
            get
            {
                // X:\jsc.svn\examples\javascript\Test\TestXElementAdd\TestXElementAdd\Application.cs



                // http://bugs.jqueryui.com/ticket/5557
                // http://ejohn.org/blog/nodename-case-sensitivity/
                var nodeName = this.InternalElement.nodeName;

                // for html elements lets lowercase em. or are we working on xml object?
                // firefox has innerHTML for xml also. 

                if (this.InternalElement.namespaceURI == "http://www.w3.org/1999/xhtml")
                {
                    //if (Expando.InternalIsMember(this.InternalValue, "innerHTML"))
                    nodeName = nodeName.ToLower();
                }

                return new __XName { InternalValue = nodeName };
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #region ctor
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


        public __XElement(XElement other)
        {
            this.InternalValue = ((__XElement)(object)other).InternalElement.cloneNode(true);
        }
        #endregion


        public string Value
        {
            get
            {
                return this.InternalElement.text;
            }
            set
            {
                this.RemoveAll();
                this.Add(value);
            }
        }

        public void RemoveAll()
        {
            if (this.InternalElement == null)
                return;

            RemoveNodes();
            RemoveAttributes();
        }

        public void RemoveAttributes()
        {
            foreach (var item in Attributes().ToArray())
            {
                this.InternalElement.removeAttribute(item.Name.LocalName);
            }
        }

        public XAttribute Attribute(XName name)
        {
            var e = InternalElement;

            if (null == e.getAttribute(name.LocalName))
                return null;

            // http://msdn.microsoft.com/en-us/library/ms757048(VS.85).aspx
            // there is no hasAttribute method in IE

            //if (e.hasAttribute(name.LocalName))
            return (XAttribute)(object)new __XAttribute(name, null) { InternalElement = this };
        }

        public IEnumerable<XAttribute> Attributes()
        {
            return this.InternalElement.attributes.Select(
                k =>
                {
                    return (XAttribute)new __XAttribute(k.name, null)
                    {
                        InternalElement = this
                    };
                }
            );
        }

        public void SetAttributeValue(XName name, object value)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\JavaScript\Extensions\INodeExtensionsWithXLinq.cs

            var a = this.Attribute(name);
            if (a == null)
            {
                this.Add(new XAttribute(name, value));
                return;
            }

            a.Value = global::System.Convert.ToString(value);
        }

        //script: error JSC1000: No implementation found for this native method, please implement [System.Xml.Linq.XElement.SetAttributeValue(System.Xml.Linq.XName, System.Object)]


        public static XElement Parse(string text)
        {
            // X:\jsc.svn\examples\javascript\test\TestNullObjectFromWebService\TestNullObjectFromWebService\Application.cs

            if (text == null)
                throw new ArgumentNullException("XElement.Parse text is null");

            if (text == "")
                throw new ArgumentNullException("XElement.Parse Root element is missing.");

            // xml still is not yet correclty available for service workers.

            // X:\jsc.svn\examples\javascript\test\TestServiceWorkerFetchHTML\TestServiceWorkerFetchHTML\Application.cs

            var x = __XDocument.Parse(text);

            return x.Root;
        }

        public void ReplaceAll(object content)
        {
            this.RemoveAll();

            var x = (content as XElement);
            if (x != null)
            {
                InternalAddFrom(x);
            }

        }

        public void InternalAddFrom(XElement x)
        {
            var a = x.Attributes();
            foreach (var item in a)
            {
                this.Add(item);
            }

            var nodes = x.Nodes().ToArray();
            foreach (var item in nodes)
            {
                this.Add(item);
            }
        }

        public void ReplaceAll(params object[] collection)
        {
            this.RemoveAll();

            // is this correct?
            foreach (var content in collection)
            {
                var x = (content as XElement);
                if (x != null)
                {
                    InternalAddFrom(x);
                }
            }
        }

        public IEnumerable<XElement> DescendantsAndSelf()
        {
            return Enumerable.Concat(
                new[] { (XElement)(object)this },
                this.Descendants()
            );
        }


        // script: error JSC1000: No implementation found for this native method, please implement [static System.Xml.Linq.XElement.op_Explicit(System.Xml.Linq.XElement)]
        public static explicit operator string(__XElement element)
        {
            // X:\jsc.svn\examples\javascript\linq\VBWebSQLXElement\VBWebSQLXElement\Application.vb

            return element.ToString();
        }

        public static implicit operator XElement(__XElement x)
        {
            return (XElement)(object)x;
        }

        public static implicit operator __XElement(XElement x)
        {
            return (__XElement)(object)x;
        }
    }
}

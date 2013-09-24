using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.XML;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.Extensions
{
    [Script]
    public static class XLinqExtensions
    {
        public static IHTMLElement AsHTMLElement(this XElement value)
        {
            var c = new IHTMLDiv();

            c.innerHTML = value.ToString();

            return ((IHTMLElement)c.firstChild);
        }

        public static XDocument ToXDocument(this IXMLDocument doc)
        {
            return (__XDocument)doc;
        }

        public static XElement AsXElement(this IElement e)
        {
            return (XElement)(object)new __XElement((XName)null) { InternalValue = e };
        }

      
        public static IXMLDocument AsIXMLDocument(this XDocument doc)
        {
            return ((__XDocument)(object)doc).InternalDocument;
        }

    }
}

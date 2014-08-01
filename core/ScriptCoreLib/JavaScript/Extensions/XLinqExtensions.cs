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
            // X:\jsc.svn\examples\javascript\Test\TestXElementAdd\TestXElementAdd\Application.cs

            if (value == null)
                return null;

            // X:\jsc.svn\examples\javascript\appengine\StopwatchTimetravelExperiment\StopwatchTimetravelExperiment\Application.cs

            var __XElement = (__XElement)value;

            var c = new IHTMLDiv();


            var xml = value.ToString();
            //Console.WriteLine("AsHTMLElement " + new { xml });
            c.innerHTML = xml;

            var firstChild = ((IHTMLElement)c.firstChild);

            __XElement.InternalValue = firstChild;


            //var zxml = value.ToString();
            //Console.WriteLine("AsHTMLElement " + new { zxml });

            return firstChild;
        }

        public static XDocument ToXDocument(this IXMLDocument doc)
        {
            return (__XDocument)doc;
        }

        public static XElement AsXElement(this IElement e)
        {
            // X:\jsc.svn\examples\javascript\forms\test\TestBindingSource\TestBindingSource\Application.cs
            if (e == null)
                return null;

            return (XElement)(object)new __XElement((XName)null) { InternalValue = e };
        }


        public static IXMLDocument AsIXMLDocument(this XDocument doc)
        {
            return ((__XDocument)(object)doc).InternalDocument;
        }

    }
}

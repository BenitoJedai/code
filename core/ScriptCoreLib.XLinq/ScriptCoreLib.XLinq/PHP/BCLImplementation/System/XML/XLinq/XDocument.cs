using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.PHP.DOM;

namespace ScriptCoreLib.PHP.BCLImplementation.System.XML.XLinq
{
    [Script(Implements = typeof(global::System.Xml.Linq.XDocument))]
    internal class __XDocument : __XContainer
    {
        public DOMDocument InternalDocument;

        public static __XDocument Parse(string text)
        {
            var doc = new DOMDocument();

            doc.loadXML(text);

            return new __XDocument { InternalDocument = doc };
        }

        public override string ToString()
        {
            return InternalDocument.saveXML();
        }
    }
}

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

        __XElement _Root;

        public XElement Root
        {
            get
            {
                if (_Root == null)
                {
                    _Root = new __XElement(null, null) { InternalValue = InternalDocument.documentElement };
                }

                return (XElement)(object)_Root;
            }
        }

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

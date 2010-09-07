using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.BCLImplementation.System.Xml
{
    [Script(Implements = typeof(global::System.Xml.XmlReader))]
    internal class __XmlReader
    {
        public XDocument InternalDocument;

        public static __XmlReader Create(string inputUri)
        {
            var source = new WebClient().DownloadString(inputUri);

            return new __XmlReader { InternalDocument = XDocument.Parse(source) };
        }
    }
}

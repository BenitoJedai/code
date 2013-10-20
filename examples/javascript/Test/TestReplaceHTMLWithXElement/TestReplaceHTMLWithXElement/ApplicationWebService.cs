using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestReplaceHTMLWithXElement
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public async Task<string> SpecialContent()
        {
            Thread.Sleep(1000);
            return "'hello world'";
        }

        public async Task<XElement> Content()
        {
            Thread.Sleep(1000);

            return new XElement("output",
                "hello world",

                new XComment("a comment"),
                new XElement("u", "underline"),

                new XText("XText")

             );
        }
    }


    // ?
    [Obsolete("PHTMLOutput")]
    class XHTMLOutput
    {
        public XElement xml;
    }
}

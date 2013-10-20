using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestAttachXElementToDocument
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public async Task<XElement> Content()
        {
            //<TargetFrameworkVersion>4.5.1</TargetFrameworkVersion>

            return new XElement("b",
                "hello world",

                new XComment("a comment"),
                new XElement("u", "underline"),

                new XText("XText")

             );
        }

    }
}

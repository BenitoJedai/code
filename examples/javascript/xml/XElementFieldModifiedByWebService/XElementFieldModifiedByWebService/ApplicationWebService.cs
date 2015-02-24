using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XElementFieldModifiedByWebService
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // what about parent?
        public XElement output;

        public XElement Content;

        public Task<string> Modify()
        {
            // couuld we be interacting with svg/collada
            // could we be doing it via webrtc?

            Content.ReplaceNodes(

                new XElement(
                    "span",
                    new XAttribute("style", "color: red;"),
                    "Modified!"
                )
            );


            // {"The parent is missing."}
            output.ReplaceAll(
                XElement.Parse(
                    XElementFieldModifiedByWebService.HTML.Pages.FooSource.Text
                ).Nodes()
            );

            // Set-Cookie:InternalFields=field_Content=<p id="Content" style="padding: 2em;     color: blue;">hello world</p>; path=/

            return "ok".ToTaskResult();
        }
    }
}

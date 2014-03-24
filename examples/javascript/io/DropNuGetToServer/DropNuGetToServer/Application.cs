using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DropNuGetToServer;
using DropNuGetToServer.Design;
using DropNuGetToServer.HTML.Pages;
using System.Diagnostics;

namespace DropNuGetToServer
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            Native.document.body.ondragover +=
                evt =>
                {
                    evt.stopPropagation();
                    evt.preventDefault();

                    evt.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.

                    page.Header.innerText = new { files = evt.dataTransfer.items.length }.ToString();

                };

            Native.document.body.ondragleave +=
                delegate
                {
                    page.Header.innerText = "...";
                };

            Native.document.body.ondrop +=
                 evt =>
                 {
                     evt.stopPropagation();
                     evt.stopImmediatePropagation();

                     evt.preventDefault();


                     var s = Stopwatch.StartNew();
                     page.Header.innerText = "uploading...";

                     evt.dataTransfer.files.AsEnumerable().WithEach(
                         async f =>
                         {
                             // { name = Chrome.Web.Server.1.0.0.0.nupkg, type = , size = 22940 }

                             page.Header.innerText += new { f.size, f.name, }.ToString();

                             // signature check
                             // like code contracts?
                             var bytes = await f.readAsBytes();

                             // { size = 47079, name = Abstractatech.JavaScript.ApplicationPerformance.1.0.0.0.nupkg }80

                             // { size = 31572, name = GIFEncoder.1.0.0.0.nupkg }0x504b

                             page.Header.innerText += "0x";
                             page.Header.innerText += bytes[0].ToString("x2");
                             page.Header.innerText += bytes[1].ToString("x2");

                             var d = new FormData();


                             d.append("foo", f, f.name);

                             var xhr = new IXMLHttpRequest();

                             xhr.open(ScriptCoreLib.Shared.HTTPMethodEnum.POST, "/upload");

                             xhr.InvokeOnComplete(
                                 delegate
                                 {
                                     page.Header.innerText = "upload complete... " + new { s.ElapsedMilliseconds };
                                 }
                              );

                             xhr.send(d);

                         }
                     );
                 };
        }

    }
}

using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FileExplorer.Design;
using FileExplorer.HTML.Pages;

namespace FileExplorer
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.getExternalStoragePublicDirectory(
                "",
                root =>
                {
                    Action<string, IHTMLElement> listFiles = null;

                    listFiles =
                        (xroot, xcontainer) =>
                        {
                            if (!xroot.EndsWith("/"))
                                xroot += "/";

                            new IHTMLPre { innerText = xroot }.AttachTo(xcontainer);

                            service.listFiles(xroot,
                                f =>
                                {
                                    var ch = new IHTMLDiv();

                                    ch.style.marginLeft = "1em";


                                    new IHTMLButton { innerText = f }.AttachTo(xcontainer).With(
                                        btn =>
                                        {
                                            btn.style.display = IStyle.DisplayEnum.block;

                                            btn.onclick +=
                                                delegate
                                                {
                                                    if (f.EndsWith("/"))
                                                    {
                                                        listFiles(xroot + f, ch);
                                                    }
                                                };
                                        }
                                    );

                                    ch.AttachTo(xcontainer);

                                }
                            );
                        };


                    listFiles(root, Native.Document.body);
                }
            );
        }

    }
}

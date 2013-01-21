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
using AndroidListApplications.Design;
using AndroidListApplications.HTML.Pages;

namespace AndroidListApplications
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
        public Application(IApp page)
        {

            new IHTMLButton { innerText = "Install" }.AttachToDocument().With(
                   btn =>
                   {
                       // http://help.adobe.com/en_US/air/build/WSfffb011ac560372f-5d0f4f25128cc9cd0cb-7ffd.html

                       btn.onclick +=
                           delegate
                           {
                               service.Install("assets/AndroidListApplications/foo.apk");
                           };
                   }
               );

            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                (packageName, name) =>
                {
                    new IHTMLButton { innerText = "Remove" }.AttachToDocument().With(
                        btn =>
                        {
                            // http://help.adobe.com/en_US/air/build/WSfffb011ac560372f-5d0f4f25128cc9cd0cb-7ffd.html

                            btn.onclick +=
                                delegate
                                {
                                    if (!Native.Window.confirm("Remove " + name + "?"))
                                        return;

                                    service.Remove(packageName, name);
                                };
                        }
                    );
                    new IHTMLButton { innerText = "Launch" }.AttachToDocument().With(
                        btn =>
                        {
                            // http://help.adobe.com/en_US/air/build/WSfffb011ac560372f-5d0f4f25128cc9cd0cb-7ffd.html

                            btn.onclick +=
                                delegate
                                {
                                    service.Launch(packageName, name);
                                };
                        }
                    );

                    new IHTMLSpan { innerText = name }.AttachToDocument();

                    new IHTMLBreak().AttachToDocument();

                }
            );
        }

    }
}

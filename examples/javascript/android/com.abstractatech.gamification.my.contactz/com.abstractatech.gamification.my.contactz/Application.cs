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
using com.abstractatech.gamification.my.contactz.Design;
using com.abstractatech.gamification.my.contactz.HTML.Pages;
using idea_remixer.tumblr.com;

namespace com.abstractatech.gamification.my.contactz
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
            Action<string> y = x =>
                {
                    new IHTMLAnchor
                    {
                        href = "#" + x + "#fake.mp3",
                        innerText = x
                    }.AttachToDocument();

                };

            service.WebMethod2("",
                y: y,
                done: delegate
                {

                    new ApplicationImplementation();
                }
            );


        }

    }
}

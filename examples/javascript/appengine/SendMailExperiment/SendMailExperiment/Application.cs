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
using SendMailExperiment.Design;
using SendMailExperiment.HTML.Pages;

namespace SendMailExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            new IHTMLButton { innerText = "send" }.AttachToDocument().WhenClicked(
                async delegate
                {

                    await new ApplicationWebService
                    {
                        FromAddress = "admin@example.com",
                        FromName = "Example.com Admin",

                        ToAddress = "user@example.com",
                        ToName = "Mr. User",

                        Subject = "foo",

                        MessageString = "hello world"
                    }.SendEMail();


                }
            );

        }

    }
}

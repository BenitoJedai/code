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
using SHA1Experiment.Design;
using SHA1Experiment.HTML.Pages;

namespace SHA1Experiment
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

            new IHTMLButton { innerText = "do" }.AttachToDocument().WhenClicked(
                async delegate
                {
                    //var bytes = await this.GetSHA1Bytes(
                    //    Encoding.UTF8.GetBytes("hello world")
                    //);

                    //new IHTMLPre { innerText = bytes.ToHexString() }.AttachToDocument();

                    var x = await this.GetSHA1HexString(
                          "hello world"
                      );

                    new IHTMLPre { innerText = x }.AttachToDocument();
                }
            );
        }

    }
}

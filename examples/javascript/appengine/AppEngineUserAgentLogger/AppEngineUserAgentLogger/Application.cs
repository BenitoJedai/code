using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AppEngineUserAgentLogger;
using AppEngineUserAgentLogger.Design;
using AppEngineUserAgentLogger.HTML.Pages;
using System.Threading;
using System.Data;

namespace AppEngineUserAgentLogger
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
            //screen size

            Native.document.body.querySelectorAll("script").WithEach(
                x => x.Orphanize()
            );

            this.body = Native.document.body;

            Console.WriteLine(new { this.body });

            var body_a = this.body.Attributes().ToArray();
            var body_n = this.body.Nodes().ToArray();




            this.SetScreenSize(Native.window.screen.width, Native.window.screen.height);
            //Native.window.screen.width

            page.NextPage.WhenClicked(
                async delegate
                {
                    await this.GoNextPage();
                }
            );

        }

    }
}

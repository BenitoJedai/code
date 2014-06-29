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
using TestScopeWithDelegate;
using TestScopeWithDelegate.Design;
using TestScopeWithDelegate.HTML.Pages;

namespace TestScopeWithDelegate
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        Action<string> yield = x => Native.css.style.backgroundColor = x;


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //  Task.Factory.StartNewWithProgress(
            // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Extensions\TaskAsyncExtensions.cs

            Native.css.style.transition = "background-color 300ms linear";

            // future jsc will allow a background thread to directly talk to the DOM, while creating a callsite in the background
            IProgress<string> set_backgroundColor = new Progress<string>(yield);

            var g = new
            {
                yield,
                colors = new
                {
                    white = "white",
                    yellow = "yellow",
                    cyan = "cyan"
                }
            };

            //var colors = new { yellow = "yellow", cyan = "cyan" };

            new IHTMLButton { "invoke" }.AttachToDocument().onclick +=
                async e =>
                {
                    await Task.Run(async delegate
                    {
                        // we could also support direct delegates?

                        set_backgroundColor.Report(g.colors.yellow);

                        // the .css thing is under our control is it not. could be the first to get special support
                        //Native.css.style.backgroundColor = "red";

                        await Task.Delay(1000);

                        set_backgroundColor.Report(g.colors.cyan);
                    });
                    await Task.Delay(1000);
                    set_backgroundColor.Report(g.colors.white);
                };
        }

    }
}

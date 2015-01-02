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
using TestAsyncMouseOver;
using TestAsyncMouseOver.Design;
using TestAsyncMouseOver.HTML.Pages;

namespace TestAsyncMouseOver
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150102

            // whats the references/ analyzers/ ?
            // X:\jsc.svn\examples\javascript\async\Test\Test453Async\Test453Async\Program.cs

            new { }.With(
                async scope =>
                {
                    // works with 2012 web update4, not with 2013, not with 2015?
                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/20150101/async

                    Native.css.style.backgroundColor = "yellow";

                    //await Native.document.onmouseover;
                    //await Native.document.async.onm;
                    await Native.document.body.async.onmouseover;

                    Native.css.style.backgroundColor = "cyan";

                }
            );

        }

    }
}

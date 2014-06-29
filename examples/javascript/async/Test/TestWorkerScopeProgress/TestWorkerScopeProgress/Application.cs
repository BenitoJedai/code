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
using TestWorkerScopeProgress;
using TestWorkerScopeProgress.Design;
using TestWorkerScopeProgress.HTML.Pages;

namespace TestWorkerScopeProgress
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140629
            // can we get support for any level1 scope progress elements?
            // after that we may want to allow Tasks being sent back to the worker.
            // what about sharing static bools too. we already share strings..
            // what about synchronizing scope objects once worker is running?
            // a Thread Signal or Task Yield may request the sync to take place.
            // this would allow data sharing during computation?
            // then we may also want to get delegate sharing..




        }

    }
}

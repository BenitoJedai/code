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
using TestNavigatorCores;
using TestNavigatorCores.Design;
using TestNavigatorCores.HTML.Pages;
using System.Threading;

namespace TestNavigatorCores
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
            new IHTMLPre {
               new {
                       Native.window.navigator.hardwareConcurrency }
           }.AttachToDocument();
            // {{ hardwareConcurrency = 4 }}

            // http://stackoverflow.com/questions/6474081/which-core-is-a-task-running-on
            // http://stackoverflow.com/questions/18059569/does-a-c-sharp-task-run-on-one-core

            new IHTMLPre {
               new {
                      Environment.ProcessorCount
                }
           }.AttachToDocument();
            // {{ ProcessorCount = 4 }}

            // ready for multicore tasks!

        }

    }
}

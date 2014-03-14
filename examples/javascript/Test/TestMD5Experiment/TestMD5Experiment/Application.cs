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
using TestMD5Experiment;
using TestMD5Experiment.Design;
using TestMD5Experiment.HTML.Pages;

namespace TestMD5Experiment
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
            var a = new MD5.MD5();

            //a.FingerPrint
            a.Value = "hello";

            // -- { FingerPrint = D41D8CD98F00B204E9800998ECF8427E }
            // a.FingerPrint = "D41D8CD98F00B204E9800998ECF8427E"
            // { FingerPrint = FFFFFFA3FFFFFFF8FFFFFFA1FFFFFFBD }
            new IHTMLPre { new { a.FingerPrint } }.AttachToDocument();

        }

    }
}

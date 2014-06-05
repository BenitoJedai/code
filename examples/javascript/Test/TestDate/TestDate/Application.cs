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
using TestDate;
using TestDate.Design;
using TestDate.HTML.Pages;

namespace TestDate
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
            // X:\jsc.svn\examples\javascript\linq\test\TestSelectDate\TestSelectDate\ApplicationWebService.cs

            //{ { x = 05.06.2014 08:12:03 } }
            //{ { Ticks = 635375419230390000 } }
            //{ { Date = 05.06.2014 03:00:00 } }


            var x = DateTime.Now;

            new IHTMLPre { new { x } }.AttachToDocument();

            new IHTMLPre { new { x.Ticks } }.AttachToDocument();

            // script: error JSC1000: No implementation found for this native method, please implement [System.DateTime.get_Date()]
            new IHTMLPre { new { x.Date } }.AttachToDocument();


        }

    }
}

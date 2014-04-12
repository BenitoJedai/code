using CRCExample.ActionScript;
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
using TestCRC;
using TestCRC.Design;
using TestCRC.HTML.Pages;

namespace TestCRC
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140412

            var crc = new Crc32Helper();
            crc.ComputeCrc32(new byte[] { 1, 2, 0xfe, 0xff });

            // Crc32Value = 1027690409
            // 1027690409 vs 1027690409
            new IHTMLPre { "" + crc.Crc32Value + " vs 1027690409" }.AttachToDocument();


        }

    }
}

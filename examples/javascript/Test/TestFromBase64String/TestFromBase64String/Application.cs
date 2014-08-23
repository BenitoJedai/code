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
using TestFromBase64String;
using TestFromBase64String.Design;
using TestFromBase64String.HTML.Pages;

namespace TestFromBase64String
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
            // {{ capacity = 10.666666666666666 }}
            var capacity = 4 * "RENJTQ==".Length / 3;
            new IHTMLPre { new { capacity } }.AttachToDocument();


            // d = (((4 * nx4ABtNdQz66ZYUODttTfw(b)) / 3));
            // b: "RENJTQ=="
            // ScriptCoreLib.Shared.BCLImplementation.System.__Convert.FromBase64String
            // did we not test it already in every possible way?
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Convert.FromBase64String.cs
            var x = Convert.FromBase64String("RENJTQ==");

            new IHTMLPre { new { x.Length } }.AttachToDocument();
        }

    }
}

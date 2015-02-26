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
using TestUTF8FromBase64StringOrDefault;
using TestUTF8FromBase64StringOrDefault.Design;
using TestUTF8FromBase64StringOrDefault.HTML.Pages;

namespace TestUTF8FromBase64StringOrDefault
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201502/20150226
            //var n = ScriptCoreLib.Library.StringConversions.UTF8FromBase64StringOrDefault(e: Application.Subject);


            //45ms enter __Convert.FromBase64String {{ Length = 84 }}
            //2015-02-26 22:17:13.293 :18988/view-source:50243 49ms exit __Convert.FromBase64String {{ Length = 63 }}

            // wtf?

            // Uncaught RangeError: Maximum call stack size exceeded



            //          // ScriptCoreLib.JavaScript.DOM.IStyleSheet.get_all
            //          this.WQkABoiqUze4dnBbdLSgcA = function()
            //{
            //              var b, c, d;

            //              d = !!_4gMABIiqUze4dnBbdLSgcA.pxYABvyO4TCZatbolZHoyA();

        }

        public const string Subject = "PGgxPkpTQyAtIFRoZSAuTkVUIGNyb3NzY29tcGlsZXIgZm9yIHdlYiBwbGF0Zm9ybXMuIHJlYWR5LjwvaDE+";
    }

    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var n = ScriptCoreLib.Library.StringConversions.UTF8FromBase64StringOrDefault(e: Application.Subject);


            jsc.meta.Commands.Rewrite.RewriteToUltraApplication.
                RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

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
using TestInlinePrefixDecementWhile;
using TestInlinePrefixDecementWhile.Design;
using TestInlinePrefixDecementWhile.HTML.Pages;

namespace TestInlinePrefixDecementWhile
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140720
            // X:\jsc.svn\examples\javascript\Test\TestInlinePostfixDecrementWhile\TestInlinePostfixDecrementWhile\Application.cs

            var count = 7;


            //while (--count >= 0)
            //{
            //    //crc = CrcTable[(crc ^ buffer[offset++]) & 0xFF] ^ (crc >> 8);

            //    new IHTMLPre { new { count } }.AttachToDocument();

            //}


            var flag = --count >= 0;
            while (flag)
            {
                //crc = CrcTable[(crc ^ buffer[offset++]) & 0xFF] ^ (crc >> 8);

                new IHTMLPre { new { count } }.AttachToDocument();

                flag = --count >= 0;
            }
        }

    }
}

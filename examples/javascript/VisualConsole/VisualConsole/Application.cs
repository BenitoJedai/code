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
using VisualConsole;
using VisualConsole.Design;
using VisualConsole.HTML.Pages;
using System.IO;

namespace VisualConsole
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
            // http://stackoverflow.com/questions/5170745/clear-output-in-telnet
            //Console.Clear();
            Native.document.body.Clear();

            Console.Title = "visual console";

            new IHTMLPre { "VisualConsole" }.AttachToDocument();

            var c = new xConsole();
            Console.SetOut(c);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("prefix: ");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("hello");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("world");

            c.WriteLine("can we see it?");
        }

        #region xConsole
        //class xConsole : StringWriter
        [Obsolete("jsc:js does not allow to overrider an override?")]
        class xConsole : TextWriter
        {
            // http://www.danielmiessler.com/study/encoding_encryption_hashing/
            [Obsolete("can we have encrypted encoding?")]
            public override Encoding Encoding
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public override void Write(string value)
            {
                var p = new IHTMLCode { innerText = value }.AttachToDocument();
                var s = p.style;

                // jsc, enum tostring?
                if (Console.ForegroundColor == ConsoleColor.Red)
                    s.color = "red";

                if (Console.ForegroundColor == ConsoleColor.Blue)
                    s.color = "blue";

                if (Console.ForegroundColor == ConsoleColor.Gray)
                    s.color = "gray";
            }

            public override void WriteLine(string value)
            {
                //Console.WriteLine(new { value });


                Write(value);

                new IHTMLBreak { }.AttachToDocument();
            }
        }
        #endregion

    }
}

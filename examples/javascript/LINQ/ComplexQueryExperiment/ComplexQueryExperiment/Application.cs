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
using ComplexQueryExperiment;
using ComplexQueryExperiment.Design;
using ComplexQueryExperiment.HTML.Pages;
using System.IO;

namespace ComplexQueryExperiment
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
            Native.document.body.Clear();
            Native.document.body.style.backgroundColor = "black";
            Native.document.body.style.color = "white";
            Native.document.body.css.descendant.style.whiteSpace = IStyle.WhiteSpaceEnum.pre;

            Console.SetOut(new xConsole());

            // ternary op not supported
            // it compiles to js but runtime info is missing for now 

            var x = from z in new xTable()
                    //select z;
                    select new { z.field1 };

            var f = x.FirstOrDefault();

        }

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

            if (Console.ForegroundColor == ConsoleColor.Yellow)
                s.color = "yellow";

            if (Console.ForegroundColor == ConsoleColor.Magenta)
                s.color = "magneta";
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

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
using CallerFilePathExceptionExperiment;
using CallerFilePathExceptionExperiment.Design;
using CallerFilePathExceptionExperiment.HTML.Pages;
using System.Runtime.CompilerServices;

namespace CallerFilePathExceptionExperiment
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
            Native.window.onerror +=
                e =>
                {
                    // Uncaught Error: { CallerFilePath = x:\jsc.svn\examples\javascript\future\CallerFilePathExceptionExperiment\CallerFilePathExceptionExperiment\Application.cs, CallerLineNumber = 46, Message = error! }
                    new IHTMLPre { e.message }.AttachToDocument();

                    e.preventDefault();
                    e.stopPropagation();
                };

            new IHTMLButton { "throw" }.AttachToDocument().onclick +=
                delegate
                {
                    throw "error!".AsException();
                };

        }

    }

    // ncaught SyntaxError: Unexpected token .
    // CallerFilePathExceptionExperiment.MyException..ctor
    //type$PT8xlAp4NjygityvZqQogg.bgAABgp4NjygityvZqQogg = function (b)
    //{
    //  var a = [this];

    //  a[0]..ctor(b);
    //};
    // jsc wont allow intheritance to native class. could we do it?

    //[Script(IsNative = true)]
    //[Script(IsNative = true)]
    //public class MyException : Exception
    //{

    //    public MyException([CallerFilePath] string message = "") : base(message) { }

    //}

    public static class X
    {
        //    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140420
        [Obsolete("how can we use CallerFilePath from ScriptCoreLib CLR 4.0?")]
        public static Exception AsException(this object Message,
            [CallerFilePath] string CallerFilePath = "",
            [CallerLineNumber] int CallerLineNumber = 0
            )
        {
            // http://stackoverflow.com/questions/912420/throw-exceptions-with-custom-stack-trace

            return new Exception(
                new { CallerFilePath, CallerLineNumber, Message }.ToString()
            );

        }
    }
}

// For more information visit:
// http://studio.jsc-solutions.net

// View as Visual Basic project
// http://do.jsc-solutions.net/View-as-Visual-Basic-project

// View as Visual FSharp project
// http://do.jsc-solutions.net/View-as-Visual-FSharp-project

using System;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;

namespace TestSolutionBuilderV1
{
    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// In debug build you can just hit F5 and debug the server side code.
        /// </summary>
        /// <param name="args">Commandline arguments</param>
        public static void Main(string[] args)
        {
//1f68:01:01 004d:0498 TestSolutionBuilderV1.Application define ScriptCoreLib.Extensions::ScriptCoreLib.JavaScript.Extensions.FormExtensions+<>c__DisplayClass2`1
//TranslatedTargetMethod null, are we on the same stack path? why? { TargetMethod = System.Windows.Forms.Form AttachFormTo[Form](System.Windows.Forms.Form, ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement) }

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

// For more information visit:
// http://studio.jsc-solutions.net/

// View as Visual Basic project
// http://do.jsc-solutions.net/View-as-Visual-Basic-project

// View as Visual FSharp project
// http://do.jsc-solutions.net/View-as-Visual-FSharp-project

using System;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;

namespace TestLinkedAssets
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// In debug build you can just hit F5 and debug the server side code.
        /// </summary>
        /// <param name="args">Commandline arguments</param>
        public static void Main(string[] args)
        {
            // Prepare the yield value for
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

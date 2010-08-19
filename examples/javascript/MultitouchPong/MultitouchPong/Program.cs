// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;

namespace MultitouchPong
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

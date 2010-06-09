// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Ultra.IDL;
using System.IO;

namespace ScriptCoreLib.Ultra.Components.IDL
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
#if DEBUG
            IDLModule m = File.ReadAllText("Design/IDLFiles/webgl.idl");
#else
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif

        }

    }
}

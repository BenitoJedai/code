using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Forms.Extensions;
using System;

namespace TestFormsBC
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // we are meant to test BC on web apps.

//#if DEBUG
//			DesktopFormsExtensions.Launch(
//				() => new ApplicationControl()
//			);
//#else
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
//#endif
        }

    }
}

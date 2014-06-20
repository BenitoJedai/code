using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Diagnostics;

namespace TestLINQ
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            new ApplicationWebService().select_x();

            Debugger.Break();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

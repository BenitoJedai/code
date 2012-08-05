using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestSelectMany
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Foo.InvokeTest(Console.WriteLine);
            //System.Diagnostics.Debugger.Break();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

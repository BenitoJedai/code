using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestMD5Experiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var a = new MD5.MD5();

            //a.FingerPrint
            a.Value = "the string";

            Console.WriteLine(new { a.FingerPrint });

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

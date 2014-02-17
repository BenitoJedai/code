using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace AsyncIncompleteTask
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //var w = new ApplicationWebService();
            //var t = w.WebMethod2();
            //var r = t.Result;

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestWebClient
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // Invalid URI: The format of the URI could not be determined.
            //var u = new Uri("/jsc");


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

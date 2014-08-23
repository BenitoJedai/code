using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestFromBase64String
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var capacity = 4 * "RENJTQ==".Length / 3;

            var x = Convert.FromBase64String("RENJTQ==");
            // x = {byte[4]}

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

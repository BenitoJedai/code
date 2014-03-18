using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestIntegerDiv
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var x = 32;
            var y = 17;

            var z = x / y;
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

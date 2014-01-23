using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestDateTime
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static DateTime Foo()
        {
            var loc = default(DateTime);

            return loc;
        }


        public static void Main(string[] args)
        {
            var x = Foo();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

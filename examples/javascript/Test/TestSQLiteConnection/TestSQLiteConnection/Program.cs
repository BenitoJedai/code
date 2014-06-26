using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestSQLiteConnection
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Foo.Invoke().Wait();
            Foo.Invoke2();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

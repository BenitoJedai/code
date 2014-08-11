using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestPropertyGetMethodExpression
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            new Foo();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

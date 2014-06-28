using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestDelegateObjectScopeInspection
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //Foo.Invoke();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

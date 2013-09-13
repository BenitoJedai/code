using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace ByRefLdarg0Experiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var z = new foo { i = 3 };

            z.invoke();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

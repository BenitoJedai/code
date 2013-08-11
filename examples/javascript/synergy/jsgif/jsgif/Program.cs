using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace jsgif
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    public static class Program
    {
        public static void Main(string[] args)
        {
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestByRefThis
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            __Invoke loc0;

            loc0.state = 5;
            loc0.MoveNext();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

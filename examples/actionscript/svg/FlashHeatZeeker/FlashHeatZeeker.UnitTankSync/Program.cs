using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace FlashHeatZeeker.UnitTankSync
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150110/hz

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

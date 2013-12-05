using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace FlashHeatZeeker.UnitJeepSync
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // 259c:01:01 RewriteToAssembly error: System.IO.FileNotFoundException: Could not load file or assembly 'net.hires.debug, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

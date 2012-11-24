using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Extensions;
using System;

namespace IntegrationToFaceInput
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {

            // partial jsc builds cause a fault:
            //Write Assembly Failed!. ERROR:Type 'Main_ballAsset' was not completed.

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

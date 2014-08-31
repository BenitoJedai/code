using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestWebCryptoKeyExport
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //ApplicationWebService.ExportParameters();


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

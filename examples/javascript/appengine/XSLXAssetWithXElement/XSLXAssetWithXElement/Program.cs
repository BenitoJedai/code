using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace XSLXAssetWithXElement
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var x = new ApplicationWebService();
            x.WebMethod2();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

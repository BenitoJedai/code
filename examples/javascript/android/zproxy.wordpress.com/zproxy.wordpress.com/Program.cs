using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace zproxy.wordpress.com
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            new ApplicationWebService().WebMethod2("", delegate { });

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestSelectOfSelect
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            new ApplicationWebService().WebMethod2();
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace WebCamAvatarsExperiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //new ApplicationWebService().Insert(
            //    new Abstractatech.JavaScript.Avatar.Design.WebCamAvatarsSheet1Row()
            //);

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

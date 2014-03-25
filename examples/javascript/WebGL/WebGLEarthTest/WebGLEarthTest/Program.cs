using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace WebGLEarthTest
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var x = new ApplicationWebService();

            x.GetAllCities();


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

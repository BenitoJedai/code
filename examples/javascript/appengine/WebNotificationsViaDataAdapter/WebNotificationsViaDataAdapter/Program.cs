using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace WebNotificationsViaDataAdapter
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // self test

            var x = new ApplicationWebService();

            var z = x[0, 1];


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

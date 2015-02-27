using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Xml.Linq;

namespace TestNullObjectFromWebService
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // Additional information: Value cannot be null.
            //var x = XElement.Parse(null);
            // Additional information: Root element is missing.
            //var x = XElement.Parse("");

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

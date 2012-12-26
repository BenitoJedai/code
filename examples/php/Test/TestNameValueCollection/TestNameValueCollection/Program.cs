using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Collections.Specialized;

namespace TestNameValueCollection
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var s = new StringDictionary();
            var x = s["x"];

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

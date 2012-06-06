using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Linq;
using System.Net;
using ScriptCoreLib.Extensions;

namespace NASDAQSNA
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
           


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Abstractatech.ActionScript.Audio
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

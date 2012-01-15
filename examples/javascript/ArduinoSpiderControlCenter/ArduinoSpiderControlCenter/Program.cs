using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.IO.Ports;
using ScriptCoreLib.Extensions;
using System.Collections.Generic;
using System.Threading;

namespace ArduinoSpiderControlCenter
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

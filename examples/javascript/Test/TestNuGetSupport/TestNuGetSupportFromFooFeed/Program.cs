using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Net;
using System.Net.Sockets;

namespace TestNuGetSupportFromFooFeed
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //ScriptCoreLib.Extensions.TcpListenerExtensions.BridgeConnectionToPort(
            //    new TcpListener(IPAddress.Any, 29019),
            //    59966,
            //    "?> ", 
            //    "?< "
            //);

            //Console.WriteLine("any key!");
            //Console.ReadLine();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

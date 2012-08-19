using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.IO;

namespace HybridCLRJVMAPKWebServer
{

    /*

     */

    public class Class1
    {
        public static void Main(string[] args)
        {
            // CLR wrapper for JVM app that calls CLR part in release build
            // this app shall run on 
            // - CLR
            // - JVM
            // - Android
            // this app shalls be the server for a jsc application
            // previous effort: 
            // "Y:\jsc.svn\examples\java\android\xavalon.net\xavalon.net.sln"
            // "Y:\jsc.svn\examples\java\android\AndroidTcpListenerActivity\AndroidTcpListenerActivity.sln"

            var ipa = Dns.GetHostAddresses("127.0.0.1")[0];

            Action<string> Console_WriteLine = x => Console.WriteLine(x);

            Console.WriteLine(typeof(object).FullName);

            var t = ClassLibrary1.Class1Shared.CreateServer(ipa, 8088, Console_WriteLine);

            t.Start();

            t.Join();

            // without using it jsc causes pain.
            CLRProgram.CLRMain();
        }

    }


    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            Console.WriteLine(typeof(object).FullName);
        }
    }
}

using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridCLRJVMAPKWebServer
{
    public class Class1
    {
        public static void Main(string[] e)
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

            Console.WriteLine(typeof(object).FullName);

            //CLRProgram.CLRMain();
        }
    }

    //[SwitchToCLRContext]
    //static class CLRProgram
    //{
    //    [STAThread]
    //    public static void CLRMain()
    //    {
    //        Console.WriteLine(typeof(object).FullName);
    //    }
    //}
}

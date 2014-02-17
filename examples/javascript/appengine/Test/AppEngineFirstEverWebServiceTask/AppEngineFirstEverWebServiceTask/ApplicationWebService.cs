using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppEngineFirstEverWebServiceTask
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // E/Web Console( 6547): Uncaught SyntaxError: 
        // Unexpected token function at http://83.191.200.38:2764/view-source:56475
        //  type$wPbomspuLjGhA_az1BEMUOQ.function = null;
        // E/Web Console( 6900): Uncaught SyntaxError: Unexpected token function at http://83.191.200.38:18213/view-source:56475


        public async void AsyncVoid()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140216/task
            //WriteFile { filename = ScriptCoreLib.dll.js, Length = 1648418 }
            //WriteFile { filename = AppEngineFirstEverWebServiceTask.Application.exe.js, Length = 52483 }
            //WriteFile { filename = ScriptCoreLib.dll.js, Length = 1648418 }
            //WriteFile { filename = AppEngineFirstEverWebServiceTask.Application.exe.js, Length = 52483 }
            //WriteFile { filename = assets/ScriptCoreLib/jsc.ico, Length = 58582 }
            //enter AsyncVoid


            Console.WriteLine("enter AsyncVoid");
        }

        public async Task AsyncTask()
        {
            // X:\jsc.svn\examples\java\async\Test\JVMCLRThreadBackgroundTask\JVMCLRThreadBackgroundTask\Program.cs

            Console.WriteLine("enter AsyncTask");

            // slow down synchronously
            Thread.Sleep(1000);

            Console.WriteLine("exit AsyncTask");
        }

        public async Task<string> AsyncStringTask()
        {

            Console.WriteLine("enter AsyncStringTask");


            return "the result";
        }



        public async Task<string> AsyncStringTaskAwaiting()
        {

            Console.WriteLine("enter AsyncStringTaskAwaiting");

            var x = await AsyncStringTask();

            Console.WriteLine("exit AsyncStringTaskAwaiting");

            return "the result";
        }
    }
}

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
            //Implementation not found for type import :
            //type: System.Runtime.CompilerServices.AsyncTaskMethodBuilder
            //method: System.Runtime.CompilerServices.AsyncTaskMethodBuilder Create()
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!

            Console.WriteLine("enter AsyncTask");

            // slow down synchronously
            Thread.Sleep(1000);

            Console.WriteLine("exit AsyncTask");
        }
    }
}

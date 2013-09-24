using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AsyncWebMethod
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService
    {
        // what about sending bytes?
        // what about Stream ?
        // what about file upload?
        // what about events?
        // what about intefaces?

        public async Task<string> WebMethod4(string e, Action<string> y)
        {
            y("event stream!");

            Console.WriteLine("delay");
            Thread.Sleep(1000);

            Console.WriteLine("delay done");
            return new { e }.ToString();
        }


        public async Task WebMethod8(string e, Action<string> y)
        {
            y("event stream!");

            Console.WriteLine("delay");
            Thread.Sleep(1000);

            Console.WriteLine("delay done");
        }
    }


}

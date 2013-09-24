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
        // what about properties and indexers?


        //Additional information: '.', hexadecimal value 0x00, is an invalid character. Line 1, position 1.

        //    AsyncWebMethod.ApplicationWebService.exe!AsyncWebMethod.Global.Invoke() + 0xf3 bytes	
        //>	ScriptCoreLib.Ultra.Library.dll!ScriptCoreLib.Ultra.WebService.InternalGlobalExtensions.InternalApplication_BeginRequest(ScriptCoreLib.Ultra.WebService.InternalGlobal g) + 0x93f bytes	
        //    AsyncWebMethod.ApplicationWebService.exe!AsyncWebMethod.Global.Application_BeginRequest() + 0x23 bytes	


        public async Task<data> WebMethod16(string e, Action<data> y)
        {
            y(new data { text = "event stream!" });

            Console.WriteLine("delay");
            Thread.Sleep(1000);

            Console.WriteLine("delay done");

            return (new data { text = "at return" });
        }

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


    public sealed class data
    {
        public string text;

        // TaskComplete! before SetResult { ResultType = AsyncWebMethod.data, XElementConversionPattern_IsValid = False }


        public static implicit operator XElement(data e)
        {

            var xml = new XElement("data",

                new XAttribute("style", "color: gray; display: block;"),

                new XAttribute("text", e.text),

                "show me " + new { e.text } + " did you see it?");


            return xml;
        }

        public static implicit operator data(XElement e)
        {
            //Console.WriteLine(new { e });
            var data = new data
            {
                text = e.Attribute("text").Value,
            };


            return data;
        }
    }

}

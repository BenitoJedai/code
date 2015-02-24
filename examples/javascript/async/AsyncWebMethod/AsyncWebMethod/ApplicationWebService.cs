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

        // redux roslyn rebuild causing the issue?
        // %5EY%E0%DF%25%ADD

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

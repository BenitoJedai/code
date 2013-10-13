using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestWebApplicationToString
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {

        // only if the field is referenced by both sides
        // it is subject to sync!
        public string foo = "foo";

        public override string ToString()
        {
            // this will also run on client
            return new { foo }.ToString();
        }

        public int Count;

        public async Task<string> Refresh()
        {
            Count = 7;

            //return null;

            return "why cannot we use Task nor Task<object> null?";
        }

        public int Index;

        public IEnumerable<Task<ApplicationWebService>> GetItems()
        {
            Console.WriteLine("enter GetItems");
            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine("yield GetItems " + new { i });
                yield return GetItem(i);
            }
            Console.WriteLine("exit GetItems");
        }

        public async Task<ApplicationWebService> GetItem(int i)
        {
            // slow down!
            Thread.Sleep(333 + new Random().Next(3000));

            return new ApplicationWebService { Index = i };
        }


        public static string operator +(ApplicationWebService x, int e)
        {
            return new { x, e }.ToString();
        }
    }


}

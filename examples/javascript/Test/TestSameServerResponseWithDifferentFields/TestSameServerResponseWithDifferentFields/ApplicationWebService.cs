using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestSameServerResponseWithDifferentFields
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService : IDisposable
    {
        // do we already also support auto properties as fields
        // this enables interfaces
        public int Counter;

        // Set-Cookie:InternalFields=field_Counter=1&field_AtFoo=<_02000015 />; path=/
        // what about binary xml and workders?
        // what about events?
        public Action AtFoo;

        // should fields survive in localStorage for refresh?
        public byte[] binary;

        public Stopwatch stopwatch = Stopwatch.StartNew();


        public async Task<string> WebMethod2(string e)
        {
            if (AtFoo != null)
                AtFoo();

            Counter++;

            return e;
        }


        // how can we autosubscribe to client events? wat if its not the browser but api client?
        public void Dispose()
        {
            var hex = binary.ToHexString();

            Console.WriteLine("Dispose " + new { stopwatch.ElapsedMilliseconds, Counter, hex });
        }
    }
}

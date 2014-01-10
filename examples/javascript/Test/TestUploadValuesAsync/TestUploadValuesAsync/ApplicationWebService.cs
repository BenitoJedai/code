using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestUploadValuesAsync
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {



        public Task<long> WebMethod2(string username, string psw)
        {
            Console.WriteLine("at WebMethod2");

            return 13L.AsResult();
        }

        public Task WebMethod4(string username, string psw)
        {
            Console.WriteLine("at WebMethod2");

            return 13L.AsResult();
        }
    }
}

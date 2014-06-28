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

namespace AsyncComputeAndThenCallServer
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public string Field1;

        //public async Task<string> WebMethod2()
        public async Task WebMethod2()
        {
            // <document><TaskComplete><TaskResult>b2s=</TaskResult></TaskComplete></document>
            // Uncaught TypeError: Cannot read property 'documentElement' of null 
            // http://stackoverflow.com/questions/9133918/parsing-xml-in-web-workers

            Console.WriteLine(new { this.Field1 });


            // do we support BAML yet? BXML
            //return "ok";
        }

    }
}

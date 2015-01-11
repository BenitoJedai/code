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

namespace TestLongWebMethod
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// The static content defined in the HTML file will be update to the dynamic content once application is running.
        /// </summary>
        public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");
        // could we talk to service worker? 


        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        //public async Task<string> WebMethod2()
        public async Task WebMethod2()
        {
            // what if we changed only server side?
            // could we just reconnect to a running client?



            // Show Details	Severity	Code	Description	Project	File	Line
            //Warning CS1998  This async method lacks 'await' operators and will run synchronously.Consider using the 'await' operator to await non - blocking API calls, or 'await Task.Run(...)' to do CPU - bound work on a background thread.	TestLongWebMethod ApplicationWebService.cs    31

            var wait = new Random().Next(300, 1300);


            Header.Value = "back from the server! wait \{wait}ms";

            await Task.Delay(wait);





            //return "send back a value to send back fields?";
        }

    }
}

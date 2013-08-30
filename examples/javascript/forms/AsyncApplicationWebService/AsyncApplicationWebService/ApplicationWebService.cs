using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AsyncApplicationWebService
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public async void WebMethod2(string e, Action<string> y)
        {
            Console.WriteLine("enter WebMethod2");

            // when will this work for android? :P
            await Task.Delay(1000);

            // Send it back to the caller.
            y(e);

            Console.WriteLine("exit WebMethod2");
        }

    }
}

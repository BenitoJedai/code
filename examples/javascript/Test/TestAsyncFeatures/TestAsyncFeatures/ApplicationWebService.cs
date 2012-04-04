using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Threading;

namespace AsyncResearch
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript. JSC supports string data type for all platforms.</param>
        /// <param name="y">A callback to javascript. In the future all platforms will allow Action&lt;XElementConvertable&gt; delegates.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            #region noticeable slow down so that windows will display (Not Responding)
            // http://social.msdn.microsoft.com/Forums/en-US/vsdebug/thread/688e4769-9c84-4826-9b01-9e1af0bdeb67/
            // debugger cannot be present
            // ctrl+F5 to run without debugger.

            var i = DateTime.Now;


            while (true)
            {
                Console.Write(".");

                var cpu_burn = DateTime.Now;

                if ((cpu_burn - i).TotalSeconds > 120)
                    break;
            }

            #endregion


            // Send it back to the caller.
            y("s: " + e);
        }

    }
}

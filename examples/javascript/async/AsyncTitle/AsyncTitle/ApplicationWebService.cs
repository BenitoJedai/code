using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AsyncTitle
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }


        public Task<string> Title
        {
            get
            {
                return Task.FromResult("new title!");
            }
        }

        public Task<string> OtherTitle
        {
            get
            {
                return Task.FromResult("other new title!");
            }
        }
    }
}

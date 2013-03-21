using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;

namespace UserHostAddress
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

        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            var Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":");

            h.Context.Response.ContentType = "text/plain";
            h.Context.Response.Write(
                new { h.Context.Request.UserHostAddress, Host }
            );

            //h.cont
            //h.Context.Request.UserHostAddress
            h.CompleteRequest();
        }
    }
}

using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;

namespace WoodsXmasByRobert
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



        public void Handler(WebServiceHandler h)
        {
            //Console.WriteLine(
            //    h.Context.Request.Path
            //);

            //  The following information can be helpful to determine why the assembly 'ScriptCoreLib.Ultra, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null' could not be loaded.
            var ref0 = typeof(ScriptCoreLib.Shared.RemotingToken);


            if (h.Context.Request.Path.StartsWith("/sound/"))
            {
                // jsc is not correctly updating the path of assets?
                h.Context.Response.WriteFile("/assets/WoodsXmasByRobert/" + h.Context.Request.Path.SkipUntilOrEmpty("/sound/"));
                h.CompleteRequest();
                return;
            }

            if (h.Context.Request.Path.StartsWith("/img/"))
            {
                // jsc is not correctly updating the path of assets?
                h.Context.Response.WriteFile("/assets/WoodsXmasByRobert/" + h.Context.Request.Path.SkipUntilOrEmpty("/img/"));
                h.CompleteRequest();
                return;
            }

     
        }
    }
}

using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;

namespace WebGLDoomByInt13h
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


            var f = h.GetFiles().FirstOrDefault(k => k.Name.SkipUntilLastOrEmpty("/") == h.Context.Request.Path.SkipUntilLastOrEmpty("/"));
            if (f != null)
            {
                if (h.Context.Request.Path != "/" + f.Name)
                {
                    h.Context.Response.Redirect("/" + f.Name);
                    h.CompleteRequest();
                    return;
                }
            }
        }
    }
}

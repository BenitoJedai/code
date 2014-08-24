using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    // http://referencesource.microsoft.com/#System.Web/xsp/system/Web/IHttpHandler.cs

    [Script(Implements = typeof(global::System.Web.IHttpHandler))]
    internal  interface __IHttpHandler
    {
        bool IsReusable { get; }

        void ProcessRequest(HttpContext context);        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.IHttpHandler))]
    internal interface __IHttpHandler
    {
        bool IsReusable { get; }

        void ProcessRequest(HttpContext context);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Profile;

namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    // http://referencesource.microsoft.com/#System.Web/xsp/system/Web/HttpContext.cs

    [Script(Implements = typeof(global::System.Web.HttpContext))]
    public class __HttpContext
    {
        // X:\jsc.svn\examples\java\android\ApplicationWebService\ApplicationWebService\ApplicationActivity.cs

        public HttpRequest Request { get; set; }
        public HttpResponse Response { get; set; }

        public ProfileBase Profile { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    // http://referencesource.microsoft.com/#System.Web/xsp/system/Web/HttpRequest.cs

    [Script(Implements = typeof(global::System.Web.HttpRequest))]
    public sealed class __HttpRequest
    {
        // X:\jsc.svn\core\ScriptCoreLibJava.Web\ScriptCoreLibJava.Web\BCLImplementation\System\Web\HttpRequest.cs
        // X:\jsc.svn\examples\java\android\ApplicationWebService\ApplicationWebService\ApplicationActivity.cs

        public string UserHostAddress { get; set; }

        public string Path { get; set; }

        public string HttpMethod { get; set; }


        public NameValueCollection QueryString { get; internal set; }

        public NameValueCollection Form { get; internal set; }

        public NameValueCollection Headers { get; internal set; }

        public HttpCookieCollection Cookies { get; set; }

        public __HttpRequest()
        {
            this.Files = (HttpFileCollection)(object)new __HttpFileCollection();
            this.QueryString = new NameValueCollection();
            this.Form = new NameValueCollection();
            this.Headers = new NameValueCollection();
            this.Cookies = new HttpCookieCollection();
        }


        public HttpFileCollection Files { get; set; }

    }
}

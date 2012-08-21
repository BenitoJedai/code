using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.HttpRequest))]
    public sealed class __HttpRequest
    {
        public string Path { get; set; }

        public string HttpMethod { get; set; }

        public NameValueCollection QueryString { get; internal set; }

        public NameValueCollection Form { get; internal set; }

        public NameValueCollection Headers { get; internal set; }

        public __HttpRequest()
        {
            this.QueryString = new NameValueCollection();
            this.Form = new NameValueCollection();
            this.Headers = new NameValueCollection();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.HttpRequest))]
    internal sealed class __HttpRequest
    {
        public string Path { get; internal set; }

        public string HttpMethod { get; internal set; }

        public NameValueCollection QueryString { get; }

        public NameValueCollection Form { get; }
    }
}

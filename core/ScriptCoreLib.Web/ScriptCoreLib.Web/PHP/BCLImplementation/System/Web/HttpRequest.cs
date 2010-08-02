using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.HttpRequest))]
    internal class __HttpRequest
    {

        public string Path
        {
            get
            {
                return (string)Native.SuperGlobals.Request["REQUEST_URI"];
            }
        }

        public string HttpMethod
        {
            get
            {
                return (string)Native.SuperGlobals.Request["REQUEST_METHOD"];
            }
        }

        public NameValueCollection Form
        {
            get
            {
                return null;
            }
        }

        public NameValueCollection QueryString
        {
            get
            {
                return null;
            }
        }
    }
}

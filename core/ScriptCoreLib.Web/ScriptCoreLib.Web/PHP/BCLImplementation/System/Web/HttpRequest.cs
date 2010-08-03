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
                var r = (string)Native.SuperGlobals.Server[Native.SuperGlobals.ServerVariables.REQUEST_URI];
                var i = r.IndexOf("?");

                if (i > 0)
                    r = r.Substring(0, i);

                return r;
            }
        }

        public string HttpMethod
        {
            get
            {
                var r = (string)Native.SuperGlobals.Server["REQUEST_METHOD"];

                return r;
            }
        }

        NameValueCollection InternalForm;
        public NameValueCollection Form
        {
            get
            {

                if (InternalForm == null)
                {
                    InternalForm = new NameValueCollection();
                    InitializeForm();
                }

                return InternalForm;

            }
        }

        private void InitializeForm()
        {
            var e = Native.SuperGlobals.Post;

            foreach (var item in e.Keys)
            {
                InternalForm[item] = e[item];
            }
        }


        NameValueCollection InternalQueryString;
        public NameValueCollection QueryString
        {
            get
            {
                if (InternalQueryString == null)
                {
                    InternalQueryString = new NameValueCollection();

                    var _get = Native.SuperGlobals.Get;

                    foreach (var item in _get.Keys)
	                {
                        InternalQueryString[item] = _get[item]; 
	                }
                    
                }

                return InternalQueryString;
            }
        }
    }
}

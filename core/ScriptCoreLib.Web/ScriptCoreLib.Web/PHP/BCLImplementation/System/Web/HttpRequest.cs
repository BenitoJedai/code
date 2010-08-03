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
            
        }


        NameValueCollection InternalQueryString;
        public NameValueCollection QueryString
        {
            get
            {
                if (InternalQueryString == null)
                {
                    InternalQueryString = new NameValueCollection();
                }

                return InternalQueryString;
            }
        }
    }
}

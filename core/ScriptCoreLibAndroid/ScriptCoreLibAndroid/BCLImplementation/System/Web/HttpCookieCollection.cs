using ScriptCoreLibJava.BCLImplementation.System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.HttpCookieCollection))]
    internal class __HttpCookieCollection : __NameObjectCollectionBase
    {
        public HttpCookie this[string name]
        {
            get
            {
                return null;
            }
        }
    }
}

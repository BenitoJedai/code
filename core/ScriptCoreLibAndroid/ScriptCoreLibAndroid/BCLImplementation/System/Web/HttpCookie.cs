using ScriptCoreLibJava.BCLImplementation.System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.HttpCookie))]
    internal class __HttpCookie
    {
        public DateTime Expires { get; set; }
        public string Value { get; set; }
    }
}

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
        public __HttpCookie()
        {

        }

        public __HttpCookie(string name)
        {
            this.Name = name;
        }

        public __HttpCookie(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }

        public DateTime Expires { get; set; }
        public string Value { get; set; }
    }
}

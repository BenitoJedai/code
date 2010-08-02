using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.HttpResponse))]
    internal class __HttpResponse
    {
        public int StatusCode
        {
            get;
            set;
        }

        public string ContentType
        {
            get;
            set;
        }

        public void Write(string s)
        {
        }

        public void Redirect(string url)
        {
        }

        public void AddHeader(string name, string value)
        {
        }

        public void WriteFile(string filename)
        {
        }
    }
}

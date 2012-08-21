using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.HttpResponse))]
    internal sealed class __HttpResponse
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; }
        public void AddHeader(string name, string value)
        {

        }
        public void Redirect(string url)
        {
        }

        public void WriteFile(string filename)
        {
            // assets only?
        }
    }
}

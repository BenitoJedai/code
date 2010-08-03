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
            get
            {
                return 200;
            }
            set
            {
                
            }
        }

        public string ContentType
        {
            get
            {
                return "";
            }
            set
            {
                Native.SetContentType(value);
            }
        }

        public void Write(string s)
        {
            Native.echo(s);
        }

        public void Redirect(string url)
        {
            Native.Redirect(url);
        }

        public void AddHeader(string name, string value)
        {
            Native.header(name + ":" + value);
        }

        public void WriteFile(string filename)
        {
            // ?
        }
    }
}

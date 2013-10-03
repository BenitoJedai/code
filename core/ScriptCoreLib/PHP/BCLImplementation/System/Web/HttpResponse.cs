using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.HttpResponse))]
    internal class __HttpResponse
    {
        public Stream OutputStream
        {
            get
            {
                // not yet
                // affected
                // X:\jsc.svn\examples\php\PHPXElementExample\PHPXElementExample\ApplicationWebService.cs

                return null;
            }
        }

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

        public void Flush()
        {
            Native.API.flush();
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
            // wow this fix was expensive.
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/201209/20120925-varnish


            // we only work with absolute paths anyway
            if (filename.StartsWith("/"))
                filename = filename.Substring(1);

            var fp = Native.API.fopen(filename, "rb");

            Native.API.fpassthru(fp);
        }

        public __HttpCachePolicy Cache
        {
            get
            {
                return new __HttpCachePolicy { InternalResponse = this };
            }
        }
    }
}

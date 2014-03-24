using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.HttpPostedFile))]
    internal class __HttpPostedFile
    {
        public int ContentLength { get; set; }

        public string ContentType { get; set; }
        public string FileName { get; set; }
        public Stream InputStream { get; set; }
    }
}

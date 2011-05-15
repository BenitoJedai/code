using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.io;

namespace java.util.zip
{
    // http://download.oracle.com/javase/1.4.2/docs/api/java/util/zip/ZipEntry.html
    [Script(IsNative = true)]
    public class ZipEntry
    {
        public string getName()
        {
            return default(string);
        }

        public long getSize()
        {
            return default(long);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.io;

namespace java.util.zip
{
    // http://download.oracle.com/javase/1.4.2/docs/api/java/util/zip/ZipInputStream.html
    [Script(IsNative = true)]
    public class ZipInputStream
    {
        public ZipInputStream(InputStream @in)
        {

        }

        public ZipEntry getNextEntry()
        {
            return default(ZipEntry);
        }

        public int read(sbyte[] b,
                       int off,
                       int len)
        {
            return default(int);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.io;

namespace java.util.zip
{
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
    }
}

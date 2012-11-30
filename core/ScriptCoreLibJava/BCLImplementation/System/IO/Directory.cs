﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
    [Script(Implements = typeof(global::System.IO.Directory))]
    internal static class __Directory
    {

        public static string __GetFullPath(string e)
        {
            // http://www.devx.com/tips/Tip/13804

            var f = new java.io.File(e);
            var c = default(string);

            try
            {
                c = f.getCanonicalPath();
            }
            catch
            {
                throw;
            }

            return c;
        }

        public static DirectoryInfo CreateDirectory(string e)
        {
            var f = __GetFullPath(e);

            if (!new java.io.File(e).mkdir())
                f = null;

            if (f == null)
                return null;

            return new DirectoryInfo(f);
        }
    }
}

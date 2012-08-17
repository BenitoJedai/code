using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
    [Script(Implements = typeof(global::System.IO.Path))]
    internal static class __Path
    {

        public static string GetDirectoryName(string path)
        {
            var z = path.LastIndexOf(@"\");
            var y = path.LastIndexOf("/");

            var i = z;

            if (y > z)
                i = z;

            if (i == -1)
                return path;

            return path.Substring(0, i + 1);
        }

        public static string GetFileNameWithoutExtension(string path)
        {
            var x = GetFileName(path);
            var i = x.LastIndexOf(".");

            if (i < 0)
                return x;

            return x.Substring(0, i);
        }

        public static string GetExtension(string path)
        {
            var p = path.Replace("/", "\\");
            var i = p.LastIndexOf(".");

            if (i < 0)
                return "";

            return p.Substring(i);
        }

        public static string ChangeExtension(string path, string extension)
        {
            var p = path.Replace("/", "\\");
            var i = p.LastIndexOf(".");

            // ?
            if (i < 0)
                return path + extension;

            return p.Substring(0, i) + extension;
        }

        public static bool HasExtension(string path)
        {
            var x = path.LastIndexOf(".");

            if (x < 0)
                return false;

            var z = path.LastIndexOf(@"\");


            if (z > -1)
                if (z > x)
                    return false;

            var y = path.LastIndexOf("/");

            if (y > -1)
                if (y > x)
                    return false;

            return true;
        }

        public static string GetFileName(string e)
        {
            // http://www.devx.com/tips/Tip/13804

            var f = new java.io.File(e);
            var c = default(string);

            try
            {
                c = f.getName();
            }
            catch
            {
                throw;
            }

            return c;
        }

        public static string GetFullPath(string e)
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

        public static string Combine(string path1, string path2)
        {
            if (path1.EndsWith("/"))
                return path1 + path2;

            if (path1.EndsWith("\\"))
                return path1 + path2;


            return path1 + "/" + path2;
        }
    }
}

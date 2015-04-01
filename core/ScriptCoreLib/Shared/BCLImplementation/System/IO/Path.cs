using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.IO
{
	// http://referencesource.microsoft.com/#mscorlib/system/io/path.cs
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System.IO/Path.cs
	// https://github.com/dotnet/coreclr/blob/master/src/pal/src/file/path.cpp
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/IO/Path.cs


	[Script(Implements = typeof(global::System.IO.Path))]
    internal static class __Path
    {
        public static bool HasExtension(string path)
        {
            var a = path.LastIndexOf("/");
            var b = path.LastIndexOf(@"\");
            var c = path.LastIndexOf(".");

            if (c < 0)
                return false;

            if (a > c)
                return false;

            if (b > c)
                return false;

            return true;
        }

        public static string Combine(string path1, string path2)
        {
            if (path1.EndsWith("/"))
                return path1 + path2;

            if (path1.EndsWith("\\"))
                return path1 + path2;


            return path1 + "/" + path2;
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


        public static string GetFileName(string path)
        {
            // http://www.devx.com/tips/Tip/13804

            var z = path.LastIndexOf(@"\");
            var y = path.LastIndexOf("/");

            var i = z;

            if (y > z)
                i = z;

            if (i == -1)
                return path;

            return path.Substring(i + 1);
        }
    }
}

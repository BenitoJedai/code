using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Extensions
{
    public static class IOExtensions
    {
        public static string ToRelativePath(this FileInfo that, DirectoryInfo root)
        {
            return that.FullName.Replace("/", @"\").SkipUntilOrEmpty(root.FullName.Replace("/", @"\") + @"\");
        }

        public static DirectoryInfo WhenExists(this DirectoryInfo that)
        {
            if (that.Exists)
                return that;

            return null;
        }
        public static DirectoryInfo Clear(this DirectoryInfo that)
        {
            foreach (var item in that.GetFiles())
            {
                item.Delete();
            }

            return that;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Extensions
{
    public static class IOExtensions
    {
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

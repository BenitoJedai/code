using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Extensions
{
    public static class IOGenericExtensions
    {
        public static IEnumerable<FileInfo> GetAllFilesByPattern(this DirectoryInfo e, params string[] p)
        {
            return p.SelectMany(
                i =>
                {
                    if (e.Exists)
                        return e.GetFiles(i, SearchOption.AllDirectories);

                    return new FileInfo[0];
                }
            );
        }

        public static IEnumerable<FileInfo> GetFilesByPattern(this DirectoryInfo e, params string[] p)
        {
            return p.SelectMany(
             i =>
             {
                 if (e.Exists)
                     return e.GetFiles(i);

                 return new FileInfo[0];
             }
         );
        }
    }
}

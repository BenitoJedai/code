using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Extensions
{
    public static class IOGenericExtensions
    {
        public static IEnumerable<FileInfo> CopyFilesTo(this DirectoryInfo root, IEnumerable<FileInfo> files, DirectoryInfo target)
        {
            return from f in files
                   where f.FullName.StartsWith(root.FullName)
                   let r = f.Directory.FullName.SkipUntilOrEmpty(root.FullName).Substring(1)
                   let x = target.CreateSubdirectory(r)
                   let c = f.CopyTo(Path.Combine(x.FullName, f.Name), true)
                   select c;
        }

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

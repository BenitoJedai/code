using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace jsc.Languages
{
    using ScriptCoreLib;

    using Languages;
    using Script;

    partial class CompilerJob
    {
		public static void ExtractEmbeddedResources(DirectoryInfo dir, Assembly e)
		{
			ExtractEmbeddedResources(dir, e,
				 (v, tf, Path, File) =>
					  CopyStream(e.GetManifestResourceStream(v), tf.OpenWrite())
			);


		}

        public static void ExtractEmbeddedResources(DirectoryInfo dir, Assembly e, Action<string, FileInfo, string, string> handler)
        {
            var DefaultResources = new ScriptResourcesAttribute("assets/" + e.GetName().Name);

            ScriptResourcesAttribute[] a =
                ((ScriptResourcesAttribute[])e.GetCustomAttributes(typeof(ScriptResourcesAttribute), false))
                .Concat(
                new[] { 
                    // default
                    DefaultResources
                }).OrderByDescending(i => i.Value.Length).ToArray();

            string[] r = e.GetManifestResourceNames();

            AssemblyName n = new AssemblyName(e.FullName);

            var prefix1 = n.Name + "." + dir.Name;

            Func<AssemblyName, string> GetTopMostNamespace =
                an =>
                {
                    var x = an.Name;
                    var i = x.IndexOf(".");

                    if (i == -1)
                        return x;

                    return x.Substring(0, i);
                };

            var prefix2 = GetTopMostNamespace(n) + "." + dir.Name;

            // fixme: empty directory or overlapping directories with files containing "." .
            // fixme: 
            // "ScriptCoreLib.web.assets.Controls.NatureBoy.dude6.274.png"
            // "ScriptCoreLib.Controls.NatureBoy.web"

            Func<string, string, string> EnsureStartsWith =
                (_prefix, _subject) =>
                {
                    if (_subject.StartsWith(_prefix))
                        return _subject;

                    return _prefix + _subject;
                };

            Func<string, string, string> EnsureNotStartsWith =
               (_prefix, _subject) =>
               {
                   if (_subject.StartsWith(_prefix))
                       return _subject.Substring(_prefix.Length);

                   return _subject;
               };

            var prefixes = new[] { prefix1, prefix2 };

            var query = from v in r
                        from prefix in prefixes.Distinct()
                        where v.StartsWith(prefix)
                        let z = (
                                    from av in a
                                    let ap = prefix + EnsureStartsWith(".", av.Value.Replace('/', '.'))
                                    where v.StartsWith(ap)
                                    select new { ap, av }
                                  ).FirstOrDefault()
                        where z != null
                        let NewSubDir = EnsureNotStartsWith("/", z.av.Value)
                        let t = string.IsNullOrEmpty(NewSubDir) ? dir : dir.CreateSubdirectory(NewSubDir)
                        let f = string.IsNullOrEmpty(NewSubDir) ? v.Substring(z.ap.Length) : v.Substring(z.ap.Length + 1)
                        let tf = new FileInfo(t.FullName + "/" + f)
						select new { v, tf, File = f, Path = z.av.Value };

            foreach (var p in query)
            {
				handler(p.v, p.tf, p.Path, p.File);

				//CopyStream(e.GetManifestResourceStream(p.v), p.tf.OpenWrite());
            }
        }

        public static void CopyStream(Stream FromStream, Stream ToStream)
        {

            try
            {
                //Creat a file to save to


                //use the binary reader & writer because
                //they can work with all formats
                //i.e images, text files ,avi,mp3..



                BinaryReader br = new BinaryReader(FromStream);


                BinaryWriter bw = new BinaryWriter(ToStream);


                //copy data from the FromStream to the outStream
                //convert from long to int 
                bw.Write(br.ReadBytes((int)FromStream.Length));
                //save
                bw.Flush();
                //clean up 
                bw.Close();
                br.Close();
            }

             //use Exception e as it can handle any exception 
            catch (Exception e)
            {
                //code if u like 
            }
        }
    }
}

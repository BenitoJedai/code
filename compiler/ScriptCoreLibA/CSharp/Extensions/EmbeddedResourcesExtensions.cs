﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using ScriptCoreLib.Shared;

namespace ScriptCoreLib.CSharp.Extensions
{
    public static partial class EmbeddedResourcesExtensions
    {

        public class ManifestResourceEntry
        {
            public ScriptResourcesAttribute VirtualPath;

            public string File;

            public string ResourceName;

            public Stream Stream;

            public string PrefixlessResourceName;
        }

        internal class BooleanProperty
        {
            public bool Value;
        }


        static void ExtractEmbeddedResources_nop()
        { 
        }

        public static void ExtractEmbeddedResources(DirectoryInfo dir, Assembly e)
        {
            ExtractEmbeddedResources(dir, e,
                 (v, tf, Path, File) =>
                 {
                     var f = new FileInfo(tf);

                     if (f.Exists)
                         f.Delete();

                     // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201302/201
                     //var m = new MemoryStream();

                     //e.GetManifestResourceStream(v).CopyTo(m);
                     //m.Position = 0;

                     CopyStream(e.GetManifestResourceStream(v), f.OpenWrite());

                     ExtractEmbeddedResources_nop();
                 }
            );


        }

        public static ScriptResourcesAttribute[] GetScriptResourcesAttributes(Assembly assembly)
        {
            var assembly_types =
                from assembly_type in assembly.GetTypes()
                let assembly_type_attribute = assembly_type.GetCustomAttributes(typeof(ScriptResourcesAttribute), false).Cast<ScriptResourcesAttribute>().SingleOrDefault()
                where assembly_type_attribute != null
                select assembly_type;

            return assembly.GetCustomAttributes(typeof(ScriptResourcesAttribute), false).Cast<ScriptResourcesAttribute>().Concat(
                                from assembly_type in assembly_types
                                from assembly_type_constant in assembly_type.GetFields()
                                where assembly_type_constant.IsLiteral
                                where assembly_type_constant.IsStatic
                                where assembly_type_constant.FieldType == typeof(string)
                                select new ScriptResourcesAttribute((string)assembly_type_constant.GetValue(assembly_type))
                             ).ToArray();
        }


        public static void ExtractEmbeddedResources(DirectoryInfo dir, Assembly e, Action<string, string, string, string> handler)
        {
            //System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.Security.SecurityException: Request for the permission of type 'System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089' failed.
            //   at System.Security.CodeAccessSecurityEngine.Check(Object demand, StackCrawlMark& stackMark, Boolean isPermSet)
            //   at System.Security.CodeAccessPermission.Demand()
            //   at System.Reflection.Assembly.VerifyCodeBaseDiscovery(String codeBase)
            //   at System.Reflection.Assembly.GetName(Boolean copiedName)
            //   at System.Reflection.Assembly.GetName()
            //   at ScriptCoreLib.CSharp.Extensions.EmbeddedResourcesExtensions.ExtractEmbeddedResources(DirectoryInfo dir, Assembly e, Action`4 handler)

            // http://social.msdn.microsoft.com/Forums/en-US/wpf/thread/b92c1f6a-9160-42fe-ad2c-4961814d044d/

            // to support partial trust - this workaround should be implemented:
            //Assembly assembly = Assembly.GetEntryAssembly();
            //AssemblyName[] names = assembly.GetReferencedAssemblies();
            //foreach (AssemblyName name in names)
            //{
            //    MessageBox.Show(name.Name + " " + name.Version.ToString());
            //}
            var DefaultResources = new ScriptResourcesAttribute("assets/" + e.GetName().Name);

            ScriptResourcesAttribute[] a =
                GetScriptResourcesAttributes(e)
                .Concat(
                new[] { 
                    // default
                    DefaultResources
                }).OrderByDescending(i => i.Value.Length).ToArray();

            string[] r = e.GetManifestResourceNames();

            AssemblyName n = new AssemblyName(e.FullName);

            var dir_Name = dir == null ? "web" : dir.Name;

            var prefix1 = n.Name + "." + dir_Name;

            Func<AssemblyName, string> GetTopMostNamespace =
                an =>
                {
                    var x = an.Name;
                    var i = x.IndexOf(".");

                    if (i == -1)
                        return x;

                    return x.Substring(0, i);
                };

            var prefix2 = GetTopMostNamespace(n) + "." + dir_Name;

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
                        let t = (string.IsNullOrEmpty(NewSubDir) || dir == null) ? dir : dir.CreateSubdirectory(NewSubDir)
                        let f = string.IsNullOrEmpty(NewSubDir) ? v.Substring(z.ap.Length) : v.Substring(z.ap.Length + 1)
                        // FileInfo cannot be used in partial trust
                        let tf = (t == null ? "" : t.FullName) + "/" + f
                        select new { v, tf, File = f, Path = z.av.Value };

            foreach (var p in query)
            {
                handler(p.v, p.tf, p.Path, p.File);

                //CopyStream(e.GetManifestResourceStream(p.v), p.tf.OpenWrite());
            }
        }

        public static void CopyStream(Stream FromStream, Stream ToStream)
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

        public static string[] GetEmbeddedResources(DirectoryInfo web, Assembly e)
        {
            var a = new List<string>();

            #region fields
            EmbeddedResourcesExtensions.ExtractEmbeddedResources(web, e,
                (v, tf, Path, File) =>
                {
                    var source = Path + "/" + File;

                    a.Add(source);
                }
            );
            #endregion

            return a.ToArray();
        }

    }
}

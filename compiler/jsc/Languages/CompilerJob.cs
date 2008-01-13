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

    public partial class CompilerJob
    {
        //public CompilerJob ParentJob;

        public FileInfo AssamblyFile;
        public Assembly AssamblyInfo;

        public List<ScriptNamespaceRenameAttribute> NamespaceRenameList = new List<ScriptNamespaceRenameAttribute>();

        public ScriptNamespaceRenameAttribute[] GetNamespaceRenameList()
        {
            return (ScriptNamespaceRenameAttribute[])Attribute.GetCustomAttributes(AssamblyInfo, typeof(ScriptNamespaceRenameAttribute), false);
        }

        public ScriptTypeFilterAttribute[] GetTypeFilterList()
        {
            return (ScriptTypeFilterAttribute[])Attribute.GetCustomAttributes(AssamblyInfo, typeof(ScriptTypeFilterAttribute), false);
        }

        public ScriptTypeFilterAttribute[] GetTypeFilterListByType(ScriptType e)
        {
            List<ScriptTypeFilterAttribute> u = new List<ScriptTypeFilterAttribute>();

            foreach (ScriptTypeFilterAttribute var in GetTypeFilterList())
            {
                if (var.Type == e)
                    u.Add(var);
            }

            return u.ToArray();
        }




        public static void Compile(string p, CompileSessionInfo sinfo)
        {
            sinfo.Logging.LogMessage("will compile '{0}'", p);

            // we need to build namespace rename and depends tree for function ommition

            CompilerJob j = new CompilerJob();

            j.AssamblyInfo = Assembly.LoadFile(p);
            j.AssamblyFile = new FileInfo(p);

            if (!j.AssamblyFile.Exists)
            {
                sinfo.Logging.LogMessage("file not found");


                return;
            }

            //Environment.CurrentDirectory = j.AssamblyFile.DirectoryName;

            Assembly.LoadFile(j.AssamblyFile.FullName);


            // we need all the namespace fixups from all assemblies
            foreach (var reference in ScriptCoreLib.SharedHelper.LoadReferencedAssemblies(j.AssamblyInfo, true))
            {
                var n = reference.GetCustomAttributes<ScriptNamespaceRenameAttribute>();

                // todo: deal with overlapping attributes here
                j.NamespaceRenameList.AddRange(n);
            }
            

            // we support java only at this time

            ////bool _java = false;

            ////foreach (ScriptTypeFilterAttribute var in j.GetTypeFilterListByType(ScriptType.Java))
            ////{
            ////    _java = true;

            ////    sinfo.Logging.LogMessage(" * assambly contains '{0}'", var.FilterTypeName);
            ////}

            // compile for language # java

            if (j.GetTypeFilterListByType(ScriptType.Java).Any()) 
                CompileJava(j, sinfo);

            if (j.GetTypeFilterListByType(ScriptType.ActionScript).Any())
                CompileActionScript(j, sinfo);


        }


        private Type[] LoadTypes(ScriptType scriptType)
        {
            List<Type> a = new List<Type>();

            foreach (Assembly x in SharedHelper.LoadReferencedAssemblies(this.AssamblyInfo, true))
            {
                a.AddRange(ScriptAttribute.FindTypes(x, scriptType));

            }


            return a.ToArray();
        }

        internal string NamespaceFixup(string _ns)
        {
            foreach (ScriptNamespaceRenameAttribute var in this.NamespaceRenameList)
            {
                if (_ns.StartsWith(var.NativeNamespaceName))
                {
                    _ns = var.VirtualNamespaceName + _ns.Substring(var.NativeNamespaceName.Length);

                    if (_ns.StartsWith("."))
                        _ns = _ns.Substring(1);

                    break;
                }
            }

            return _ns;
        }

        public static void InvokeEntryPoints(DirectoryInfo dir, Assembly a)
        {
            foreach (Type v in a.GetTypes())
            {
                BindingFlags all = BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly;

                MethodInfo[] m = v.GetMethods(all);

                foreach (MethodInfo z in m)
                {
                    if (z.IsStatic)
                    {
                        ParameterInfo[] p = z.GetParameters();

                        if (p.Length == 1)
                        {
                            if (p[0].ParameterType == typeof(IEntryPoint))
                            {
                                z.Invoke(null, new object[] { new DefaultEntryPointWrapper(dir) });
                            }
                        }
                    }
                }
            }
        }

        class DefaultEntryPointWrapper : IEntryPoint
        {
            DirectoryInfo dir;

            public DefaultEntryPointWrapper(DirectoryInfo e)
            {
                dir = e;
            }

            #region IEntryPoint Members

            public void Define(string filename, string content)
            {
                Console.WriteLine(filename);

                FileInfo f = new FileInfo(dir.FullName + "/" + filename);

                StreamWriter x = new StreamWriter(new FileStream(f.FullName, FileMode.Create));


                x.Write(content);
                x.Close();
            }

            public string this[string filename]
            {
                set { this.Define(filename, value); }
            }

            #endregion
        }


        public static void ExtractEmbeddedResources(DirectoryInfo dir, Assembly e)
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

            var prefixes = new[] { prefix1, prefix2 };

            var query = from v in r
                        from prefix in prefixes
                        where v.StartsWith(prefix)
                        let z = (
                                    from av in a
                                    let ap = prefix + "." + av.Value.Replace('/', '.')
                                    where v.StartsWith(ap)
                                    select new { ap, av }
                                  ).FirstOrDefault()
                        where z != null
                        let f = v.Substring(z.ap.Length + 1)
                        let t = dir.CreateSubdirectory(z.av.Value.Replace('.', '/'))
                        let tf = new FileInfo(t.FullName + "/" + f)
                        select new { v, tf };

            foreach (var p in query)
            {
                CopyStream(e.GetManifestResourceStream(p.v), p.tf.OpenWrite());
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

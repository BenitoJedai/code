using System;
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


            //sinfo.Logging.LogMessage("assambly loaded '{0}'", j.AssamblyInfo.FullName);

            j.NamespaceRenameList.AddRange(j.GetNamespaceRenameList());

            // we support java only at this time

            bool _java = false;

            foreach (ScriptTypeFilterAttribute var in j.GetTypeFilterListByType(ScriptType.Java))
	        {
                _java = true;

                sinfo.Logging.LogMessage(" * assambly contains '{0}'", var.FilterTypeName);
	        }
            
            // compile for language # java

            if (_java) CompileJava(j, sinfo);
            

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

                }
            }

            return _ns;
        }

        public static void InvokeEntryPoints(DirectoryInfo dir, Assembly a)
        {
            foreach (Type v in a.GetTypes())
            {
                BindingFlags all = BindingFlags.Public  | BindingFlags.Static | BindingFlags.DeclaredOnly;

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
            ScriptResourcesAttribute[] a = (ScriptResourcesAttribute[]) e.GetCustomAttributes(typeof(ScriptResourcesAttribute), false);

            string[] r = e.GetManifestResourceNames();

            AssemblyName n = new AssemblyName(e.FullName);

            string p = n.Name + "." + dir.Name;

            // fixme: empty directory or overlapping directories with files containing "." .

            foreach (string v in r)
            {
                //if (v == n.Name + ".g.resources")
                //{
                //    ManifestResourceInfo mri = e.GetManifestResourceInfo(v);
                    
                //    e.
                //}
                //else 
                if (v.StartsWith(p))
                {
                    foreach (ScriptResourcesAttribute av in a)
                    {
                        string ap = p + "." + av.Value.Replace('/', '.');

                        if (v.StartsWith(ap))
                        {
                            string f = v.Substring(ap.Length + 1);

                            DirectoryInfo t = dir.CreateSubdirectory(av.Value.Replace('.', '/'));

                            FileInfo tf = new FileInfo(t.FullName + "/" + f);

                            CopyStream(e.GetManifestResourceStream(v), tf.OpenWrite());

                            break;
                        }
                    }

                }
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

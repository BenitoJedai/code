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
        public static Type[] JustMyCodeFilter(bool jmc, Type[] e, Assembly assembly)
        {
            if (!jmc)
                return e;

            List<Type> a = new List<Type>(e.Length);

            foreach (Type v in e)
            {
                if (v.Assembly == assembly)
                    a.Add(v);
            }
            


            return a.ToArray();
        }

        private static void CompileJava(CompilerJob j, CompileSessionInfo sinfo)
        {
            IdentWriter xw = new IdentWriter();

            xw.Session = new AssamblyTypeInfo();

            sinfo.Logging.LogMessage("loading types");

            Type[] alltypes = j.LoadTypes(ScriptType.Java);


            xw.Session.Types = JustMyCodeFilter(sinfo.Options.JustMyCode, alltypes, j.AssamblyInfo);

            xw.Session.ImplementationTypes.AddRange(alltypes);

            sinfo.Logging.LogMessage("found {0} types to be compiled", xw.Session.Types.Length);

			DirectoryInfo TargetDirectory = sinfo.Options.TargetAssembly.Directory.CreateSubdirectory("web");
            DirectoryInfo SourceDir = TargetDirectory.CreateSubdirectory("java");
            DirectoryInfo SourceCompiledDir = TargetDirectory.CreateSubdirectory("release");
            DirectoryInfo SourceCompiledHeadersDir = TargetDirectory.CreateSubdirectory("headers");
            DirectoryInfo SourceNativeDir = TargetDirectory.CreateSubdirectory("native");
            DirectoryInfo SourceBinDir = TargetDirectory.CreateSubdirectory("bin");
            DirectoryInfo SourceVersionDir = TargetDirectory.CreateSubdirectory("version");

            // assets
            foreach (Assembly v in SharedHelper.LoadReferencedAssemblies(Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName), true))
            {
                Languages.CompilerJob.ExtractEmbeddedResources(TargetDirectory, v);
            }

            FileInfo SourceVersion = new FileInfo(
                SourceVersionDir.FullName + "/" 
                + j.AssamblyInfo.ManifestModule.Name 
                + "." + Enum.GetName(typeof(ScriptType), ScriptType.Java) 
                + ".version.txt"
                );

            if (SourceVersion.Exists)
            {
                if (j.AssamblyFile.LastWriteTime <= SourceVersion.LastWriteTime)
                {
                    Console.WriteLine("this version is already built: " + SourceVersion.FullName);

					//if (!Debugger.IsAttached)
					//{
                        return;
					//}
                }
            }

        

            Helper.WorkPool n = new Helper.WorkPool();

            n.IsThreaded = !Debugger.IsAttached && !sinfo.Options.IsNoThreads;

            using (new Helper.ConsoleStopper("java type compiler"))
            {
                n.ForEach(xw.Session.Types,
                    delegate(Type xx)
                    {
                        if (xx.IsEnum) return;

						if (xx.Assembly != j.AssamblyInfo)
							return;


                        CompilerBase c = new Languages.Java.JavaCompiler(new StringWriter(), xw.Session);

                        c.CurrentJob = j;

                        Program.AttachXMLDoc(new FileInfo(xx.Assembly.ManifestModule.FullyQualifiedName), c);

                        if (c.CompileType(xx))
                        {
                            c.ToConsole(xx, sinfo);


                            Program.WriteSingleScriptFile(Languages.Java.JavaCompiler.FileExtension, SourceDir, c, xx);
                        }


                    }
                );

                Languages.CompilerJob.InvokeEntryPoints(TargetDirectory, j.AssamblyInfo);

            }


			StreamWriter SVW = new StreamWriter(SourceVersion.OpenWrite());

			SVW.WriteLine("ConstantCompilerBuildDate: " + CompilerBase.ConstantCompilerBuildDate);
			SVW.WriteLine("AssamblyFile.LastWriteTime: " + j.AssamblyFile.LastWriteTime);
			SVW.WriteLine("SourceVersion.LastWriteTime: " + SourceVersion.LastWriteTime);

			SVW.Close();
        }

    }
}

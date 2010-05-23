using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using ScriptCoreLib.Extensions;

namespace jsc.Languages
{
    using ScriptCoreLib;

    using Languages;
    using Script;
    using ScriptCoreLib.CSharp.Extensions;
    using ScriptCoreLib.Ultra.Library;

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

            Type[] alltypes = CompilerJob.LoadTypesFromReferencedAssemblies(ScriptType.Java, Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName));


            xw.Session.Types = JustMyCodeFilter(sinfo.Options.JustMyCode, alltypes, j.AssamblyInfo);

            xw.Session.ImplementationTypes.AddRange(alltypes);

            sinfo.Logging.LogMessage("found {0} types to be compiled", xw.Session.Types.Length);

            // yay, look at all these little folders we are creating!

            DirectoryInfo TargetDirectory = sinfo.Options.TargetAssembly.Directory.CreateSubdirectory("web");
            DirectoryInfo SourceDir = TargetDirectory.CreateSubdirectory("java");
            DirectoryInfo SourceCompiledDir = TargetDirectory.CreateSubdirectory("release");
            DirectoryInfo SourceCompiledHeadersDir = TargetDirectory.CreateSubdirectory("headers");
            DirectoryInfo SourceNativeDir = TargetDirectory.CreateSubdirectory("native");
            DirectoryInfo SourceBinDir = TargetDirectory.CreateSubdirectory("bin");
            //DirectoryInfo SourceVersionDir = TargetDirectory.CreateSubdirectory("version");

            // assets
            foreach (Assembly v in SharedHelper.LoadReferencedAssemblies(Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName), true))
            {
                EmbeddedResourcesExtensions.ExtractEmbeddedResources(TargetDirectory, v);
            }

            var VersionLookup = sinfo.Options.CachedFileGeneratorConstructor(
                new CachedFileGeneratorBase.Arguments
                {
                    TargetDirectory = TargetDirectory,
                    AssamblyFile = new FileInfo(j.AssamblyInfo.Location),
                    Language = ScriptType.Java
                }
            );

            if (VersionLookup.Validate())
            {
                Console.WriteLine("this version is already built: " + VersionLookup.SourceVersion.Name);

                return;
            }





            //Helper.WorkPool n = new Helper.WorkPool();

            //n.IsThreaded = !Debugger.IsAttached && !sinfo.Options.IsNoThreads;

            //using (new Helper.ConsoleStopper("java type compiler"))
            //{
            //    n.ForEach(

            xw.Session.Types.WithEach(
                delegate(Type xx)
                {
                    if (xx.IsEnum) return;

                    if (xx.Assembly != j.AssamblyInfo)
                        return;


                    CompilerBase c = new Languages.Java.JavaCompiler(new StringWriter(), xw.Session);

                    c.CurrentJob = j;

                    if (c.CompileType(xx))
                    {
                        c.ToConsole(xx, sinfo);



                        //content.WriteToFile(p + "/" + c.GetTypeNameForFilename(x) + "." + ScriptFileExtension);
                        //Program.WriteSingleScriptFile(Languages.Java.JavaCompiler.FileExtension, SourceDir, c, xx);

                        c.MyWriter.Flush();

                        var Namespace__ = string.Join(
                            "/",
                            
                            new [] {
                                SourceDir.FullName
                            }.Concat(
                                c.NamespaceFixup(xx.Namespace, xx).Split('.').Select(
                                    k => c.GetSafeLiteral(k)
                                )
                            ).ToArray()
                        );

                        VersionLookup.Add(
                             Namespace__ + "/" + c.GetTypeNameForFilename(xx) + ".java",
                            c.MyWriter.ToString()
                        );
                    }


                }
            );

            Languages.CompilerJob.InvokeEntryPoints(TargetDirectory, j.AssamblyInfo);

            //}

            VersionLookup.WriteTokens();
        }

    }
}

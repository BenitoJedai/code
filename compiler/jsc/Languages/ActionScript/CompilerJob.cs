using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.CSharp.Extensions;
using System.Linq;
using jsc.Script;

namespace jsc.Languages
{


    public partial class CompilerJob
    {

        private static void CompileActionScript(CompilerJob j, CompileSessionInfo sinfo)
        {
			DirectoryInfo TargetDirectory = sinfo.Options.TargetAssembly.Directory.CreateSubdirectory("web");
			//DirectoryInfo SourceDir = TargetDirectory.CreateSubdirectory("java");
			//DirectoryInfo SourceCompiledDir = TargetDirectory.CreateSubdirectory("release");
			//DirectoryInfo SourceCompiledHeadersDir = TargetDirectory.CreateSubdirectory("headers");
			//DirectoryInfo SourceNativeDir = TargetDirectory.CreateSubdirectory("native");
			//DirectoryInfo SourceBinDir = TargetDirectory.CreateSubdirectory("bin");
			DirectoryInfo SourceVersionDir = TargetDirectory.CreateSubdirectory("version");

			#region SourceVersion
			FileInfo SourceVersion = new FileInfo(
				SourceVersionDir.FullName + "/"
				+ j.AssamblyInfo.ManifestModule.Name
				+ "." + Enum.GetName(typeof(ScriptType), ScriptType.ActionScript)
				+ ".version.txt"
				);

			if (SourceVersion.Exists)
			{
				if (j.AssamblyFile.LastWriteTime <= SourceVersion.LastWriteTime)
				{
					Console.WriteLine("this version is already built: " + SourceVersion.Name);

					//if (!Debugger.IsAttached)
					//{
					return;
					//}
				}
			}


			#endregion


            IdentWriter xw = new IdentWriter();

            xw.Session = new AssamblyTypeInfo();
			xw.Session.Options = sinfo.Options;

            //sinfo.Logging.LogMessage("loading types");

			Type[] alltypes = CompilerJob.LoadTypesFromReferencedAssemblies(ScriptType.ActionScript, Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName));

			var __CommonExtensions = alltypes.Where(k => k.FullName.Contains("CommonExtensions")).ToArray();


            xw.Session.Types = /*JustMyCodeFilter(sinfo.Options.JustMyCode,*/ alltypes/*, j.AssamblyInfo)*/;

            xw.Session.ImplementationTypes.AddRange(alltypes);

			//sinfo.Logging.LogMessage("found {0} types to be compiled", xw.Session.Types.Length);

           
            // assets
            foreach (Assembly v in SharedHelper.LoadReferencedAssemblies(Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName), true))
            {
				EmbeddedResourcesExtensions.ExtractEmbeddedResources(TargetDirectory, v);
            }

           

            Helper.WorkPool n = new Helper.WorkPool();

			//n.IsThreaded = !Debugger.IsAttached && !sinfo.Options.IsNoThreads;

            using (new Helper.ConsoleStopper("actionscript type compiler"))
            {
                n.ForEach(xw.Session.Types,
                    delegate(Type xx)
                    {
                        if (xx.IsEnum) return;

                        if (xx.Assembly != j.AssamblyInfo)
                            return;

                        CompilerBase c = new Languages.ActionScript.ActionScriptCompiler(new StringWriter(), xw.Session);

                        c.CurrentJob = j;

                        Program.AttachXMLDoc(new FileInfo(xx.Assembly.ManifestModule.FullyQualifiedName), c);

                        if (c.CompileType(xx))
                        {

                            c.ToConsole(xx, sinfo);

                            Program.WriteSingleScriptFile(Languages.ActionScript.ActionScriptCompiler.FileExtension, TargetDirectory, c, xx);
                        }
                    }
                );

				Languages.ActionScript.EntryPointProvider.WriteEntryPoints(j.AssamblyInfo, TargetDirectory, xw.Session.Types);

                // Languages.CompilerJob.InvokeEntryPoints(TargetDirectory, j.AssamblyInfo);
            }

            if (!Debugger.IsAttached)
            {
                StreamWriter SVW = new StreamWriter(SourceVersion.OpenWrite());

                SVW.WriteLine("ConstantCompilerBuildDate: " + CompilerBase.ConstantCompilerBuildDate);
                SVW.WriteLine("AssamblyFile.LastWriteTime: " + j.AssamblyFile.LastWriteTime);
                SVW.WriteLine("SourceVersion.LastWriteTime: " + SourceVersion.LastWriteTime);

                SVW.Close();
            }
        }

    }
}

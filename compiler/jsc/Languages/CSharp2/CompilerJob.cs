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
	using ScriptCoreLib.CSharp.Extensions;

	public partial class CompilerJob
	{

		private static void CompileCSharp2(CompilerJob j, CompileSessionInfo sinfo)
		{
			IdentWriter xw = new IdentWriter();

			xw.Session = new AssamblyTypeInfo();

			//sinfo.Logging.LogMessage("loading types");

			Type[] alltypes = CompilerJob.LoadTypes(ScriptType.CSharp2, Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName));


			xw.Session.Types = /*JustMyCodeFilter(sinfo.Options.JustMyCode,*/ alltypes/*, j.AssamblyInfo)*/;

			xw.Session.ImplementationTypes.AddRange(alltypes);

			sinfo.Logging.LogMessage("found {0} types to be compiled", xw.Session.Types.Length);

			DirectoryInfo TargetDirectory = sinfo.Options.TargetAssembly.Directory.CreateSubdirectory("web");
			//DirectoryInfo SourceDir = TargetDirectory.CreateSubdirectory("java");
			//DirectoryInfo SourceCompiledDir = TargetDirectory.CreateSubdirectory("release");
			//DirectoryInfo SourceCompiledHeadersDir = TargetDirectory.CreateSubdirectory("headers");
			//DirectoryInfo SourceNativeDir = TargetDirectory.CreateSubdirectory("native");
			//DirectoryInfo SourceBinDir = TargetDirectory.CreateSubdirectory("bin");
			DirectoryInfo SourceVersionDir = TargetDirectory.CreateSubdirectory("version");

			// assets
			foreach (Assembly v in SharedHelper.LoadReferencedAssemblies(Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName), true))
			{
				EmbeddedResourcesExtensions.ExtractEmbeddedResources(TargetDirectory, v);
			}

			#region SourceVersion
			FileInfo SourceVersion = new FileInfo(
				SourceVersionDir.FullName + "/"
				+ j.AssamblyInfo.ManifestModule.Name
				+ "." + Enum.GetName(typeof(ScriptType), ScriptType.CSharp2)
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

			Helper.WorkPool n = new Helper.WorkPool();

			//n.IsThreaded = !Debugger.IsAttached && !sinfo.Options.IsNoThreads;

			using (new Helper.ConsoleStopper("csharp2 type compiler"))
			{
				n.ForEach(xw.Session.Types,
					delegate(Type xx)
					{
						// this language supports enums
						if (xx.IsEnum)
						{
							//   return;
						}

						if (xx.IsDelegate())
						{
							//   return;
						}

						if (xx.Assembly != j.AssamblyInfo)
							return;

						CompilerBase c = new Languages.CSharp2.CSharp2Compiler(new StringWriter(), xw.Session);

						c.CurrentJob = j;

						Program.AttachXMLDoc(new FileInfo(xx.Assembly.ManifestModule.FullyQualifiedName), c);

						if (c.CompileType(xx))
						{
							c.ToConsole(xx, sinfo);

							Program.WriteSingleScriptFile(Languages.CSharp2.CSharp2Compiler.FileExtension, TargetDirectory, c, xx);
						}
					}
				);

				//Languages.ActionScript.EntryPointProvider.WriteEntryPoints(j.AssamblyInfo, TargetDirectory);

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

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Linq;

namespace jsc.Languages
{
	using ScriptCoreLib;

	using Languages;
	using Script;
	using ScriptCoreLib.CSharp.Extensions;
	using System.Xml;

	public partial class CompilerJob
	{
		public static void AttachXMLDoc(FileInfo TargetAssamblyFile, CompilerBase c)
		{
			FileInfo XmlDocumentInfo = new FileInfo(TargetAssamblyFile.Name.Substring(0, TargetAssamblyFile.Name.LastIndexOf(".")) + ".xml");

			#region doc
			if (XmlDocumentInfo.Exists)
			{
				c.XmlDoc = new XmlDocument();
				c.XmlDoc.Load(XmlDocumentInfo.FullName);

				//Console.WriteLine("*** xmldoc at " + XmlDocumentInfo.FullName);
			}
			else
			{
				//WarningNoXMLDoc(XmlDocumentInfo);
			}
			#endregion
		}

		private static void CompileC(CompilerJob j, CompileSessionInfo sinfo)
		{
			IdentWriter xw = new IdentWriter();

			xw.Session = new AssamblyTypeInfo();
			xw.Session.Options = sinfo.Options;

			sinfo.Logging.LogMessage("loading types");

			Type[] alltypes = CompilerJob.LoadTypes(ScriptType.C, Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName));


			xw.Session.Types = JustMyCodeFilter(sinfo.Options.JustMyCode, alltypes, j.AssamblyInfo);

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

			FileInfo SourceVersion = new FileInfo(
				SourceVersionDir.FullName + "/"
				+ j.AssamblyInfo.ManifestModule.Name
				+ "." + Enum.GetName(typeof(ScriptType), ScriptType.C)
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

			//n.IsThreaded = !Debugger.IsAttached && !sinfo.Options.IsNoThreads;

			using (new Helper.ConsoleStopper("C type compiler"))
			{


				var xx = j.AssamblyInfo;

				if (Path.GetFullPath(xx.Location) == sinfo.Options.TargetAssembly.FullName)
				{

					#region c
					jsc.Languages.C.CCompiler c = new jsc.Languages.C.CCompiler(new StringWriter(), xw.Session);

					AttachXMLDoc(new FileInfo(xx.Location), c);

					c.HeaderFileName = Path.GetFileName(xx.Location) + ".h";

					// .c
					c.Compile(xx);

					StreamWriter _stream2 = new StreamWriter(new FileStream(TargetDirectory.FullName + "/" + Path.GetFileName(xx.Location) + ".c", FileMode.Create));

					_stream2.Write(c.MyWriter.ToString());
					_stream2.Flush();
					_stream2.Close();

					// .h
					c.IsHeaderOnlyMode = true;
					c.MyWriter = new StringWriter();

					c.Compile(xx);


					StreamWriter _stream = new StreamWriter(new FileStream(TargetDirectory.FullName + "/" + c.HeaderFileName, FileMode.Create));

					_stream.Write(c.MyWriter.ToString());
					_stream.Flush();

					_stream.Close();
					#endregion

				


					Languages.CompilerJob.InvokeEntryPoints(TargetDirectory, j.AssamblyInfo);
				}

			}


			StreamWriter SVW = new StreamWriter(SourceVersion.OpenWrite());

			SVW.WriteLine("ConstantCompilerBuildDate: " + CompilerBase.ConstantCompilerBuildDate);
			SVW.WriteLine("AssamblyFile.LastWriteTime: " + j.AssamblyFile.LastWriteTime);
			SVW.WriteLine("SourceVersion.LastWriteTime: " + SourceVersion.LastWriteTime);

			SVW.Close();
		}

	}
}

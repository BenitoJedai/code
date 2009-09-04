using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jsc.meta.Library;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using ScriptCoreLib;
using System.Diagnostics;
using System.Threading;

namespace jsc.meta
{
	partial class Program
	{



		public static void ExtendToJavaConsole(FileInfo assembly, string type, DirectoryInfo javapath)
		{
			// could todo: JNI could be used to implement externs
			// http://tirania.org/blog/archive/2009/Aug-11.html
			// http://blogs.msdn.com/junfeng/archive/2007/07/09/reverse-p-invoke-marshaling-performance.aspx

			// http://www.xs4all.nl/~vkessels/articles/jnijarapplet.html

			Console.WriteLine("will create a java console application for you");

			// http://social.msdn.microsoft.com/Forums/en-US/vbide/thread/0e946e63-a481-45b1-990d-af727914ff15
			// in obj folder we build our binaries
			var staging = assembly.Directory.CreateSubdirectory("staging");

			Environment.CurrentDirectory = staging.FullName;
			
			staging.DefinesTypes(
				typeof(ScriptCoreLib.ScriptAttribute),
				typeof(ScriptCoreLibJava.IAssemblyReferenceToken),

				// do we need it?
				// we should omit this if there are no extern calls in the assembly
				// this would mean we need to have a look at all the 
				// [Script] types for such methods
				typeof(ScriptCoreLibJava.jni.IAssemblyReferenceToken)
			);

			new ExtendToJavaConsoleBuilder
			{
				staging = staging,
				// in bin we copy what we consider as the product
				bin = assembly.Directory,
				javapath = javapath,
				assembly = assembly.LoadAssemblyAt(staging)
			}.Build(type);
		}


		class ExtendToJavaConsoleBuilder
		{
			public DirectoryInfo bin;
			public DirectoryInfo staging;
			public DirectoryInfo javapath;

			public Assembly assembly;

			public Type assembly_type;

			public void Build(string type)
			{
				if (type == null)
					assembly_type = assembly.EntryPoint.DeclaringType;

				if (assembly_type == null)
					assembly_type = assembly.GetType(type);

				if (assembly_type == null)
					throw new InvalidOperationException("entrypoint type is missing");

				var assembly_metaentrypoint = default(MethodInfo);

				// 2
				var MetaScript = InternalBuild(k => assembly_metaentrypoint = k);
				// 3
				#region jsc
				// jsc + javac

				Console.WriteLine("- jsc");
				jsc.Program.TypedMain(
					new jsc.CompileSessionInfo
					{
						Options = new jsc.CommandLineOptions
						{
							TargetAssembly = MetaScript,
							IsJava = true
						}
					}
				);
				#endregion

				var obj_web = Path.Combine(staging.FullName, "web");

				#region javac
				Console.WriteLine("- javac");
				var proccess_javac = Process.Start(
					new ProcessStartInfo(
						Path.Combine(javapath.FullName, "javac.exe"),
						@"-classpath java -d release java\" + assembly_metaentrypoint.DeclaringType.FullName.Replace(".", @"\") + @".java"
						)
					{
						UseShellExecute = false,

						WorkingDirectory = obj_web
					}
				);

				proccess_javac.WaitForExit();
				#endregion

				var obj_web_bin = Path.Combine(obj_web, "bin");

				var bin_jar = new FileInfo(Path.Combine(obj_web_bin, Path.GetFileNameWithoutExtension(assembly.Location) + @".jar"));


				#region jar
				Console.WriteLine("- jar:");
				Console.WriteLine(bin_jar.FullName);

				var proccess_jar =
					new Process
					{
						StartInfo = new ProcessStartInfo(
							Path.Combine(javapath.FullName, "jar.exe"),
							@"cvM -C release ."
						)
						{
							UseShellExecute = false,

							WorkingDirectory = obj_web,

							RedirectStandardOutput = true,
						}
					};

				if (bin_jar.Exists)
					bin_jar.Delete();

				using (var bin_jar_stream = bin_jar.OpenWrite())
				{
					proccess_jar.Start();

					var proccess_jar_output = new Thread(
						delegate()
						{
							while (true)
							{
								var data = new byte[4096];
								var datac = proccess_jar.StandardOutput.BaseStream.Read(data, 0, data.Length);

								if (datac <= 0)
									break;

								bin_jar_stream.Write(data, 0, datac);
							}
						}
					)
					{
						IsBackground = true
					};

					proccess_jar_output.Start();
					proccess_jar.WaitForExit();
					proccess_jar_output.Join();
				}
				#endregion


				#region run_jar
				// 4
				var run_jar = Path.Combine(bin.FullName, Path.GetFileNameWithoutExtension(assembly.Location) + ".jar.bat");
				Console.WriteLine("- created bat entrypoint:");
				Console.WriteLine(run_jar);


				File.WriteAllText(run_jar, 
					@"
@echo off
:: Let's open our working directory. This folder shall contain our jar and dll files.
pushd " + bin_jar.Directory.FullName + @"
call """ + javapath.FullName + @"\java.exe"" -cp ""%PATH%;" + bin_jar.Name + @""" " + assembly_metaentrypoint.DeclaringType.FullName + @"
popd
"

				);
				#endregion


			}


			FileInfo InternalBuild(Action<MethodInfo> AnnounceEntrypoint)
			{



				// Main
				var assembly_type_Main = assembly_type.GetMethod("Main", BindingFlags.Public | BindingFlags.Static);

				var name = new AssemblyName(assembly.GetName().Name + MetaScript);
				var a = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave);
				var m = a.DefineDynamicModule(name.Name, name.Name + ".exe");


				// yay attributes
				var ScriptAttribute = typeof(ScriptCoreLib.ScriptAttribute);

				var ScriptTypeFilterAttribute = default(Func<ScriptType, ScriptTypeFilterAttribute>).ToConstructorInfo();

				//var ScriptTypeFilterAttribute = typeof(ScriptCoreLib.ScriptTypeFilterAttribute);

				a.DefineScriptLibraries(
					assembly_type, 
					typeof(ScriptCoreLibJava.IAssemblyReferenceToken),
					typeof(ScriptCoreLibJava.jni.IAssemblyReferenceToken)
				);

				

				a.SetCustomAttribute(
					new CustomAttributeBuilder(
						ScriptTypeFilterAttribute, new object[] { ScriptType.Java }
					)
				);


				var t = m.DefineType(assembly_type.FullName + MetaScript);

				var main = t.DefineMethod("Main", MethodAttributes.Static, CallingConventions.Standard, typeof(void), new[] { typeof(string[]) });
				var main_il = main.GetILGenerator();


				if (assembly_type_Main.GetParameters().Length > 0)
					main_il.Emit(OpCodes.Ldarg_0);

				//main_il.Emit(OpCodes.Ldnull);
				main_il.EmitCall(OpCodes.Call, assembly_type_Main, null);

				main_il.Emit(OpCodes.Ret);

				t.CreateType();

				a.SetEntryPoint(main);
				AnnounceEntrypoint(main);

				var Product = new FileInfo(Path.Combine(staging.FullName, name.Name + ".exe"));

				a.Save(
					Product.Name
				);

				return Product;
			}


		}
	}
}

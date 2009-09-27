using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Linq;

using jsc.meta.Library;
using ScriptCoreLib;
using System.Collections.Generic;

namespace jsc.meta.Commands.Extend
{

	public class ExtendToJavaConsole
	{
		public FileInfo assembly;
		public string type;
		public DirectoryInfo javapath;
		public DirectoryInfo staging;

		public void Invoke()
		{

			// could todo: JNI could be used to implement externs
			// http://tirania.org/blog/archive/2009/Aug-11.html
			// http://blogs.msdn.com/junfeng/archive/2007/07/09/reverse-p-invoke-marshaling-performance.aspx

			// http://www.xs4all.nl/~vkessels/articles/jnijarapplet.html

			Console.WriteLine("will create a java console application for you");

			// http://social.msdn.microsoft.com/Forums/en-US/vbide/thread/0e946e63-a481-45b1-990d-af727914ff15
			// in obj folder we build our binaries

			if (this.staging == null)
				this.staging = this.assembly.Directory.CreateSubdirectory("staging");
			else if (!staging.Exists)
				this.staging.Create();

			//Environment.CurrentDirectory = staging.FullName;


			staging.DefinesTypes(
				typeof(ScriptCoreLib.ScriptAttribute),
				typeof(ScriptCoreLibJava.IAssemblyReferenceToken),

				// do we need it?
				// we should omit this if there are no extern calls in the assembly
				// this would mean we need to have a look at all the 
				// [Script] types for such methods
				typeof(ScriptCoreLibJava.jni.IAssemblyReferenceToken)
			);

			var assembly = this.assembly.LoadAssemblyAtWithReferences(staging);




			new ExtendToJavaConsoleBuilder
			{
				staging = staging,
				// in bin we copy what we consider as the product
				bin = this.assembly.Directory,
				javapath = javapath,
				assembly = assembly
			}.Build(type);
		}

	}

	class ExtendToJavaConsoleBuilder
	{
		const string MetaScript = "MetaScript";

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

			if (javapath == null)
			{
				Console.WriteLine("java path not specified");
				return;
			}

			var obj_web = Path.Combine(staging.FullName, "web");
			var obj_web_bin = Path.Combine(obj_web, "bin");
			var bin_jar = new FileInfo(Path.Combine(obj_web_bin, Path.GetFileNameWithoutExtension(assembly.Location) + @".jar"));

			#region javac
			Console.WriteLine("- javac");
			var TargetSourceFiles = "java";

			foreach (var r in from k in Directory.GetFiles(obj_web_bin, "*.jar")
							  where k != bin_jar.FullName
							  select k)
			{
				TargetSourceFiles += ";" + Path.Combine("bin", Path.GetFileName(r));
			}

			var proccess_javac = Process.Start(
				new ProcessStartInfo(
					Path.Combine(javapath.FullName, "javac.exe"),
					@"-classpath " + TargetSourceFiles + @" -d release java\" + assembly_metaentrypoint.DeclaringType.FullName.Replace(".", @"\") + @".java"
					)
				{
					UseShellExecute = false,

					WorkingDirectory = obj_web
				}
			);

			proccess_javac.WaitForExit();
			#endregion




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

			var library_path = bin_jar.Directory.FullName.Substring(new FileInfo(run_jar).Directory.FullName.Length + 1);

			var ClassPath = library_path + @"\" + bin_jar.Name;

			foreach (var r in from k in Directory.GetFiles(obj_web_bin, "*.jar")
							  where k != bin_jar.FullName
							  select k)
			{
				ClassPath += ";" + Path.Combine(library_path, Path.GetFileName(r));
			}

			File.WriteAllText(run_jar,
				@"
@echo off
setlocal

call """ + javapath.FullName + @"\java.exe"" -Djava.library.path=""" + library_path + @""" -cp """ + ClassPath + @""" " + assembly_metaentrypoint.DeclaringType.FullName + @" %*

endlocal
"

			);
			#endregion


		}


		FileInfo InternalBuild(Action<MethodInfo> AnnounceEntrypoint)
		{



			// Main
			var assembly_type_Main = assembly_type.GetMethod("Main", BindingFlags.Public | BindingFlags.Static);

			var name = new AssemblyName(assembly.GetName().Name + MetaScript);

			var Product = new FileInfo(Path.Combine(staging.FullName, name.Name + ".exe"));
			if (Product.Exists)
				Product.Delete();


			var a = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave, staging.FullName);
			var m = a.DefineDynamicModule(name.Name, name.Name + ".exe");


			// yay attributes
			var ScriptAttribute = typeof(ScriptCoreLib.ScriptAttribute);

			var ScriptTypeFilterAttribute = default(Func<ScriptType, ScriptTypeFilterAttribute>).ToConstructorInfo();

			//var ScriptTypeFilterAttribute = typeof(ScriptCoreLib.ScriptTypeFilterAttribute);


			// Visual Basic is generating a My namespace
			// at this time we should exclude it from the console application


			var AssemblyScriptAttribute = new ScriptAttribute
			{
				IsScriptLibrary = true,
				ScriptLibraries = new[] {
						assembly_type,
						typeof(ScriptCoreLibJava.IAssemblyReferenceToken),
						typeof(ScriptCoreLibJava.jni.IAssemblyReferenceToken)
					},
				NonScriptTypes = assembly.GetTypes().Where(
					k =>
						k.Namespace.EndsWith(".My") ||
						k.Namespace.EndsWith(".My.Resources")
				).ToArray()
			};

			a.DefineScriptAttribute(
				new
				{
					AssemblyScriptAttribute.IsScriptLibrary,
					AssemblyScriptAttribute.ScriptLibraries,
					AssemblyScriptAttribute.NonScriptTypes
				}
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


			a.Save(
				Product.Name
			);


			Product.Refresh();
			if (Product.Exists)
				Console.WriteLine("Product: " + Product.FullName);
			else
				throw new FileNotFoundException(Product.FullName);


			return Product;
		}


	}
}

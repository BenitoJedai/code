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



		static void ExtendToJavaConsole(FileInfo assembly, string type, DirectoryInfo javapath)
		{
			Console.WriteLine("will create a java console application for you");

			// http://social.msdn.microsoft.com/Forums/en-US/vbide/thread/0e946e63-a481-45b1-990d-af727914ff15
			// in obj folder we build our binaries
			var obj = assembly.Directory.CreateSubdirectory("obj");

			Environment.CurrentDirectory = obj.FullName;

			var obj_assembly = Path.Combine(obj.FullName, assembly.Name);

			assembly.CopyTo(obj_assembly, true);

			var ScriptCoreLibA = new FileInfo(typeof(ScriptCoreLib.ScriptAttribute).Assembly.Location);
			ScriptCoreLibA.CopyTo(Path.Combine(obj.FullName, ScriptCoreLibA.Name), true);

			var ScriptCoreLibJava = new FileInfo(typeof(ScriptCoreLibJava.IAssemblyReferenceToken).Assembly.Location);
			ScriptCoreLibJava.CopyTo(Path.Combine(obj.FullName, ScriptCoreLibJava.Name), true);
			new ExtendToJavaConsoleBuilder
			{
				obj = obj,
				// in bin we copy what we consider as the product
				bin = assembly.Directory,
				javapath = javapath,
				assembly = Assembly.LoadFile(obj_assembly)
			}.Build(type);
		}


		class ExtendToJavaConsoleBuilder
		{
			public DirectoryInfo bin;
			public DirectoryInfo obj;
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
				// jsc + javac
				var build = Path.Combine(obj.FullName, "build.bat");

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

				var obj_web = Path.Combine(obj.FullName, "web");

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

				var bin_jar = new FileInfo(Path.Combine(bin.FullName, Path.GetFileNameWithoutExtension(assembly.Location) + @".jar"));


				#region jar
				Console.WriteLine("- jar");
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



				// 4
				var run_jar = Path.Combine(bin.FullName, Path.GetFileNameWithoutExtension(assembly.Location) + ".jar.bat");
				Console.WriteLine("- created bat entrypoint:");
				Console.WriteLine(run_jar);

				File.WriteAllText(run_jar, @"@call """ + javapath.FullName + @"\java.exe"" -cp ""%PATH%;" + bin_jar.FullName + @""" " + assembly_metaentrypoint.DeclaringType.FullName);


			}


			FileInfo InternalBuild(Action<MethodInfo> AnnounceEntrypoint)
			{
				var suffix = "MetaScript";



				// Main
				var assembly_type_Main = assembly_type.GetMethod("Main", BindingFlags.Public | BindingFlags.Static);

				var name = new AssemblyName(assembly.GetName().Name + suffix);
				var a = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave);
				var m = a.DefineDynamicModule(name.Name, name.Name + ".mod");


				// yay attributes
				var ScriptAttribute = typeof(ScriptCoreLib.ScriptAttribute);

				var ScriptTypeFilterAttribute = default(Func<ScriptType, ScriptTypeFilterAttribute>).ToConstructorInfo();

				//var ScriptTypeFilterAttribute = typeof(ScriptCoreLib.ScriptTypeFilterAttribute);

				a.SetCustomAttribute(
					new CustomAttributeBuilder(
						ScriptAttribute.GetConstructors().Single(),
						new object[0],
						new[] { 
								ScriptAttribute.GetField("ScriptLibraries"),
								ScriptAttribute.GetField("IsScriptLibrary")},
						new object[] { 
								new Type[] { 
									assembly_type, 
									typeof(ScriptCoreLibJava.IAssemblyReferenceToken),
								},
								true
							}
					)
				);

				a.SetCustomAttribute(
					new CustomAttributeBuilder(
						ScriptTypeFilterAttribute, new object[] { ScriptType.Java }
					)
				);


				var t = m.DefineType(assembly_type.FullName + suffix);

				var main = t.DefineMethod("Main", MethodAttributes.Static, CallingConventions.Standard, typeof(void), new[] { typeof(string[]) });
				var main_il = main.GetILGenerator();



				main_il.Emit(OpCodes.Ldarg_0);
				//main_il.Emit(OpCodes.Ldnull);
				main_il.EmitCall(OpCodes.Call, assembly_type_Main, null);

				main_il.Emit(OpCodes.Ret);

				t.CreateType();

				a.SetEntryPoint(main);
				AnnounceEntrypoint(main);

				var Product = new FileInfo(Path.Combine(obj.FullName, name.Name + ".exe"));

				a.Save(
					Product.Name
				);

				return Product;
			}


		}
	}
}

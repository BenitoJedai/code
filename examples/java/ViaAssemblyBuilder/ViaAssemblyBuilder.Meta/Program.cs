using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViaAssemblyBuilder.Meta
{
	using Library;
	using System.IO;
	using System.Reflection;
	using System.Reflection.Emit;
	using ScriptCoreLib;

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("This is a meta compiler component where we will provide extension to the application.");

			args.AsParametersFor((Action<FileInfo, string>)Extend);
		}

		public static void Extend(FileInfo assembly, string type)
		{
			Console.WriteLine(new { assembly, type });

			// http://social.msdn.microsoft.com/Forums/en-US/vbide/thread/0e946e63-a481-45b1-990d-af727914ff15
			// in obj folder we build our binaries
			var obj = assembly.Directory.CreateSubdirectory("obj");

			// in bin we copy what we consider as the product
			var bin = assembly.Directory.CreateSubdirectory("bin");

			Environment.CurrentDirectory = obj.FullName;

			var obj_assembly = Path.Combine(obj.FullName, assembly.Name);

			assembly.CopyTo(obj_assembly, true);

			var ViaAssemblyBuilder_ExtensionPoint = new FileInfo(typeof(ViaAssemblyBuilder.ExtensionPoint.Definition).Assembly.Location);
			ViaAssemblyBuilder_ExtensionPoint.CopyTo(Path.Combine(obj.FullName, ViaAssemblyBuilder_ExtensionPoint.Name), true);

			var ScriptCoreLibA = new FileInfo(typeof(ScriptCoreLib.ScriptAttribute).Assembly.Location);
			ScriptCoreLibA.CopyTo(Path.Combine(obj.FullName, ScriptCoreLibA.Name), true);

			var ScriptCoreLibJava = new FileInfo(typeof(ScriptCoreLibJava.IAssemblyReferenceToken).Assembly.Location);
			ScriptCoreLibJava.CopyTo(Path.Combine(obj.FullName, ScriptCoreLibJava.Name), true);

			new MetaBuilder
			{
				obj = obj,
				bin = bin,
				assembly = Assembly.LoadFile(obj_assembly)
			}.Build(type);

		}

		class MetaBuilder
		{
			public DirectoryInfo obj;
			public DirectoryInfo bin;

			public Assembly assembly;


			public void Build(string type)
			{
				// 1
				var Meta = Build(type, false);
				// 2
				var MetaScript = Build(type, true);
				// 3
				// jsc + javac
				var build = Path.Combine(obj.FullName, "build.bat");

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


				File.WriteAllText(build, @"
@echo off
::echo jsc
::call c:\util\jsc\bin\jsc.exe " + MetaScript.FullName + @" -java
pushd web
echo - javac
""C:\Program Files\Java\jdk1.6.0_14\bin\javac.exe"" -classpath java -d release java\" + type.Replace(".", @"\") + @"MetaScript.java
echo - jar
""C:\Program Files\Java\jdk1.6.0_14\bin\jar.exe"" cvM -C release . > bin\" + Path.GetFileNameWithoutExtension(assembly.Location) + @".jar
popd
				");
				// 4
				var run_jar = Path.Combine(obj.FullName, "run.jar.bat");
				File.WriteAllText(run_jar, @"
@echo off
pushd web\bin
""C:\Program Files\Java\jdk1.6.0_14\bin\java.exe"" -cp ""%PATH%;" + Path.GetFileNameWithoutExtension(assembly.Location) + @".jar"" " + type + @"MetaScript
popd
				");
			}

			FileInfo Build(string type, bool IsScript)
			{
				var suffix = "Meta";

				if (IsScript)
					suffix += "Script";

				var assembly_type = assembly.GetType(type);

				// ExtensionPoint
				var assembly_type_ExtensionPoint = assembly_type.GetField("ExtensionPoint");
				// Main
				var assembly_type_Main = assembly_type.GetMethod("Main", BindingFlags.Public | BindingFlags.Static);

				var name = new AssemblyName(assembly.GetName().Name + suffix);
				var a = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave);
				var m = a.DefineDynamicModule(name.Name, name.Name + ".mod");

				if (IsScript)
				{
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
									typeof(ViaAssemblyBuilder.ExtensionPoint.Definition)
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

				}

				var t = m.DefineType(assembly_type.FullName + suffix);

				var main = t.DefineMethod("Main", MethodAttributes.Static, CallingConventions.Standard, typeof(void), new[] { typeof(string[]) });
				var main_il = main.GetILGenerator();

				if (IsScript)
				{
					main_il.EmitWriteLine("This assembly was prepared to run on java virtual machine.");
				}
				else
				{
					main_il.EmitWriteLine("This assembly was not prepared to run on java virtual machine.");
				}


				BindExtensionPoint(main_il, assembly_type_ExtensionPoint);

				//main_il.Emit(OpCodes.Ldarg_0);
				main_il.Emit(OpCodes.Ldnull);
				main_il.EmitCall(OpCodes.Call, assembly_type_Main, null);

				main_il.Emit(OpCodes.Ret);

				t.CreateType();

				a.SetEntryPoint(main);

				var Product = new FileInfo(Path.Combine(obj.FullName, name.Name + ".exe"));

				a.Save(
					Product.Name
				);

				return Product;
			}

			private void BindExtensionPoint(ILGenerator main_il, FieldInfo assembly_type_ExtensionPoint)
			{
				// we must copy the assembly we are referencing!
				var ViaAssemblyBuilder_ExtensionPoint = new FileInfo(typeof(ViaAssemblyBuilder.ExtensionPoint.Definition).Assembly.Location);


				Action Definition_Invoke = ViaAssemblyBuilder.ExtensionPoint.Definition.Invoke;

				main_il.Emit(OpCodes.Ldnull);
				main_il.Emit(OpCodes.Ldftn, Definition_Invoke.Method);
				main_il.Emit(OpCodes.Newobj, typeof(Action).GetConstructors().Single());
				main_il.Emit(OpCodes.Stsfld, assembly_type_ExtensionPoint);

			}
		}
	}
}

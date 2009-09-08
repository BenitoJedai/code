﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jsc.meta.Library;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using ScriptCoreLib;
using ScriptCoreLib.Archive.ZIP;


#pragma warning disable 1720
// http://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=482159

namespace jsc.meta
{
	partial class Program
	{
		public class ExtendToWindowsFormsEverywhere
		{
			public FileInfo assembly;
			public string type;
			public DirectoryInfo staging;
			public FileInfo zip;


			public void Invoke()
			{

				// todo: compare to http://tirania.org/blog/archive/2009/Jul-28.html

				// think windows forms but on javascript, followed by flash and java

				Console.WriteLine("will create a javascript application for you");

				// http://social.msdn.microsoft.com/Forums/en-US/vbide/thread/0e946e63-a481-45b1-990d-af727914ff15

				if (this.staging == null)
					this.staging = this.assembly.Directory.CreateSubdirectory("staging");
				else if (!staging.Exists)
					this.staging.Create();

				Environment.CurrentDirectory = staging.FullName;

				staging.DefinesTypes(
					typeof(ScriptCoreLib.ScriptAttribute),
					typeof(ScriptCoreLib.Shared.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Net.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Query.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Drawing.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Windows.Forms.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Avalon.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Avalon.Integration.IAssemblyReferenceToken)
				);


				new ExtendToWindowsFormsEverywhereBuilder
				{
					context = this,
					assembly = this.assembly.LoadAssemblyAt(staging)
				}.Build(this.type);
			}
		}

		class ExtendToWindowsFormsEverywhereBuilder
		{
			public ExtendToWindowsFormsEverywhere context;

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

				Console.WriteLine("- jsc");
				jsc.Program.TypedMain(
					new jsc.CompileSessionInfo
					{
						Options = new jsc.CommandLineOptions
						{
							TargetAssembly = MetaScript,
							IsJavaScript = true
						}
					}
				);
				#endregion

				#region web to .js.zip
				var staging_web = new DirectoryInfo(Path.Combine(this.context.staging.FullName, "web"));

				if (staging_web.Exists)
					if (this.context.zip != null)
					{
						var zip = new ZIPFile();

						foreach (var file in staging_web.GetFiles("*", SearchOption.AllDirectories))
						{
							zip.Add(file.FullName.Substring(this.context.staging.FullName.Length + 1), File.ReadAllBytes(file.FullName));
						}

						var zzm = new MemoryStream();
						using (var w = new BinaryWriter(zzm))
						{
							zip.WriteTo(w);
						}

						File.WriteAllBytes(this.context.zip.FullName, zzm.ToArray());
					}
				#endregion

			}


			FileInfo InternalBuild(Action<MethodInfo> AnnounceEntrypoint)
			{
				// Main
				// C# project template in .net 4 has internal Main... who knew...
				var assembly_type_Main = assembly_type.GetMethod("Main", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

				var name = new AssemblyName(assembly.GetName().Name + MetaScript);
				var a = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave);
				var m = a.DefineDynamicModule(name.Name, name.Name + ".exe");


				// yay attributes
				var ScriptAttribute = typeof(ScriptCoreLib.ScriptAttribute);

				a.DefineScriptLibraries(
					assembly_type,
					typeof(ScriptCoreLib.Shared.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Net.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Query.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Drawing.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Windows.Forms.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Avalon.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Avalon.Integration.IAssemblyReferenceToken)
				);


				var t_redirect = m.DefineType(assembly_type.FullName + MetaScript);

				var main = t_redirect.DefineMethod("Main", MethodAttributes.Static, CallingConventions.Standard, typeof(void), new[] { typeof(string[]) });
				var main_il = main.GetILGenerator();


				if (assembly_type_Main.GetParameters().Length > 0)
					main_il.Emit(OpCodes.Ldarg_0);

				//main_il.Emit(OpCodes.Ldnull);
				main_il.EmitCall(OpCodes.Call, assembly_type_Main, null);

				main_il.Emit(OpCodes.Ret);



				var t = m.DefineType(assembly_type.Namespace + ".JavaScript." + assembly_type.Assembly.GetName().Name + "Document");

				var DefineScriptAttribute = default(Func<ScriptAttribute>).DefineAttributeAt(t);
				var DefineScriptApplicationEntryPointAttribute = default(Func<ScriptApplicationEntryPointAttribute>).DefineAttributeAt(t);

				DefineScriptAttribute();
				DefineScriptApplicationEntryPointAttribute();


				var t_ctor = t.DefineConstructor(MethodAttributes.Public | MethodAttributes.HideBySig, CallingConventions.HasThis, new Type[0]);
				var t_ctor_il = t_ctor.GetILGenerator();


				//L_0000: ldarg.0 
				//L_0001: call instance void [mscorlib]System.Object::.ctor()
				t_ctor_il.Emit(OpCodes.Ldarg_0);
				t_ctor_il.Emit(OpCodes.Call, typeof(object).GetConstructor(new Type[0]));

				// could pass some params around...
				if (assembly_type_Main.GetParameters().Length > 0)
					t_ctor_il.Emit(OpCodes.Ldnull);

				t_ctor_il.EmitCall(OpCodes.Call, assembly_type_Main, null);
				t_ctor_il.Emit(OpCodes.Ret);


				#region spawn

				// we are going to show GUI from cctor.. this wont look nice under .net virtual machine! :)
				// it is only for jsc compiler at this time...
				var cctor = t.DefineTypeInitializer();
				var cctor_il = cctor.GetILGenerator();

				// L_0000: nop 
				//L_0001: ldtoken WindowsFormsApplication1Document.js.WindowsFormsApplication1Document
				//L_0006: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
				//L_000b: call void [ScriptCoreLib]ScriptCoreLib.JavaScript.Extensions.Extensions::Spawn(class [mscorlib]System.Type)
				//L_0010: nop 
				//L_0011: ret 

				cctor_il.Emit(OpCodes.Ldtoken, t);
				cctor_il.EmitCall(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"), null);
				cctor_il.EmitCall(OpCodes.Call, ((Action<Type>)ScriptCoreLib.JavaScript.Extensions.Extensions.SpawnEntrypointWithBrandning).Method, null);

				cctor_il.Emit(OpCodes.Ret);

				#endregion



				t_redirect.CreateType();
				var tt = t.CreateType();


				//var DefineScriptTypeFilterAttribute = default(Func<ScriptType, Type, ScriptTypeFilterAttribute>).DefineAttributeAt(a);

				//DefineScriptTypeFilterAttribute(ScriptType.JavaScript, tt);

				var DefineScriptTypeFilterAttribute = default(Func<ScriptType, string, ScriptTypeFilterAttribute>).DefineAttributeAt(a);

				DefineScriptTypeFilterAttribute(ScriptType.JavaScript, tt.Namespace);

				//var DefineScriptTypeFilterAttribute = default(Func<ScriptType, ScriptTypeFilterAttribute>).DefineAttributeAt(a);

				//DefineScriptTypeFilterAttribute(ScriptType.JavaScript);


				a.SetEntryPoint(main);
				AnnounceEntrypoint(main);

				var Product = new FileInfo(Path.Combine(this.context.staging.FullName, name.Name + ".exe"));

				a.Save(
					Product.Name
				);

				return Product;

			}
		}
	}
}

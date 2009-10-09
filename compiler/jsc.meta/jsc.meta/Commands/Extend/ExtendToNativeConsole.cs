using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using jsc.meta.Library;
using ScriptCoreLib;
using System.Collections.Generic;
using jsc.meta.Tools;
using System.Reflection;
using System.Reflection.Emit;
using System.Diagnostics;

namespace jsc.meta.Commands.Extend
{
	public class ExtendToNativeConsole
	{
		public FileInfo c1;
		public FileInfo assembly;
		public DirectoryInfo staging;


		public void Invoke()
		{

			if (this.staging == null)
				this.staging = this.assembly.Directory.CreateSubdirectory("staging");
			else if (!staging.Exists)
				this.staging.Create();


			var assembly = this.assembly.LoadAssemblyAt(staging);


			staging.DefinesTypes(
				// this reference will cause the assembly to be loaded from the 
				// wrong location, how could we go ahead and actually fix it?

				// so yes captain, we have a problem

				// we could tell our loader strategy manager
				// that the assembliyes shall be loaded at staging
				typeof(ScriptCoreLibNative.IAssemblyReferenceToken)

			);

			// load the rest of the references
			assembly.LoadReferencesAt(staging, this.assembly.Directory);

			// we can now see that in the Debug/Windows/Modules all
			// readable assemblies are stored under staging

			new Builder
			{
				context = this,
				assembly = assembly
			}.Invoke();
		}

		public class Builder
		{
			const string MetaScript = "MetaScript";

			/// <summary>
			/// The context shall contain all the input parameters
			/// we have acted upon thus so far
			/// </summary>
			public ExtendToNativeConsole context;

			/// <summary>
			/// This is the assembly which we will need to translate
			/// to a native console application.
			/// 
			/// It should have been loaded from the staging area.
			/// </summary>
			public Assembly assembly;

			public void Invoke()
			{
				var name = new AssemblyName(assembly.GetName().Name + MetaScript);
				var Product = new FileInfo(Path.Combine(this.context.staging.FullName, name.Name + ".exe"));
				if (Product.Exists)
					Product.Delete();
				var a = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave, this.context.staging.FullName);
				var m = a.DefineDynamicModule(name.Name, name.Name + ".exe");


				// yay attributes
				var ScriptAttribute = typeof(ScriptCoreLib.ScriptAttribute);
				var DefineScriptTypeFilterAttribute = default(Func<ScriptType, string, ScriptTypeFilterAttribute>).DefineAttributeAt(a);


				var AssemblyScriptAttribute = new ScriptAttribute
				{
					IsScriptLibrary = true,
					ScriptLibraries = new[] {
							assembly.EntryPoint.DeclaringType,
							typeof(ScriptCoreLibNative.IAssemblyReferenceToken),
					},
					NonScriptTypes = assembly.GetTypes().Where(
						k =>
							k.Namespace != null && (
								k.Namespace.EndsWith(".My") ||
								k.Namespace.EndsWith(".My.Resources")
							)

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
						default(Func<string, string, System.CodeDom.Compiler.GeneratedCodeAttribute>).ToConstructorInfo(),
						new object[] { Assembly.GetExecutingAssembly().GetName().Name, Assembly.GetExecutingAssembly().GetName().Version.ToString() }
					)
				);

				// why did we need a redirect?
				// it is for .net entrypoint as it cannot be in another library
				// the redirect wont be translated
				#region CLR entrypoint
				{

					var t = m.DefineType(assembly.EntryPoint.DeclaringType.Namespace + ".CLR." + assembly.GetName().Name + "Document");




					var main = t.DefineMethod("Main", MethodAttributes.Static, CallingConventions.Standard, typeof(void), new[] { typeof(string[]) });
					var main_il = main.GetILGenerator();


					if (assembly.EntryPoint.GetParameters().Length > 0)
						main_il.Emit(OpCodes.Ldarg_0);

					//main_il.Emit(OpCodes.Ldnull);
					main_il.EmitCall(OpCodes.Call, assembly.EntryPoint, null);

					main_il.Emit(OpCodes.Ret);


					default(Func<STAThreadAttribute>).DefineAttributeAt(main)();

					t.CreateType();


					a.SetEntryPoint(main);
				}
				#endregion


				#region Native entrypoint
				{

					var t = m.DefineType(assembly.EntryPoint.DeclaringType.Namespace + ".Native." + assembly.GetName().Name + "Document");




					var main = t.DefineMethod("Main", MethodAttributes.Static, CallingConventions.Standard, typeof(void), new Type[] { 
						//typeof(int), typeof(string[]) 
					});
					var main_il = main.GetILGenerator();

					
					// string[] is not actually supported due to the fact
					// we we wont be passing on the count in the array
					// we should be storing it by some other means
					// like Environment.Commandline
					if (assembly.EntryPoint.GetParameters().Length > 0)
						throw new NotImplementedException();

					//main_il.Emit(OpCodes.Ldnull);
					main_il.EmitCall(OpCodes.Call, assembly.EntryPoint, null);

					main_il.Emit(OpCodes.Ret);


					//default(Func<STAThreadAttribute>).DefineAttributeAt(main)();

					var tt = t.CreateType();

					DefineScriptTypeFilterAttribute(ScriptType.C, tt.Namespace);

					//a.SetEntryPoint(main);
				}
				#endregion

				a.Save(
					Product.Name
				);

				Product.Refresh();


				Product.ToC(this.context.c1);
			}
		}
	}
}

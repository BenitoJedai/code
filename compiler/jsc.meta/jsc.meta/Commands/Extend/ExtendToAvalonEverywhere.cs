using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows.Controls;
using jsc.Languages.IL;
using jsc.meta.Library;
using jsc.meta.Tools;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Diagnostics;

namespace jsc.meta.Commands.Extend
{
	public class ExtendToAvalonEverywhere
	{
		public FileInfo assembly;
		// If we are targeting a library it will not have an entrypoint
		// and we cannot infer the main canvas
		//public string type;
		public DirectoryInfo staging;
		public FileInfo zip;
		public FileInfo mxmlc;

		/// <summary>
		/// Setting this field to false disables javascript generation.
		/// </summary>
		public bool javascript = true;

		/// <summary>
		/// The swf file shall be converted to an exe file.
		/// </summary>
		public FileInfo flashplayer;

		public bool operawidget;

		public void Invoke()
		{
			//Debugger.Launch();

			if (this.staging == null)
				this.staging = this.assembly.Directory.CreateSubdirectory("staging");
			else if (!staging.Exists)
				this.staging.Create();

			staging.DefinesTypes(
				typeof(ScriptCoreLib.ScriptAttribute),
				typeof(ScriptCoreLib.Shared.IAssemblyReferenceToken),
				typeof(ScriptCoreLib.Shared.Net.IAssemblyReferenceToken),
				typeof(ScriptCoreLib.Shared.Query.IAssemblyReferenceToken),
				typeof(ScriptCoreLib.Shared.Avalon.IAssemblyReferenceToken)
			);

			var assembly = this.assembly.LoadAssemblyAtWithReferences(staging);



			new Builder
			{
				context = this,
				assembly = assembly
			}.Invoke();
		}

		public class Builder
		{
			const string MetaScript = "MetaScript";

			public ExtendToAvalonEverywhere context;

			public Assembly assembly;


			public void Invoke()
			{

				// we actually need to look at who will be called
				// with or .ToWindow function
				// and we need to mark this type as NonScript
				var EntryPoint_il = new ILBlock(assembly.EntryPoint);

				/*
+		[0]	{[0x0000] nop        +0 -0}	jsc.ILBlock.Prestatement
+		[1]	{[0x0010] pop        +0 -1{[0x000b] callvirt   +1 -1{[0x0006] call       +1 -1{[0x0001] newobj     +1 -0} } } }	jsc.ILBlock.Prestatement
+		[2]	{[0x0011] ret        +0 -0}	jsc.ILBlock.Prestatement
				 */

				var c = Enumerable.Single(
					from k in EntryPoint_il.Instructrions
					where k.OpCode == OpCodes.Newobj
					let DeclaringType = k.TargetConstructor.DeclaringType
					where typeof(Canvas).IsAssignableFrom(DeclaringType)
					select new { DeclaringType, k }
				);

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
							typeof(ScriptCoreLib.Shared.IAssemblyReferenceToken),
							typeof(ScriptCoreLib.Shared.Net.IAssemblyReferenceToken),
							typeof(ScriptCoreLib.Shared.Query.IAssemblyReferenceToken),
							typeof(ScriptCoreLib.Shared.Avalon.IAssemblyReferenceToken)

							
							//we should first implement forms support for flash, atleast for user controls
							//typeof(ScriptCoreLib.Shared.Avalon.Integration.IAssemblyReferenceToken)
					},
					NonScriptTypes = assembly.GetTypes().Where(
						k =>
							k.Namespace != null && (
								k.Namespace.EndsWith(".My") ||
								k.Namespace.EndsWith(".My.Resources")
							)
					).Concat(
						new[]
						{
							assembly.EntryPoint.DeclaringType
						}
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

				#region JavaScript entrypoint
				{
					var t = m.DefineType(assembly.EntryPoint.DeclaringType.Namespace + ".JavaScript." + assembly.GetName().Name + "Document");

					var DefineScriptAttribute = default(Func<ScriptAttribute>).DefineAttributeAt(t);
					var DefineScriptApplicationEntryPointAttribute = default(Func<ScriptApplicationEntryPointAttribute>).DefineAttributeAt(t);

					DefineScriptAttribute();
					DefineScriptApplicationEntryPointAttribute();

					#region SpawnToHandler
					var SpawnToHandler = t.DefineMethod("InitializeComponent", MethodAttributes.Static, CallingConventions.Standard, typeof(void), new[] { typeof(IHTMLElement) });

					{
						var il = SpawnToHandler.GetILGenerator();

						// create canvas and add it before arg1



						Action<IHTMLElement> TemplateSpawnToHandler =
							ContainerMarker =>
							{
								var Container = new IHTMLDiv();
								var Canvas = new Canvas();

								ContainerMarker.insertPreviousSibling(Container);

								global::ScriptCoreLib.JavaScript.Extensions.AvalonExtensions.AttachToContainer(Canvas, Container);
							};


						TemplateSpawnToHandler.EmitTo(il,
							ctor =>
							{
								if (ctor.DeclaringType == typeof(Canvas))
									return c.k.TargetConstructor;

								return ctor;
							}
						);


					}
					#endregion


					#region spawn

					// we are going to show GUI from cctor.. this wont look nice under .net virtual machine! :)
					// it is only for jsc compiler at this time...
					var cctor = t.DefineTypeInitializer();
					{
						var il = cctor.GetILGenerator();

						// L_0000: nop 
						//L_0001: ldtoken WindowsFormsApplication1Document.js.WindowsFormsApplication1Document
						//L_0006: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
						//L_000b: call void [ScriptCoreLib]ScriptCoreLib.JavaScript.Extensions.Extensions::Spawn(class [mscorlib]System.Type)
						//L_0010: nop 
						//L_0011: ret 

						il.Emit(OpCodes.Ldtoken, t);
						il.EmitCall(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"), null);

						il.Emit(OpCodes.Ldnull);
						il.Emit(OpCodes.Ldftn, SpawnToHandler);

						il.Emit(OpCodes.Newobj, typeof(Action<IHTMLElement>).GetConstructors().Single());

						il.EmitCall(OpCodes.Call, ((Action<Type, Action<IHTMLElement>>)ScriptCoreLib.JavaScript.Extensions.Extensions.SpawnTo).Method, null);

						il.Emit(OpCodes.Ret);
					}
					#endregion



					var tt = t.CreateType();




					DefineScriptTypeFilterAttribute(ScriptType.JavaScript, tt.Namespace);

				}
				#endregion

				var ActionScriptEntryPoint = default(Type);

				#region ActionScript entrypoint
				{
					var t = m.DefineType(assembly.EntryPoint.DeclaringType.Namespace + ".ActionScript." + assembly.GetName().Name + "Sprite", TypeAttributes.Public, typeof(global::ScriptCoreLib.ActionScript.flash.display.Sprite));

					ActionScriptEntryPoint = t;

					var DefineScriptAttribute = default(Func<ScriptAttribute>).DefineAttributeAt(t);

					DefineScriptAttribute();

					//DefaultWidth.IsLiteral	true	bool
					//DefaultWidth.GetRawConstantValue()	480	object {int}


					var DefaultWidth = c.DeclaringType.GetLiteralInt32("DefaultWidth", 600);
					var DefaultHeight = c.DeclaringType.GetLiteralInt32("DefaultHeight", 400);


					t.DefineAttribute<ScriptApplicationEntryPointAttribute>(
						// we should detect the default size of the canvas
						new { Width = DefaultWidth, Height = DefaultHeight, WithResources = true }
					);

					t.DefineAttribute<global::ScriptCoreLib.ActionScript.SWFAttribute>(
						// we should detect the default size of the canvas
						new { width = DefaultWidth, height = DefaultHeight }
					);

					#region SpawnToHandler
					var InitializeComponent = t.DefineMethod("InitializeComponent", MethodAttributes.Static, CallingConventions.Standard, typeof(void), new[] { typeof(global::ScriptCoreLib.ActionScript.flash.display.Sprite) });

					{
						var il = InitializeComponent.GetILGenerator();

						// create canvas and add it before arg1



						Action<global::ScriptCoreLib.ActionScript.flash.display.Sprite> TemplateSpawnToHandler =
							(Container) =>
							{
								var Canvas = new Canvas();


								global::ScriptCoreLib.ActionScript.Extensions.AvalonExtensions.AttachToContainer(Canvas, Container);
							};


						TemplateSpawnToHandler.EmitTo(il,
							ctor =>
							{
								if (ctor.DeclaringType == typeof(Canvas))
									return c.k.TargetConstructor;

								return ctor;
							}
						);


					}
					#endregion



					var t_ctor = t.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, new Type[0]);

					{
						var il = t_ctor.GetILGenerator();

						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Call, typeof(global::ScriptCoreLib.ActionScript.flash.display.Sprite).GetConstructor(new Type[0]));
						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Call, InitializeComponent);
						il.Emit(OpCodes.Ret);
					}


					var tt = t.CreateType();




					DefineScriptTypeFilterAttribute(ScriptType.ActionScript, tt.Namespace);

				}
				#endregion



				a.Save(
					Product.Name
				);

				Product.Refresh();

				//Product.ToJavaScript();

				
				Product.ToActionScript(this.context.mxmlc, this.context.flashplayer, ActionScriptEntryPoint, null);
			}




		}
	}
}

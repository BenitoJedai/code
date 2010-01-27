using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using java.applet;
using jsc.meta.Library;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using jsc.meta.Tools;
using System.Reflection.Emit;
using jsc.Languages.IL;
using jsc.meta.Library.Templates;

namespace jsc.meta.Commands.Rewrite
{
	[Description("This command will tare an assembly to compile java and flash objects separatly.")]
	public partial class RewriteToJavaScriptDocument : CommandBase
	{

		/* How was this feature implemented in the long run?
		 * 
		 * 1. Adding a new command to the command chain
		 * 2. Test if the parameters are passed with a test project
		 * 3. Save to the svn
		 * 4. Rewrite components to their staging folders to be proccessed by the backend compilers
		 * 5. Get javascript to be compiled by jsc
		 * 6. Get flash to compile without alchemy
		 * 7. Get java to compile
		 */

		public override void Invoke()
		{
			this.staging = this.staging.Create(() => this.assembly.Directory.CreateSubdirectory("staging"));

			Console.WriteLine("RewriteToJavaScriptDocument: " + this.assembly.FullName);

			var assembly = Assembly.LoadFile(this.assembly.FullName);

			var targets = Enumerable.ToArray(
				from TargetType in assembly.GetTypes()

				let EntryPoint = TargetType.GetCustomAttributes<ScriptApplicationEntryPointAttribute>().SingleOrDefault()

				where EntryPoint != null

				// what about Forms/Avalon ?
				let IsActionScript = typeof(Sprite).IsAssignableFrom(TargetType)
				let IsJava = typeof(Applet).IsAssignableFrom(TargetType)
				let IsJavaScript = !IsActionScript && !IsJava

				// possible name clash?
				let StagingFolder = this.staging.CreateSubdirectory(TargetType.FullName)

				// we are guessing the product name ahead of time...

				let Product = IsJava ? Path.Combine(StagingFolder.FullName, @"web\bin\" + TargetType.Name + ".jar") :
							  IsActionScript ? Path.Combine(StagingFolder.FullName, @"web\" + TargetType.Name + ".swf") :
							  null

				// javascript objects will embedd upper level objects
				// javascript objects could be defined within one code file?
				orderby IsJavaScript


				select new { TargetType, EntryPoint, IsActionScript, IsJava, IsJavaScript, StagingFolder, Product }

			);

			foreach (var k in targets)
			{
				// lets do a rewrite and inject neccesary bootstrap and proxy code

				var __InternalElementProxy = typeof(__InternalElementProxy);
				var __InternalElementProxyToElement = __InternalElementProxy.GetImplicitOperators(null, typeof(IHTMLElement)).Single();



				var r = new RewriteToAssembly
				{
					assembly = this.assembly,
					staging = k.StagingFolder,

					PrimaryTypes = new[] { k.TargetType },

					product = k.TargetType.FullName,

					#region if we are going to inject code from jsc we need to copy it
					rename = new RewriteToAssembly.NamespaceRenameInstructions[] {
						"jsc.meta->" +  Path.GetFileNameWithoutExtension( this.assembly.Name),
						"jsc->" +  Path.GetFileNameWithoutExtension( this.assembly.Name),
					},

					merge = new RewriteToAssembly.MergeInstruction[] {
						"jsc.meta",
						"jsc"
					},
					#endregion



					PostTypeRewrite =
						a =>
						{
							// we need to inject bootstrap code
							if (a.Type == a.context.TypeCache[k.TargetType])
							{
								if (k.IsJavaScript)
								{
									// look, we are injecting IL code :)
									// to bad jsc backend had to do this the ugly way in the past...
									InjectJavaScriptBootstrap(a);
								}
							}
						},

					PreRewrite =
						a =>
						{
							if (k.IsJavaScript)
							{
								//var t = a.Module.DefineType("__InternalElementProxy", TypeAttributes.Public | TypeAttributes.Abstract);

								//__InternalElement = t.DefineField(
								//    "__InternalElement",
								//    typeof(IHTMLElement),
								//    FieldAttributes.Public | FieldAttributes.InitOnly
								//);

								//t.CreateType();

								//__InternalElementProxy = t;
							}


						},

					PostRewrite =
						a =>
						{
							a.Assembly.DefineAttribute<ObfuscationAttribute>(
							   new
							   {
								   Feature = "script",
							   }
							);

							if (k.IsJavaScript)
							{
								// javascript will embed objects
								// as it can create them

								foreach (var asset in from kk in targets where !kk.IsJavaScript select kk)
								{
									if (!File.Exists(asset.Product))
										throw new FileNotFoundException("", asset.Product);

									a.Module.DefineManifestResource(k.TargetType + ".web.assets." + k.TargetType.FullName + "." + Path.GetFileName(asset.Product),
										new MemoryStream(File.ReadAllBytes(asset.Product))
									, ResourceAttributes.Public);

								}
							}
						}
				};

				if (k.IsJavaScript)
				{
					r.ILOverride =
						x =>
						{
							x[OpCodes.Castclass] =
								e =>
								{
									// do we know something else to do here instead of default?
									if (e.i.TargetType == typeof(IHTMLElement))
									{

										if (typeof(Sprite).IsAssignableFrom(e.i.StackBeforeStrict[0].SingleStackInstruction.ReferencedType))
										{

											e.il.Emit(OpCodes.Call, r.RewriteArguments.context.MethodCache[__InternalElementProxyToElement]);
											return;
										}
									}

									e.Default();
								};
						};

					#region TypeCache
					r.ExternalContext.TypeCache.Resolve +=
						source =>
						{
							if (r.ExternalContext.TypeCache.BaseDictionary.ContainsKey(source))
								if (r.ExternalContext.TypeCache.BaseDictionary[source] != source)
									return;


							var c = targets.SingleOrDefault(kk => kk.TargetType == source);

							if (c != null)
								if (c.IsActionScript || c.IsJava)
								{
									// we have to generate a proxy!

									var t = r.RewriteArguments.Module.DefineType(source.FullName,
										source.Attributes,

										// hmm, no base for proxies!
										r.RewriteArguments.context.TypeCache[__InternalElementProxy],

										// no interfaces either at this time!
										null
									);

									r.ExternalContext.TypeCache[source] = t;


									// create DOM object
									// implicit operator?

									foreach (var kk in source.GetConstructors(
										BindingFlags.DeclaredOnly |
										BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
									{
										var km = r.ExternalContext.ConstructorCache[kk];
									}

									t.CreateType();


									return;
								}

							if (typeof(Sprite).IsAssignableFrom(source))
							{
								// erase
								r.ExternalContext.TypeCache[source] = r.RewriteArguments.context.TypeCache[__InternalElementProxy];
								return;
							}

							// keep it
						};
					#endregion

					#region ConstructorCache
					r.ExternalContext.ConstructorCache.Resolve +=
						source =>
						{
							if (r.ExternalContext.ConstructorCache.BaseDictionary.ContainsKey(source))
								if (r.ExternalContext.ConstructorCache.BaseDictionary[source] != source)
									return;


							var c = targets.SingleOrDefault(kk => kk.TargetType == source.DeclaringType);

							if (c != null)
								if (c.IsActionScript)
								{
									var DeclaringType = (TypeBuilder)r.ExternalContext.TypeCache[source.DeclaringType];


									var __InternalElement = r.RewriteArguments.context.TypeFieldCache[__InternalElementProxy].Single(kk => kk.Name == "__InternalElement");

									var ctor = DeclaringType.DefineConstructor(source.Attributes, source.CallingConvention, source.GetParameterTypes());

									var il = ctor.GetILGenerator();


									WriteInitialization_ActionScriptInternalElement(il, c.TargetType, k.TargetType, c.EntryPoint, __InternalElement);


									r.ExternalContext.ConstructorCache[source] = ctor;
								}

						};

					#endregion
				}

				r.Invoke();

				if (debug1)
					continue;

				if (k.IsJava)
				{
					r.Output.ToJava(this.javapath, null, null, k.TargetType.Name + ".jar", k.TargetType);
				}

				if (k.IsActionScript)
				{
					r.Output.ToActionScript(this.mxmlc, this.flashplayer, k.TargetType, null);
				}

				if (k.IsJavaScript)
				{
					r.Output.ToJavaScript();
				}
			}
		}

		private void WriteInitialization_ActionScriptInternalElement(ILGenerator il, Type proxy, Type context, ScriptApplicationEntryPointAttribute entry, FieldInfo __InternalElement)
		{
			Action Implementation1 =
				delegate
				{
					var o = new IHTMLEmbed();

					o.src = @"assets/Ultra1.UltraApplication/UltraSprite.swf";
					o.style.SetSize(1, 2);
				};

			var il_a = new ILTranslationExtensions.EmitToArguments();

			//il_a.TranslateTargetType = t => t == typeof(Implementation1) ? a.Type : t;
			//il_a.TranslateTargetMethod = m => m == Implementation4.Method ? __cctor_1 : m;

			il_a[OpCodes.Ldc_I4_1] =
				e =>
				{
					il.Emit(OpCodes.Ldc_I4, entry.Width);
				};

			il_a[OpCodes.Ldc_I4_2] =
				e =>
				{
					il.Emit(OpCodes.Ldc_I4, entry.Height);
				};

			il_a[OpCodes.Ldstr] =
				e =>
				{
					il.Emit(OpCodes.Ldstr, "assets/" + context.FullName + "/" + proxy.Name + ".swf");
				};
			il_a[OpCodes.Ret] =
				e =>
				{
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldloc_0);
					il.Emit(OpCodes.Stfld, __InternalElement);
					il.Emit(OpCodes.Ret);
				};

			Implementation1.Method.EmitTo(il, il_a);

		}




	}
}

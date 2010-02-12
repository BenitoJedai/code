using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using java.applet;
using jsc.Languages.IL;
using jsc.meta.Library;
using jsc.meta.Library.Templates;
using jsc.meta.Tools;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

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
		 * 8. Emit flash and java object proxies and allow them to be cast to IHTMLElement
		 */

		public override void Invoke()
		{
			this.staging = this.staging.Create(() => this.assembly.Directory.CreateSubdirectory("staging"));

			jsc.meta.Loader.LoaderStrategy.Hints.Add(this.assembly.Directory);

			Console.WriteLine("RewriteToJavaScriptDocument: " + this.assembly.FullName);

			var assembly = Assembly.LoadFile(this.assembly.FullName);

			var targets = Enumerable.ToArray(

				from TargetType in assembly.GetTypes()
				where !TargetType.IsAbstract
				let EntryPoint = InferScriptApplicationEntryPoint(TargetType)
				where EntryPoint != null

				// what about Forms/Avalon ?
				let IsActionScript = typeof(Sprite).IsAssignableFrom(TargetType)
				let IsJava = typeof(Applet).IsAssignableFrom(TargetType)
				let IsJavaScript = !IsActionScript && !IsJava

				// possible name clash?
				let StagingFolder = this.staging.CreateSubdirectory(TargetType.FullName)

				// we are guessing the product name ahead of time...

				let Product = IsJava ? Path.Combine(StagingFolder.FullName, @"web\bin\" + TargetType.FullName + ".jar") :
							  IsActionScript ? Path.Combine(StagingFolder.FullName, @"web\" + TargetType.FullName + ".swf") :
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

				// in flash we need to export our functions...
				var __ExternalCallback = new List<MethodBuilderInfo>();

				var r = default(RewriteToAssembly);

				r = new RewriteToAssembly
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
					/*
					merge = new RewriteToAssembly.MergeInstruction[] {
						"jsc.meta",
						"jsc"
					},
					 * */
					#endregion


					#region PreTypeRewrite
					PreTypeRewrite =
						a =>
						{
							if (a.Type == a.context.TypeCache[k.TargetType])
							{
								// so where are we?

								if (k.IsActionScript || k.IsJava)
								{

									var p = new ExternalInterfaceProvider
									{
										SourceType = k.TargetType,
										DeclaringType = a.Type,
										a = a,
										Rewrite = r,
									};

									if (k.IsActionScript)
									{
										p.ExternalCall = InternalActionScriptToJavaScriptBridge.ExternalInterface_Invoke;
										p.ExternalCallback = __ExternalCallback.Add;
									}

									if (k.IsJava)
									{
										p.ExternalCall = InternalJavaToJavaScriptBridge.ExternalInterface_Invoke;
									}

									p.Implement();
								}
							}
						},
					#endregion

					#region PostTypeRewrite
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



								#region we need to inject entrypoint attributes
								if (null == k.TargetType.GetCustomAttributes<ScriptApplicationEntryPointAttribute>().SingleOrDefault())
								{
									// we have to manually add it now...
									var s = InferScriptApplicationEntryPoint(k.TargetType);

									a.Type.DefineAttribute(
										s,
										typeof(ScriptApplicationEntryPointAttribute)
									);

									if (k.IsActionScript)
									{
										var swf = k.TargetType.GetCustomAttributes<SWFAttribute>().SingleOrDefault();

										if (swf == null)
										{
											a.Type.DefineAttribute(
												new SWFAttribute { width = s.Width, height = s.Height },
												typeof(SWFAttribute)
											);

										}
									}
								}
								#endregion


							}
						},
					#endregion

					PreRewrite =
						a =>
						{

						},

					#region PostRewrite
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

								// the file is not expeced to be there..
								if (IsRewriteOnly)
									return;

								foreach (var asset in from kk in targets where !kk.IsJavaScript select kk)
								{
									if (!File.Exists(asset.Product))
									{
										throw new FileNotFoundException("", asset.Product);
									}

									a.Module.DefineManifestResource(k.TargetType + ".web.assets." + k.TargetType.FullName + "." + Path.GetFileName(asset.Product),
										new MemoryStream(File.ReadAllBytes(asset.Product))
									, ResourceAttributes.Public);

								}
							}

						}
					#endregion
				};

				if (k.IsJava)
				{
					#region TypeCache
					r.ExternalContext.TypeCache.Resolve +=
						source =>
						{
							var c = targets.SingleOrDefault(kk => kk.TargetType == source);

							if (c != null)
								if (c.IsJavaScript)
								{
									// so... flash could reference javascript element? :)
									// basically flash could subscribe to events in
									// javascript!

									var t = r.RewriteArguments.Module.DefineType(source.FullName,
										source.Attributes,

										// hmm, no base for proxies!
										null,

										// no interfaces either at this time!
										null
									);

									r.ExternalContext.TypeCache[source] = t;
									t.CreateType();
								}
						};

					#endregion
				}

				#region IsActionScript IL patching
				if (k.IsActionScript)
				{
					#region ILOverride (Ret)
					r.ILOverride =
						(context, x) =>
						{
							if (context is ConstructorInfo && context.DeclaringType == k.TargetType)
							{
								// we need to inject code right after base ctor call

								x[OpCodes.Call] =
									e =>
									{

										e.Default();

										// will it mess up the offcet patching later on if
										// the IL has branches ? and try/catch clauses?

										if (e.i.TargetConstructor != null && e.i.TargetConstructor.DeclaringType == k.TargetType.BaseType)
										{
											var DeclaringType = (TypeBuilder)r.RewriteArguments.context.TypeCache[k.TargetType];

											WriteInitialization_ActionScriptExternalInterface(r, e, DeclaringType, k.TargetType,
												__ExternalCallback
											);


										}


									};
							}
						};
					#endregion

					#region TypeCache
					r.ExternalContext.TypeCache.Resolve +=
						source =>
						{
							var c = targets.SingleOrDefault(kk => kk.TargetType == source);

							if (c != null)
								if (c.IsJavaScript)
								{
									// so... flash could reference javascript element? :)
									// basically flash could subscribe to events in
									// javascript!

									// atleast we need to define a nested type declaring type...

									var t = r.RewriteArguments.Module.DefineType(source.FullName,
										source.Attributes,

										// hmm, no base for proxies!
										null,

										// no interfaces either at this time!
										null
									);

									r.ExternalContext.TypeCache[source] = t;
									t.CreateType();
								}
						};

					#endregion
				}
				#endregion

				#region IsJavaScript IL patching
				if (k.IsJavaScript)
				{
					// In javascript we will define a type with
					// InternalConstructors which will act as a native interface
					var IHTMLElementCoTypes = new Dictionary<TypeBuilder, TypeBuilder>();

					var ExternalInterfaceConsumerCache = new Dictionary<TypeBuilder, ExternalInterfaceConsumer>();

					#region ILOverride (Castclass)
					r.ILOverride =
						(context, x) =>
						{
							x[OpCodes.Castclass] =
								e =>
								{
									// do we know something else to do here instead of default?
									if (typeof(IHTMLElement).IsAssignableFrom(e.i.TargetType))
									{
										var ReferencedType = e.i.StackBeforeStrict[0].SingleStackInstruction.ReferencedType;

										if (typeof(Sprite).IsAssignableFrom(ReferencedType) ||
											typeof(Applet).IsAssignableFrom(ReferencedType))
										{

											e.il.Emit(OpCodes.Call, r.RewriteArguments.context.MethodCache[__InternalElementProxyToElement]);
											return;
										}
									}

									e.Default();
								};
						};
					#endregion



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

									var Interfaces = Enumerable.ToArray(

										from y in source.GetInterfaces()

										let ym = source.GetInterfaceMap(y)

										// fixme: we should actually look
										// where are the interfaces defined
										// if they are within actionscript/java namespaces then exclude 

										// is any of the method implemented in this concrete type?
										where ym.TargetMethods.Any(yy => yy.DeclaringType == source)

										select r.RewriteArguments.context.TypeCache[y]
									);

									var DeclaringType = source.IsNested ?
										((TypeBuilder)r.RewriteArguments.context.TypeCache[source.DeclaringType]).DefineNestedType(
											source.FullName,
											source.Attributes,
											r.RewriteArguments.context.TypeCache[__InternalElementProxy],
											Interfaces
										)


										: r.RewriteArguments.Module.DefineType(
											source.FullName,
											source.Attributes,
											r.RewriteArguments.context.TypeCache[__InternalElementProxy],
											Interfaces
									);

									r.ExternalContext.TypeCache[source] = DeclaringType;

									var __InternalElement = r.RewriteArguments.context.FieldCache[__InternalElementProxy.GetField("__InternalElement")];


									#region CoType1
									if (c.IsJava)
									{
										// in flash we will need to use CallFunction!

										var CoType1 = DeclaringType.DefineNestedType("IHTML" + source.Name,
											   TypeAttributes.Sealed | TypeAttributes.NestedAssembly,

											   // hmm, no base for proxies!
											   typeof(IHTMLElement),

											   // no interfaces either at this time!
											   null
									   );

										CoType1.DefineAttribute(
											new ScriptAttribute
											{
												InternalConstructor = true
											},
											typeof(ScriptAttribute)
										);

										IHTMLElementCoTypes[DeclaringType] = CoType1;
									}
									#endregion


									var Consumer = new ExternalInterfaceConsumer
									{
										DeclaringType = DeclaringType,
										Rewrite = r,
										RewriteArguments = r.RewriteArguments,
										SourceType = source,

										#region DefineMethod
										DefineMethod =
											e =>
											{
												// we should do the object to string mapping here actually!


												var m = e.Method;

												var il = m.GetILGenerator();
												var source_Attributes = MethodAttributes.Public | MethodAttributes.NewSlot | MethodAttributes.Final;

												if (c.IsJava)
												{
													var DeclaringTypeCoType = IHTMLElementCoTypes.First(kk => kk.Key == DeclaringType).Value;




													#region DeclaringTypeCoTypeMethod
													var DeclaringTypeCoTypeMethod = DeclaringTypeCoType.DefineMethod(

														// in java land we have to define a new method to translate
														// from string to event
														e.RemoteName,

														source_Attributes,
														CallingConventions.Standard,
														e.ReturnType,
														e.ParameterTypes
													);


													{
														var co_il = DeclaringTypeCoTypeMethod.GetILGenerator();

														// fixme: some methods like add_event1(Action) need rewireing!

														co_il.EmitCode(() => { throw new NotSupportedException(); });
													}
													#endregion


													il.Emit(OpCodes.Ldarg_0);
													il.Emit(OpCodes.Ldfld, e.DeclaringTypeContext);
													il.Emit(OpCodes.Ldfld, __InternalElement);
													il.Emit(OpCodes.Castclass, DeclaringTypeCoType);
													for (short i = 0; i < e.ParameterTypes.Length; i++)
													{
														il.Emit(OpCodes.Ldarg, (short)(i + 1));
													}

													il.Emit(OpCodes.Call, DeclaringTypeCoTypeMethod);
												}
												else
												{
													var __args = il.EmitStringArgumentsAsArray(true, e.ParameterTypes);

													il.Emit(OpCodes.Ldarg_0);
													il.Emit(OpCodes.Ldfld, e.DeclaringTypeContext);
													il.Emit(OpCodes.Ldfld, __InternalElement);
													il.Emit(OpCodes.Ldstr, e.RemoteName);

													// <>.FromType ?
													il.Emit(OpCodes.Ldloc, (short)__args.LocalIndex);

													Func<IHTMLEmbedFlash, string, string[], string>
														CallFunction = IHTMLEmbedFlashExtensions.CallFunction;

													il.Emit(OpCodes.Call, CallFunction.Method);

													if (e.ReturnType == typeof(void))
														il.Emit(OpCodes.Pop);


												}



												il.Emit(OpCodes.Ret);

												return m;
											}

										#endregion

,

									};

									Consumer.Implement();

									ExternalInterfaceConsumerCache[DeclaringType] = Consumer;

									// create DOM object
									// implicit operator?



									var __InternalElementProxy__ = r.RewriteArguments.context.TypeCache[__InternalElementProxy];

									// triggering members to be copied...

									#region copy constructors
									foreach (var kk in source.GetConstructors(
										BindingFlags.DeclaredOnly |
										BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
									{
										var km = r.ExternalContext.ConstructorCache[kk];
									}
									#endregion

									#region copy methods
									foreach (var kk in Consumer.SourceTypeMethods)
									{
										var km = r.ExternalContext.MethodCache[kk];
									}
									#endregion

									if (c.IsJava)
									{
										IHTMLElementCoTypes[DeclaringType].CreateType();
									}


									if (source.IsNested)
									{
										r.TypeCreated +=
											e =>
											{

												if (e.SourceType == source.DeclaringType)
												{
													DeclaringType.CreateType();

													Consumer.NestedTypesCreated();
												}
											};
									}
									else
									{
										DeclaringType.CreateType();
										Consumer.NestedTypesCreated();
									}


									return;
								}

							if (typeof(Sprite).IsAssignableFrom(source) ||
								typeof(Applet).IsAssignableFrom(source))
							{
								// erase
								r.ExternalContext.TypeCache[source] = r.RewriteArguments.context.TypeCache[__InternalElementProxy];
								return;
							}

							// keep it
						};
					#endregion

					#region MethodCache
					r.ExternalContext.MethodCache.Resolve +=
						source =>
						{
							if (r.ExternalContext.MethodCache.BaseDictionary.ContainsKey(source))
								if (r.ExternalContext.MethodCache.BaseDictionary[source] != source)
									return;

							var c = targets.SingleOrDefault(kk => kk.TargetType == source.DeclaringType);

							if (c != null)
								if (c.IsActionScript || c.IsJava)
								{
									var __InternalElement = r.RewriteArguments.context.FieldCache[__InternalElementProxy.GetField("__InternalElement")];

									var DeclaringType = (TypeBuilder)r.ExternalContext.TypeCache[source.DeclaringType];

									var source_Attributes = source.Attributes | MethodAttributes.NewSlot | MethodAttributes.Final;


									var DeclaringTypeMethod = DeclaringType.DefineMethod(
										source.Name,
										source_Attributes,
										source.CallingConvention,

										 r.RewriteArguments.context.TypeCache[source.ReturnType],

										Enumerable.ToArray(
											from p in source.GetParameterTypes()
											select r.RewriteArguments.context.TypeCache[p]
										)

									);

									//Console.WriteLine("from js: " + source.Name);

									source.GetParameters().CopyTo(DeclaringTypeMethod);


									var Consumer = ExternalInterfaceConsumerCache[DeclaringType];

									Consumer.ImplementTranslationMethod(source, DeclaringTypeMethod.GetILGenerator(), Consumer.OutgoingInterfaceField, null, null);



									r.ExternalContext.MethodCache[source] = DeclaringTypeMethod;
								}
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
								if (c.IsActionScript || c.IsJava)
								{
									#region WriteInitialization_*InternalElement
									var DeclaringType = (TypeBuilder)r.ExternalContext.TypeCache[source.DeclaringType];

									// we need an instance :)

									var __InternalElement = r.RewriteArguments.context.FieldCache[__InternalElementProxy.GetField("__InternalElement")];

									var ctor = DeclaringType.DefineConstructor(source.Attributes, source.CallingConvention, source.GetParameterTypes());

									var il = ctor.GetILGenerator();

									il.Emit(OpCodes.Ldarg_0);
									il.Emit(OpCodes.Call,
										r.RewriteArguments.context.ConstructorCache[__InternalElementProxy.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).Single()]
									);

									// we also need to expose our incoming methods...

									if (c.IsActionScript)
									{
										WriteInitialization_ActionScriptInternalElement(
											il,
											c.TargetType,
											k.TargetType,
											c.EntryPoint,
											__InternalElement,
											r.RewriteArguments.context.MethodCache,
											ExternalInterfaceConsumerCache[DeclaringType]
										);

									}

									if (c.IsJava)
									{
										WriteInitialization_JavaInternalElement(
											il,
											c.TargetType,
											k.TargetType,
											c.EntryPoint,
											__InternalElement,
											ExternalInterfaceConsumerCache[DeclaringType]
										);
									}

									r.ExternalContext.ConstructorCache[source] = ctor;
									#endregion
								}

						};

					#endregion
				}
				#endregion

				r.Invoke();

				if (IsRewriteOnly)
					continue;

				#region jsc backend
				if (k.IsJava)
				{
					r.Output.ToJava(this.javapath, null, null, k.TargetType.FullName + ".jar", k.TargetType);
				}

				if (k.IsActionScript)
				{
					r.Output.ToActionScript(this.mxmlc, this.flashplayer, k.TargetType, null,
						k.TargetType.FullName + ".swf"
					);
				}

				if (k.IsJavaScript)
				{
					r.Output.ToJavaScript();
				}
				#endregion

			}
		}



		private static bool SignatureTypesSupportedForProxy(MethodInfo kk)
		{
			return kk.GetSignatureTypes().All(
														kkk => kkk == typeof(void) || kkk == typeof(string) || typeof(Delegate).IsAssignableFrom(kkk)
													);
		}

		private static ScriptApplicationEntryPointAttribute InferScriptApplicationEntryPoint(Type TargetType)
		{
			var t = TargetType.GetCustomAttributes<ScriptApplicationEntryPointAttribute>().SingleOrDefault();

			if (t != null)
				return t;

			if (TargetType.IsSealed || TargetType.IsPublic)
			{
				// we should infer only from sealed types...

				if (TargetType.GetConstructor(typeof(IHTMLElement)) != null && !TargetType.IsNested)
				{
					return new ScriptApplicationEntryPointAttribute();
				}

				if (TargetType.GetConstructor() != null && typeof(Applet).IsAssignableFrom(TargetType.BaseType))
				{
					var s = new ScriptApplicationEntryPointAttribute();

					s.Width = TargetType.GetLiteralInt32("DefaultWidth", s.Width);
					s.Height = TargetType.GetLiteralInt32("DefaultHeight", s.Height);

					return s;
				}

				if (TargetType.GetConstructor() != null && typeof(Sprite).IsAssignableFrom(TargetType.BaseType))
				{
					var s = new ScriptApplicationEntryPointAttribute { WithResources = true };

					s.Width = TargetType.GetLiteralInt32("DefaultWidth", s.Width);
					s.Height = TargetType.GetLiteralInt32("DefaultHeight", s.Height);



					return s;
				}
			}

			return null;
		}







	}
}

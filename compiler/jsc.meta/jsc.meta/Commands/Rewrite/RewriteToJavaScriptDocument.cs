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
using jsc.meta.Commands.Rewrite.Templates;
using jsc.meta.Library.Templates.Java;
using System.Xml.Linq;

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

				// we want sealed types
				where !TargetType.IsAbstract && TargetType.IsSealed

				// how do we detect ad hoc web services? a suffix will do for now...
				// if it defines some cool fields we will need to populate them later
				let IsWebService = TargetType.Name.EndsWith("WebService")
				let IsWebServicePHP = false
				let IsWebServiceJava = false

				let EntryPoint = InferScriptApplicationEntryPoint(TargetType)
				where IsWebService || EntryPoint != null

				// what about Forms/Avalon ?
				let IsActionScript = typeof(Sprite).IsAssignableFrom(TargetType)
				let IsJava = typeof(Applet).IsAssignableFrom(TargetType)
				let IsJavaScript = TargetType.GetConstructor(typeof(IHTMLElement)) != null



				let StagingFolder = this.staging.CreateSubdirectory(TargetType.FullName)


				// we are guessing the product name ahead of time...

				let Product = IsJava ? Path.Combine(StagingFolder.FullName, @"web\bin\" + TargetType.FullName + ".jar") :
							  IsActionScript ? Path.Combine(StagingFolder.FullName, @"web\" + TargetType.FullName + ".swf") :
							  null

				// javascript objects will embedd upper level objects
				// javascript objects could be defined within one code file?
				orderby IsJavaScript || IsWebService, IsWebService


				select new
				{
					TargetType,
					EntryPoint,
					IsActionScript,
					IsJava,
					IsJavaScript,
					IsWebService,
					IsWebServicePHP,
					IsWebServiceJava,
					StagingFolder,
					Product
				}

			);

			#region manage WebService
			var targets_variations = targets.SelectMany(
				k => k.IsWebService ?
					new[]
					{
						new {
							k.TargetType,
							k.EntryPoint,
							k.IsActionScript,
							k.IsJava,
							k.IsJavaScript,
							k.IsWebService,
							k.IsWebServicePHP,
							k.IsWebServiceJava,
							StagingFolder  = k.StagingFolder.CreateSubdirectory("staging.net/bin"),
							k.Product
						},
						this.DisableWebServicePHP ? null :

						new {
							k.TargetType,
							k.EntryPoint,
							k.IsActionScript,
							k.IsJava,
							k.IsJavaScript,
							k.IsWebService,
							IsWebServicePHP = true,
							k.IsWebServiceJava,
							StagingFolder  = k.StagingFolder.CreateSubdirectory("staging.php"),
							k.Product
						},

						this.DisableWebServiceJava ? null :
						new {
							k.TargetType,
							k.EntryPoint,
							k.IsActionScript,
							k.IsJava,
							k.IsJavaScript,
							k.IsWebService,
							k.IsWebServicePHP,
							IsWebServiceJava = true,
							StagingFolder  = k.StagingFolder.CreateSubdirectory("staging.java"),
							k.Product
						},
					}
				:
					new[] { k }
			).Where(k => k != null).ToArray();
			#endregion

			var RewriteOutput = targets.ToDictionary(k => k, k => default(FileInfo));

			var ki = -1;
			foreach (var k in targets_variations)
			{
				ki++;

				Action<string> RaiseProccessStatusChanged =
					e =>
					{
						this.RaiseProccessStatusChanged("Component '" + k.TargetType.FullName + "' (" + (ki + 1) + " of " + targets_variations.Length + "): " + e);
					};

				RaiseProccessStatusChanged("rewriting");

				var InvokeAfterBackendCompiler = new List<Action>();

				// lets do a rewrite and inject neccesary bootstrap and proxy code

				var __InternalElementProxy = typeof(__InternalElementProxy);
				var __InternalElementProxyToElement = __InternalElementProxy.GetImplicitOperators(null, typeof(IHTMLElement)).Single();

				// in flash we need to export our functions...
				var __ExternalCallback = new List<MethodBuilderInfo>();
				var __WebServiceForJavaScript = default(WebServiceForJavaScript);

				var r = default(RewriteToAssembly);

				var PrimaryTypes = new[] { k.TargetType };


				if (k.IsWebServiceJava)
				{
					k.StagingFolder.DefinesTypes(
						typeof(ScriptCoreLib.ScriptAttribute),
						typeof(ScriptCoreLibJava.IAssemblyReferenceToken),
						typeof(ScriptCoreLibJava.Web.IAssemblyReferenceToken),
						typeof(ScriptCoreLibJava.Web.Services.IAssemblyReferenceToken)
					);

				}


				r = new RewriteToAssembly
				{
					assembly = this.assembly,
					staging = k.StagingFolder,

					PrimaryTypes = PrimaryTypes,

					product = k.TargetType.FullName,


					rename = new RewriteToAssembly.NamespaceRenameInstructions[] {
						"jsc.meta->" +  Path.GetFileNameWithoutExtension( this.assembly.Name),
						"jsc->" +  Path.GetFileNameWithoutExtension( this.assembly.Name),
					},



					#region PreTypeRewrite
					PreTypeRewrite =
						a =>
						{
							RaiseProccessStatusChanged("rewriting " + a.Type.FullName);

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
								if (k.IsWebService)
								{
									// no entrypoints for me?
								}
								else
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
							if (k.IsJavaScript || k.IsActionScript || k.IsJava)
							{
								a.Assembly.DefineAttribute<ObfuscationAttribute>(
								   new
								   {
									   Feature = "script",
								   }
								);
							}

							if (k.IsJavaScript)
							{
								// javascript will embed objects
								// as it can create them

								// the file is not expeced to be there..
								if (IsRewriteOnly)
									return;

								foreach (var asset in from kk in targets where kk.IsActionScript || kk.IsJava select kk)
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

							if (k.IsWebService)
							{
								var __js = targets.Single(kk => kk.IsJavaScript);

								WriteGlobalApplication(r, a, k.TargetType,
									k.StagingFolder,
									__js.StagingFolder,
									__js.TargetType,
									RewriteOutput[__js],
									k.IsWebServicePHP,
									k.IsWebServiceJava,
									InvokeAfterBackendCompiler.Add
								);

								if (k.IsWebServiceJava)
								{
									#region yay attributes
									var ScriptAttribute = typeof(ScriptCoreLib.ScriptAttribute);

									var ScriptTypeFilterAttribute = default(Func<ScriptType, ScriptTypeFilterAttribute>).ToConstructorInfo();


									var AssemblyScriptAttribute = new ScriptAttribute
									{
										IsScriptLibrary = true,
										ScriptLibraries = new[] {
											typeof(ScriptCoreLibJava.IAssemblyReferenceToken),
											typeof(ScriptCoreLibJava.Web.IAssemblyReferenceToken),
											typeof(ScriptCoreLibJava.Web.Services.IAssemblyReferenceToken),
										},

										// at this point we should not have NonScriptTypes anymore...

										//NonScriptTypes = assembly.GetTypes().Where(
										//    k =>
										//        k.Namespace.EndsWith(".My") ||
										//        k.Namespace.EndsWith(".My.Resources")
										//).ToArray()
									};

									a.Assembly.DefineScriptAttribute(
										new
										{
											AssemblyScriptAttribute.IsScriptLibrary,
											AssemblyScriptAttribute.ScriptLibraries,
											AssemblyScriptAttribute.NonScriptTypes
										}
									);

									a.Assembly.SetCustomAttribute(
										new CustomAttributeBuilder(
											ScriptTypeFilterAttribute, new object[] { ScriptType.Java }
										)
									);
									#endregion

									var xmlns = new
									{
										appengine = (XNamespace)"http://appengine.google.com/ns/1.0",
										javaee = (XNamespace)"http://java.sun.com/xml/ns/javaee"
									};

									var Handler = a.context.TypeCache[typeof(InternalHttpServlet)];

									var _application = "jsc-project";
									var _version = "5";

									var res = new ScriptResourceWriter(a.Assembly, a.Module)
									{
										#region appengine-web.xml
										{
											"java/WEB-INF/appengine-web.xml",

											new XElement(xmlns.appengine + "appengine-web-app",
												new XElement(xmlns.appengine + "application", _application),
												new XElement(xmlns.appengine + "version", _version)
											)
										},
										#endregion
										#region web.xml
										{
											"java/WEB-INF/web.xml",

											new XElement(xmlns.javaee + "web-app",
												new [] {
													new XElement(xmlns.javaee + "display-name", _application),

													new XElement(xmlns.javaee + "servlet", 
														new XElement(xmlns.javaee + "servlet-name", Handler.Name),
														new XElement(xmlns.javaee + "servlet-class", Handler.FullName)
													),

													new XElement(xmlns.javaee + "servlet-mapping", 
														new XElement(xmlns.javaee + "servlet-name", Handler.Name),
														
														// http://www.coderanch.com/t/414165/Servlets/java/url-pattern-web-xml

														new XElement(xmlns.javaee + "url-pattern", "/*")
													)
												}
											)

										}
										#endregion

									};
								}
							}

						}
					#endregion
				};

				if (k.IsJava || k.IsActionScript || k.IsWebService)
				{
					#region TypeCache
					r.ExternalContext.TypeCache.Resolve +=
						SourceType =>
						{
							var c = targets.SingleOrDefault(kk => kk.TargetType == SourceType);

							if (c != null)
								if (c.IsJavaScript)
								{
									// so... flash could reference javascript element? :)
									// basically flash could subscribe to events in
									// javascript!

									var t = r.RewriteArguments.Module.DefineType(SourceType.FullName,
										SourceType.Attributes,

										// hmm, no base for proxies!
										null,

										// no interfaces either at this time!
										null
									);

									r.ExternalContext.TypeCache[SourceType] = t;
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
						SourceType =>
						{
							if (r.ExternalContext.TypeCache.BaseDictionary.ContainsKey(SourceType))
								if (r.ExternalContext.TypeCache.BaseDictionary[SourceType] != SourceType)
									return;

							// webservice will have .net, php and java outputs

							var c = targets.SingleOrDefault(kk => kk.TargetType == SourceType);

							if (c != null)
								if (c.IsWebService)
								{
									__WebServiceForJavaScript = new WebServiceForJavaScript
									{
										r = r,
										SourceType = SourceType
									};

									__WebServiceForJavaScript.WriteType();
								}
								else if (c.IsActionScript || c.IsJava)
								{
									// we have to generate a proxy!

									var Interfaces = Enumerable.ToArray(

										from y in SourceType.GetInterfaces()

										let ym = SourceType.GetInterfaceMap(y)

										// fixme: we should actually look
										// where are the interfaces defined
										// if they are within actionscript/java namespaces then exclude 

										// is any of the method implemented in this concrete type?
										where ym.TargetMethods.Any(yy => yy.DeclaringType == SourceType)

										select r.RewriteArguments.context.TypeCache[y]
									);

									var DeclaringType = SourceType.IsNested ?
										((TypeBuilder)r.RewriteArguments.context.TypeCache[SourceType.DeclaringType]).DefineNestedType(
											SourceType.Name,
											SourceType.Attributes,
											r.RewriteArguments.context.TypeCache[__InternalElementProxy],
											Interfaces
										)


										: r.RewriteArguments.Module.DefineType(
											SourceType.FullName,
											SourceType.Attributes,
											r.RewriteArguments.context.TypeCache[__InternalElementProxy],
											Interfaces
									);

									r.ExternalContext.TypeCache[SourceType] = DeclaringType;

									var __InternalElement = r.RewriteArguments.context.FieldCache[__InternalElementProxy.GetField("__InternalElement")];


									#region CoType1
									if (c.IsJava)
									{
										// in flash we will need to use CallFunction!

										var CoType1 = DeclaringType.DefineNestedType("IHTML" + SourceType.Name,
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
										SourceType = SourceType,

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
									foreach (var kk in SourceType.GetConstructors(
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


									if (SourceType.IsNested)
									{
										r.TypeCreated +=
											e =>
											{

												if (e.SourceType == SourceType.DeclaringType)
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

							if (typeof(Sprite).IsAssignableFrom(SourceType) ||
								typeof(Applet).IsAssignableFrom(SourceType))
							{
								// erase
								r.ExternalContext.TypeCache[SourceType] = r.RewriteArguments.context.TypeCache[__InternalElementProxy];
								return;
							}

							// keep it
						};
					#endregion

					#region MethodCache
					r.ExternalContext.MethodCache.Resolve +=
						SourceMethod =>
						{
							if (r.ExternalContext.MethodCache.BaseDictionary.ContainsKey(SourceMethod))
								if (r.ExternalContext.MethodCache.BaseDictionary[SourceMethod] != SourceMethod)
									return;

							var c = targets.SingleOrDefault(kk => kk.TargetType == SourceMethod.DeclaringType);


							if (c != null)
								if (c.IsWebService)
								{
									__WebServiceForJavaScript.WriteMethod(SourceMethod);
								}
								else if (c.IsActionScript || c.IsJava)
								{
									var __InternalElement = r.RewriteArguments.context.FieldCache[__InternalElementProxy.GetField("__InternalElement")];


									var source_Attributes = SourceMethod.Attributes | MethodAttributes.NewSlot | MethodAttributes.Final;

									var DeclaringType = (TypeBuilder)r.ExternalContext.TypeCache[SourceMethod.DeclaringType];

									var DeclaringTypeMethod = DeclaringType.DefineMethod(
										SourceMethod.Name,
										source_Attributes,
										SourceMethod.CallingConvention,

										 r.RewriteArguments.context.TypeCache[SourceMethod.ReturnType],

										Enumerable.ToArray(
											from p in SourceMethod.GetParameterTypes()
											select r.RewriteArguments.context.TypeCache[p]
										)

									);

									//Console.WriteLine("from js: " + source.Name);

									SourceMethod.GetParameters().CopyTo(DeclaringTypeMethod);


									var Consumer = ExternalInterfaceConsumerCache[DeclaringType];

									Consumer.ImplementTranslationMethod(SourceMethod, DeclaringTypeMethod.GetILGenerator(), Consumer.OutgoingInterfaceField, null, null);



									r.ExternalContext.MethodCache[SourceMethod] = DeclaringTypeMethod;
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

							if (!source.IsStatic)
							{
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
							}

						};

					#endregion
				}
				#endregion

				r.Invoke();

				RewriteOutput[k] = r.Output;

				if (!IsRewriteOnly)
				{
					RaiseProccessStatusChanged("backend compilation for " + k.TargetType.FullName);

					#region jsc backend
					if (k.IsJava)
					{
						r.Output.ToJava(this.javapath, null, null, k.TargetType.FullName + ".jar", k.TargetType);
					}

					if (k.IsWebServiceJava)
					{
						jsc.Program.TypedMain(
							new jsc.CompileSessionInfo
							{
								Options = new jsc.CommandLineOptions
								{
									TargetAssembly = r.Output,
									IsJava = true,
									IsNoLogo = true
								}
							}
						);
					}

					if (k.IsActionScript)
					{
						r.Output.ToActionScript(this.mxmlc, this.flashplayer, k.TargetType, null,
							k.TargetType.FullName + ".swf",
							RaiseProccessStatusChanged
						);
					}

					if (k.IsJavaScript)
					{
						r.Output.ToJavaScript(RaiseProccessStatusChanged);
					}
					#endregion

					foreach (var item in InvokeAfterBackendCompiler)
					{
						item();
					}
					InvokeAfterBackendCompiler.Clear();
				}

				if (k.IsWebService && !k.IsWebServiceJava && !k.IsWebServicePHP)
				{
					if (this.AtWebServiceReady != null)
						this.AtWebServiceReady(
							new AtWebServiceReadyArguments
							{
								Assembly = r.Output,
								GlobalType = k.TargetType.Namespace + ".Global"
							}
						);
				}
			}

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

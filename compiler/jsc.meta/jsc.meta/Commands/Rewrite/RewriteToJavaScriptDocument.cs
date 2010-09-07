using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;
using java.applet;
using jsc.Languages;
using jsc.Languages.IL;
using jsc.meta.Commands.Rewrite.Templates;
using jsc.meta.Library;
using jsc.meta.Library.Templates;
using jsc.meta.Library.Templates.Java;
using jsc.meta.Library.VolumeFunctions;
using jsc.meta.Tools;
using jsc.Script;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Remoting;
using ScriptCoreLib.Ultra.Library;
using jsc.Library;

namespace jsc.meta.Commands.Rewrite
{
    [Description("This command will tare an assembly to compile java and flash objects separatly.")]
    public partial class RewriteToJavaScriptDocument : RewriteToUltraApplication.RewriteToUltraApplication
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
            if (this.AttachDebugger)
                Debugger.Launch();


            var staging = this.staging.Create(() => this.assembly.Directory.CreateSubdirectory("staging"));

            using (var v = staging.ToVirtualDrive())
            {
                this.staging = v.VirtualDirectory;

                InternalInvoke(v);
            }
        }

        void InternalInvoke(VolumeFunctionsExtensions.ToVirtualDriveToDirectory v)
        {
            //if (this.AttachDebugger)
            //    Debugger.Launch();



            this.staging = this.staging.Create(() => this.assembly.Directory.CreateSubdirectory("staging"));

            jsc.meta.Loader.LoaderStrategyImplementation.Hints.Add(this.assembly.Directory);

            Console.WriteLine("RewriteToUltraApplication: " + this.assembly.FullName);

            // This assembly is built by a runtime newer than the currently loaded runtime and cannot be loaded. (Exception from HRESULT: 0x8013101B)
            var assembly = Assembly.LoadFile(this.assembly.FullName);

            var targets = Enumerable.ToArray(

                from TargetType in assembly.GetTypes()

                // we want sealed types
                where !TargetType.IsAbstract && TargetType.IsSealed

                let SafeName = CompilerBase.GetSafeLiteral(TargetType.Name, null)

                where SafeName == TargetType.Name

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


                let JavaScriptConstructorArgument = (
                    from ctor in TargetType.GetConstructors()
                    let ctor_p = ctor.GetParameters()
                    where ctor_p.Length == 1
                    let ctor_a = ctor_p.Single().ParameterType

                    // note that the IUltraComponent style is now the preferred way
                    where ctor_a == typeof(IHTMLElement) || typeof(IUltraComponent).IsAssignableFrom(ctor_a)

                    select ctor_a).FirstOrDefault()

                let IsJavaScript = JavaScriptConstructorArgument != null



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
                    Product,

                    JavaScriptConstructorArgument

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
							StagingFolder = 
                                this.DisableWebServiceTypeMerge ?
                                    k.StagingFolder.CreateSubdirectory("staging.net.debug/bin") 
                                    : k.StagingFolder.CreateSubdirectory("staging.net/bin"),
							k.Product,
							k.JavaScriptConstructorArgument
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
							k.Product,
							k.JavaScriptConstructorArgument
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
							k.Product,
							k.JavaScriptConstructorArgument
						},
					}
                :
                    new[] { k }
            ).Where(k => k != null).ToArray();
            #endregion

            var RewriteOutput = targets.ToDictionary(k => k, k => default(FileInfo));

            var __InternalElementProxy = typeof(__InternalElementProxy);
            var __InternalElementProxyToElement = __InternalElementProxy.GetImplicitOperators(null, typeof(IHTMLElement)).Single();


            var ki = -1;
            foreach (var k in targets_variations)
            {
                ki++;

                var AssembliesForDebugging = new List<string>();
                var ScriptLibraries = new List<Type>();

                // while rewriting we may decide to merge script
                // resources. This is the cache.
                var CurrentScriptResources = new ScriptResources();

                #region RaiseProccessStatusChanged
                Action<string> RaiseProccessStatusChanged =
                    e =>
                    {
                        this.RaiseProccessStatusChanged("Component '" + k.TargetType.FullName + "' (" + (ki + 1) + " of " + targets_variations.Length + "): " + e);
                    };

                RaiseProccessStatusChanged("rewriting");
                #endregion

                var InvokeAfterBackendCompiler = new List<Action>();
                var WebDevLauncher = default(FileInfo);

                // lets do a rewrite and inject neccesary bootstrap and proxy code


                // in flash we need to export our functions...
                var __ExternalCallback = new List<MethodBuilderInfo>();
                var __WebServiceForJavaScript = default(WebServiceForJavaScript);

                var r = default(RewriteToAssembly);

                var PrimaryTypes = new[] { k.TargetType };

                if (k.IsJavaScript)
                {
                    k.StagingFolder.DefinesTypes(
                        typeof(ScriptCoreLib.Shared.Query.IAssemblyReferenceToken),
                        typeof(ScriptCoreLib.Shared.XLinq.IAssemblyReferenceToken)
                    );

                }

                if (k.IsWebServicePHP)
                {
                    ScriptLibraries.AddRange(
                        new[]
						    {
							    typeof(ScriptCoreLib.Shared.IAssemblyReferenceToken),
							    typeof(ScriptCoreLib.Shared.Web.IAssemblyReferenceToken),
							    typeof(ScriptCoreLib.Shared.Web.Services.IAssemblyReferenceToken),
						    }
                    );
                }

                if (k.IsWebServiceJava)
                {
                    ScriptLibraries.AddRange(
                        new[]
						{
							typeof(ScriptCoreLibJava.IAssemblyReferenceToken),
							typeof(ScriptCoreLibJava.Web.IAssemblyReferenceToken),
							typeof(ScriptCoreLibJava.Web.Services.IAssemblyReferenceToken),
						}
                    );

                    k.StagingFolder.DefinesTypes(
                        typeof(ScriptCoreLib.ScriptAttribute),
                        typeof(ScriptCoreLibJava.IAssemblyReferenceToken),
                        typeof(ScriptCoreLibJava.XLinq.IAssemblyReferenceToken),
                        typeof(ScriptCoreLibJava.Drawing.IAssemblyReferenceToken),
                        typeof(ScriptCoreLibJava.Windows.Forms.IAssemblyReferenceToken),
                        typeof(ScriptCoreLibJava.Web.IAssemblyReferenceToken),
                        typeof(ScriptCoreLibJava.Web.Services.IAssemblyReferenceToken)
                    );

                }

                var ScriptCoreLib_Query = typeof(global::ScriptCoreLib.Shared.Query.IAssemblyReferenceToken);
                var System_Core = typeof(global::System.Linq.Enumerable).Assembly;

                var ScriptCoreLib_XLinq = typeof(global::ScriptCoreLib.Shared.XLinq.IAssemblyReferenceToken);
                var ScriptCoreLib_Windows_Forms = typeof(global::ScriptCoreLib.Shared.Windows.Forms.IAssemblyReferenceToken);
                var ScriptCoreLibJava_XLinq = typeof(global::ScriptCoreLibJava.XLinq.IAssemblyReferenceToken);
                var ScriptCoreLibJava_Windows_Forms = typeof(global::ScriptCoreLibJava.Windows.Forms.IAssemblyReferenceToken);
                var System_XLinq = typeof(global::System.Xml.Linq.XElement).Assembly;


                var System_Windows_Forms = typeof(global::System.Windows.Forms.Control).Assembly;




                #region RewriteToAssembly
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

                    BCLImplementationMergeAssemblies = new RewriteToAssembly.BCLImplementationMergeAssembly[]
                    {
                        // will be looking up Ultra.BCLImplementation..
                        typeof(global::ScriptCoreLib.Ultra.BCLImplementation.System.__Tuple).Assembly.Location
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
                                        p.ExternalCallback = __ExternalCallback;
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
                                        InjectJavaScriptBootstrap(a, k.JavaScriptConstructorArgument);
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

                    PreAssemblyRewrite =
                        a =>
                        {

                        },

                    #region PostAssemblyRewrite
                    PostAssemblyRewrite =
                        a =>
                        {
                            if (k.IsJavaScript || k.IsActionScript || k.IsJava || k.IsWebServiceJava || k.IsWebServicePHP)
                            {
                                #region IsScriptLibrary
                                a.Assembly.DefineAttribute(
                                   new ScriptAttribute
                                   {
                                       IsScriptLibrary = true,
                                       ScriptLibraries = ScriptLibraries.ToArray()
                                   },
                                   typeof(ScriptAttribute)
                                );
                                #endregion


                                // IsScriptLibrary still needs to know which languages are being used...
                                #region  ScriptTypeFilterAttribute
                                a.Assembly.DefineAttribute(
                                    new ScriptTypeFilterAttribute(
                                        k.IsJavaScript ? ScriptType.JavaScript :
                                        k.IsActionScript ? ScriptType.ActionScript :
                                        k.IsWebServicePHP ? ScriptType.PHP :
                                        ScriptType.Java
                                    ),

                                    typeof(ScriptTypeFilterAttribute)
                                );
                                #endregion



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

                            #region IsWebService
                            if (k.IsWebService)
                            {
                                var __js = targets.Single(kk => kk.IsJavaScript);

                                WriteGlobalApplication(
                                    r,
                                    a,
                                    k.TargetType,
                                    v.FromVirtual(k.StagingFolder),

                                    // we should be passing both?
                                    v.FromVirtual(__js.StagingFolder),

                                    __js.TargetType,
                                    RewriteOutput[__js],
                                    k.IsWebServicePHP,
                                    k.IsWebServiceJava,
                                    InvokeAfterBackendCompiler.Add,
                                    n => WebDevLauncher = v.FromVirtual(n)
                                );



                                if (k.IsWebServiceJava)
                                {


                                    var xmlns = new
                                    {
                                        appengine = (XNamespace)"http://appengine.google.com/ns/1.0",
                                        javaee = (XNamespace)"http://java.sun.com/xml/ns/javaee"
                                    };

                                    // yay! :) InternalHttpServlet depends on TypelessImplementation which was defined
                                    // by WriteGlobalApplication

                                    var Handler = a.context.TypeCache[typeof(InternalHttpServlet)];

                                    var _application = "jsc-project";
                                    var _version = "5";

                                    var res = new ScriptResourceWriter(a.Assembly, a.Module, a.context)
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
                            #endregion


                        }
                    #endregion
                };
                #endregion



                if (k.IsJava || k.IsActionScript || k.IsWebService)
                {
                    r.AtShouldCopyType +=
                        t =>
                        {
                            if (k.IsWebService && this.DisableWebServiceTypeMerge)
                            {
                                // We have to disable type merge!

                                t.DisableCopyType = true;

                                if (t.ContextType.Assembly != k.TargetType.Assembly)
                                {
                                    // cassini wont load if the dll's aren't there.

                                    var AssemblyForDebugging = new FileInfo(t.ContextType.Assembly.Location);

                                    if (AssemblyForDebugging.Directory.FullName ==
                                        Path.GetDirectoryName(k.TargetType.Assembly.Location))
                                    {
                                        if (!AssembliesForDebugging.Contains(AssemblyForDebugging.FullName))
                                            AssembliesForDebugging.Add(AssemblyForDebugging.FullName);
                                    }
                                }
                            }


                        };

                    #region TypeCache
                    r.ExternalContext.TypeCache.Resolve +=
                        SourceType =>
                        {
                            if (k.IsActionScript || k.IsWebServicePHP)
                            {
                                if (ScriptCoreLib_Query != null && SourceType.Assembly == System_Core)
                                {
                                    ScriptLibraries.Add(ScriptCoreLib_Query);
                                    ScriptCoreLib_Query = null;
                                }

                                if (ScriptCoreLib_XLinq != null && SourceType.Assembly == System_XLinq)
                                {
                                    ScriptLibraries.Add(ScriptCoreLib_XLinq);
                                    ScriptCoreLib_XLinq = null;
                                }
                            }

                            if (k.IsJava || k.IsWebServiceJava)
                            {
                                // java applet and google app engine servlets using XLinq.

                                if (ScriptCoreLibJava_XLinq != null && SourceType.Assembly == System_XLinq)
                                {
                                    ScriptLibraries.Add(ScriptCoreLibJava_XLinq);
                                    ScriptCoreLibJava_XLinq = null;
                                }

                                if (ScriptCoreLibJava_Windows_Forms != null && SourceType.Assembly == System_Windows_Forms)
                                {
                                    ScriptLibraries.Add(ScriptCoreLibJava_Windows_Forms);
                                    ScriptCoreLibJava_Windows_Forms = null;
                                }
                            }

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
                    r.AtILOverride +=
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



                #region AtILOverride copy assets
                r.AtILOverride +=
                    (context, x) =>
                    {
                        // how slow will it be if we run this method for each instruction? :)

                        x.BeforeInstructions +=
                            e =>
                            {
                                if (e.SourceMethod.Name == "__ENCAddToList")
                                {
                                    // gee, thanks vbc... 
                                    // lets omit the implementation!
                                    e.il.Emit(OpCodes.Ret);
                                    e.Complete();
                                }
                            };

                        x.BeforeInstruction +=
                            e =>
                            {
                                if (e.i.OpCode == OpCodes.Ldstr)
                                {
                                    // if it is a websource we need to copy it.

                                    CurrentScriptResources.Cache[e.i.OwnerMethod.DeclaringType.Assembly].AddWhenResource(
                                        r.RewriteArguments.ScriptResourceWriter,
                                        e.i.TargetLiteral
                                    );
                                }
                            };
                    };
                #endregion


                #region IsJavaScript IL patching
                if (k.IsJavaScript)
                {
                    // In javascript we will define a type with
                    // InternalConstructors which will act as a native interface
                    var IHTMLElementCoTypes = new Dictionary<TypeBuilder, TypeBuilder>();

                    var ExternalInterfaceConsumerCache = new Dictionary<TypeBuilder, ExternalInterfaceConsumer>();

                    #region ILOverride (Castclass)
                    r.AtILOverride +=
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

                    // because we currently do not support overriding TypeDefinitionCache
                    // we must prefetch these binding to enable constraints
                    //r.PreAssemblyRewrite +=
                    //    delegate
                    //    {
                    //        r.RewriteArguments.context.TypeCache[typeof(Sprite)] = r.RewriteArguments.context.TypeCache[__InternalElementProxy];
                    //        r.RewriteArguments.context.TypeCache[typeof(Applet)] = r.RewriteArguments.context.TypeCache[__InternalElementProxy];
                    //    };


                    #region TypeCache
                    r.ExternalContext.TypeCache.Resolve +=
                        SourceType =>
                        {
                            #region BCLImplementations
                            if (ScriptCoreLib_Query != null && SourceType.Assembly == System_Core)
                            {
                                ScriptLibraries.Add(ScriptCoreLib_Query);
                                ScriptCoreLib_Query = null;
                            }

                            if (ScriptCoreLib_XLinq != null && SourceType.Assembly == System_XLinq)
                            {
                                ScriptLibraries.Add(ScriptCoreLib_XLinq);
                                ScriptCoreLib_XLinq = null;
                            }

                            if (ScriptCoreLib_Windows_Forms != null && SourceType.Assembly == System_Windows_Forms)
                            {
                                ScriptLibraries.Add(ScriptCoreLib_Windows_Forms);
                                ScriptCoreLib_Windows_Forms = null;
                            }
                            #endregion


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
                                    var __TypeCache_InternalElementProxy = r.RewriteArguments.context.TypeCache[
                                            __InternalElementProxy
                                    ];

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


                                    // Duplicate type name within an assembly?
                                    // would be awesome if you told me where the previous type was being created...

                                    if (r.RewriteArguments.context.TypeDefinitionCache.BaseDictionary.ContainsKey(SourceType))
                                    {
                                        throw new InvalidOperationException("TypeDefinitionCache");
                                    }

                                    var DeclaringType = SourceType.IsNested ?
                                        ((TypeBuilder)r.RewriteArguments.context.TypeCache[SourceType.DeclaringType]).DefineNestedType(
                                            SourceType.Name,
                                            SourceType.Attributes,
                                            __TypeCache_InternalElementProxy,
                                            Interfaces
                                        )


                                        : r.RewriteArguments.Module.DefineType(
                                            SourceType.FullName,
                                            SourceType.Attributes,
                                            __TypeCache_InternalElementProxy,
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
                                                    // why is java using IHTMLElementCoTypes?

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
                                        //var km = r.ExternalContext.ConstructorCache[kk];
                                        var km = r.RewriteArguments.context.ConstructorCache[kk];
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
                                        // Could not load type 'PromotionWebApplication1.Library.Templates.__InternalElementProxy' from 
                                        // assembly 'PromotionWebApplication1.Application, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'.

                                        DeclaringType.CreateType();
                                        Consumer.NestedTypesCreated();
                                    }


                                    return;
                                }

                            if (SourceType.IsGenericParameter)
                            {
                                // we should not be replacing T!
                            }
                            else
                            {
                                if (typeof(Sprite).IsAssignableFrom(SourceType) ||
                                    typeof(Applet).IsAssignableFrom(SourceType))
                                {
                                    // erase
                                    r.ExternalContext.TypeCache[SourceType] = r.RewriteArguments.context.TypeCache[__InternalElementProxy];
                                    return;
                                }
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
                                        r.RewriteArguments.context.TypeCache[SourceMethod.GetParameterTypes()]
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

                            var DeclaringType__ = r.RewriteArguments.context.TypeCache[source.DeclaringType];

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
                                        var DeclaringType = (TypeBuilder)DeclaringType__;

                                        // we need an instance :)

                                        var __InternalElement = r.RewriteArguments.context.FieldCache[__InternalElementProxy.GetField("__InternalElement")];

                                        // Unable to change after type has been created.
                                        var ctor = DeclaringType.DefineConstructor(source.Attributes, source.CallingConvention, source.GetParameterTypes());
                                        r.ExternalContext.ConstructorCache[source] = ctor;

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
                        var javapath = new DirectoryInfo(Path.Combine(this.javahome.FullName, "bin"));

                        r.Output.ToJava(
                            javapath,
                            null,
                            null,
                            k.TargetType.FullName + ".jar",
                            k.TargetType,
                            this.InternalCreateNoWindow
                        );
                    }

                    if (k.IsWebServicePHP)
                    {
                        jsc.Program.TypedMain(
                            new jsc.CompileSessionInfo
                            {
                                Options = new jsc.CommandLineOptions
                                {
                                    CachedFileGeneratorConstructor = CachedFileGenerator.Create,
                                    TargetAssembly = r.Output,
                                    IsPHP = true,
                                    IsNoLogo = true
                                }
                            }
                        );

                        #region Application_Main@index.php
                        var w = new StringBuilder();

                        // http://terrychay.com/article/short_open_tag.shtml
                        w.AppendLine("<?php");

                        foreach (var kk in SharedHelper.LoadReferencedAssemblies(Assembly.LoadFile(r.Output.FullName), true))
                        {
                            w.AppendLine("require_once '" + Path.GetFileName(kk.Location) + ".php';");
                        }

                        // could we tell jsc to make a call for us? 
                        w.Append(
                            ((Action)global::jsc.meta.Library.Templates.PHP.PHPWebServiceProvider.PHPWebServiceProvider_Serve).Method.Name
                        );

                        w.AppendLine("();");

                        w.AppendLine("?>");

                        File.WriteAllText(Path.Combine(r.Output.Directory.CreateSubdirectory("web").FullName, "index.php"), w.ToString());
                        #endregion
                    }

                    if (k.IsWebServiceJava)
                    {
                        using (var __r_Output = r.Output.ToVirtualDrive())
                        {
                            jsc.Program.TypedMain(
                                new jsc.CompileSessionInfo
                                {
                                    Options = new jsc.CommandLineOptions
                                    {
                                        CachedFileGeneratorConstructor = CachedFileGenerator.Create,
                                        TargetAssembly = __r_Output.VirtualFile,
                                        IsJava = true,
                                        IsNoLogo = true
                                    }
                                }
                            );
                        }
                    }

                    if (k.IsActionScript)
                    {
                        r.Output.ToActionScript(
                            this.mxmlc,
                            this.flashplayer,
                            k.TargetType,
                            null,
                            k.TargetType.FullName + ".swf",
                            RaiseProccessStatusChanged,
                            InternalCreateNoWindow
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
                    Console.WriteLine("looking for referenced assemblies to be deployed for WebService...");

                    var AssembliesReferencedByFieldsScope = Path.GetDirectoryName(assembly.Location);

                    var WebServiceMethods = new VirtualDictionary<Type, IEnumerable<FileInfo>>();

                    WebServiceMethods.Resolve +=
                        SourceType =>
                        {
                            WebServiceMethods[SourceType] = null;

                            if (SourceType.Assembly != assembly)
                                return;

                            Console.WriteLine(SourceType.FullName);

                            WebServiceMethods[SourceType] = Enumerable.ToArray(
                                     from Method in SourceType.GetMethods()
                                     let Body = new ILBlock(Method)
                                     where Body.Instructrions != null
                                     from i in Body.Instructrions
                                     from ReferencedType in i.GetReferencedTypes().WithEach(
                                        kk => { var _ = WebServiceMethods[kk]; }
                                       )

                                     where ReferencedType != null
                                     let ReferencedAssembly = ReferencedType.Assembly
                                     where ReferencedAssembly != assembly
                                     let ReferencedAssemblyPath = new FileInfo(ReferencedAssembly.Location)
                                     where ReferencedAssemblyPath.Directory.FullName == AssembliesReferencedByFieldsScope
                                     select new
                                     {
                                         Method,
                                         ReferencedType,
                                         ReferencedAssembly,
                                         ReferencedAssemblyPath
                                     }

                             ).GroupBy(kk => kk.ReferencedAssemblyPath).Select(kk => kk.Key);
                        };


          

                    var Trigger = WebServiceMethods[k.TargetType];

                    foreach (var item in WebServiceMethods.BaseDictionary.Values.Where(kk => kk != null).SelectMany(kk => kk).Distinct())
                    {
                        if (!AssembliesForDebugging.Contains(item.FullName))
                        {
                            Console.WriteLine("found dependancy: " + item.Name);

                            AssembliesForDebugging.Add(item.FullName);
                        }

                    }

                    // assemblies for for cassini
                    foreach (var item in AssembliesForDebugging)
                    {
                        var Target = Path.Combine(r.Output.Directory.FullName, Path.GetFileName(item));

                        File.Copy(item, Target, true);
                    }

                    if (this.AtWebServiceReady != null)
                        this.AtWebServiceReady(
                            new AtWebServiceReadyArguments
                            {
                                Assembly = v.FromVirtual(r.Output),
                                GlobalType = k.TargetType.Namespace + ".Global",
                                WebDevLauncher = WebDevLauncher
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

                var IsPage = (from ctor in TargetType.GetConstructors()
                              let ctor_p = ctor.GetParameters()
                              where ctor_p.Length == 1
                              let ctor_a = ctor_p.Single().ParameterType
                              where ctor_a == typeof(IHTMLElement) || typeof(IUltraComponent).IsAssignableFrom(ctor_a)
                              select ctor_a).Any();

                if (IsPage || (TargetType.GetConstructor(typeof(IHTMLElement)) != null && !TargetType.IsNested))
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Web.Services;
using System.Xml.Linq;
using jsc.Languages.IL;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Library;
using jsc.meta.Library;
using jsc.meta.Library.Web;
using ScriptCoreLib;

namespace jsc.meta.Commands.Extend
{
	[Description("An example how ASP.NET webservices could be translated to Google App Engine")]
	public class ExtendToGoogleAppEngineWebService
	{
		// asp.net web service
		// running on gae
		// this implementation could be shared
		// with php to some extent

		public FileInfo assembly;
		public DirectoryInfo staging;

		public FileInfo ant;
		public DirectoryInfo appengine;

		public string application;
		public string version;

		public void Invoke()
		{
			// find all WebMethods
			// create a wapper
			// compile with ant

			// we should rewrite source for jsc

			#region this is our output directory
			if (this.staging == null)
				this.staging = this.assembly.Directory.CreateSubdirectory("staging");
			else if (!staging.Exists)
				this.staging.Create();
			#endregion

			// we are loading it at where it currently is
			var assembly = Assembly.LoadFile(this.assembly.FullName);

			// we need to copy referenced assemblies to staging folder
			staging.DefinesTypes(
				typeof(ScriptCoreLib.ScriptAttribute),
				typeof(ScriptCoreLibJava.IAssemblyReferenceToken),
				typeof(ScriptCoreLibJava.Web.Services.IAssemblyReferenceToken)
			);

			new Builder
			{
				context = this,
				assembly = assembly
			}.Invoke();

		}

		const string Handler = "Handler";


		public class Builder
		{
			public ExtendToGoogleAppEngineWebService context;
			public Assembly assembly;

			public void Invoke()
			{
				var t = Enumerable.ToArray(
					from WebService in assembly.GetTypes()
					where typeof(System.Web.Services.WebService).IsAssignableFrom(WebService)
					let Methods = Enumerable.ToArray(
						from Method in WebService.GetMethods()
						let WebMethod = Method.GetCustomAttributes<WebMethodAttribute>()
						where WebMethod.Any()
						select new { Method, WebMethod }
					)
					where Methods.Any()
					let HandlerFullName = WebService.FullName + Handler
					let HandlerName = WebService.Name + Handler
					select new { WebService, Methods, HandlerFullName, HandlerName }
				);


				// at this point we need to rewrite this assembly
				// so that jsc could convert it to java code

				foreach (var item in t.SelectMany(k => k.Methods.Select(m => new { k.WebService, m.Method })))
				{
					Console.WriteLine(item.WebService.FullName + " " + item.Method.Name);
				}



				// okay now lets rewrite the primary webservice and add data for jsc
				var r = new RewriteToAssembly
				{
					assembly = context.assembly,
					staging = context.staging,

					// at this time we can focus only on the first webservice and consider it as primary
					PrimaryTypes = t.Select(k => k.WebService).ToArray(),

					product = Path.GetFileNameWithoutExtension(context.assembly.Name),

					#region if we are going to inject code from jsc we need to copy it
					rename = new RewriteToAssembly.NamespaceRenameInstructions[] {
						"jsc.meta->" +  Path.GetFileNameWithoutExtension( context.assembly.Name),
						"jsc->" +  Path.GetFileNameWithoutExtension( context.assembly.Name),
					},

					merge = new RewriteToAssembly.MergeInstruction[] {
						"jsc.meta",
						"jsc"
					},
					#endregion

					PostRewrite =
						a =>
						{
							foreach (var item in t)
							{
								var Handler = a.Module.DefineType(item.HandlerFullName, TypeAttributes.Public | TypeAttributes.Sealed, a.context.TypeCache[typeof(WebServiceServlet)]);
								var InvokeWebService = Handler.DefineMethod("InvokeWebService", MethodAttributes.Virtual | MethodAttributes.Public, typeof(void), new[] { a.context.TypeCache[typeof(WebServiceServlet.InvokeWebServiceArguments)] });

								#region DispatchList
								var DispatchList = item.Methods.Select(
									m =>
									{
										var Dispatch = Handler.DefineMethod("Dispatch" + m.Method.Name, MethodAttributes.Static, typeof(void),
											new[] { a.context.TypeCache[item.WebService], a.context.TypeCache[typeof(WebServiceServlet.InvokeWebServiceArguments)] }
										);

										{
											var il = Dispatch.GetILGenerator();
											il.Emit(OpCodes.Ldarg_1);
											il.Emit(OpCodes.Ldarg_0);

											// do we need any arguments?

											foreach (var p in m.Method.GetParameters())
											{
												il.Emit(OpCodes.Ldarg_1);
												il.Emit(OpCodes.Ldstr, p.Name);
												il.Emit(OpCodes.Call, a.context.MethodCache[typeof(WebServiceServlet.InvokeWebServiceArguments).GetMethod("GetString")]);
											}

											il.Emit(OpCodes.Call, a.context.MethodCache[m.Method]);


											il.Emit(OpCodes.Call, a.context.MethodCache[typeof(WebServiceServlet.InvokeWebServiceArguments).GetMethod("SetReturnParameterString")]);


											il.Emit(OpCodes.Ret);
										}

										return new { Dispatch, m };
									}
								).ToArray();
								#endregion


								#region Dispatch by GetMethodName
								{
									var il = InvokeWebService.GetILGenerator();
									var loc_WebService = il.DeclareInitializedLocal(a.context.TypeCache[item.WebService]);

									var loc_MethodName = il.DeclareLocal(typeof(string));
									var loc_IsMethodName = il.DeclareLocal(typeof(bool));

									il.Emit(OpCodes.Ldarg_1);
									il.Emit(OpCodes.Call, a.context.MethodCache[typeof(WebServiceServlet.InvokeWebServiceArguments).GetMethod("GetMethodName")]);
									il.Emit(OpCodes.Stloc, loc_MethodName);

									foreach (var m in DispatchList)
									{

										il.Emit(OpCodes.Ldloc, loc_MethodName);
										il.Emit(OpCodes.Ldstr, m.m.Method.Name);
										il.Emit(OpCodes.Call, typeof(string).GetMethod("op_Equality", new[] { typeof(string), typeof(string) }));
										il.Emit(OpCodes.Ldc_I4_0);
										il.Emit(OpCodes.Ceq);
										il.Emit(OpCodes.Stloc, loc_IsMethodName);

										var next = il.DefineLabel();

										il.Emit(OpCodes.Ldloc, loc_IsMethodName);
										il.Emit(OpCodes.Brtrue, next);

										il.Emit(OpCodes.Ldloc, loc_WebService);
										il.Emit(OpCodes.Ldarg_1);
										il.Emit(OpCodes.Call, m.Dispatch);
										il.Emit(OpCodes.Ret);

										il.MarkLabel(next);
									}

									// lets dispatch now

									il.Emit(OpCodes.Ret);
								}
								#endregion

								Handler.CreateType();
							}

							var xmlns = new
							{
								appengine = (XNamespace)"http://appengine.google.com/ns/1.0",
								javaee = (XNamespace)"http://java.sun.com/xml/ns/javaee"
							};

							var res = new ScriptResourceWriter(a.Assembly, a.Module)
							{
								#region appengine-web.xml
								{
									"java/WEB-INF/appengine-web.xml",

									new XElement(xmlns.appengine + "appengine-web-app",
										new XElement(xmlns.appengine + "application", this.context.application),
										new XElement(xmlns.appengine + "version", this.context.version)
									)
								},
								#endregion
								#region web.xml
								{
									"java/WEB-INF/web.xml",

									new XElement(xmlns.javaee + "web-app",
										new [] {
											new XElement(xmlns.javaee + "display-name", this.context.application)
										}.Concat(
											from k in t
											select new XElement(xmlns.javaee + "servlet", 
												new XElement(xmlns.javaee + "servlet-name", k.HandlerName),
												new XElement(xmlns.javaee + "servlet-class", k.HandlerFullName)
											)
										).Concat(
											from k in t
											select new XElement(xmlns.javaee + "servlet-mapping", 
												new XElement(xmlns.javaee + "servlet-name", k.HandlerName),
												new XElement(xmlns.javaee + "url-pattern", "/" + k.HandlerName + ".asmx/*")
											)
										)
									)

								}
								#endregion

							};


							#region yay attributes
							var ScriptAttribute = typeof(ScriptCoreLib.ScriptAttribute);

							var ScriptTypeFilterAttribute = default(Func<ScriptType, ScriptTypeFilterAttribute>).ToConstructorInfo();


							var AssemblyScriptAttribute = new ScriptAttribute
							{
								IsScriptLibrary = true,
								ScriptLibraries = new[] {
									typeof(ScriptCoreLibJava.IAssemblyReferenceToken),
									typeof(ScriptCoreLibJava.Web.Services.IAssemblyReferenceToken),
								},
								NonScriptTypes = assembly.GetTypes().Where(
									k =>
										k.Namespace.EndsWith(".My") ||
										k.Namespace.EndsWith(".My.Resources")
								).ToArray()
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

						}
				};
				
				r.Invoke();

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
		}
	}
}

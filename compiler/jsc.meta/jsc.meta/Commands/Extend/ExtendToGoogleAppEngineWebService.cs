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
using jsc.meta.Library.Web;
using ScriptCoreLib;
using System.Xml;
using System.Xml.XPath;
using System.Collections;
using System.Diagnostics;
using jsc.meta.Library.Web.Java;

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

		public DirectoryInfo javahome;
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

					PostAssemblyRewrite =
						a =>
						{
							foreach (var item in t)
							{
								var Handler = a.Module.DefineType(item.HandlerFullName, TypeAttributes.Public | TypeAttributes.Sealed, a.context.TypeCache[typeof(WebServiceServlet)]);
								var InvokeWebService = Handler.DefineMethod("InvokeWebService", MethodAttributes.Virtual | MethodAttributes.Public, typeof(void), new[] { a.context.TypeCache[typeof(InvokeWebServiceArguments)] });

								#region DispatchList
								var DispatchList = item.Methods.Select(
									m =>
									{
										var Dispatch = Handler.DefineMethod("Dispatch" + m.Method.Name, MethodAttributes.Static, typeof(void),
											new[] { a.context.TypeCache[item.WebService], a.context.TypeCache[typeof(InvokeWebServiceArguments)] }
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
												il.Emit(OpCodes.Call, a.context.MethodCache[typeof(InvokeWebServiceArguments).GetMethod("GetString")]);
											}

											il.Emit(OpCodes.Call, a.context.MethodCache[m.Method]);


											il.Emit(OpCodes.Call, a.context.MethodCache[typeof(InvokeWebServiceArguments).GetMethod("SetReturnParameterString")]);


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

									il.Emit(OpCodes.Ldarg_1);
									il.Emit(OpCodes.Ldstr, item.WebService.Name);

									il.Emit(OpCodes.Stfld, a.context.FieldCache[typeof(InvokeWebServiceArguments).GetField("ServiceName")]);

									var loc_MethodName = il.DeclareLocal(typeof(string));
									var loc_IsMethodName = il.DeclareLocal(typeof(bool));

									il.Emit(OpCodes.Ldarg_1);
									il.Emit(OpCodes.Call, a.context.MethodCache[typeof(InvokeWebServiceArguments).GetMethod("GetMethodName")]);
									il.Emit(OpCodes.Stloc, loc_MethodName);

									//il.EmitWriteLine("method: ");
									//il.EmitWriteLine(loc_MethodName); 

									#region Overview
									{
										il.Emit(OpCodes.Ldloc, loc_MethodName);
										il.Emit(OpCodes.Ldstr, "");
										il.Emit(OpCodes.Call, typeof(string).GetMethod("op_Equality", new[] { typeof(string), typeof(string) }));
										il.Emit(OpCodes.Ldc_I4_0);
										il.Emit(OpCodes.Ceq);
										il.Emit(OpCodes.Stloc, loc_IsMethodName);

										var skip_overview = il.DefineLabel();

										il.Emit(OpCodes.Ldloc, loc_IsMethodName);
										il.Emit(OpCodes.Brtrue, skip_overview);

										//il.EmitWriteLine(m.Dispatch.Name);

										#region Display one operation
										var loc_Operation = il.DeclareLocal(typeof(string));
										il.Emit(OpCodes.Ldarg_1);
										il.Emit(OpCodes.Call, a.context.MethodCache[typeof(InvokeWebServiceArguments).GetMethod("GetOperationName")]);
										il.Emit(OpCodes.Stloc, loc_Operation);
										il.EmitWriteLine(loc_Operation);

										var loc_OperationParameters = il.DeclareLocal(a.context.TypeCache[typeof(SimpleParameterInfo[])]);
										var loc_OperationParameter = il.DeclareLocal(a.context.TypeCache[typeof(SimpleParameterInfo)]);

										foreach (var m in DispatchList)
										{

											il.Emit(OpCodes.Ldloc, loc_Operation);
											il.Emit(OpCodes.Ldstr, m.m.Method.Name);
											il.Emit(OpCodes.Call, typeof(string).GetMethod("op_Equality", new[] { typeof(string), typeof(string) }));
											il.Emit(OpCodes.Ldc_I4_0);
											il.Emit(OpCodes.Ceq);
											il.Emit(OpCodes.Stloc, loc_IsMethodName);

											var next = il.DefineLabel();

											il.Emit(OpCodes.Ldloc, loc_IsMethodName);
											il.Emit(OpCodes.Brtrue, next);

											RenderOperationPage(il, m.m.Method, a, loc_OperationParameters, loc_OperationParameter);

											il.Emit(OpCodes.Ret);

											il.MarkLabel(next);
										}
										#endregion

										#region Display all methods
										var loc_Methods = il.DeclareLocal(typeof(string[]));

										il.Emit(OpCodes.Ldc_I4, DispatchList.Length);
										il.Emit(OpCodes.Newarr, typeof(string));
										il.Emit(OpCodes.Stloc, loc_Methods);


										foreach (var m in DispatchList.Select((k, i) => new { i, k }))
										{
											il.Emit(OpCodes.Ldloc, loc_Methods);
											il.Emit(OpCodes.Ldc_I4, m.i);
											il.Emit(OpCodes.Ldstr, m.k.m.Method.Name);
											il.Emit(OpCodes.Stelem_Ref);

											//L_0008: ldloc.1 
											//L_0009: ldc.i4.0 
											//L_000a: ldstr "foo"
											//L_000f: stelem.ref 

										}

										il.Emit(OpCodes.Ldarg_1);
										il.Emit(OpCodes.Ldloc, loc_Methods);
										il.Emit(OpCodes.Call, a.context.MethodCache[typeof(InvokeWebServiceArguments).GetMethod("RenderMethodsToDocumentContent")]);
										#endregion

										il.Emit(OpCodes.Ret);

										il.MarkLabel(skip_overview);
									}
									#endregion

									#region Call the dispatch
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

										//il.EmitWriteLine(m.Dispatch.Name);

										il.Emit(OpCodes.Ldloc, loc_WebService);
										il.Emit(OpCodes.Ldarg_1);
										il.Emit(OpCodes.Call, m.Dispatch);
										il.Emit(OpCodes.Ret);

										il.MarkLabel(next);

									}
									#endregion

									// lets dispatch now
									//il.EmitWriteLine("No such method!");

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
												
												// http://www.coderanch.com/t/414165/Servlets/java/url-pattern-web-xml

												new XElement(xmlns.javaee + "url-pattern", "/" + k.WebService.Name + ".asmx/*"),
												new XElement(xmlns.javaee + "url-pattern", "/" + k.WebService.Name + ".asmx")
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

				#region ant_build_xml, run.bat, upload.bat
				{
					var ant_build_xml = XDocument.Load(
						XmlReader.Create(
							typeof(ExtendToGoogleAppEngineWebService).Assembly.GetManifestResourceStream("jsc.meta.Tools.ant.GoogleAppEngine.build.xml")
						)
					);

					ant_build_xml.Root.AddFirst(new XComment("modified by jsc.meta"));

					var r_Output_web = r.Output.Directory.CreateSubdirectory("web");

					// http://www.larswilhelmsen.com/2008/12/12/linq-to-xml-xpathselectelement-annoyance/
					// http://msdn.microsoft.com/en-us/library/bb341675.aspx
					var location = ((IEnumerable)ant_build_xml.XPathEvaluate("/project/property[@name='appengine.sdk']/@location")).Cast<XAttribute>().Single();

					if (this.context.appengine != null)
						location.Value = this.context.appengine.FullName;

					var ant_build_xml_file = Path.Combine(r_Output_web.FullName, "build.xml");
					ant_build_xml.Save(ant_build_xml_file);


					var proccess_ant_info = new ProcessStartInfo(
							this.context.ant.FullName,
							"-f build.xml"
							)
						{
							UseShellExecute = false,

							WorkingDirectory = r_Output_web.FullName
						};

					proccess_ant_info.EnvironmentVariables["JAVA_HOME"] = this.context.javahome.FullName;

					var proccess_ant = Process.Start(proccess_ant_info);

					proccess_ant.WaitForExit();


					// ----

					File.WriteAllText(
						Path.Combine(r_Output_web.FullName, "run.bat"),
						@"
@echo off

echo killing all java in pure hope to terminate the servlet...
echo if there is no java running this will show as an error
taskkill /IM java.exe /F
taskkill /FI ""WINDOWTITLE eq volatile_dev_appserver*"" /F
start ""volatile_dev_appserver"" /MIN """ + this.context.appengine + @"\bin\dev_appserver.cmd"" www

echo waiting 6 seconds for the server to load...
PING 1.1.1.1 -n 1 -w 6000 >NUL
start ""web"" explorer ""http://localhost:8080/" + t.First().WebService.Name + @".asmx""
echo thanks! :)
"
					);

					File.WriteAllText(
						Path.Combine(r_Output_web.FullName, "upload.bat"),
						@"
@echo off
call """ + this.context.appengine + @"\bin\appcfg.cmd"" update www

"
					);
				}
				#endregion

			}

			private void RenderOperationPage(
				ILGenerator il,
				MethodInfo m,
				RewriteToAssembly.AssemblyRewriteArguments a,
				LocalBuilder loc_OperationParameters,
				LocalBuilder loc_OperationParameter
				)
			{
				var p = m.GetParameters().Select((k, i) => new { k, i }).ToArray();

				il.Emit(OpCodes.Ldc_I4, p.Length);
				il.Emit(OpCodes.Newarr, a.context.TypeCache[typeof(SimpleParameterInfo)]);
				il.Emit(OpCodes.Stloc, loc_OperationParameters);

				foreach (var item in p)
				{
					il.Emit(OpCodes.Newobj, a.context.ConstructorCache[typeof(SimpleParameterInfo).GetConstructor(new Type[0])]);
					il.Emit(OpCodes.Stloc, loc_OperationParameter);

					il.Emit(OpCodes.Ldloc, loc_OperationParameter);
					il.Emit(OpCodes.Ldstr, item.k.Name);
					il.Emit(OpCodes.Stfld, a.context.FieldCache[typeof(SimpleParameterInfo).GetField("Name")]);

					//L_001c: ldtoken string
					//L_0021: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)

					il.Emit(OpCodes.Ldloc, loc_OperationParameter);
					il.Emit(OpCodes.Ldtoken, item.k.ParameterType);
					il.Emit(OpCodes.Call, 
						typeof(Type).GetMethod("GetTypeFromHandle", 
							new [] {typeof(System.RuntimeTypeHandle) }
						)
					);


					il.Emit(OpCodes.Stfld, a.context.FieldCache[typeof(SimpleParameterInfo).GetField("Type")]);

					il.Emit(OpCodes.Ldloc, loc_OperationParameters);
					il.Emit(OpCodes.Ldc_I4, item.i);
					il.Emit(OpCodes.Ldloc, loc_OperationParameter);
					il.Emit(OpCodes.Stelem_Ref);
				}

				// are we sure argument 1 is there for us?
				il.Emit(OpCodes.Ldarg_1);
				il.Emit(OpCodes.Ldstr, m.Name);
				il.Emit(OpCodes.Ldloc, loc_OperationParameters);
				il.Emit(OpCodes.Call,
					a.context.MethodCache[typeof(InvokeWebServiceArguments).GetMethod("RenderOperationToDocumentContent")]
				);


			}
		}
	}
}

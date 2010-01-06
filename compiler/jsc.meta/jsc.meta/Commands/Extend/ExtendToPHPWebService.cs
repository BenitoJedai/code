using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Web.Services;
using jsc.Languages.IL;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Library;
using jsc.meta.Library.Web;
using jsc.meta.Library.Web.PHP;
using ScriptCoreLib;

namespace jsc.meta.Commands.Extend
{
	[Description("An example how ASP.NET webservices could be translated to PHP")]
	public class ExtendToPHPWebService
	{
		public FileInfo assembly;
		public DirectoryInfo staging;


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
				typeof(ScriptCoreLib.Shared.IAssemblyReferenceToken),
				typeof(ScriptCoreLib.Shared.Web.Services.IAssemblyReferenceToken)
			);

			new Builder
			{
				context = this,
				assembly = assembly
			}.Invoke();

		}

		const string Handler = "Handler";
		const string Application_Main = "Application_Main";

		public class Builder
		{
			public ExtendToPHPWebService context;
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

							var WebServiceServletImplementation = a.Module.DefineType("WebServiceServletImplementation", TypeAttributes.Public, a.context.TypeCache[typeof(WebServiceServlet)]);
							var WebServiceServletImplementation_ctor = WebServiceServletImplementation.DefineDefaultConstructor(MethodAttributes.Public);



							var Application_Main_ = WebServiceServletImplementation.DefineMethod(Application_Main, MethodAttributes.Static);

							Application_Main_.DefineAttribute<ScriptAttribute>(
								new
								{
									NoDecoration = true
								}
							);



							{
								var il = Application_Main_.GetILGenerator();
								var il_ret = il.DefineLabel();

								var loc1 = il.DeclareLocal(a.context.TypeCache[typeof(WebServiceServlet)]);

								il.Emit(OpCodes.Newobj, WebServiceServletImplementation_ctor);
								il.Emit(OpCodes.Stloc, loc1);

								var loc_WebMethod = il.DeclareLocal(typeof(string));
								var loc_IsWebMethod = il.DeclareLocal(typeof(bool));

								var loc_Operation = il.DeclareLocal(typeof(string));
								var loc_IsOperation = il.DeclareLocal(typeof(bool));

								var loc_ServiceName = il.DeclareLocal(typeof(string));
								var loc_IsServiceName = il.DeclareLocal(typeof(bool));

								var loc_QueryString = il.DeclareLocal(typeof(string));
								var loc_IsWSDL = il.DeclareLocal(typeof(bool));

								var loc_Methods = il.DeclareLocal(typeof(string[]));
								var loc_WSDLProvider = il.DeclareLocal(a.context.TypeCache[typeof(WSDLProvider)]);

								il.Emit(OpCodes.Ldloc, loc1);
								il.Emit(OpCodes.Call, a.context.MethodCache[typeof(WebServiceServlet).GetMethod("get_ServiceName")]);
								il.Emit(OpCodes.Stloc, loc_ServiceName);

								il.Emit(OpCodes.Ldloc, loc1);
								il.Emit(OpCodes.Call, a.context.MethodCache[typeof(WebServiceServlet).GetMethod("get_Operation")]);
								il.Emit(OpCodes.Stloc, loc_Operation);

								il.Emit(OpCodes.Ldloc, loc1);
								il.Emit(OpCodes.Call, a.context.MethodCache[typeof(WebServiceServlet).GetMethod("get_WebMethod")]);
								il.Emit(OpCodes.Stloc, loc_WebMethod);

								il.Emit(OpCodes.Ldloc, loc1);
								il.Emit(OpCodes.Call, a.context.MethodCache[typeof(WebServiceServlet).GetMethod("get_QueryString")]);
								il.Emit(OpCodes.Stloc, loc_QueryString);

								foreach (var k in t)
								{
									#region DispatchList
									var DispatchList = k.Methods.Select(
										m =>
										{
											var Dispatch = WebServiceServletImplementation.DefineMethod("_" + m.Method.MetadataToken + "_Dispatch",
												MethodAttributes.Family, typeof(void),
												new[] { a.context.TypeCache[k.WebService] }
											);

											{
												var Dispatch_il = Dispatch.GetILGenerator();
												Dispatch_il.Emit(OpCodes.Ldarg_0);
												Dispatch_il.Emit(OpCodes.Ldarg_1);

												// do we need any arguments?

												foreach (var p in m.Method.GetParameters())
												{
													Dispatch_il.Emit(OpCodes.Ldarg_0);
													Dispatch_il.Emit(OpCodes.Ldstr, p.Name);
													Dispatch_il.Emit(OpCodes.Call, a.context.MethodCache[typeof(WebServiceServlet).GetMethod("GetString")]);
												}

												Dispatch_il.Emit(OpCodes.Call, a.context.MethodCache[m.Method]);


												Dispatch_il.Emit(OpCodes.Call, a.context.MethodCache[typeof(WebServiceServlet).GetMethod("SetReturnParameterString")]);


												Dispatch_il.Emit(OpCodes.Ret);
											}

											return new { Dispatch, m };
										}
									).ToArray();
									#endregion

									#region GetParametersList
									var GetParametersList = k.Methods.Select(
										m =>
										{
											var GetParameters = WebServiceServletImplementation.DefineMethod("_" + m.Method.MetadataToken + "_GetParameters",
												MethodAttributes.Family, a.context.TypeCache[typeof(SimpleParameterInfo[])],
												new Type[0] { }
											);



											{
												var _il = GetParameters.GetILGenerator();

												var loc_OperationParameters = _il.DeclareLocal(a.context.TypeCache[typeof(SimpleParameterInfo[])]);
												var loc_OperationParameter = _il.DeclareLocal(a.context.TypeCache[typeof(SimpleParameterInfo)]);


												var OperationParameters = m.Method.GetParameters().Select((kkk, i) => new { k = kkk, i }).ToArray();

												_il.Emit(OpCodes.Ldc_I4, OperationParameters.Length);
												_il.Emit(OpCodes.Newarr, a.context.TypeCache[typeof(SimpleParameterInfo)]);
												_il.Emit(OpCodes.Stloc, loc_OperationParameters);

												foreach (var item in OperationParameters)
												{
													_il.Emit(OpCodes.Newobj, a.context.ConstructorCache[typeof(SimpleParameterInfo).GetConstructor(new Type[0])]);
													_il.Emit(OpCodes.Stloc, loc_OperationParameter);

													_il.Emit(OpCodes.Ldloc, loc_OperationParameter);
													_il.Emit(OpCodes.Ldstr, item.k.Name);
													_il.Emit(OpCodes.Stfld, a.context.TypeFieldCache[typeof(SimpleParameterInfo)].Single(kkk => kkk.Name == "Name"));

													//L_001c: ldtoken string
													//L_0021: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)

													_il.Emit(OpCodes.Ldloc, loc_OperationParameter);
													_il.Emit(OpCodes.Ldtoken, item.k.ParameterType);
													_il.Emit(OpCodes.Call,
														typeof(Type).GetMethod("GetTypeFromHandle",
															new[] { typeof(System.RuntimeTypeHandle) }
														)
													);


													_il.Emit(OpCodes.Stfld, a.context.TypeFieldCache[typeof(SimpleParameterInfo)].Single(kkk => kkk.Name == "Type"));

													_il.Emit(OpCodes.Ldloc, loc_OperationParameters);
													_il.Emit(OpCodes.Ldc_I4, item.i);
													_il.Emit(OpCodes.Ldloc, loc_OperationParameter);
													_il.Emit(OpCodes.Stelem_Ref);
												}

												_il.Emit(OpCodes.Ldloc, loc_OperationParameters);
												_il.Emit(OpCodes.Ret);
											}

											return new { GetParameters, m };
										}
									).ToArray();
									#endregion

									#region GetMethodsList

									var GetMethods = WebServiceServletImplementation.DefineMethod("_" + k.WebService.MetadataToken + "_GetMethods",
										MethodAttributes.Family, a.context.TypeCache[typeof(SimpleMethodInfo[])],
										new Type[0] { }
									);

									{
										var _il = GetMethods.GetILGenerator();

										var loc_List = _il.DeclareLocal(a.context.TypeCache[typeof(SimpleMethodInfo[])]);
										var loc_Current = _il.DeclareLocal(a.context.TypeCache[typeof(SimpleMethodInfo)]);


										//var OperationParameters = m.Method.GetParameters().Select((kkk, i) => new { k = kkk, i }).ToArray();

										_il.Emit(OpCodes.Ldc_I4, k.Methods.Length);
										_il.Emit(OpCodes.Newarr, a.context.TypeCache[typeof(SimpleMethodInfo)]);
										_il.Emit(OpCodes.Stloc, loc_List);

										foreach (var item in k.Methods.Select((kk, i) => new { i, kk }))
										{
											_il.Emit(OpCodes.Newobj, a.context.ConstructorCache[typeof(SimpleMethodInfo).GetConstructor(new Type[0])]);
											_il.Emit(OpCodes.Stloc, loc_Current);

											_il.Emit(OpCodes.Ldloc, loc_Current);
											_il.Emit(OpCodes.Ldstr, item.kk.Method.Name);
											_il.Emit(OpCodes.Stfld, a.context.TypeFieldCache[typeof(SimpleMethodInfo)].Single(kkk => kkk.Name == "Name"));

											_il.Emit(OpCodes.Ldloc, loc_Current);

											_il.Emit(OpCodes.Ldarg_0);

											_il.Emit(OpCodes.Call,
												GetParametersList.Single(kkk => kkk.m == item.kk).GetParameters
											);

											_il.Emit(OpCodes.Stfld, a.context.TypeFieldCache[typeof(SimpleMethodInfo)].Single(kkk => kkk.Name == "Parameters"));



											_il.Emit(OpCodes.Ldloc, loc_List);
											_il.Emit(OpCodes.Ldc_I4, item.i);
											_il.Emit(OpCodes.Ldloc, loc_Current);
											_il.Emit(OpCodes.Stelem_Ref);
										}

										_il.Emit(OpCodes.Ldloc, loc_List);
										_il.Emit(OpCodes.Ret);
									}

									#endregion

									var loc_Service = il.DeclareInitializedLocal(a.context.TypeCache[k.WebService]);

									il.Emit(OpCodes.Ldloc, loc_ServiceName);
									il.Emit(OpCodes.Ldstr, k.WebService.Name);
									il.Emit(OpCodes.Call, typeof(string).GetMethod("op_Equality", new[] { typeof(string), typeof(string) }));
									il.Emit(OpCodes.Ldc_I4_0);
									il.Emit(OpCodes.Ceq);
									il.Emit(OpCodes.Stloc, loc_IsServiceName);

									var next_ServiceName = il.DefineLabel();

									il.Emit(OpCodes.Ldloc, loc_IsServiceName);
									il.Emit(OpCodes.Brtrue, next_ServiceName);

									#region ?WSDL

									il.Emit(OpCodes.Ldloc, loc_QueryString);
									il.Emit(OpCodes.Ldstr, "WSDL");
									il.Emit(OpCodes.Call, typeof(string).GetMethod("op_Equality", new[] { typeof(string), typeof(string) }));
									il.Emit(OpCodes.Ldc_I4_0);
									il.Emit(OpCodes.Ceq);
									il.Emit(OpCodes.Stloc, loc_IsWSDL);

									var skip_WSDL = il.DefineLabel();

									il.Emit(OpCodes.Ldloc, loc_IsWSDL);
									il.Emit(OpCodes.Brtrue, skip_WSDL);

									il.Emit(OpCodes.Newobj, a.context.ConstructorCache[typeof(WSDLProvider).GetConstructor(new Type[0])]);
									il.Emit(OpCodes.Stloc, loc_WSDLProvider);

									#region loc_WSDLProvider.Methods = this.~GetMethods
									il.Emit(OpCodes.Ldloc, loc_WSDLProvider);
									il.Emit(OpCodes.Ldloc, loc1);
									il.Emit(OpCodes.Call, GetMethods);
									il.Emit(OpCodes.Stfld, a.context.TypeFieldCache[typeof(WSDLProvider)].Single(kkk => kkk.Name == "Methods"));
									#endregion

									#region loc_WSDLProvider.ServiceName = loc_ServiceName
									il.Emit(OpCodes.Ldloc, loc_WSDLProvider);
									il.Emit(OpCodes.Ldloc, loc_ServiceName);
									il.Emit(OpCodes.Stfld, a.context.TypeFieldCache[typeof(WSDLProvider)].Single(kkk => kkk.Name == "ServiceName"));
									#endregion




									il.Emit(OpCodes.Ldloc, loc1);
									il.Emit(OpCodes.Ldloc, loc_WSDLProvider);

									il.Emit(OpCodes.Call, a.context.MethodCache[typeof(WebServiceServlet).GetMethod("RenderWSDL")]);

									// jsc is unable to detect plain ret opcode? must be a bug
									il.Emit(OpCodes.Br, il_ret);

									il.MarkLabel(skip_WSDL);


									#endregion

									#region are we calling the method?
									foreach (var m in DispatchList.Select((kk, i) => new { i, kk }))
									{
										il.Emit(OpCodes.Ldloc, loc_WebMethod);
										il.Emit(OpCodes.Ldstr, m.kk.m.Method.Name);
										il.Emit(OpCodes.Call, typeof(string).GetMethod("op_Equality", new[] { typeof(string), typeof(string) }));
										il.Emit(OpCodes.Ldc_I4_0);
										il.Emit(OpCodes.Ceq);
										il.Emit(OpCodes.Stloc, loc_IsWebMethod);

										var next_WebMethod = il.DefineLabel();

										il.Emit(OpCodes.Ldloc, loc_IsWebMethod);
										il.Emit(OpCodes.Brtrue, next_WebMethod);

										il.Emit(OpCodes.Ldloc, loc1);
										il.Emit(OpCodes.Ldloc, loc_Service);
										il.Emit(OpCodes.Call, m.kk.Dispatch);

										il.Emit(OpCodes.Br, il_ret);

										il.MarkLabel(next_WebMethod);

									}
									#endregion

									#region are we showing what parameters are needed?
									foreach (var m in k.Methods.Select((kk, i) => new { i, kk }))
									{
										il.Emit(OpCodes.Ldloc, loc_Operation);
										il.Emit(OpCodes.Ldstr, m.kk.Method.Name);
										il.Emit(OpCodes.Call, typeof(string).GetMethod("op_Equality", new[] { typeof(string), typeof(string) }));
										il.Emit(OpCodes.Ldc_I4_0);
										il.Emit(OpCodes.Ceq);
										il.Emit(OpCodes.Stloc, loc_IsOperation);

										var next_Operation = il.DefineLabel();

										il.Emit(OpCodes.Ldloc, loc_IsOperation);
										il.Emit(OpCodes.Brtrue, next_Operation);

										#region time to render params



										// are we sure argument 1 is there for us?
										il.Emit(OpCodes.Ldloc, loc1);
										//il.Emit(OpCodes.Ldstr, m.kk.Method.Name);
										//il.Emit(OpCodes.Ldloc, loc_OperationParameters);
										il.Emit(OpCodes.Ldloc, loc1);

										il.Emit(OpCodes.Call,
											GetParametersList.Single(kkk => kkk.m == m.kk).GetParameters
										);

										il.Emit(OpCodes.Call,
											a.context.MethodCache[typeof(WebServiceServlet).GetMethod("RenderOperationToDocumentContent")]
										);

										#endregion

										il.Emit(OpCodes.Br, il_ret);

										il.MarkLabel(next_Operation);

									}
									#endregion

									// show the goods! list the methods!
									#region Display all methods


									il.Emit(OpCodes.Ldc_I4, k.Methods.Length);
									il.Emit(OpCodes.Newarr, typeof(string));
									il.Emit(OpCodes.Stloc, loc_Methods);


									foreach (var m in k.Methods.Select((kk, i) => new { i, kk }))
									{
										il.Emit(OpCodes.Ldloc, loc_Methods);
										il.Emit(OpCodes.Ldc_I4, m.i);
										il.Emit(OpCodes.Ldstr, m.kk.Method.Name);
										il.Emit(OpCodes.Stelem_Ref);
									}

									il.Emit(OpCodes.Ldloc, loc1);
									il.Emit(OpCodes.Ldloc, loc_Methods);
									il.Emit(OpCodes.Call, a.context.MethodCache[typeof(WebServiceServlet).GetMethod("RenderMethodsToDocumentContent")]);
									#endregion

									// jsc is unable to detect plain ret opcode? must be a bug
									il.Emit(OpCodes.Br, il_ret);

									il.MarkLabel(next_ServiceName);
								}

								// no dice? diagnostics?
								il.Emit(OpCodes.Ldloc, loc1);
								il.Emit(OpCodes.Call, a.context.MethodCache[typeof(WebServiceServlet).GetMethod("Invoke")]);
								il.MarkLabel(il_ret);
								il.Emit(OpCodes.Ret);
							}

							WebServiceServletImplementation.CreateType();

							var res = new ScriptResourceWriter(a.Assembly, a.Module)
							{
								// any assets needed?
							};


							#region yay attributes
							var ScriptAttribute = typeof(ScriptCoreLib.ScriptAttribute);

							var ScriptTypeFilterAttribute = default(Func<ScriptType, ScriptTypeFilterAttribute>).ToConstructorInfo();


							var AssemblyScriptAttribute = new ScriptAttribute
							{
								IsScriptLibrary = true,
								ScriptLibraries = new[] {
									typeof(ScriptCoreLib.Shared.IAssemblyReferenceToken),
									typeof(ScriptCoreLib.Shared.Web.Services.IAssemblyReferenceToken),
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
									ScriptTypeFilterAttribute, new object[] { ScriptType.PHP }
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
							IsPHP = true,
							IsNoLogo = true
						}
					}
				);

				{
					var web = r.Output.Directory.CreateSubdirectory("web");

					#region Application_Main@index.php
					var w = new StringBuilder();

					// http://terrychay.com/article/short_open_tag.shtml
					w.AppendLine("<?php");

					foreach (var kk in SharedHelper.LoadReferencedAssemblies(Assembly.LoadFile(r.Output.FullName), true))
					{
						var k = Path.GetFileName(kk.Location);

						w.AppendLine("require_once '" + k + ".php';");
					}

					w.AppendLine(Application_Main + "();");

					w.AppendLine("?>");

					File.WriteAllText(Path.Combine(web.FullName, "index.php"), w.ToString());
					#endregion

					#region htaccess
					// http://www.dynamicdrive.com/forums/showthread.php?t=43774
					// http://corz.org/serv/tricks/htaccess2.php
					File.WriteAllText(Path.Combine(web.FullName, ".htaccess"),
@"Options +FollowSymlinks
RewriteEngine on
RewriteBase /" + Path.GetFileNameWithoutExtension(context.assembly.Name) + @"
DirectorySlash off 
Options -Indexes
RewriteRule ^(.*)$ index\.php [NC]");
					#endregion


				}
			}

		}

	}
}

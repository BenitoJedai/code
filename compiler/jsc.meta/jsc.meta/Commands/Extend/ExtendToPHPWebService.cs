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

								var loc1 = il.DeclareLocal(a.context.TypeCache[typeof(WebServiceServlet)]);

								il.Emit(OpCodes.Newobj, WebServiceServletImplementation_ctor);
								il.Emit(OpCodes.Stloc, loc1);

								il.Emit(OpCodes.Ldloc, loc1);
								il.Emit(OpCodes.Call, a.context.MethodCache[typeof(WebServiceServlet).GetMethod("Invoke")]);
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
DirectorySlash off 
Options -Indexes
RewriteRule ^(.*)$ index\.php [NC]");
					#endregion


				}
			}

			private void RenderOperationPage(
				ILGenerator il,
				MethodInfo m,
				RewriteToAssembly.PostRewriteArguments a,
				LocalBuilder loc_OperationParameters,
				LocalBuilder loc_OperationParameter
				)
			{
				//var p = m.GetParameters().Select((k, i) => new { k, i }).ToArray();

				//il.Emit(OpCodes.Ldc_I4, p.Length);
				//il.Emit(OpCodes.Newarr, a.context.TypeCache[typeof(InvokeWebServiceArguments.ParameterInfo)]);
				//il.Emit(OpCodes.Stloc, loc_OperationParameters);

				//foreach (var item in p)
				//{
				//    il.Emit(OpCodes.Newobj, a.context.ConstructorCache[typeof(InvokeWebServiceArguments.ParameterInfo).GetConstructor(new Type[0])]);
				//    il.Emit(OpCodes.Stloc, loc_OperationParameter);

				//    il.Emit(OpCodes.Ldloc, loc_OperationParameter);
				//    il.Emit(OpCodes.Ldstr, item.k.Name);
				//    il.Emit(OpCodes.Stfld, a.context.TypeFieldCache[typeof(InvokeWebServiceArguments.ParameterInfo)].Single(k => k.Name == "Name"));

				//    //L_001c: ldtoken string
				//    //L_0021: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)

				//    il.Emit(OpCodes.Ldloc, loc_OperationParameter);
				//    il.Emit(OpCodes.Ldtoken, item.k.ParameterType);
				//    il.Emit(OpCodes.Call,
				//        typeof(Type).GetMethod("GetTypeFromHandle",
				//            new[] { typeof(System.RuntimeTypeHandle) }
				//        )
				//    );


				//    il.Emit(OpCodes.Stfld, a.context.TypeFieldCache[typeof(InvokeWebServiceArguments.ParameterInfo)].Single(k => k.Name == "Type"));

				//    il.Emit(OpCodes.Ldloc, loc_OperationParameters);
				//    il.Emit(OpCodes.Ldc_I4, item.i);
				//    il.Emit(OpCodes.Ldloc, loc_OperationParameter);
				//    il.Emit(OpCodes.Stelem_Ref);
				//}

				//// are we sure argument 1 is there for us?
				//il.Emit(OpCodes.Ldarg_1);
				//il.Emit(OpCodes.Ldstr, m.Name);
				//il.Emit(OpCodes.Ldloc, loc_OperationParameters);
				//il.Emit(OpCodes.Call,
				//    a.context.MethodCache[typeof(InvokeWebServiceArguments).GetMethod("RenderOperationToDocumentContent")]
				//);


			}
		}

	}
}

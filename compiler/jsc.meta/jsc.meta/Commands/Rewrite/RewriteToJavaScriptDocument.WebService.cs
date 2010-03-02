using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using java.applet;
using jsc.Languages.IL;
using jsc.meta.Commands.Rewrite.Templates;
using jsc.meta.Library;
using jsc.meta.Library.Templates;
using jsc.meta.Library.Templates.Java;
using jsc.meta.Library.Templates.JavaScript;
using jsc.meta.Library.Templates.WebService;
using jsc.meta.Library.VolumeFunctions;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.external;
using ScriptCoreLib.CSharp.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace jsc.meta.Commands.Rewrite
{

	partial class RewriteToJavaScriptDocument
	{
		// todo: http://ajaxpatterns.org/HTTP_Streaming

		private void WriteGlobalApplication(
			RewriteToAssembly r,
			RewriteToAssembly.AssemblyRewriteArguments a,
			Type SourceType,
			DirectoryInfo __web_bin,
			DirectoryInfo __js_StagingFolder__,
			Type js_TargetType,
			FileInfo js_RewriteOutput,
			bool IsWebServicePHP,
			bool IsWebServiceJava,

			Action<Action> InvokeAfterBackendCompiler

			)
		{

			var __root = (!IsWebServiceJava && !IsWebServicePHP) ? __web_bin.Parent : __web_bin;

			using (var __js_StagingFolder_virtual = __js_StagingFolder__.ToVirtualDrive())
			using (var __root_virtual = __root.ToVirtualDrive())
			{
				var js_staging_web = __js_StagingFolder_virtual.VirtualDirectory.CreateSubdirectory("web");

				var root = __root_virtual.VirtualDirectory;



				var TypeCache = r.RewriteArguments.context.TypeCache;
				var ConstructorCache = r.RewriteArguments.context.ConstructorCache;
				var MethodCache = r.RewriteArguments.context.MethodCache;
				var FieldCache = r.RewriteArguments.context.FieldCache;

				#region Global
				var GlobalFullName = SourceType.Namespace + ".Global";

				var Global = a.Module.DefineType(GlobalFullName,
					TypeAttributes.Public,
					TypeCache[typeof(InternalGlobal)],
					new Type[0]
				);

				var Global_ctor = Global.DefineDefaultConstructor(MethodAttributes.Public);

				#region Application_BeginRequest
				var Application_BeginRequest = Global.DefineMethod("Application_BeginRequest", MethodAttributes.Public,
					CallingConventions.Standard, typeof(void),
					new[] { typeof(object), typeof(EventArgs) }
				);

				{
					var il = Application_BeginRequest.GetILGenerator();

					//var __WebService = il.DeclareInitializedLocal(TypeCache[SourceType]);

					il.Emit(OpCodes.Ldarg_0);



					il.Emit(OpCodes.Call, r.RewriteArguments.context.MethodCache[
						((Action<InternalGlobal>)InternalGlobalExtensions.InternalApplication_BeginRequest).Method
					]);


					il.Emit(OpCodes.Ret);
				}
				#endregion


				var __Files = js_staging_web.GetFilesByPattern("*.js", "*.htm").Concat(js_staging_web.CreateSubdirectory("assets").GetFiles("*.*", SearchOption.AllDirectories));
				var __Files2 = Enumerable.ToArray(

					from k in __Files

					let Name0 = k.FullName.Substring(js_staging_web.FullName.Length)
					let Name2 = Name0.Replace("\\", "/")

					let Name1 = Name2.StartsWith("/") ? Name0.Substring(1) : Name0
					let Name = Name2.StartsWith("/") ? Name2.Substring(1) : Name2

					select new
					{
						k,

						Name1,
						Name

					}

				);

				var __Files1 = __Files2.Select(k => new InternalFileInfo { Name = k.Name }).ToArray();


				#region GetFiles
				var GetFiles = Global.DefineMethod("GetFiles",
						MethodAttributes.Virtual | MethodAttributes.Public, CallingConventions.Standard, TypeCache[typeof(InternalFileInfo[])],
						null
					);

				GetFiles.GetILGenerator().EmitReturnSerializedArray(__Files1,
					TypeCache,
					ConstructorCache,
					FieldCache
				);
				#endregion

				var __WebMethods = Enumerable.ToArray(
					from m in SourceType.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)
					select new InternalWebMethodInfo
					{
						TypeFullName = SourceType.FullName,
						MetadataToken = m.MetadataToken.ToString("x8"),
						Name = m.Name,
						Parameters =
						Enumerable.ToArray(
							from p in m.GetParameters()
							select new InternalWebMethodParameterInfo
							{
								Name = p.Name,
								IsDelegate = p.ParameterType.IsDelegate()
							}
						)
					}
				);

				#region GetWebMethods
				var GetWebMethods = Global.DefineMethod("GetWebMethods",
					MethodAttributes.Virtual | MethodAttributes.Public, CallingConventions.Standard,
					TypeCache[typeof(InternalWebMethodInfo[])],
					null
				);

				GetWebMethods.GetILGenerator().EmitReturnSerializedArray(__WebMethods,
					TypeCache,
					ConstructorCache,
					FieldCache
				);
				#endregion

				#region GetScriptApplications
				var GetScriptApplications = Global.DefineMethod("GetScriptApplications",
					MethodAttributes.Virtual | MethodAttributes.Public, CallingConventions.Standard,
					TypeCache[typeof(InternalScriptApplication[])],
					null
				);

				#region References
				var References_ =
					jsc.Loader.LoaderStrategy.LoadReferencedAssemblies(Assembly.LoadFile(js_RewriteOutput.FullName), new[] { ScriptType.JavaScript })
								.Reverse()
								.Distinct();

				var References =
								References_
								.OrderByDescending(k => k.ToScriptAttributeOrDefault().IsCoreLib)
								.Select(k => Path.GetFileName(k.Location)).ToArray();
				#endregion

				GetScriptApplications.GetILGenerator().EmitReturnSerializedArray(
					new[]
				{
					// in the future we could enable multiple script applications
					// with different references...
					// each ScriptApplication could define a path from which it should run 
					// for now we assume single application and should show it in default path

					new InternalScriptApplication
					{
						TypeName = js_TargetType.Name,
						TypeFullName = js_TargetType.FullName,
						References = References.Select(k =>
							new InternalScriptApplication.Reference 
							{
								AssemblyFile = k
								// so what libraries are referenced in the IsJavascript product assembly?
							}
						).ToArray()
					}
				}
					,
					TypeCache,
					ConstructorCache,
					FieldCache
				);
				#endregion

				#region Global_Invoke
				var Global_Invoke = Global.DefineMethod("Invoke", MethodAttributes.Virtual | MethodAttributes.Public,
					CallingConventions.Standard,
					typeof(void), new[] { TypeCache[typeof(InternalWebMethodInfo)] }

				);

				{
					var il = Global_Invoke.GetILGenerator();

					var loc0 = il.DeclareInitializedLocal(TypeCache[SourceType], ConstructorCache[SourceType.GetConstructor()]);
					var loc1 = il.DeclareLocal(typeof(bool));

					foreach (var item in SourceType.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance))
					{
						il.Emit(OpCodes.Ldarg_1);
						il.Emit(OpCodes.Ldfld, FieldCache[typeof(InternalWebMethodInfo).GetField("MetadataToken")]);
						il.Emit(OpCodes.Ldstr, item.MetadataToken.ToString("x8"));
						il.Emit(OpCodes.Call, ((Func<string, string, bool>)string.Equals).Method);
						il.Emit(OpCodes.Stloc, (short)loc1.LocalIndex);

						var skip = il.DefineLabel();

						il.Emit(OpCodes.Ldloc, (short)loc1.LocalIndex);
						il.Emit(OpCodes.Brfalse, skip);

						var work = Global.DefineNestedType("<>" + item.MetadataToken.ToString("x8"),
							TypeAttributes.NestedPublic,
							TypeCache[typeof(InternalWebMethodWorker)]
						);

						var work_ctor = work.DefineDefaultConstructor(MethodAttributes.Public);

						var work_Results = FieldCache[typeof(InternalWebMethodWorker).GetField("Results")];

						var loc2 = il.DeclareInitializedLocal(work, work_ctor);


						il.Emit(OpCodes.Ldloc, (short)(loc0.LocalIndex));

						foreach (var p in item.GetParameters())
						{
							if (p.ParameterType.IsDelegate())
							{
								var p_Invoke = p.ParameterType.GetMethod("Invoke");

								var AddResult = work.DefineMethod(p.Name, MethodAttributes.Public, CallingConventions.Standard,
									typeof(void),
									p_Invoke.GetParameterTypes()
								);

								var AddResult_il = AddResult.GetILGenerator();

								var loc3 = AddResult_il.DeclareInitializedLocal(TypeCache[typeof(InternalWebMethodInfo)], ConstructorCache[typeof(InternalWebMethodInfo).GetConstructor()]);

								AddResult_il.Emit(OpCodes.Ldloc, (short)(loc3.LocalIndex));
								AddResult_il.Emit(OpCodes.Ldstr, p.Name);
								AddResult_il.Emit(OpCodes.Stfld, FieldCache[typeof(InternalWebMethodInfo).GetField("Name")]);

								foreach (var p_InvokeParameter in p_Invoke.GetParameters())
								{
									AddResult_il.Emit(OpCodes.Ldloc, (short)(loc3.LocalIndex));
									AddResult_il.Emit(OpCodes.Ldstr, p_InvokeParameter.Name);
									AddResult_il.Emit(OpCodes.Ldarg, (short)(p_InvokeParameter.Position + 1));
									AddResult_il.Emit(OpCodes.Call,
										MethodCache[
											(
												(Action<InternalWebMethodInfo, string, string>)InternalWebMethodInfo.AddParameter
											).Method
										]
									);
								}

								AddResult_il.Emit(OpCodes.Ldarg_0);
								AddResult_il.Emit(OpCodes.Ldloc, (short)(loc3.LocalIndex));
								AddResult_il.Emit(OpCodes.Call,
									MethodCache[((Action<InternalWebMethodWorker, InternalWebMethodInfo>)InternalWebMethodWorker.Add).Method]
								);

								AddResult_il.Emit(OpCodes.Ret);

								il.Emit(OpCodes.Ldloc, (short)(loc2.LocalIndex));
								il.Emit(OpCodes.Ldftn, AddResult);
								il.Emit(OpCodes.Newobj, ConstructorCache[p.ParameterType.GetConstructors().Single()]);
							}
							else
							{
								il.Emit(OpCodes.Ldarg_1);
								il.Emit(OpCodes.Ldstr, p.Name);
								il.Emit(OpCodes.Call, MethodCache[
									((Func<InternalWebMethodInfo, string, string>)InternalWebMethodInfo.GetParameterValue).Method
									]
								);
							}
						}

						il.Emit(OpCodes.Call, MethodCache[item]);

						il.Emit(OpCodes.Ldloc, (short)(loc2.LocalIndex));
						il.Emit(OpCodes.Ldarg_1);
						il.Emit(OpCodes.Call, MethodCache[
							((Action<InternalWebMethodWorker, InternalWebMethodInfo>)InternalWebMethodWorker.ApplyTo).Method
							]
						);



						il.Emit(OpCodes.Ret);

						il.MarkLabel(skip);




						work.CreateType();
					}



					il.Emit(OpCodes.Ret);
				}
				#endregion

				Global.CreateType();
				#endregion

				#region IsWebServiceJava
				if (IsWebServiceJava)
				{
					r.ExternalContext.TypeCache.Resolve +=
						__SourceType =>
						{
							if (__SourceType == typeof(TypelessImplementation1))
							{
								r.ExternalContext.TypeCache[__SourceType] = Global;
								return;
							}

						};

					r.ExternalContext.MethodCache.Resolve +=
						__SourceMethod =>
						{
							if (__SourceMethod.DeclaringType == typeof(TypelessImplementation1))
							{
								r.ExternalContext.MethodCache[__SourceMethod] = Application_BeginRequest;
								return;
							}
						};


					r.ExternalContext.ConstructorCache.Resolve +=
						__SourceConstructor =>
						{
							if (__SourceConstructor.DeclaringType == typeof(TypelessImplementation1))
							{
								r.ExternalContext.ConstructorCache[__SourceConstructor] = Global_ctor;
								return;
							}
						};

					var InternalHttpServlet = TypeCache[typeof(InternalHttpServlet)];
				}
				#endregion


				#region asp.net
				if (!IsWebServiceJava && !IsWebServicePHP)
				{
					//DirectoryInfo web = __VirtualTo.VirtualDirectory;

					foreach (var item in __Files2)
					{
						new FileInfo(Path.Combine(root.FullName, item.Name1)).Directory.Create();

						item.k.CopyTo(Path.Combine(root.FullName, item.Name1), true);
					}

					#region global_asax
					var global_asax = a.Module.DefineType("ASP.global_asax", TypeAttributes.Public, Global);

					var __initialized = global_asax.DefineField("__initialized", typeof(bool), FieldAttributes.Private | FieldAttributes.Static);

					var get_Profile = global_asax.DefineMethod("get_Profile", MethodAttributes.Family, CallingConventions.Standard, typeof(DefaultProfile), new Type[0]);

					{
						var il = get_Profile.GetILGenerator();

						il.Emit(OpCodes.Ldarg_0);


						il.Emit(OpCodes.Call, r.RewriteArguments.context.MethodCache[
							((Func<InternalGlobal, DefaultProfile>)InternalGlobalExtensions.InternalGetProfile).Method
						]);


						il.Emit(OpCodes.Ret);
					}


					var Profile = global_asax.DefineProperty("Profile", PropertyAttributes.None, typeof(DefaultProfile), null);

					Profile.SetGetMethod(get_Profile);

					global_asax.CreateType();
					#endregion

					if (this.DisableWebServiceTypeMerge)
					{
						// we are running under ASP.NET are we?

					}
					else
					{
						WriteWebDevLauncher(
							new FileInfo(
								Path.Combine(root.FullName, SourceType.Namespace + ".exe")
							)
						);

						#region DevServer
						File.WriteAllText(Path.Combine(root.CreateSubdirectory("bin").FullName, "App_global.asax.compiled"),
			@"<?xml version='1.0' encoding='utf-8'?>
<preserve resultType='8' virtualPath='/global.asax'  flags='150000' assembly='" + r.product + @"' type='ASP.global_asax'>
  <filedeps>
    <filedep name='/global.asax' />
  </filedeps>
</preserve>
");


						File.WriteAllText(Path.Combine(root.FullName, "web.config"),
			@"<?xml version='1.0'?>
<configuration>
	<system.web>
		<compilation debug='true'/>
  </system.web>
</configuration>
".Trim());

						File.WriteAllText(Path.Combine(root.FullName, "PrecompiledApp.config"), "<precompiledApp version='2' updatable='false'/>");
						File.WriteAllText(Path.Combine(root.FullName, "Default.htm"), "");


						#region staging.net.bat
						// now we can run the rewritten app in .net :)
						File.WriteAllText(
							Path.Combine(root.FullName,
								"WebDev.WebServer.bat"
								),
							@"call ""C:\Program Files\Common Files\Microsoft Shared\DevServer\9.0\WebDev.WebServer.exe"" /port:8081 /path:""" + __root.FullName + @""" /vpath:""/"""
						);
						#endregion

						#region staging.net.bat
						// now we can run the rewritten app in .net :)
						File.WriteAllText(
							Path.Combine(root.FullName,
								"WebDev.WebServer40.bat"
								),
							@"call ""C:\Program Files\Common Files\Microsoft Shared\DevServer\10.0\WebDev.WebServer40.exe"" /port:8081 /path:""" + __root.FullName + @""" /vpath:""/"""
						);
						#endregion
						#endregion

						// C:\Windows\assembly\GAC_32\WebDev.WebHost\9.0.0.0__b03f5f7f11d50a3a\WebDev.WebHost.dll
					}
				}
				#endregion

				if (IsWebServiceJava)
				{
					DirectoryInfo __web = root.CreateSubdirectory("web/www");

					using (var __web_virtual = __web.ToVirtualDrive())
					{
						var web = __web_virtual.VirtualDirectory;

						foreach (var item in __Files2)
						{
							var TargetFolder = Path.Combine(web.FullName, item.Name1);

							new FileInfo(Path.Combine(web.FullName, item.Name1)).Directory.Create();

							item.k.CopyTo(Path.Combine(web.FullName, item.Name1), true);
						}
					}

					#region ant_build_xml, run.bat, upload.bat
					{
						var ant_build_xml = XDocument.Load(
							XmlReader.Create(
								typeof(RewriteToJavaScriptDocument).Assembly.GetManifestResourceStream("jsc.meta.Tools.ant.GoogleAppEngine.build.xml")
							)
						);

						ant_build_xml.Root.AddFirst(new XComment("modified by jsc.meta"));

						var r_Output_web = r.Output.Directory.CreateSubdirectory("web");

						#region ant build
						// http://www.larswilhelmsen.com/2008/12/12/linq-to-xml-xpathselectelement-annoyance/
						// http://msdn.microsoft.com/en-us/library/bb341675.aspx
						var location = ((IEnumerable)ant_build_xml.XPathEvaluate("/project/property[@name='appengine.sdk']/@location")).Cast<XAttribute>().Single();

						if (this.appengine != null)
							location.Value = this.appengine.FullName;

						var ant_build_xml_file = Path.Combine(r_Output_web.FullName, "build.xml");
						ant_build_xml.Save(ant_build_xml_file);


						var proccess_ant_info = new ProcessStartInfo(
								this.ant.FullName,
								"-f build.xml"
								)
						{
							UseShellExecute = false,

							WorkingDirectory = r_Output_web.FullName
						};

						proccess_ant_info.EnvironmentVariables["JAVA_HOME"] = this.javahome.FullName;

						#region upload.bat
						File.WriteAllText(
							Path.Combine(r_Output_web.FullName, "build.bat"),
							@"
@echo off
echo current path cannot be very long...

set JAVA_HOME=" + this.javahome.FullName + @"
subst b: " + r_Output_web.FullName + @" 
b:
call " + this.ant.FullName + @" -f build.xml
subst b: /D
"
						);
						#endregion

						//InvokeAfterBackendCompiler(
						//    delegate
						//    {
						//        var proccess_ant = Process.Start(proccess_ant_info);

						//        proccess_ant.WaitForExit();
						//    }
						//);

						#endregion


						// ----

						#region run.bat
						var w = new StringWriter();

						w.WriteLine(@"
@echo off

echo killing all java in pure hope to terminate the servlet...
echo.
echo error is OK
echo.
taskkill /IM java.exe /F
taskkill /FI ""WINDOWTITLE eq volatile_dev_appserver*"" /F
start ""volatile_dev_appserver"" /MIN """ + this.appengine + @"\bin\dev_appserver.cmd"" www
");

						for (int i = 10; i >= 0; i--)
						{
							w.WriteLine(@"
echo waiting " + i + @" seconds for the server to load...
PING 1.1.1.1 -n 1 -w 1000 >NUL
");
						}


						w.WriteLine(@"
start ""web"" explorer ""http://localhost:8080/""
echo thanks! :)
");

						File.WriteAllText(
							Path.Combine(r_Output_web.FullName, "run.bat"),
							w.ToString()
						);
						#endregion

						//                        File.WriteAllText(
						//                            Path.Combine(r_Output_web.FullName, "build.run.bat"), @"
						//@echo off
						//call build.bat
						//call run.bat
						//"
						//                        );
						#region upload.bat
						File.WriteAllText(
							Path.Combine(r_Output_web.FullName, "upload.bat"),
							@"
@echo off
call """ + this.appengine + @"\bin\appcfg.cmd"" update www

"
						);
						#endregion

					}
					#endregion

				}
			}
		}


		public class WebServiceForJavaScript
		{
			public RewriteToAssembly r;
			public Type SourceType;

			public TypeBuilder DeclaringType;

			public void WriteType()
			{
				var Interfaces = SourceType.GetInterfaces().Select(k => r.RewriteArguments.context.TypeCache[k]).ToArray();

				this.DeclaringType = SourceType.IsNested ?
					((TypeBuilder)r.RewriteArguments.context.TypeCache[SourceType.DeclaringType]).DefineNestedType(
						SourceType.Name,
						SourceType.Attributes,
						null,
						Interfaces
					)


					: r.RewriteArguments.Module.DefineType(
						SourceType.FullName,
						SourceType.Attributes,
						null,
						Interfaces
				);

				r.ExternalContext.TypeCache[SourceType] = DeclaringType;


				foreach (var item in SourceType.GetNestedTypes())
				{
					var k = r.RewriteArguments.context.TypeCache[item];
				}

				foreach (var item in SourceType.GetConstructors())
				{
					var k = r.RewriteArguments.context.ConstructorCache[item];
				}



				foreach (var item in SourceType.GetMethods())
				{
					var k = r.RewriteArguments.context.MethodCache[item];
				}

				if (SourceType.IsNested)
				{
					r.TypeCreated +=
						e =>
						{

							if (e.SourceType == SourceType.DeclaringType)
							{
								DeclaringType.CreateType();

							}
						};
				}
				else
				{
					DeclaringType.CreateType();
				}
			}


			public void WriteMethod(MethodInfo SourceMethod)
			{

				var TypeCache = r.RewriteArguments.context.TypeCache;
				var FieldCache = r.RewriteArguments.context.FieldCache;
				var MethodCache = r.RewriteArguments.context.MethodCache;

				if (SourceMethod.ReturnType != typeof(void))
					throw new NotSupportedException();

				var m = this.DeclaringType.DefineMethod(
					SourceMethod.Name,
					SourceMethod.Attributes,
					SourceMethod.CallingConvention,
					TypeCache[SourceMethod.ReturnType],
					TypeCache[SourceMethod.GetParameterTypes()]
				);

				r.ExternalContext.MethodCache[SourceMethod] = m;

				var request = this.DeclaringType.DefineNestedType("<>" + SourceMethod.MetadataToken.ToString("x8"),
					TypeAttributes.NestedPublic,
					TypeCache[typeof(InternalWebMethodRequest)]
				);

				var request_ctor = request.DefineDefaultConstructor(MethodAttributes.Public);


				var request_delegates = Enumerable.ToDictionary(
					from p in SourceMethod.GetParameters()
					where p.ParameterType.IsDelegate()
					select new KeyValuePair<ParameterInfo, FieldBuilder>(p, request.DefineField(p.Name, TypeCache[p.ParameterType], FieldAttributes.Public))
					, k => k.Key, k => k.Value
				);


				var request_InvokeCallback = request.DefineMethod("InvokeCallback",
					MethodAttributes.Virtual | MethodAttributes.Public | MethodAttributes.HideBySig,
					CallingConventions.Standard,
					typeof(void),
					new[] { typeof(string), TypeCache[typeof(InternalWebMethodRequest.ParameterLookup)] }
				);

				{
					var il = request_InvokeCallback.GetILGenerator();

					var loc0 = il.DeclareLocal(typeof(bool));

					foreach (var p in SourceMethod.GetParameters())
					{
						if (p.ParameterType.IsDelegate())
						{
							il.Emit(OpCodes.Ldstr, p.Name);
							il.Emit(OpCodes.Ldarg_1);
							il.Emit(OpCodes.Call, ((Func<string, string, bool>)string.Equals).Method);
							il.Emit(OpCodes.Stloc_0);

							il.Emit(OpCodes.Ldloc_0);

							var skip = il.DefineLabel();
							il.Emit(OpCodes.Brfalse, skip);


							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Ldfld, request_delegates[p]);

							foreach (var pp in p.ParameterType.GetMethod("Invoke").GetParameters())
							{
								il.Emit(OpCodes.Ldarg_2);
								il.Emit(OpCodes.Ldstr, pp.Name);
								il.Emit(OpCodes.Call, MethodCache[typeof(InternalWebMethodRequest.ParameterLookup).GetMethod("Invoke")]);
							}

							il.Emit(OpCodes.Call, MethodCache[p.ParameterType.GetMethod("Invoke")]);
							il.Emit(OpCodes.Ret);

							il.MarkLabel(skip);
						}
					}

					il.Emit(OpCodes.Ret);
				}

				{
					var il = m.GetILGenerator();

					var loc0 = il.DeclareInitializedLocal(request, request_ctor);

					var request_MetadataToken = FieldCache[typeof(InternalWebMethodRequest).GetField("MetadataToken")];

					il.Emit(OpCodes.Ldloc, (short)(loc0.LocalIndex));
					il.Emit(OpCodes.Ldstr, SourceMethod.MetadataToken.ToString("x8"));
					il.Emit(OpCodes.Stfld, request_MetadataToken);

					foreach (var p in SourceMethod.GetParameters())
					{
						if (p.ParameterType.IsDelegate())
						{
							il.Emit(OpCodes.Ldloc, (short)(loc0.LocalIndex));
							il.Emit(OpCodes.Ldarg, (short)(p.Position + 1));
							il.Emit(OpCodes.Stfld, request_delegates[p]);
						}
						else
						{
							il.Emit(OpCodes.Ldloc, (short)(loc0.LocalIndex));
							il.Emit(OpCodes.Ldstr, p.Name);
							il.Emit(OpCodes.Ldarg, (short)(p.Position + 1));
							il.Emit(OpCodes.Call,
								MethodCache[((Action<InternalWebMethodRequest, string, string>)InternalWebMethodRequest.AddParameter).Method]

							);
						}
					}

					il.Emit(OpCodes.Ldloc, (short)(loc0.LocalIndex));
					il.Emit(OpCodes.Call,
						MethodCache[((Action<InternalWebMethodRequest>)InternalWebMethodRequest.Invoke).Method]

					);

					il.Emit(OpCodes.Ret);
				}

				request.CreateType();
			}
		}
	}



}

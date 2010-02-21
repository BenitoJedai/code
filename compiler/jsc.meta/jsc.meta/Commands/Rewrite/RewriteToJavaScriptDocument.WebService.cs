using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using java.applet;
using jsc.Languages.IL;
using jsc.meta.Library;
using jsc.meta.Library.Templates;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.external;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System.Web;
using jsc.meta.Commands.Rewrite.Templates;
using System.Web.Profile;
using System.Collections;
using ScriptCoreLib.CSharp.Extensions;
using jsc.meta.Library.Templates.Java;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;

namespace jsc.meta.Commands.Rewrite
{

	partial class RewriteToJavaScriptDocument
	{


		private void WriteGlobalApplication(
			RewriteToAssembly r,
			RewriteToAssembly.AssemblyRewriteArguments a,
			Type SourceType,
			DirectoryInfo web_bin,
			DirectoryInfo js_StagingFolder,
			Type js_TargetType,
			FileInfo js_RewriteOutput,
			bool IsWebServicePHP,
			bool IsWebServiceJava,

			Action<Action> InvokeAfterBackendCompiler

			)
		{

			var js_staging_web = js_StagingFolder.CreateSubdirectory("web");

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
			var __Files2 = __Files.Select(k => new
				{
					k,

					Name1 = k.FullName.Substring(js_staging_web.FullName.Length + 1),
					Name = k.FullName.Substring(js_staging_web.FullName.Length + 1).Replace("\\", "/")

				}

			).ToArray();

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
				DirectoryInfo web = web_bin.Parent;

				foreach (var item in __Files2)
				{
					new FileInfo(Path.Combine(web.FullName, item.Name1)).Directory.Create();

					item.k.CopyTo(Path.Combine(web.FullName, item.Name1), true);
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

				File.WriteAllText(Path.Combine(web_bin.FullName, "App_global.asax.compiled"),
	@"<?xml version='1.0' encoding='utf-8'?>
<preserve resultType='8' virtualPath='/global.asax'  flags='150000' assembly='" + r.product + @"' type='ASP.global_asax'>
  <filedeps>
    <filedep name='/global.asax' />
  </filedeps>
</preserve>
");


				File.WriteAllText(Path.Combine(web.FullName, "web.config"),
	@"<?xml version='1.0'?>
<configuration>
	<system.web>
		<compilation debug='true'/>
  </system.web>
</configuration>
".Trim());

				File.WriteAllText(Path.Combine(web.FullName, "PrecompiledApp.config"), "<precompiledApp version='2' updatable='false'/>");
				File.WriteAllText(Path.Combine(web.FullName, "Default.htm"), "");


				#region staging.net.bat
				// now we can run the rewritten app in .net :)
				File.WriteAllText(
					Path.Combine(web.FullName,
						"WebDev.WebServer.bat"
						),
					@"call ""C:\Program Files\Common Files\Microsoft Shared\DevServer\9.0\WebDev.WebServer.exe"" /port:8081 /path:""" + web.FullName + @""" /vpath:""/"""
				);
				#endregion
			}
			#endregion

			if (IsWebServiceJava)
			{
				DirectoryInfo web = web_bin.CreateSubdirectory("web/www");

				foreach (var item in __Files2)
				{
					new FileInfo(Path.Combine(web.FullName, item.Name1)).Directory.Create();

					item.k.CopyTo(Path.Combine(web.FullName, item.Name1), true);
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

					File.WriteAllText(
						Path.Combine(r_Output_web.FullName, "build.run.bat"), @"
@echo off
call build.bat
call run.bat
"
					);
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

	namespace Templates
	{
		public class InternalFileInfo
		{
			public string Name;
		}

		public class InternalWebMethodParameterInfo
		{
			public string Name;
			public string Value;

			public bool IsDelegate;
		}

		public class InternalWebMethodInfo
		{
			// whats up with const support jsc.meta?

			public static string QueryKey = "WebMethod";

			public string Name;
			public string TypeFullName;

			public string MetadataToken;


			public InternalWebMethodParameterInfo[] Parameters;

			public ArrayList InternalParameters;

			public static void AddParameter(InternalWebMethodInfo that, string Name, string Value)
			{
				if (that.InternalParameters == null)
					that.InternalParameters = new ArrayList();

				var n = new InternalWebMethodParameterInfo
				{
					Name = Name,
					Value = Value
				};

				that.InternalParameters.Add(n);

				that.Parameters = (InternalWebMethodParameterInfo[])that.InternalParameters.ToArray(typeof(InternalWebMethodParameterInfo));
			}

			public InternalWebMethodInfo[] Results;

			public string ToQueryString()
			{
				return "?" + QueryKey + "=" + MetadataToken;
			}

			public static InternalWebMethodInfo First(InternalWebMethodInfo[] e, string MetadataToken)
			{
				var k = default(InternalWebMethodInfo);

				if (!string.IsNullOrEmpty(MetadataToken))
					foreach (var item in e)
					{
						if (item.MetadataToken == MetadataToken)
						{
							k = item;
							break;
						}
					}

				return k;
			}

			public static string GetParameterValue(InternalWebMethodInfo that, string name)
			{
				var r = default(string);

				//Console.WriteLine("GetParameterValue: name: " + name);


				foreach (var item in that.Parameters)
				{
					//Console.WriteLine("GetParameterValue: item.name: " + item.Name);

					if (item.Name == name)
					{
						//Console.WriteLine("GetParameterValue: item.value: " + item.Value);

						r = item.Value;
						break;
					}
				}

				return r;
			}

			public void LoadParameters(HttpContext c)
			{
				foreach (var Parameter in this.Parameters)
				{
					if (Parameter.IsDelegate)
					{
					}
					else
					{
						//WriteFormKeysToConsole(c);

						// do we support null parameters?
						var value = "";

						//Console.WriteLine("LoadParameters: name: " + Parameter.Name);

						var key = "_" + this.MetadataToken + "_" + Parameter.Name;

						//Console.WriteLine("LoadParameters: key: " + key);
						var value_Form = c.Request.Form[key];

						if (null != value_Form)
							value = value_Form;


						//Console.WriteLine("LoadParameters: value: " + value);

						Parameter.Value = value;
					}
				}
			}

			private static void WriteFormKeysToConsole(HttpContext c)
			{
				foreach (var item in c.Request.Form.AllKeys)
				{
					Console.WriteLine("WriteFormKeysToConsole: existing key: " + item);
				}
			}
		}

		public delegate void StringAction(string e);

		public static class InternalGlobalExtensions
		{
			public static bool FileExists(InternalGlobal g)
			{
				var that = g.Application;

				bool x = false;
				foreach (var item in g.GetFiles())
				{
					if (that.Request.Path == "/" + item.Name)
					{
						x = true;
						break;
					}
				}
				return x;
			}

			static string escapeXML(string s)
			{
				return s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
			}

			public static void InternalApplication_BeginRequest(InternalGlobal g)
			{
				var that = g.Application;
				var Context = that.Context;

				if (InternalGlobalExtensions.FileExists(g))
				{
					// fake lag
					//if (that.Request.Path.EndsWith(".js"))
					//    System.Threading.Thread.Sleep(1000);

					return;
				}

				if (Context.Request.Path == "/favicon.ico")
				{
					Context.Response.Redirect("http://jsc.sf.net/favicon.ico");
					that.CompleteRequest();
					return;
				}

				if (Context.Request.Path == "/robots.txt")
				{
					Context.Response.StatusCode = 404;
					that.CompleteRequest();
					return;
				}

				if (Context.Request.Path == "/crossdomain.xml")
				{
					Context.Response.StatusCode = 404;
					that.CompleteRequest();
					return;
				}

				StringAction Write = Context.Response.Write;

				var WebMethods = g.GetWebMethods();

				Console.WriteLine();

				foreach (var item in WebMethods)
				{
					item.LoadParameters(that.Context);
				}

				if (Context.Request.HttpMethod == "POST")
				{
					var WebMethod = InternalWebMethodInfo.First(WebMethods, Context.Request.QueryString[InternalWebMethodInfo.QueryKey]);
					if (WebMethod == null)
					{
						Context.Response.StatusCode = 404;
						that.CompleteRequest();
						return;
					}

					g.Invoke(WebMethod);

					if (that.Context.Request.Path == "/xml")
					{
						WriteXDocument(g, Write, WebMethod);
						that.CompleteRequest();
						return;
					}

					that.Response.ContentType = "text/html";
					WriteDiagnosticsResults(Write, WebMethod);
					WriteDiagnostics(g, Write, WebMethods);
					that.CompleteRequest();
					return;

				}

				if (that.Request.Path == "/jsc")
				{
					that.Response.ContentType = "text/html";
					WriteDiagnostics(g, Write, WebMethods);
					that.CompleteRequest();
					return;
				}

				if (IsDefaultPath(that.Request.Path))
				{
					that.Response.ContentType = "text/html";

					var app = g.GetScriptApplications()[0];

					WriteScriptApplication(Write, app);

					that.CompleteRequest();
					return;
				}
				// we could invoke web service handler now?
				that.Response.Redirect("/jsc");
			}

			private static void WriteScriptApplication(StringAction Write, InternalScriptApplication app)
			{
				StringAction WriteLine = k => Write(k + Environment.NewLine);

				// this function is running in .net, google app engine java and php
				// this function is based on JavaScript.EntrypointProvider
				// we could show a cool loading animation?

				WriteLine(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">");
				WriteLine(@"<html>");
				WriteLine(@"<head>");
				WriteLine(@"<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">");
				WriteLine(@"<title>Loading...</title>");


				//WriteLine(@"<script></script>");
				WriteLine(@"</head>");
				WriteLine(@"<body style='margin: 0; overflow: hidden;'><noscript>ScriptApplication cannot run without JavaScript!</noscript>");

				// should we display custom logo?
				// only the first image will be fetched, then the script...
				//WriteLine(@"<div style='border-style: none; position: absolute; left: 50%; top: 50%;' >");
				//WriteLine(@"<img class='LoadingAnimation' src='/assets/ScriptCoreLib/jsc.png' title='jsc' style='border-style: none; margin-left: -48px; margin-top: -48px; ' /> ");
				//WriteLine(@"</div>");

				// http://www.ajaxload.info/
				WriteLine(@"<div style='border-style: none; position: absolute; left: 50%; top: 50%;' >");
				WriteLine(@"<img class='LoadingAnimation' src='/assets/ScriptCoreLib/loading.gif' title='loading...'  style='border-style: none; margin-left: -16px; margin-top: -16px; ' /> ");
				WriteLine(@"</div>");

				WriteLine(@"<script type='text/xml' class='" + app.TypeName + "'></script>");


				foreach (var item in app.References)
				{
					Write(@"<script type='text/javascript' src='" + item.AssemblyFile + @".js'></script>");

				}

				WriteLine(@"</body>");
				WriteLine(@"</html>");
			}

			public static bool IsDefaultPathOrSpecialPath(string e)
			{
				if (IsDefaultPath(e))
					return true;

				if (e == "/jsc")
					return true;

				if (e == "/xml")
					return true;

				return false;
			}
			public static bool IsDefaultPath(string e)
			{
				if (e == "/")
					return true;

				if (e == "/default.htm")
					return true;

				if (e == "/default.aspx")
					return true;

				return false;
			}

			private static void WriteDiagnosticsResults(StringAction Write, InternalWebMethodInfo WebMethod)
			{
				if (WebMethod.Results == null)
				{

					Write("<h2>No Results</h2>");
				}
				else
				{
					Write("<h2>" + WebMethod.Results.Length + " Results</h2>");

					foreach (var item in WebMethod.Results)
					{
						WriteWebMethod(Write, item,
							Parameter =>
							{
								if (Parameter == null)
									return;

								Write(" = '<code style='color: red'>" + escapeXML(Parameter.Value) + "</code>'");
							}
						);
					}

					Write("<br />");
				}
			}

			private static void WriteDiagnostics(InternalGlobal g, StringAction Write, InternalWebMethodInfo[] WebMethods)
			{
				var Context = g.Application.Context;


				Write("<a href='http://jsc-solutions.net'><img border='0' src='/assets/ScriptCoreLib/jsc.png' /></a>");

				Write("<h2>Special pages</h2>");

				Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' /> special page: " + "<a href='/robots.txt'>/robots.txt</a>");
				Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' /> special page: " + "<a href='/xml'>/xml</a>");
				Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' /> special page: " + "<a href='/crossdomain.xml'>/crossdomain.xml</a>");
				Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' /> special page: " + "<a href='/favicon.ico'>/favicon.ico</a>");
				Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' /> special page: " + "<a href='/jsc'>/jsc</a>");

				Write("<h2>WebMethods</h2>");



				foreach (var item in WebMethods)
				{
					WriteWebMethodForm(g, Write, item);
				}

				Write("<title>Powered by jsc: " + Context.Request.Path + "</title>");

				Write("<br /> HttpMethod : " + Context.Request.HttpMethod);

				Write("<h2>Form</h2>");
				foreach (var item in Context.Request.Form.AllKeys)
				{
					Write("<br /> " + "<img src='http://i.msdn.microsoft.com/w144atby.pubproperty(en-us,VS.90).gif' /> <code>");
					Write(item);
					Write(" = ");
					Write(escapeXML(Context.Request.Form[item]));
					Write("</code>");
				}

				Write("<h2>QueryString</h2>");
				foreach (var item in Context.Request.QueryString.AllKeys)
				{
					Write("<br /> " + "<img src='http://i.msdn.microsoft.com/w144atby.pubproperty(en-us,VS.90).gif' /> <code>");
					Write(item);
					Write(" = ");
					Write(escapeXML(Context.Request.QueryString[item]));
					Write("</code>");
				}

				Write("<h2>Script Applications</h2>");

				foreach (var item in g.GetScriptApplications())
				{
					Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' /> script application: " + item.TypeName);

					foreach (var r in item.References)
					{
						Write("<br /> &nbsp;&nbsp;&nbsp;&nbsp;");

						Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' /> reference: ");
						Write(r.AssemblyFile);

					}
				}

				Write("<h2>Files</h2>");

				foreach (var item in g.GetFiles())
				{
					Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' />" + " file: <a href='" + item.Name + "'>" + item.Name + "</a>");
				}



			}

			private static void WriteXDocument(InternalGlobal g, StringAction Write, InternalWebMethodInfo WebMethod)
			{
				var that = g.Application;
				var Context = that.Context;

				Context.Response.ContentType = "text/xml";

				Write("<document>");

				if (WebMethod.Results != null)
					foreach (var item in WebMethod.Results)
					{
						Write("<" + item.Name + ">");

						foreach (var p in item.Parameters)
						{
							Write("<" + p.Name + ">");
							Write(escapeXML(p.Value));
							Write("</" + p.Name + ">");

						}

						Write("</" + item.Name + ">");

					}

				Write("</document>");

				that.CompleteRequest();
			}

			private static void WriteWebMethodForm(InternalGlobal that, StringAction Write, InternalWebMethodInfo WebMethod)
			{
				Write("<form target='_blank' action='" + WebMethod.ToQueryString() + "' method='POST'>");
				WriteWebMethod(Write, WebMethod,
					Parameter =>
					{
						if (Parameter == null)
						{
							Write("<input type='submit' value='Invoke'  />");

							return;
						}

						var key = "_" + WebMethod.MetadataToken + "_" + Parameter.Name;

						Write(" = ");
						Write("<input type='text'  name='" + key + "' value='" + Parameter.Value.Replace("'", "&apos;") + "' />");
					}
				);
				Write("</form>");
			}

			public delegate void InternalWebMethodParameterInfoAction(InternalWebMethodParameterInfo p);

			private static void WriteWebMethod(StringAction Write, InternalWebMethodInfo item, InternalWebMethodParameterInfoAction more)
			{
				if (string.IsNullOrEmpty(item.MetadataToken))
				{
					Write("<br /> ");
					Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubdelegate(en-us,VS.90).gif' />");
					Write(" method: <code>" + item.Name + "</code>");

				}
				else
				{
					Write("<br /> <img src='http://i.msdn.microsoft.com/deshae98.pubmethod(en-us,VS.90).gif' /> method: <code><a href='" + item.ToQueryString() + "'>" + item.Name + "</a></code>");
				}

				if (more != null)
					more(null);

				if (item.Parameters != null)
					foreach (var p in item.Parameters)
					{
						Write("<br /> &nbsp;&nbsp;&nbsp;&nbsp;");

						if (p.IsDelegate)
						{
							Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubdelegate(en-us,VS.90).gif' />");
							Write(" parameter: <code>" + p.Name + "</code>");


						}
						else
						{
							Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' />");
							Write(" parameter: <code>" + p.Name + "</code>");

							if (more != null)
								more(p);

						}

					}


			}

	

			public static DefaultProfile InternalGetProfile(InternalGlobal g)
			{
				var that = g.Application;
				return (DefaultProfile)that.Context.Profile;
			}

		}


		public abstract class InternalGlobal : HttpApplication
		{
			HttpApplication InternalApplicationOverride;
			public HttpApplication Application
			{
				get
				{
					if (InternalApplicationOverride != null)
						return InternalApplicationOverride;

					return this;
				}
			}

			public void SetApplication(HttpApplication value)
			{
				this.InternalApplicationOverride = value;
			}


			public bool FileExists()
			{
				return InternalGlobalExtensions.FileExists(this);
			}

			public abstract InternalFileInfo[] GetFiles();

			public abstract InternalWebMethodInfo[] GetWebMethods();

			public abstract void Invoke(InternalWebMethodInfo e);

			public abstract InternalScriptApplication[] GetScriptApplications();
		}

		public class InternalWebMethodWorker
		{
			public readonly ArrayList Results = new ArrayList();

			public static void Add(InternalWebMethodWorker that, InternalWebMethodInfo value)
			{
				that.Results.Add(value);
			}

			public InternalWebMethodInfo[] ToArray()
			{
				return (InternalWebMethodInfo[])this.Results.ToArray(typeof(InternalWebMethodInfo));
			}

			public static void ApplyTo(InternalWebMethodWorker that, InternalWebMethodInfo target)
			{
				target.Results = that.ToArray();
			}
		}

		public abstract class InternalWebMethodRequest
		{
			public string MetadataToken;

			public string Data;

			public static void AddParameter(InternalWebMethodRequest that, string name, string value)
			{
				if (null == value)
					return;

				if (string.IsNullOrEmpty(that.Data))
				{
					that.Data = "";
				}
				else
				{
					that.Data += "&";
				}

				that.Data += "_" + that.MetadataToken + "_" + name + "=" + Native.Window.escape(value);
			}

			public static void Invoke(InternalWebMethodRequest that)
			{
				var x = new IXMLHttpRequest();

				x.open(ScriptCoreLib.Shared.HTTPMethodEnum.POST, "/xml?WebMethod=" + that.MetadataToken);
				x.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

				x.send(that.Data);

				x.InvokeOnComplete(that.Complete, 50);

			}

			public void Complete(IXMLHttpRequest r)
			{
				var xml = r.responseXML;

				foreach (var item in xml.documentElement.childNodes)
				{
					//Debugger.Break();

					//Native.Window.alert("callback: " + item.nodeName);

					InvokeCallback(item.nodeName,
						x =>
						{
							//Native.Window.alert("parameter: " + x);

							var u = default(string);

							foreach (var p in item.childNodes)
							{
								if (p.nodeName == x)
								{
									u = p.text;
									break;
								}
							}

							return u;
						}
					);

					//new IHTMLDiv { innerText = "callback: " + item.nodeName }.AttachToDocument();

					//foreach (var p in item.childNodes)
					//{
					//    new IHTMLDiv { innerText = "parameter: " + p.nodeName + " = " + p.text }.AttachToDocument();

					//}
				}
			}

			public delegate string ParameterLookup(string parameter);

			public virtual void InvokeCallback(string name, ParameterLookup lookup)
			{
				throw new Exception("InvokeCallback");
			}
		}

		public class InternalScriptApplication
		{
			public string TypeName;
			public string TypeFullName;

			public class Reference
			{
				public string AssemblyFile;
			}

			public Reference[] References;
		}
	}

}

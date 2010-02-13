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
			bool IsWebServicePHP,
			bool IsWebServiceJava
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


			#region Application_BeginRequest
			var Application_BeginRequest = Global.DefineMethod("Application_BeginRequest", MethodAttributes.Public,
				CallingConventions.Standard, typeof(void),
				new[] { typeof(object), typeof(EventArgs) }
			);

			{
				var il = Application_BeginRequest.GetILGenerator();

				var __WebService = il.DeclareInitializedLocal(TypeCache[SourceType]);

				il.Emit(OpCodes.Ldarg_0);



				il.Emit(OpCodes.Call, r.RewriteArguments.context.MethodCache[
					((Action<InternalGlobal>)InternalGlobal.InternalApplication_BeginRequest).Method
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

			Global.CreateType();
			#endregion

			#region asp.net
			if (!(IsWebServiceJava || IsWebServicePHP))
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
						((Func<InternalGlobal, DefaultProfile>)InternalGlobal.InternalGetProfile).Method
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
						SourceType.FullName,
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

				m.NotImplemented();
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

				foreach (var item in that.Parameters)
				{
					if (item.Name == name)
					{
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
						var value = "";
						var key = "_" + this.MetadataToken + "_" + Parameter.Name;

						var value_Form = c.Request.Form[key];
						if (value_Form != null)
							value = value_Form;

						Parameter.Value = value;
					}
				}
			}
		}

		public abstract class InternalGlobal : HttpApplication
		{
			public delegate void StringAction(string e);

			public static void InternalApplication_BeginRequest(InternalGlobal that)
			{
				if (that.Request.Path == "/favicon.ico")
				{
					that.Response.Redirect("http://jsc.sf.net/favicon.ico");
					that.CompleteRequest();
					return;
				}

				if (FileExists(that))
					return;

				that.Response.ContentType = "text/html";

				StringAction Write = that.Response.Write;

				var WebMethods = that.GetWebMethods();

				foreach (var item in WebMethods)
				{
					item.LoadParameters(that.Context);
				}

				if (that.Request.HttpMethod == "POST")
				{
					var WebMethod = InternalWebMethodInfo.First(WebMethods, that.Request.QueryString[InternalWebMethodInfo.QueryKey]);
					if (WebMethod != null)
					{
						that.Invoke(WebMethod);

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

										Write(" = '<code>" + Parameter.Value + "</code>'");
									}
								);
							}
						}

						//WebMethod.
					}

				}



				Write("<h2>WebMethods</h2>");

				foreach (var item in WebMethods)
				{
					WriteWebMethodForm(that, Write, item);
				}

				Write("<title>Powered by jsc: " + that.Request.Path + "</title>");

				Write("<br /> HttpMethod : " + that.Request.HttpMethod);


				Write("<h2>Files</h2>");

				foreach (var item in that.GetFiles())
				{
					Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' />" + " file: <a href='" + item.Name + "'>" + item.Name + "</a>");
				}

				Write("<h2>Form Keys</h2>");

				foreach (var item in that.Request.Form.Keys)
				{
					Write("<br />  " + (string)item);

				}

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

			private static bool FileExists(InternalGlobal that)
			{
				bool x = false;
				foreach (var item in that.GetFiles())
				{
					if (that.Request.Path == "/" + item.Name)
					{
						x = true;
						break;
					}
				}
				return x;
			}

			public static DefaultProfile InternalGetProfile(InternalGlobal that)
			{
				return (DefaultProfile)that.Context.Profile;
			}

			public abstract InternalFileInfo[] GetFiles();

			public abstract InternalWebMethodInfo[] GetWebMethods();

			public abstract void Invoke(InternalWebMethodInfo e);

			//public abstract string GetApplicationSource();
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
	}

}

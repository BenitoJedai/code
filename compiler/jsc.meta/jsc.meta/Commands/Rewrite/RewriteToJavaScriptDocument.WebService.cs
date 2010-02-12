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

namespace jsc.meta.Commands.Rewrite
{
	namespace Templates
	{
		public class InternalFileInfo
		{
			public string Name;
		}


		public abstract class InternalGlobal : HttpApplication
		{

			public static void InternalApplication_BeginRequest(InternalGlobal that)
			{
				var x = false;
				foreach (var item in that.GetFiles())
				{
					if (that.Request.Path == "/" + item.Name)
					{
						x = true;
						break;
					}
				}
				if (x)
					return;

				that.Response.ContentType = "text/html";
				that.Response.Write("Powered by jsc: " + that.Request.Path);

				foreach (var item in that.GetFiles())
				{
					that.Response.Write("<br /> file: <a href='" + item.Name + "'>" + item.Name + "</a>");

				}
				that.CompleteRequest();
			}

			public static DefaultProfile InternalGetProfile(InternalGlobal that)
			{
				return (DefaultProfile)that.Context.Profile;
			}

			public abstract InternalFileInfo[] GetFiles();


			//public abstract string GetApplicationSource();
		}
	}

	partial class RewriteToJavaScriptDocument
	{
	

		private void WriteGlobalApplication(
			RewriteToAssembly r,
			RewriteToAssembly.AssemblyRewriteArguments a,
			Type type,
			DirectoryInfo web,
			DirectoryInfo web_bin,
			DirectoryInfo js_StagingFolder,
			Type js_TargetType)
		{
			var js_staging_web = js_StagingFolder.CreateSubdirectory("web");

			var TypeCache = r.RewriteArguments.context.TypeCache;
			var ConstructorCache = r.RewriteArguments.context.ConstructorCache;
			var FieldCache = r.RewriteArguments.context.FieldCache;

			#region Global
			var GlobalFullName = type.Namespace + ".Global";

			var Global = a.Module.DefineType(GlobalFullName,
				TypeAttributes.Public,
				TypeCache[typeof(InternalGlobal)],
				new Type[0]
			);


			var Application_BeginRequest = Global.DefineMethod("Application_BeginRequest", MethodAttributes.Public,
				CallingConventions.Standard, typeof(void),
				new[] { typeof(object), typeof(EventArgs) }
			);

			{
				var il = Application_BeginRequest.GetILGenerator();

				var __WebService = il.DeclareInitializedLocal(TypeCache[type]);

				il.Emit(OpCodes.Ldarg_0);



				il.Emit(OpCodes.Call, r.RewriteArguments.context.MethodCache[
					((Action<InternalGlobal>)InternalGlobal.InternalApplication_BeginRequest).Method
				]);


				il.Emit(OpCodes.Ret);
			}

			var GetFiles = Global.DefineMethod("GetFiles",
				MethodAttributes.Virtual | MethodAttributes.Public, CallingConventions.Standard, TypeCache[typeof(InternalFileInfo[])],
				null
			);

			var __Files = js_staging_web.GetFilesByPattern("*.js", "*.htm").Concat(js_staging_web.CreateSubdirectory("assets").GetFiles("*.*", SearchOption.AllDirectories));
			var __Files2 = __Files.Select(k => new 
				{ k, 
					
					Name1 = k.FullName.Substring(js_staging_web.FullName.Length + 1),
					Name = k.FullName.Substring(js_staging_web.FullName.Length + 1).Replace("\\", "/") 
				
				}
				
			).ToArray();

			var __Files1 = __Files2.Select(k => new InternalFileInfo { Name = k.Name }).ToArray();

			GetFiles.GetILGenerator().EmitReturnSerializedArray(__Files1,
				TypeCache,
				ConstructorCache,
				FieldCache
			);

			foreach (var item in __Files2)
			{
				new FileInfo(Path.Combine(web.FullName, item.Name1)).Directory.Create();

				item.k.CopyTo(Path.Combine(web.FullName, item.Name1), true);
			}




			Global.CreateType();
			#endregion


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
}

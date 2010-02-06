using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Library;
using jsc.Library;
using System.Xml.Linq;
using jsc.Languages.IL;
using System.Reflection.Emit;
using ScriptCoreLib;
using System.Xml;
using System.Collections;
using System.Xml.XPath;

namespace jsc.meta.Commands.Extend
{
	[Description("An example how ASP.NET web application could be translated to Google App Engine")]
	public class ExtendToGoogleAppEngineWebApplication
	{
		// rename to: ExtendToMetaWebApplication?
		// rename to: RewriteToMetaWebApplication?

		// examples:
		// C:\work\jsc.svn\examples\java\WebApplication1\WebApplication1\tools
		// usage:
		// c:\util\jsc\bin\jsc.meta.exe ExtendToGoogleAppEngineWebApplication /project:"$(ProjectPath)"
		// ExtendToGoogleAppEngineWebApplication /project:"C:\work\jsc.svn\templates\Orcas\OrcasMetaWebApplication\OrcasMetaWebApplication\OrcasMetaWebApplication.csproj"

		public FileInfo msbuild = new FileInfo(@"C:\Windows\Microsoft.NET\Framework\v3.5\msbuild.exe");
		public FileInfo project;
		public FileInfo aspnet_compiler = new FileInfo(@"C:\Windows\Microsoft.NET\Framework\v2.0.50727\aspnet_compiler.exe");

		public string application = "jsc-project";
		public string version = "3";

		// eAppEngineWebService /assembly:"$(TargetPath)" /application:"jsc-project" /version:"5" /ant:"C:\util\apache-ant-1.7.1\bin\ant.bat" /javahome:"C:\Program Files\Java\jdk1.6.0_14" /appengine:"C:\util\appengine-java-sdk-1.3.0" /staging:"staging.java"

		public DirectoryInfo javahome = new DirectoryInfo(@"C:\Program Files\Java\jdk1.6.0_14");
		public FileInfo ant = new FileInfo(@"C:\util\apache-ant-1.7.1\bin\ant.bat");
		public DirectoryInfo appengine = new DirectoryInfo(@"C:\util\appengine-java-sdk-1.3.0");


		public void Invoke()
		{
			// staging1 = before asp.net
			// staging2 = after asp.net
			// staging3 = merged and rewritten

			#region staging.net
			var staging1 = project.Directory.CreateSubdirectory("bin/staging.msbuild");
			var staging2 = project.Directory.CreateSubdirectory("bin/staging.aspnet");
			var staging3 = project.Directory.CreateSubdirectory("bin/staging.merge");
			var staging_net = project.Directory.CreateSubdirectory("bin/staging.net");
			var staging_net_bin = staging_net.CreateSubdirectory("bin");


			#region msbuild
			{
				var i = new ProcessStartInfo(
						this.msbuild.FullName,
						@"/t:_CopyWebApplication /property:OutDir=" + project.Directory.FullName + @"\bin\publish\ /property:WebProjectOutputDir=" + staging1.FullName + @"\ " + project.Name
					)
				{
					UseShellExecute = false,

					WorkingDirectory = project.Directory.FullName
				};


				var p = Process.Start(i);

				p.WaitForExit();
			}
			#endregion


			#region aspnet_compiler
			{
				var i = new ProcessStartInfo(
						this.aspnet_compiler.FullName,
						@"-v / -p """ + staging1.FullName + @""" -f """ + staging2.FullName + @""""
						)
				{
					UseShellExecute = false,

					WorkingDirectory = project.Directory.FullName
				};


				var p = Process.Start(i);

				p.WaitForExit();
			}
			#endregion

			var staging2bin = staging2.CreateSubdirectory("bin");

			// we get the suffix for free! :D

			// xxx.csproj
			var rewrite_product = project.Name;

			// xxx.csproj.dll
			var rewrite_Output = default(FileInfo);

			var preserve = new List<Preserve>();

			#region load preserve
			{


				foreach (var item in staging2bin.GetFiles("*.compiled"))
				{
					var compiled = XDocument.Load(item.FullName);

					// remember the virtual urls!!

					preserve.Add(
						new Preserve
						{
							type = compiled.Root.Attribute("type").Value,
							virtualPath = compiled.Root.Attribute("virtualPath").Value
						}
					);


				}
			}
			#endregion


			#region lets find out our main target
			{
				var targets = Enumerable.ToArray(
					from f in Directory.GetFiles(staging2.FullName + @"\bin", "*.dll")
					select new { f = new FileInfo(f), a = Assembly.LoadFile(f) }
				);

				var targets2 = Enumerable.ToArray(
					from k in targets
					let r = Enumerable.ToArray(
						from kr in k.a.GetReferencedAssemblies()
						let x = targets.FirstOrDefault(xr => xr.a.GetName().Name == kr.Name)
						where x != null
						select x.a
					)
					select new { r, k.a, k.f }
				);

				var target = Enumerable.Single(
					from k in targets2
					where !targets2.Any(kk => kk.r.Contains(k.a))
					select k
				);

				var target2 = Enumerable.Single(
					from k in targets2
					where k.r.Length == 0
					select k
				);

				//Console.WriteLine(target.f.FullName);

				var staging = staging3;

				// okay now lets rewrite the primary webservice and add data for jsc
				var rewrite = new RewriteToAssembly
				{
					assembly = target.f,
					staging = staging,

					// at this time we can focus only on the first webservice and consider it as primary
					PrimaryTypes = target.a.GetTypes().Concat(target2.a.GetTypes()).Where(kk => kk.IsPublic).ToArray(),

					product = rewrite_product,


					merge = Enumerable.ToArray(
						// merge with other ASP.NET user libraries
						from k in targets
						where k.a != target.a
						select (RewriteToAssembly.MergeInstruction)k.a.GetName().Name
					),


					BeforeInstructions =
						e =>
						{
							if (e.SourceType.BaseType == typeof(System.Web.HttpApplication))
							{
								if (e.SourceMethod.Name == "Application_BeginRequest")
								{
									WriteGeneratedHandler(e);
								}
							}

							if (e.SourceType == typeof(PreserveInformation))
							{
								var il = e.GetILGenerator();

								il.EmitReturnSerializedArray(preserve.ToArray(),
									e.context.TypeCache,
									e.context.ConstructorCache,
									e.context.TypeFieldCacheFunc
								);
							}
						}
				};

				rewrite.Invoke();

				rewrite_Output = rewrite.Output;

			}
			#endregion


			#region run the merged asp.net app
			{
				foreach (var item in staging2.GetFilesByPattern(
					"*.aspx", "*.config", "*.htm", "*.ashx", "*.asmx"
					)
				)
				{
					item.CopyTo(Path.Combine(staging_net.FullName, item.Name), true);
				}


				rewrite_Output.CopyTo(Path.Combine(staging_net_bin.FullName, rewrite_Output.Name), true);

				foreach (var item in staging2bin.GetFiles("*.compiled"))
				{
					var compiled = XDocument.Load(item.FullName);

					compiled.Root.Attribute("assembly").Value = rewrite_product;

					compiled.Save(Path.Combine(staging_net_bin.FullName, item.Name));
				}

				// ready for jsc...

			}
			#endregion

			#region staging.net.bat
			// now we can run the rewritten app in .net :)
			File.WriteAllText(
				Path.Combine(project.Directory.CreateSubdirectory("bin").FullName,
					staging_net.Name + ".bat"
					),
				@"call ""C:\Program Files\Common Files\Microsoft Shared\DevServer\9.0\WebDev.WebServer.exe"" /port:8081 /path:""" + staging_net.FullName + @""" /vpath:""/"""
			);
			#endregion

			#endregion


			var rewrite_assembly = Assembly.LoadFile(rewrite_Output.FullName);
			var rewrtie_PrimartTypes = preserve.Select(k => rewrite_assembly.GetType(k.type)).ToArray();

			#region staging_java
			var staging_java = project.Directory.CreateSubdirectory("bin/staging.java");

			{
				// we need to copy referenced assemblies to staging folder
				staging_java.DefinesTypes(
					typeof(ScriptCoreLib.ScriptAttribute),
					typeof(ScriptCoreLibJava.IAssemblyReferenceToken),
					typeof(ScriptCoreLibJava.Web.Services.IAssemblyReferenceToken)
				);

				var r = new RewriteToAssembly
				{
					assembly = rewrite_Output,
					staging = staging_java,

					PrimaryTypes = 
						//rewrtie_PrimartTypes.Concat(
						new[] { typeof(InternalHttpServlet) }
					//).ToArray()
					,

					product = rewrite_product,

					PostRewrite =
						a =>
						{
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

							var res = new ScriptResourceWriter(a.Assembly, a.Module)
							{
								#region appengine-web.xml
								{
									"java/WEB-INF/appengine-web.xml",

									new XElement(xmlns.appengine + "appengine-web-app",
										new XElement(xmlns.appengine + "application", this.application),
										new XElement(xmlns.appengine + "version", this.version)
									)
								},
								#endregion
								#region web.xml
								{
									"java/WEB-INF/web.xml",

									new XElement(xmlns.javaee + "web-app",
										new [] {
											new XElement(xmlns.javaee + "display-name", this.application),

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

					var proccess_ant = Process.Start(proccess_ant_info);

					proccess_ant.WaitForExit();


					// ----

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

					File.WriteAllText(
						Path.Combine(r_Output_web.FullName, "upload.bat"),
						@"
@echo off
call """ + this.appengine + @"\bin\appcfg.cmd"" update www

"
					);
				}
				#endregion

			}
			#endregion


			var staging_php = project.Directory.CreateSubdirectory("bin/staging.php");
		}



		private void WriteGeneratedHandler(RewriteToAssembly.BeforeInstructionsArguments e)
		{
			var u = e.Type.DefineMethod("<>" + e.SourceMethod.Name, e.SourceMethod.Attributes, e.SourceMethod.CallingConvention, e.SourceMethod.ReturnType, e.SourceMethod.GetParameterTypes());

			{
				// so this is how we copy the IL ?
				var il = u.GetILGenerator();

				e.SourceMethod.EmitTo(il,
					RewriteToAssembly.CreateMethodBaseEmitToArguments(
						e.SourceMethod,
						e.context.TypeCache,
						e.context.TypeFieldCache,
						e.context.ConstructorCache,
						e.context.MethodCache,
						null,
						null,
						e.SourceMethod.GetMethodBody().ExceptionHandlingClauses.ToArray()
					)
				);
			}



			{
				var il = e.GetILGenerator();

				var _1_Application_BeginRequest = typeof(TypelessImplementation1).GetMethod("_1_Application_BeginRequest");
				var _0_Application_BeginRequest = typeof(TypelessImplementation1).GetMethod("_0_Application_BeginRequest");


				var il_a = RewriteToAssembly.CreateMethodBaseEmitToArguments(
						e.SourceMethod,
						e.context.TypeCache,
						e.context.TypeFieldCache,
						e.context.ConstructorCache,
						e.context.MethodCache,
						null,
						null,
						e.SourceMethod.GetMethodBody().ExceptionHandlingClauses.ToArray()
					);

				il_a.TranslateTargetMethod = k => k == _1_Application_BeginRequest ? u : e.context.MethodCache[k];

				_0_Application_BeginRequest.EmitTo(il, il_a);
			}
		}

	}

	public class Preserve
	{
		public string virtualPath;
		public string type;
	}


	internal static class PreserveInformation
	{
		public static Preserve[] GetCurrent()
		{
			return new[] { new Preserve { type = "x", virtualPath = "/x" } };
		}
	}

	abstract class TypelessImplementation1 : System.Web.HttpApplication
	{
		public void _1_Application_BeginRequest(object sender, EventArgs e)
		{

		}

		public void _0_Application_BeginRequest(object sender, EventArgs e)
		{
			if (this.Request.Path == "/jsc")
			{
				var w = new StringBuilder();

				w.AppendLine("jsc!! @ " + this.Request.Path + "?" + this.Request.QueryString);

				var i = PreserveInformation.GetCurrent();

				w.AppendLine("length: " + i.Length);

				foreach (var item in i)
				{
					w.AppendLine("<p><a href='" + item.virtualPath + "'>" + item.type + "</a></p>");
				}

				this.Response.Write(w.ToString());

				this.Response.StatusCode = 200;
				this.Response.ContentType = "text/html";

				this.CompleteRequest();
				return;
			}

			if (this.Request.Path == "/jsc-solutions")
			{
				this.Response.Redirect("http://jsc-solutions.net");

				this.CompleteRequest();
				return;
			}

			_1_Application_BeginRequest(sender, e);
		}
	}

	public class InternalHttpServlet : javax.servlet.http.HttpServlet
	{
		// this class is a template
		// this class cannot be used in .net
		// this could be defined in ScriptCoreLib.Ultra

		protected override void doPost(javax.servlet.http.HttpServletRequest req, javax.servlet.http.HttpServletResponse resp)
		{
			InternalInvokeWebService(req, resp);
		}

		protected override void doGet(javax.servlet.http.HttpServletRequest req, javax.servlet.http.HttpServletResponse resp)
		{
			InternalInvokeWebService(req, resp);
		}

		private void InternalInvokeWebService(javax.servlet.http.HttpServletRequest req, javax.servlet.http.HttpServletResponse resp)
		{
			try
			{
				resp.setContentType("text/html; charset=utf-8");

				var Content = "Hello World";

				resp.getWriter().println(Content);

				resp.getWriter().flush();
			}
			catch (csharp.ThrowableException exc)
			{
				//Console.WriteLine("error!");
				((java.lang.Throwable)(object)exc).printStackTrace();
			}
		}

	}

}

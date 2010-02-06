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

namespace jsc.meta.Commands.Extend
{
	[Description("An example how ASP.NET web application could be translated to Google App Engine")]
	public class ExtendToGoogleAppEngineWebApplication
	{
		// examples:
		// C:\work\jsc.svn\examples\java\WebApplication1\WebApplication1\tools
		// usage:
		// c:\util\jsc\bin\jsc.meta.exe ExtendToGoogleAppEngineWebApplication /project:"$(ProjectPath)"
		// ExtendToGoogleAppEngineWebApplication /project:"C:\work\jsc.svn\templates\Orcas\OrcasMetaWebApplication\OrcasMetaWebApplication\OrcasMetaWebApplication.csproj"

		public FileInfo msbuild = new FileInfo(@"C:\Windows\Microsoft.NET\Framework\v3.5\msbuild.exe");
		public FileInfo project;
		public FileInfo aspnet_compiler = new FileInfo(@"C:\Windows\Microsoft.NET\Framework\v2.0.50727\aspnet_compiler.exe");

		public void Invoke()
		{
			// staging1 = before asp.net
			// staging2 = after asp.net
			// staging3 = merged and rewritten
			// staging4 = run on asp.net

			var staging1 = project.Directory.CreateSubdirectory("bin/staging1");
			var staging2 = project.Directory.CreateSubdirectory("bin/staging2");
			var staging2bin = staging2.CreateSubdirectory("bin");
			var staging3 = project.Directory.CreateSubdirectory("bin/staging3");
			var staging4 = project.Directory.CreateSubdirectory("bin/staging.net");
			var staging4bin = staging4.CreateSubdirectory("bin");

			var staging_java = project.Directory.CreateSubdirectory("bin/staging.java");
			var staging_php = project.Directory.CreateSubdirectory("bin/staging.php");


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

			// we get the suffix for free! :D

			// xxx.csproj
			var product = project.Name;

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

					product = product,


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
					item.CopyTo(Path.Combine(staging4.FullName, item.Name), true);
				}


				rewrite_Output.CopyTo(Path.Combine(staging4bin.FullName, rewrite_Output.Name), true);

				foreach (var item in staging2bin.GetFiles("*.compiled"))
				{
					var compiled = XDocument.Load(item.FullName);

					compiled.Root.Attribute("assembly").Value = product;

					compiled.Save(Path.Combine(staging4bin.FullName, item.Name));
				}

				// ready for jsc...

			}
			#endregion

			#region staging4.run.bat
			File.WriteAllText(
				Path.Combine(project.Directory.CreateSubdirectory("bin").FullName,
					staging4.Name + ".bat"
					), 
				@"call ""C:\Program Files\Common Files\Microsoft Shared\DevServer\9.0\WebDev.WebServer.exe"" /port:8081 /path:""" + staging4.FullName + @""" /vpath:""/"""
			);
			#endregion


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

}

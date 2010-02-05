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

using System.Xml.Linq;

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
			var staging4 = project.Directory.CreateSubdirectory("bin/staging4");
			var staging4bin = staging4.CreateSubdirectory("bin");

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

					#region if we are going to inject code from jsc we need to copy it
					// obfuscation merge=?
					rename = new RewriteToAssembly.NamespaceRenameInstructions[] {
						"jsc.meta->" + Path.GetFileNameWithoutExtension(target.f.Name),
						"jsc->" +  Path.GetFileNameWithoutExtension(target.f.Name),
					},

					merge = new RewriteToAssembly.MergeInstruction[] {
						"jsc.meta",
						"jsc"
					}.Concat(
						// merge with other ASP.NET user libraries
						from k in targets
						where k.a != target.a
						select (RewriteToAssembly.MergeInstruction)k.a.GetName().Name
					).ToArray(),
					#endregion

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

					// remember the virtual urls!!

					compiled.Root.Attribute("assembly").Value = product;

					compiled.Save(Path.Combine(staging4bin.FullName, item.Name));
				}

				// ready for jsc...

			}
			#endregion

		}


	}
}

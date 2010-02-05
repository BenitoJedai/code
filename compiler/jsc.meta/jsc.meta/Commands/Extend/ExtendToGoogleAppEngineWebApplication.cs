using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using jsc.meta.Commands.Rewrite;

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
			var publish = project.Directory.CreateSubdirectory("bin/publish");

			#region msbuild
			{
				var i = new ProcessStartInfo(
						this.msbuild.FullName,
						@"/t:_CopyWebApplication /property:OutDir=" + project.Directory.FullName + @"\bin\publish\ /property:WebProjectOutputDir=" + project.Directory.FullName + @"\bin\publish\ " + project.Name
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
						@"-v / -p """ + project.Directory.FullName + @"\bin\publish"" -f """ + project.Directory.FullName + @"\bin\staging"""
						)
				{
					UseShellExecute = false,

					WorkingDirectory = project.Directory.FullName
				};


				var p = Process.Start(i);

				p.WaitForExit();
			}
			#endregion

			#region lets find out our main target
			{
				var targets = Enumerable.ToArray(
					from f in Directory.GetFiles(project.Directory.FullName + @"\bin\staging\bin", "*.dll")
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

				//Console.WriteLine(target.f.FullName);

				var staging = target.f.Directory.CreateSubdirectory("staging");

				// okay now lets rewrite the primary webservice and add data for jsc
				var rewrite = new RewriteToAssembly
				{
					assembly = target.f,
					staging = staging,

					// at this time we can focus only on the first webservice and consider it as primary
					PrimaryTypes = target.a.GetTypes(),

					product = Path.GetFileNameWithoutExtension(target.f.Name),

					#region if we are going to inject code from jsc we need to copy it
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

				// ready for jsc...
			}
			#endregion

		}


	}
}

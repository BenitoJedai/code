using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;

namespace jsc.meta.Commands.Extend
{
	[Description("An example how ASP.NET web application could be translated to Google App Engine")]
	public class ExtendToGoogleAppEngineWebApplication
	{
		public FileInfo msbuild = new FileInfo(@"C:\Windows\Microsoft.NET\Framework\v3.5\msbuild.exe");
		public FileInfo project;
		public FileInfo aspnet_compiler = new FileInfo(@"C:\Windows\Microsoft.NET\Framework\v2.0.50727\aspnet_compiler.exe");

		public void Invoke()
		{
			#region msbuild
			{
				var i = new ProcessStartInfo(
						this.msbuild.FullName,
						"/t:_CopyWebApplication /property:OutDir=" + project.Directory.FullName + @"\bin\publish\ /property:WebProjectOutputDir=" + project.Directory.FullName + @"\bin\publish\ " + project.Name
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
				var targets = Directory.GetFiles(project.Directory.FullName + @"\bin\staging\bin", "*.dll");

				// 
				foreach (var item in targets)
				{
					Console.WriteLine(item);
				}
			}
			#endregion

		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ScriptCoreLib;

using ScriptCoreLib.Shared;
using System.Reflection;
using System.IO;

namespace ScriptApplication.source.csharp
{
	static class Settings
	{
		//# http://www.amenco.com/golivein24//tips/dynamic_content/03_apache_alias.html
		//# Alias for all php test sites
		//Alias /jsc/AvalonExampleGallery "C:\work\jsc.svn\examples\php\AvalonExampleGallery.Server\bin\Debug\web"
		//<Directory "C:\work\jsc.svn\examples\php\AvalonExampleGallery.Server\bin\Debug\web">
		//       Options Indexes FollowSymLinks ExecCGI
		//       AllowOverride All
		//       Order allow,deny
		//       Allow from all
		//</Directory>

		public static void DefineEntryPoint(IEntryPoint e)
		{
			CreatePHPIndexPage(e, php.OrcasPHPScriptApplicationBackend.Filename, php.OrcasPHPScriptApplicationBackend.Entrypoint);
		}

		private static void CreatePHPIndexPage(IEntryPoint e, string file_name, string entryfunction)
		{
			var w = new ScriptCoreLib.Shared.TextWriter();

			w.WriteLine("<?");

			SharedHelper.PHPInclude(w, 
				SharedHelper.LoadReferencedAssemblies(Assembly.GetExecutingAssembly(), true).Where(
					a => a.GetCustomAttributes(typeof(ScriptTypeFilterAttribute), false).Cast<ScriptTypeFilterAttribute>().Any(k => k.Type == ScriptType.PHP)
				).Select(k => new FileInfo(k.Location).Name).ToArray()
			);

			w.WriteLine(entryfunction + "();");

			w.Write("?>");

			e[file_name] = w.Text;
		}
	}

}

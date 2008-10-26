using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ScriptCoreLib;

using ScriptCoreLib.Shared;
using System.Reflection;
using System.IO;
using ScriptCoreLib.Shared.Lambda;

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

			// http://www.javascriptkit.com/howto/htaccess6.shtml
			e[".htaccess"] = "DirectoryIndex " + php.OrcasPHPScriptApplicationBackend.Filename;
		}

		private static void CreatePHPIndexPage(IEntryPoint e, string file_name, string entryfunction)
		{
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

			{
				var w = new StringBuilder();

				SharedHelper.LoadReferencedAssemblies(Assembly.GetExecutingAssembly(), true).Where(
					a => a.GetCustomAttributes(typeof(ScriptTypeFilterAttribute), false).Cast<ScriptTypeFilterAttribute>().Any(k => k.Type == ScriptType.JavaScript)
				).Select(k => new FileInfo(k.Location).Name).ForEach(
					src => w.AppendLine("<script type='text/javascript' src='" + src + ".js'></script>")
				);

				
				e[file_name + ".js"] = w.ToString();
			}

		}
	}

}

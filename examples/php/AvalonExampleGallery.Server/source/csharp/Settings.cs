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
			CreatePHPIndexPage(e, php.OrcasPHPScriptApplicationBackend.Filename, php.OrcasPHPScriptApplicationBackend.WebPageEntry);

			// http://www.javascriptkit.com/howto/htaccess6.shtml
			e[".htaccess"] =
				"AddType application/x-ms-xbap xbap" + Environment.NewLine +
				"DirectoryIndex " + php.OrcasPHPScriptApplicationBackend.Filename;
		}

		private static void CreatePHPIndexPage(IEntryPoint e, string file_name, Action entryfunction)
		{
			{
				var a = new StringBuilder();

				a.AppendLine("<?");

				foreach (var u in SharedHelper.LocalModulesOf(Assembly.GetExecutingAssembly(), ScriptType.PHP))
				{
					a.AppendLine("require_once '" + u + ".php';");
				}

				a.AppendLine(entryfunction.Method.Name + "();");
				a.AppendLine("?>");


				e[file_name] = a.ToString();
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

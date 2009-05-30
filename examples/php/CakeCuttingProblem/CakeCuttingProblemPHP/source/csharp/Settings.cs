using System;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;

using ScriptCoreLib.Shared;
using System.IO;
using ScriptApplication.source.php;

namespace ScriptApplication.source.csharp
{
	static class Settings
	{
		public static void DefineEntryPoint(IEntryPoint e)
		{
			CreatePHPIndexPage(e, php.OrcasPHPScriptApplicationBackend.Filename, php.OrcasPHPScriptApplicationBackend.Entrypoint);
		}

		private static void CreatePHPIndexPage(IEntryPoint e, string file_name, string entryfunction)
		{
			var w = new StringBuilder();

			w.AppendLine("<?");

			foreach (var kk in SharedHelper.LoadReferencedAssemblies(typeof(OrcasPHPScriptApplicationBackend).Assembly, true))
			{
				var k = Path.GetFileName(kk.Location);
				Console.WriteLine("adding " + k);

				w.AppendLine("require_once '" + k + ".php';");
			}

			w.AppendLine(entryfunction + "();");

			w.AppendLine("?>");

			e[file_name] = w.ToString();
		}
	}

}

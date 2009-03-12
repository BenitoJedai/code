using System;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;

using ScriptCoreLib.Shared;
using System.Reflection;

namespace ScriptApplication.source.csharp
{
    static class Settings
    {
        public static void DefineEntryPoint(IEntryPoint e)
        {
			CreatePHPIndexPage(e, php.OrcasPHPScriptApplicationBackend.Filename, php.OrcasPHPScriptApplicationBackend.WebPageEntry);
        }

		#region PHP Section
		private static void CreatePHPIndexPage(IEntryPoint e, string file_name, Action entryfunction)
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
		#endregion
    }

}

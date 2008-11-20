using System;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;

using ScriptCoreLib.Shared;

// jsc:php: does not yet support the newest asset inclusing tech
[assembly: ScriptResources("assets/WebApplication")]

namespace WebApplication
{
	static class Settings
	{
		public static void DefineEntryPoint(IEntryPoint e)
		{
			CreatePHPIndexPage(e, Server.Application.Filename, Server.Application.Application_Entrypoint);
		}

		private static void CreatePHPIndexPage(IEntryPoint e, string file_name, Action entryfunction)
		{
			
			var w = new TextWriter();

			w.WriteLine("<?");

			SharedHelper.PHPInclude(w, SharedHelper.LocalModules);

			w.WriteLine(entryfunction.Method.Name + "();");

			w.Write("?>");

			e[file_name] = w.Text;
		}
	}

}

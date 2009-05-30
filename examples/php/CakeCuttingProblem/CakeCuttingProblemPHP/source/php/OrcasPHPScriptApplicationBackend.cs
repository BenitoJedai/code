using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.PHP;
using System;
using System.Text;
using System.IO;
using CakeCuttingProblem.Library;

namespace ScriptApplication.source.php
{
	[Script]
	static class OrcasPHPScriptApplicationBackend
	{
		public const string Entrypoint = "WebPageEntry";
		public const string Filename = "index.php";

		//Alias /jsc/CakeCuttingProblemPHP "C:\work\jsc.svn\templates\CakeCuttingProblemPHP\bin\Debug\web"
		//<Directory "C:\work\jsc.svn\templates\CakeCuttingProblemPHP\bin\Debug\web">
		//       Options Indexes FollowSymLinks ExecCGI
		//       AllowOverride All
		//       Order allow,deny
		//       Allow from all
		//</Directory>

		/// <summary>
		/// php script will invoke this method
		/// </summary>
		[Script(NoDecoration = true)]
		public static void WebPageEntry()
		{

			Func<string, Action<string>> WithColor =
				 color =>
					 text =>
					 {
						 Console.WriteLine(
						 "<div style='color: " + color + "'>" + text + "</div>");


						 
					 };


	


			DemoSituation.Demo(
				WithColor("red"),
				WithColor("green"),
				WithColor("blue"),
				WithColor("yellow")
			);   

		}
	}
}

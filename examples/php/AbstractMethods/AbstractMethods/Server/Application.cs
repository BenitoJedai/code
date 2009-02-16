using ScriptCoreLib;
using ScriptCoreLib.Shared;

using System;
using System.Text;
using System.IO;

namespace AbstractMethods.Server
{
	[Script]
	static class Application
	{
		public const string Filename = "index.php";

		// change: C:\util\xampplite\apache\conf\httpd.conf

		// http://localhost/jsc/AbstractMethods

		//Alias /jsc/AbstractMethods "C:\work\jsc.svn\examples\AbstractMethods\AbstractMethods\bin\Release\web"
		//<Directory "C:\work\jsc.svn\examples\AbstractMethods\AbstractMethods\bin\Release\web">
		//       Options Indexes FollowSymLinks ExecCGI
		//       AllowOverride All
		//       Order allow,deny
		//       Allow from all
		//</Directory>

		/// <summary>
		/// php script will invoke this method
		/// </summary>
		[Script(NoDecoration = true)]
		public static void Application_Entrypoint()
		{

		

		}
	}
}

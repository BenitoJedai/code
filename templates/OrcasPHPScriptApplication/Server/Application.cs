using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.PHP;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace OrcasPHPScriptApplication.Server
{
	[Script]
	static class Application
	{
		public const string Filename = "index.php";

		//Alias /jsc/OrcasPHPScriptApplication "C:\work\jsc.svn\templates\OrcasPHPScriptApplication\bin\Debug\web"
		//<Directory "C:\work\jsc.svn\templates\OrcasPHPScriptApplication\bin\Debug\web">
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

			Console.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
			Console.WriteLine("<html>");
			Console.WriteLine("<head>");
			Console.WriteLine("<link rel='stylesheet' type='text/css' href='assets/OrcasPHPScriptApplication/WebPage.css' />");
			Console.WriteLine("</head>");
			Console.WriteLine("<body>");

			Console.WriteLine("<img src='assets/OrcasPHPScriptApplication/jsc.png' />");
			Console.WriteLine("<h1>Congratulations!</h1><h2>You are using jsc compiler to convert your C# Application to PHP Application!</h2>");

			Native.Link("Visit jsc.sourceforge.net", "http://jsc.sourceforge.net");

			Console.WriteLine("<br />");

			Native.Link("See more of this application over here!", "?more");


			if (Native.QueryString == "more")
			{
				var path = "assets/OrcasPHPScriptApplication/description.txt";

				Console.WriteLine("<h2>" + path + "</h2>");
				Console.WriteLine("<p>");
				if (File.Exists(path))
				{
					Console.WriteLine(File.ReadAllText(path));
				}
				Console.WriteLine("</p>");

				Console.WriteLine("<p><img src='" + "assets/OrcasPHPScriptApplication/tongue.gif" + "' /> hello world (php)</p>");

				Native.Link("see html for javascript OrcasPHPScriptApplicationDocument", "OrcasPHPScriptApplicationDocument.htm");

				Native.Dump(
					new { hello = "world" }
				);




				var kk = new StringBuilder();
				var k = kk.Append("hello").Append(" world");

				Console.WriteLine(
					k.ToString()
				);

				Console.WriteLine(
					new { hello = "world" }
				);

			}

			Console.WriteLine("<hr />");


			Test1();
			Test2();
			Test3();

			//Native.API.phpinfo();

			Console.WriteLine("</body>");
			Console.WriteLine("</html>");

		}

		private static void Test3()
		{

			var K = string.Empty;
			var PIN = "1234";

			foreach (var item in PIN.ToCharArray())
			{
				Console.WriteLine(((byte)item).ToString("x2"));
			}
		}

		private static void Test2()
		{
			var x = new List<string> { "Hello", "World" };
			IEnumerable a = x;

			foreach (string item in a)
			{
				Console.WriteLine(item + " ");
			}
		}

		private static void Test1()
		{
			using (Test1Create())
			{


			}
		}

		private static InformationList Test1Create()
		{
			var ii = new InformationList
				{
					new Information("Powered by jsc"),
					new Information("b", "now with multiple constructor support")
				};
			return ii;
		}


		[Script]
		public class InformationList : System.Collections.Generic.List<Information>, IDisposable
		{
			public void ToConsole()
			{
				foreach (var item in this)
				{
					Console.WriteLine(
						item.ToString()
					);
				}
			}

			#region IDisposable Members

			public void Dispose()
			{
				this.ToConsole();
			}

			#endregion
		}

		[Script]
		public class Information
		{
			string element = "div";
			string content;
			public Information(string element, string content)
			{
				this.element = element;
				this.content = content;
			}

			public Information(string content)
			{
				this.content = content;
			}

			public override string ToString()
			{
				return "<" + element + ">" + content + "</" + element + "/>";
			}
		}
	}
}

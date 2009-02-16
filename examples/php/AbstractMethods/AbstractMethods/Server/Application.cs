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
			var a = new Abstract3();

			a.Interface1Method1("");
			a.Interface2Method1("");
			a.Interface3Method1("");

		}

		[Script(IsNative = true)]
		static public void echo(object e) { }


		[Script]
		public interface Interface1
		{
			void Interface1Method1(string e);
		}

		[Script]
		public interface Interface2 : Interface1
		{
			void Interface2Method1(string e);
		}

		[Script]
		public interface Interface3 : Interface2
		{
			void Interface3Method1(string e);
		}

		[Script]
		abstract class Abstract1 : Interface3
		{

			#region Interface3 Members

			public abstract void Interface3Method1(string e);

			#endregion

			#region Interface2 Members

			public virtual void Interface2Method1(string e)
			{
				echo("Abstract1::Interface2Method1 <br />");

				Interface3Method1(e);
			}

			#endregion

			#region Interface1 Members

			public void Interface1Method1(string e)
			{
				echo("Abstract1::Interface1Method1 <br />");

				Interface2Method1(e);
			}

			#endregion
		}

		[Script]
		abstract class Abstract2 : Abstract1
		{
			public override void Interface2Method1(string e)
			{
				echo("Abstract2::Interface2Method1 <br />");
			}

		}

		[Script]
		class Abstract3 : Abstract2
		{
			public override void Interface3Method1(string e)
			{
				echo("Abstract2::Interface3Method1 <br />");
			}
		}
	}
}

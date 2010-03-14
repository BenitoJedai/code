using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication.Templates;
using jsc.meta.Library;
using System.Diagnostics;
using System.Windows.Forms;

namespace jsc.meta.Commands.Rewrite.RewriteToUltraApplication
{
	partial class RewriteToUltraApplication
	{
		public static void WriteWebDevLauncher(FileInfo Target)
		{
			// todo: use notifications!
			// http://msdn.microsoft.com/en-us/library/aa511448.aspx


			var r = default(RewriteToAssembly);

			r = new RewriteToAssembly
			{
				staging = Target.Directory,

				product = Path.GetFileNameWithoutExtension(Target.Name),
				productExtension = Target.Extension,

				// does it work? :)
				//obfuscate = Obfuscate,

				merge = new RewriteToAssembly.MergeInstruction[] { 
					// This assembly cannot be merged due to unverifiable code?
					//"WebDev.WebHost"
				},

				PostAssemblyRewrite =
					a =>
					{




						a.Assembly.SetEntryPoint(
							a.context.MethodCache[((Action<string[]>)WebDevLauncer.Launch).Method]
							//,System.Reflection.Emit.PEFileKinds.WindowApplication
						);
					}


			};

			// http://code.google.com/p/youwebit/issues/detail?id=1#c6

			var _WebDev = Assembly.LoadFile(@"C:\Windows\assembly\GAC_32\WebDev.WebHost\9.0.0.0__b03f5f7f11d50a3a\WebDev.WebHost.dll");
			var _WebDev_Server = _WebDev.GetTypes().Single(k => k.Name == "Server");

			r.ExternalContext.TypeCache.Resolve +=
				SourceType =>
				{
					if (SourceType == typeof(Server))
					{
						r.ExternalContext.TypeCache[SourceType] = r.RewriteArguments.context.TypeCache[_WebDev_Server];
						return;
					}
				};

			r.ExternalContext.ConstructorCache.Resolve +=
				SourceCtor =>
				{
					var SourceType = SourceCtor.DeclaringType;

					if (SourceType == typeof(Server))
					{
						r.ExternalContext.ConstructorCache[SourceCtor] =
							r.RewriteArguments.context.ConstructorCache[
								_WebDev_Server.GetConstructor(SourceCtor.GetParameterTypes())
							];
						return;
					}
				};



			r.ExternalContext.MethodCache.Resolve +=
				SourceMethod =>
				{
					var SourceType = SourceMethod.DeclaringType;

					if (SourceType == typeof(Server))
					{
						r.ExternalContext.MethodCache[SourceMethod] =
							r.RewriteArguments.context.MethodCache[
								_WebDev_Server.GetMethod(SourceMethod.Name, SourceMethod.GetParameterTypes())
							];
						return;
					}
				};

			r.Invoke();
		}
	}

	namespace Templates
	{
		public class Server
		{
			public Server(int port, string virtualPath, string physicalPath)
			{

			}

			public void Start()
			{

			}

			public void Stop()
			{

			}
		}

		public static class WebDevLauncer
		{
			public static void Launch(string[] args)
			{
				// http://haacked.com/archive/2006/12/12/using_webserver.webdev_for_unit_tests.aspx
				// http://weblogs.asp.net/jdanforth/archive/2003/12/16/43841.aspx
				// C:\Windows\assembly\GAC_32\WebDev.WebHost\9.0.0.0__b03f5f7f11d50a3a
				// http://rmanimaran.wordpress.com/2008/08/05/get-a-copy-of-dll-in-gac-or-add-reference-to-a-dll-in-gac/

				// http://kbalertz.com/903898/Windows-Forms-NotifyIcon-component-Visual-Basic-display-application-notification.aspx


				var port = new Random().Next(1024, short.MaxValue);


				var url = "http://localhost:" + port;



				var s = new Server(port, "/", new FileInfo(typeof(WebDevLauncer).Assembly.Location).Directory.FullName);

				s.Start();


				Console.Title = url;
				Console.WriteLine(url);
				Process.Start(url);
				Console.WriteLine("Press any key to close this Ultra Application!");
				Console.ReadKey(true);

				s.Stop();
			}
		}
	}
}

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
using System.Threading;
using System.Drawing;

namespace jsc.meta.Commands.Rewrite.RewriteToUltraApplication
{
	partial class RewriteToUltraApplication
	{
		public void WriteWebDevLauncher(FileInfo Target)
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

						a.Module.DefineManifestResource("App.ico",
							typeof(RewriteToUltraApplication).Assembly.GetManifestResourceStream("jsc.meta.Documents.App.ico"),
							ResourceAttributes.Public
						);



						a.Assembly.SetEntryPoint(
							a.context.MethodCache[((Action<string[]>)WebDevLauncer.Launch).Method]
							, System.Reflection.Emit.PEFileKinds.WindowApplication
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

			if (this.Splash != null)
			{
				this.Splash.IsConsole = false;
				this.Splash.PrimaryAssembly = r.Output;

				this.Splash.Invoke();
			}
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

				var Text = typeof(WebDevLauncer).Assembly.GetName().Name;
				const string Launcher = "Launcher";
				if (Text.EndsWith(Launcher))
					Text = Text.Substring(0, Text.Length - Launcher.Length);

				var url = "http://localhost:" + port;
				var dir = new FileInfo(typeof(WebDevLauncer).Assembly.Location).Directory.FullName;

				var t = new Thread(
					delegate()
					{
						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);


						using (var n = new NotifyIcon())
						{

							//n.Icon = new Icon(
							n.Icon = new Icon(typeof(WebDevLauncer).Assembly.GetManifestResourceStream("App.ico"));
							n.Visible = true;
							n.ContextMenuStrip = new ContextMenuStrip
							{

							};

							var Tools =
								new ToolStripMenuItem(
									"Developer Tools"
								);

							#region we need the originals
							Tools.DropDownItems.Add(
								new ToolStripMenuItem(
									"Convert to Google App Engine",
									null,
									delegate
									{

									}
								)
								{
									Enabled = false
								}
							);

							Tools.DropDownItems.Add(
								new ToolStripMenuItem(
									"Convert to PHP",
									null,
									delegate
									{

									}
								)
								{
									Enabled = false
								}
							);

							Tools.DropDownItems.Add(
								new ToolStripMenuItem(
									"Convert to ASP.NET",
									null,
									delegate
									{

									}
								)
								{
									Enabled = false
								}
							);

							Tools.DropDownItems.Add(new ToolStripSeparator());

							#endregion


							Tools.DropDownItems.Add(
								new ToolStripMenuItem(
									//"Open in Windows &Explorer",
									"Open file location",
									null,
									delegate
									{
										Process.Start(dir);
									}
								)
								{
									ToolTipText = dir
								}
							);


							Tools.DropDownItems.Add(
								new ToolStripMenuItem(
									"Browse to Diagnostics",
									null,
									delegate
									{
										Process.Start(url + "/jsc");
									}
								)

							);

							n.ContextMenuStrip.Items.Add(Tools);
							n.ContextMenuStrip.Items.Add(new ToolStripSeparator());


							n.ContextMenuStrip.Items.Add(
								new ToolStripMenuItem(
									"&Close " + Text,
									null,
									delegate
									{
										Application.Exit();
									}
								)
							);




							//n.ContextMenuStrip.Items.Add(
							//new ToolStripMenuItem(
							//    "Browse to Diagnostics",
							//    null,
							//    delegate
							//    {
							//        Process.Start(url + "/jsc");
							//    }
							//    )
							//    {
							//        ToolTipText = url
							//    }
							//);

							n.ContextMenuStrip.Items.Add(
								new ToolStripMenuItem(
									"&Browse to " + Text,
									null,
									delegate
									{
										Process.Start(url);
									}
								)
								{
									ToolTipText = url,
									Font = new Font(SystemFonts.DialogFont, FontStyle.Bold)
								}
							);
							n.DoubleClick +=
								delegate
								{
									Process.Start(url);
								};



							n.Text = Text;
							n.ShowBalloonTip(300, Text, "Loading...", ToolTipIcon.None);


							Application.Run();
						}

					}
				) { ApartmentState = ApartmentState.STA, IsBackground = true };

				t.Start();


				Process.Start(url);



				var s = new Server(port, "/", dir);

				s.Start();

				t.Join();

				//Console.Title = url;
				//Console.WriteLine(url);
				//Console.WriteLine("Press any key to close this Ultra Application!");
				//Console.ReadKey(true);

				s.Stop();
			}
		}
	}
}

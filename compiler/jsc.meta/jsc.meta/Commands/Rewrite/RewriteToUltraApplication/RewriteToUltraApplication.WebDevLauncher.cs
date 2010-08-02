using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication.Templates;
using jsc.meta.Library;
using jsc.meta.Library.VolumeFunctions;

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
                            typeof(RewriteToUltraApplication).Assembly.GetManifestResourceStream("jsc.meta.Documents.jsc.ico"),
                            ResourceAttributes.Public
                        );



                        a.Assembly.SetEntryPoint(
                            a.context.MethodCache[((Action<string[]>)WebDevLauncer.Launch).Method]
                            , System.Reflection.Emit.PEFileKinds.WindowApplication
                        );
                    }


            };

            var _WebDev_Server = GetWebDevServer();

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

        private static Type GetWebDevServer()
        {
            // http://code.google.com/p/youwebit/issues/detail?id=1#c6

            var _WebDevLocation = new[]
            {
                new FileInfo(@"C:\Windows\assembly\GAC_32\WebDev.WebHost\9.0.0.0__b03f5f7f11d50a3a\WebDev.WebHost.dll"),
                new FileInfo(@"C:\Windows\assembly\GAC_32\WebDev.WebHost20\10.0.0.0__b03f5f7f11d50a3a\WebDev.WebHost20.dll")
            }.Where(k => k.Exists).First();


            Console.WriteLine("using " + _WebDevLocation.FullName);

            var _WebDev = Assembly.LoadFile(_WebDevLocation.FullName);

            var _WebDev_Server = _WebDev.GetTypes().Single(k => k.Name == "Server");
            return _WebDev_Server;
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
                var dir__ = new FileInfo(typeof(WebDevLauncer).Assembly.Location).Directory;
                // what about long paths? :)
                // thanks for 414

                //using (var dir = dir__.Parent.ToVirtualDrive())
                //{
                //InternalLaunch(port, Text, url, dir.VirtualDirectory.CreateSubdirectory(dir__.Name).FullName);
                InternalLaunch(port, Text, url, dir__.FullName);
                //}

            }

            private static void InternalLaunch(int port, string Text, string url, string dir)
            {
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

                          // #region we need the originals
                          // Tools.DropDownItems.Add(
                          //     new ToolStripMenuItem(
                          //         "Google App Engine",
                          //         null,
                          //         delegate
                          //         {

                          //         }
                          //     )
                          //     {
                          //         Enabled = false
                          //     }
                          // );

                          // Tools.DropDownItems.Add(
                          //    new ToolStripMenuItem(
                          //        "    Build",
                          //        null,
                          //        delegate
                          //        {
                          //        }
                          //    )
                          //    {
                          //        Enabled = true
                          //    }
                          //);

                          // Tools.DropDownItems.Add(
                          //    new ToolStripMenuItem(
                          //        "    Launch",
                          //        null,
                          //        delegate
                          //        {

                          //        }
                          //    )
                          //    {
                          //        Enabled = true
                          //    }
                          //);

                          // Tools.DropDownItems.Add(
                          //     new ToolStripMenuItem(
                          //         "    Deploy",
                          //         null,
                          //         delegate
                          //         {

                          //         }
                          //     )
                          //     {
                          //         Enabled = true
                          //     }
                          // );

                          // Tools.DropDownItems.Add(new ToolStripSeparator());
                          // Tools.DropDownItems.Add(
                          //     new ToolStripMenuItem(
                          //         "PHP",
                          //         null,
                          //         delegate
                          //         {

                          //         }
                          //     )
                          //     {
                          //         Enabled = false
                          //     }
                          // );

                          // Tools.DropDownItems.Add(
                          //    new ToolStripMenuItem(
                          //        "    Build",
                          //        null,
                          //        delegate
                          //        {

                          //        }
                          //    )
                          //    {
                          //        Enabled = true
                          //    }
                          //);

                          // Tools.DropDownItems.Add(
                          //    new ToolStripMenuItem(
                          //        "    Launch",
                          //        null,
                          //        delegate
                          //        {

                          //        }
                          //    )
                          //    {
                          //        Enabled = true
                          //    }
                          //);



                          // Tools.DropDownItems.Add(new ToolStripSeparator());

                          // #endregion



                           Tools.DropDownItems.Add(
                               new ToolStripMenuItem(
                               // Visual Studio has this text
                                    "Open Folder in Windows Explorer",
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

                           Tools.DropDownItems.Add(new ToolStripSeparator());

                           Tools.DropDownItems.Add(
                               new ToolStripMenuItem(
                                   "Browse to Server Diagnostics",
                                   null,
                                   delegate
                                   {
                                       Process.Start(url + "/jsc");
                                   }
                               )

                           );

                           //var jsc_configuration = @"http://download.jsc-solutions.net";

                           //n.ContextMenuStrip.Items.Add(
                           //     new ToolStripMenuItem(
                           //    // Visual Studio has this text
                           //          "Configuration...",
                           //          null,
                           //         delegate
                           //         {
                           //             var c = new WebClient();
                           //             var f = Path.Combine(
                           //                 Path.GetTempPath(),
                           //                 Path.GetRandomFileName() + ".jsc.configuration.application"
                           //             );

                           //             c.DownloadFile(jsc_configuration, f);

                           //             Process.Start(f);
                           //         }
                           //     )
                           //     {
                           //         ToolTipText = jsc_configuration
                           //     }
                           // );


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
                )
                {
                    ApartmentState = ApartmentState.STA,
                    IsBackground = true
                };

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

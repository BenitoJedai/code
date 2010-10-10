using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Media;
using jsc.meta.Commands.Configuration;
using jsc.meta.Commands.Rewrite.RewriteToSplashScreen.Templates;
using jsc.meta.Dialogs;
using jsc.meta.Library;
using jsc.meta.Library.VolumeFunctions;
using jsc.meta.Tools;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace jsc.meta.Commands.Rewrite.RewriteToUltraApplication
{
    partial class RewriteToUltraApplication
    {
        [Obfuscation(Feature = "invalidmerge")]
        public class AsProgram
        {

            public static void Launch(Type PrimaryApplication)
            {
                ConfigurationDisposeSubst.RegisterOnce();

                new AsProgram { PrimaryApplication = PrimaryApplication }.Launch();
            }

            public bool DisableSplash;
            public Type PrimaryApplication;

            public FileInfo mxmlc = ToolsExtensions.Defaults.mxmlc;
            public FileInfo flashplayer = ToolsExtensions.Defaults.flashplayer;

            public bool Verbose;

            public void Launch()
            {
                Program.ShowLogo();

                Action Continue =
                  delegate
                  {
                  };

                Action ApplyContinueByCompileAndLaunch =
                    delegate
                    {
                        Continue =
                            delegate
                            {
                                CompileAndLaunch(PrimaryApplication);
                            };
                    };

                
                var PHPLauncher = default(FileInfo);
                var GAELauncher = default(FileInfo);

                var staging = new DirectoryInfo(
                    Path.Combine(
                        new FileInfo(PrimaryApplication.Assembly.Location).Directory.FullName, "staging"
                    ));

                if (staging.Exists)
                {
                    var staging_WebService = staging.GetDirectories("*WebService").FirstOrDefault();
                    if (staging_WebService != null)
                    {
                        PHPLauncher =
                            new FileInfo(
                                Path.Combine(
                                    staging_WebService.FullName, "staging.php/run.bat"
                                )
                            );
                        if (!PHPLauncher.Exists)
                            PHPLauncher = null;

                        GAELauncher =
                         new FileInfo(
                             Path.Combine(
                                 staging_WebService.FullName, "staging.java/web/run.bat"
                             )
                         );
                        if (!GAELauncher.Exists)
                            GAELauncher = null;
                    }
                }

                #region setup dialog

                var t = new Thread(
                    delegate()
                    {
                        var c = new WebServiceLauncherDialog();

                        c.button2.IsEnabled = PHPLauncher != null;
                        c.button3.IsEnabled = GAELauncher != null;

                        var w = c.ToWindow();

                        c.button1.Click +=
                            delegate
                            {
                                Continue =
                                    delegate
                                    {
                                        CompileAndLaunch(PrimaryApplication);
                                    };

                                w.Close();
                            };

                        c.button2.Click +=
                            delegate
                            {
                                Continue =
                                    delegate
                                    {
                                        Process.Start(
                                            new ProcessStartInfo(PHPLauncher.FullName)
                                            {
                                                WorkingDirectory = PHPLauncher.Directory.FullName
                                            }
                                        );
                                    };

                                w.Close();
                            };

                        c.button3.Click +=
                               delegate
                               {
                                   Continue =
                                       delegate
                                       {
                                           Process.Start(
                                            new ProcessStartInfo(GAELauncher.FullName)
                                            {
                                                WorkingDirectory = GAELauncher.Directory.FullName
                                            }
                                        );
                                       };

                                   w.Close();
                               };


                        w.Loaded +=
                            delegate
                            {
                                w.Focus();
                            };

                        w.Title = "studio.jsc-solutions.net - " + this.PrimaryApplication.Assembly.GetName().Name;

                        w.Background = Brushes.White;
                        w.Icon = c.image1.Source;
                        w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                        w.ShowDialog();
                    }
                )
                {
                    ApartmentState = ApartmentState.STA
                };
                #endregion

                if (PHPLauncher != null ||
                    GAELauncher != null
                    )
                {


                    t.Start();
                    t.Join();
                }
                else ApplyContinueByCompileAndLaunch();

                // configure flash debug mode
                // http://www.adobe.com/devnet/flashplayer/articles/fplayer9_security_05.html

                // UserProfile	The user's profile folder. 
                // Applications should not create files or folders at this level; 
                // they should put their data under the locations referred to by ApplicationData.
                // http://www.longhorncorner.com/Forums/ShowMessages.aspx?ThreadID=8486

                var UserProfile = Environment.GetEnvironmentVariable("UserProfile");

                // http://livedocs.adobe.com/flex/201/html/wwhelp/wwhimpl/common/html/wwhelp.htm?context=LiveDocs_Book_Parts&file=security2_117_44.html
                // http://livedocs.adobe.com/flex/3/html/help.html?content=logging_04.html
                // http://www.websector.de/blog/2007/02/20/trace-outside-the-flash-ide-with-tail/
                // http://www.timo-ernst.net/2010/04/chrome-flash-debugger-not-connecting-to-flexflash-builder/

                // http://jpauclair.net/2010/02/10/mmcfg-treasure/

                File.WriteAllLines(
                    Path.Combine(UserProfile, "mm.cfg"),
                    new[]
                    {
                        "PolicyFileLog=1   # Enables policy file logging",
                        "PolicyFileLogAppend=1  # Optional; do not clear log at startup",
                        "ErrorReportingEnable=1",
                        "TraceOutputFileEnable=1"
                    }
                );

                

                var Logfile = 
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        @"Roaming\Macromedia\Flash Player\Logs\flashlog.txt"
                    );

                
                Console.WriteLine("Configuring the debugger version of Flash Player...");

                Console.WriteLine(Logfile);

                ContinueWithSplashIfAvailable(Continue);
            }

            private void ContinueWithSplashIfAvailable(Action Continue)
            {
                var SplashTemplate = new FileInfo(@"c:\util\jsc\bin\jsc.splash.exe");

                if (!DisableSplash && SplashTemplate.Exists)
                {
                    var Splash = Assembly.LoadFile(SplashTemplate.FullName);

                    Action<Action> ShowDialogSplash = InternalSplashScreen.ShowDialogSplash;

                    var ImplementationForShowDialogSplash = Enumerable.First(
                        from i in new ILBlock(Splash.EntryPoint).Instructrions
                        where i.OpCode == OpCodes.Call
                        let m = i.TargetMethod
                        where m.GetSignatureTypes().SequenceEqual(ShowDialogSplash.Method.GetSignatureTypes())
                        select m
                    );

                    ShowDialogSplash = (Action<Action>)Delegate.CreateDelegate(typeof(Action<Action>), ImplementationForShowDialogSplash);


                    ShowDialogSplash(
                        delegate
                        {
                            Continue();
                        }
                    );
                }
                else
                {
                    Continue();
                }
            }

            private void CompileAndLaunch(Type PrimaryApplication)
            {
                // do we need to compile all components? maybe just the staging.net.debug?
                var WebDevLauncher = Compile(PrimaryApplication);

                Console.WriteLine();
                Console.WriteLine("compiled! launching server! please wait...");

                // todo: WebDev cannot handle root virtual directories. we should provide an extension for non root virtual dir.
                using (var p = WebDevLauncher.Directory.Parent.ToVirtualDrive())
                {
                    WebDevLauncher =
                        new FileInfo(
                            Path.Combine(
                                Path.Combine(p.VirtualDirectory.FullName, WebDevLauncher.Directory.Name),
                                WebDevLauncher.Name
                            )
                        );

                    var WebDevLauncherAssembly = Assembly.LoadFile(WebDevLauncher.FullName);

                    // to enable relative file ops
                    Environment.CurrentDirectory = WebDevLauncher.Directory.FullName;

                    WebDevLauncherAssembly.EntryPoint.Invoke(null, new object[] { new string[0] });
                }
            }

            private FileInfo Compile(Type PrimaryApplication)
            {
                FileInfo WebDevLauncher = null;

                var r = new jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument
                {
                    assembly = new FileInfo(PrimaryApplication.Assembly.Location),

                    //staging = new FileInfo(PrimaryApplication.Assembly.Location).Directory.CreateSubdirectory("staging.debug"),

                    DisableWebServiceJava = true,
                    DisableWebServicePHP = true,
                    DisableWebServiceTypeMerge = true,

                    InternalCreateNoWindow = Verbose ? false : true,
                    //IsRewriteOnly = true


                    mxmlc = this.mxmlc,
                    flashplayer = this.flashplayer

                };


                r.AtWebServiceReady +=
                    a =>
                    {
                        // what if there was no web service defined? :)

                        WebDevLauncher = a.WebDevLauncher;
                    };

                r.ProccessStatusChanged +=
                    e =>
                    {
                        //Console.Write(".");
                    };

                r.Invoke();
                return WebDevLauncher;
            }
        }
    }
}

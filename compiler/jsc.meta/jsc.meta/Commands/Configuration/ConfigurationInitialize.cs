using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.Ultra.Library;
using ScriptCoreLib.Extensions;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using jsc;

namespace jsc.meta.Commands.Configuration
{
    [Description("The installer will invoke this")]
    public class ConfigurationInitialize : CommandBase
    {
        public override void Invoke()
        {
            Action<string> WithText = Text =>
            {
                Console.WriteLine(Text);
            };

            var DisposeNotification = AsNotification(value => WithText += value);

            WithText("Creating project templates...");



            new ConfigurationCreateProjectTemplates().Invoke();


            #region Compile
            Action<CommandLineOptions> Compile =
                Options =>
                {
                    Options.IsNoLogo = true;

                    Options.CachedFileGeneratorConstructor =
                        a => new CachedFileGenerator(a) { DisableOutput = true };

                    
                    WithText(
                        "Converting "
                        + Path.GetFileNameWithoutExtension(Options.TargetAssembly.Name) 
                        + " to " + Options.ToScriptTypes().Single().ToString()  + "..."
                    );

                    jsc.Program.TypedMain(
                        new jsc.CompileSessionInfo
                        {
                            Options = Options
                        }
                    );
                };
            #endregion

            var Script = new Dictionary<Func<Type, CommandLineOptions>, Type[]>
            {
                
                 { 
                    SourceType =>
                    {
                        return  new CommandLineOptions
                        {
                            TargetAssembly = new System.IO.FileInfo(SourceType.Assembly.Location),
                            IsJava = true,
                        };
                    },
                    new[]
                    {
                         typeof(global::ScriptCoreLibJava.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLibJava.Drawing.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLibJava.Windows.Forms.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLibJava.XLinq.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLibJava.Web.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLibJava.Web.Services.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLibJava.jni.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLibJava.AppEngine.IAssemblyReferenceToken),
                    }
                 }

                { 
                    SourceType =>
                    {
                        return  new CommandLineOptions
                        {
                            TargetAssembly = new System.IO.FileInfo(SourceType.Assembly.Location),
                            IsJavaScript = true,
                        };
                    },
                    new[]
                    {
                         typeof(global::ScriptCoreLib.Shared.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLib.Shared.Query.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLib.Shared.Drawing.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLib.Shared.Windows.Forms.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLib.Shared.XLinq.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLib.Shared.Avalon.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLib.Shared.Avalon.Integration.IAssemblyReferenceToken),
                    }
                 },

                 { 
                    SourceType =>
                    {
                        return  new CommandLineOptions
                        {
                            TargetAssembly = new System.IO.FileInfo(SourceType.Assembly.Location),
                            IsActionScript = true,
                        };
                    },
                    new[]
                    {
                         typeof(global::ScriptCoreLib.Shared.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLib.Shared.Query.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLib.Shared.XLinq.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLib.Shared.Avalon.IAssemblyReferenceToken),
                         typeof(global::ScriptCoreLib.Shared.Avalon.Integration.IAssemblyReferenceToken),
                    }
                 },

            };


            Script.Keys.WithEach(f => Script[f].WithEach(a => Compile(f(a))));

            DisposeNotification();
        }

        public Action AsNotification(Action<Action<string>> SetWithText)
        {
            var signal = new EventWaitHandle(false, EventResetMode.AutoReset);

            Action close = delegate { };

            var t = new Thread(
                 delegate()
                 {
                     Application.EnableVisualStyles();
                     Application.SetCompatibleTextRenderingDefault(false);


                     using (var n = new NotifyIcon())
                     {

                         //n.Icon = new Icon(
                         n.Icon = new Icon(typeof(ConfigurationInitialize).Assembly.GetManifestResourceStream("jsc.meta.Documents.jsc.ico"));
                         n.Visible = true;
                         n.ContextMenuStrip = new ContextMenuStrip
                         {

                         };

                         n.Click +=
                             delegate
                             {
                                 n.ShowBalloonTip(500);
                             };

                         var s = new Queue<string>();

                         Action<string> WithText =
                              Text =>
                              {
                                  s.Enqueue(Text);

                                  if (s.Count > 5)
                                      s.Dequeue();

                                  n.BalloonTipText = string.Join(Environment.NewLine, s.ToArray());
                                  n.ShowBalloonTip(1000);
                              };


                         WithText.With(SetWithText);

                         close = n.Dispose;

                         n.Text = "jsc configuration";
                         n.BalloonTipTitle = "jsc configuration";

                         signal.Set();
                         Application.Run();

                     }

                 }
              )
            {
                ApartmentState = ApartmentState.STA,
                IsBackground = true
            };

            t.Start();

            signal.WaitOne();

            return close;
        }
    }
}

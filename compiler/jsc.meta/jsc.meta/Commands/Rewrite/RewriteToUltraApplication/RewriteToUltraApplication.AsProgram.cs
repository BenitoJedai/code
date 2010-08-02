using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Web;
using System.IO;
using jsc.meta.Library;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Reflection.Emit;
using jsc.meta.Commands.Rewrite.RewriteToSplashScreen.Templates;
using jsc.meta.Tools;
using jsc.meta.Commands.Configuration;
using jsc.meta.Library.VolumeFunctions;

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
                           CompileAndLaunch(PrimaryApplication);
                        }
                    );
                }
                else
                {
                    CompileAndLaunch(PrimaryApplication);
                }


            }

            private void CompileAndLaunch(Type PrimaryApplication)
            {
                var WebDevLauncher = Compile(PrimaryApplication);

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

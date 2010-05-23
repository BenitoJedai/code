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
    [Description("Creates jsc/cache folder for inclusion into the installer.")]
    public class ConfigurationPrecompile : CommandBase
    {
        public DirectoryInfo SDK = new DirectoryInfo(@"c:\util\jsc");

        public override void Invoke()
        {
            Action<string> WithText = Text =>
            {
                Console.WriteLine(Text);
            };



            #region Compile
            Action<CommandLineOptions> Compile =
                Options =>
                {
                    Options.IsNoLogo = true;

                    Options.CachedFileGeneratorConstructor =
                        a => new CachedFileGenerator(a)
                        {
                            DisableOutput = true,
                            SDK = SDK
                        };


                    WithText(
                        "Converting "
                        + Path.GetFileNameWithoutExtension(Options.TargetAssembly.Name)
                        + " to " + Options.ToScriptTypes().Single().ToString() + "..."
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

                         //typeof(global::ScriptCoreLib.Shared.Archive.ZIP.),
                    }
                 },

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

        }

    }
}

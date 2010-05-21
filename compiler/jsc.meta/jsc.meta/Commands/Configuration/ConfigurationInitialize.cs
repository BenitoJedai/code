using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.Ultra.Library;
using ScriptCoreLib.Extensions;

namespace jsc.meta.Commands.Configuration
{
    [Description("The installer will invoke this")]
    public class ConfigurationInitialize : CommandBase
    {
        public override void Invoke()
        {
            Console.WriteLine("Initializing configuration...");

            new ConfigurationCreateProjectTemplates().Invoke();

            new[]
            {
                 typeof(global::ScriptCoreLib.Shared.IAssemblyReferenceToken),
                 typeof(global::ScriptCoreLib.Shared.Query.IAssemblyReferenceToken),
                 typeof(global::ScriptCoreLib.Shared.Drawing.IAssemblyReferenceToken),
                 typeof(global::ScriptCoreLib.Shared.Windows.Forms.IAssemblyReferenceToken),
                 typeof(global::ScriptCoreLib.Shared.XLinq.IAssemblyReferenceToken),
                 typeof(global::ScriptCoreLib.Shared.Avalon.IAssemblyReferenceToken),
            }.WithEach(
                SourceType =>
                    jsc.Program.TypedMain(
                        new jsc.CompileSessionInfo
                        {
                            Options = new jsc.CommandLineOptions
                            {
                                TargetAssembly = new System.IO.FileInfo(SourceType.Assembly.Location),
                                IsJavaScript = true,
                                IsNoLogo = true,

                                CachedFileGeneratorConstructor = a => new CachedFileGenerator(a) { DisableOutput = true }
                            }
                        }
                 )
            );
        }
    }
}

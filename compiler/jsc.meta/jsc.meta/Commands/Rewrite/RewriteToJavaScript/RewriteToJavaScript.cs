using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace jsc.meta.Commands.Rewrite.RewriteToJavaScript
{
    public class RewriteToJavaScript : CommandBase
    {
        public FileInfo TargetAssembly;

        public override void Invoke()
        {
            // in this step we could merge ScriptCoreLib's?

            Tools.ToolsExtensions.ToJavaScript(
                TargetAssembly,
                delegate { }
            );
        }
    }
}

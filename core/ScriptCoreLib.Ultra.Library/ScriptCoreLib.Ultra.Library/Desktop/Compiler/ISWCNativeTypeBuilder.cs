using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Desktop.Compiler
{
    // Note: This type shall be internalized after before inserted into the installer
    public interface ISWCNativeTypeBuilder
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140429

        /// <summary>
        /// The implementation of this function shall create a new .NET assembly of the SWC file provided.
        /// 
        /// The ResolveExternalType callback shall provide ScriptCoreLib type references 
        /// for full type names like "flash.display.DisplayObject" and "Vector`1" where
        /// ScriptCoreLib defines types with namespace prefix "ScriptCoreLib.ActionScript".
        /// 
        /// If the callback is not available, all types must be defined within the loaded SWC files, for 
        /// example when building native types from the Flex SDK instead of third party code.
        /// </summary>
        /// <param name="SWCFile"></param>
        /// <param name="OutputAssembly"></param>
        /// <param name="ResolveExternalType"></param>
        void CreateAssembly(
            FileInfo[] SWCFiles,
            FileInfo OutputAssembly,
            Func<string, Type> ResolveExternalType = null
        );
    }
}

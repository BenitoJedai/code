using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;

namespace ScriptCoreLibNative.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/object.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Object.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Object.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Object.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Object.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Object.cs

    [Script(Implements = typeof(global::System.Object))]
    internal class __Object
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\WebCL\WebCLContext.cs

        // http://csnative.codeplex.com/
        // http://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/6742251-make-c-c-compiler-cl-exe-independent-of-ide

        // X:\jsc.svn\examples\c\Test\TestFunc\TestFunc\Program.cs
        // for C runtime we would need to manager the virtual slots manually?
        public new virtual string ToString()
        {
            return "global::System.Object";
        }
    }

}

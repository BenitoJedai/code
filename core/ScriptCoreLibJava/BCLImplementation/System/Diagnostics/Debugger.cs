using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Diagnostics
{
    // http://referencesource.microsoft.com/#mscorlib/system/diagnostics/debugger.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Diagnostics/Debugger.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Diagnostics/Debugger.cs

    // https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Diagnostics/Debug.cs
    //X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Diagnostics\Debugger.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Diagnostics\Debugger.cs


    [Script(Implements = typeof(global::System.Diagnostics.Debugger))]
    internal class __Debugger
    {
        public static bool IsAttached { get; internal set; }

        public static void Break()
        {
            // debugger? :)

            throw new InvalidOperationException("System.Diagnostics.Debugger.Break");
        }

    }
}

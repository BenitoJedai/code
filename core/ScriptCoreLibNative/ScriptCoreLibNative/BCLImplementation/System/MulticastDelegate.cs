using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;
using ScriptCoreLibNative.BCLImplementation.System.Reflection;

namespace ScriptCoreLibNative.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/multicastdelegate.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/MulticastDelegate.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\MulticastDelegate.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\MulticastDelegate.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\MulticastDelegate.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\MulticastDelegate.cs

    [Script(Implements = typeof(global::System.MulticastDelegate))]
    internal class __MulticastDelegate : __Delegate
    {
        public __MulticastDelegate(object e, global::System.IntPtr p)
            : base(e, p)
        {
        }
    }
}

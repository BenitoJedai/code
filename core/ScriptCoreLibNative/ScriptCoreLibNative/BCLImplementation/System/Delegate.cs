using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;
using ScriptCoreLibNative.BCLImplementation.System.Reflection;

namespace ScriptCoreLibNative.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/delegate.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Delegate.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Delegate.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Delegate.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Delegate.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Delegate.cs

    [Script(Implements = typeof(global::System.Delegate))]
    internal class __Delegate
    {
        public object Target { get; set; }
        public MethodInfo Method { get; set; }

        public __Delegate(object e, global::System.IntPtr p)
        {
            Console.WriteLine("__Delegate.ctor");

            // X:\jsc.svn\examples\c\Test\TestAction\TestAction\Program.cs

            this.Target = e;
            this.Method = new __MethodInfo
            {
                //InternalMethod = ((__IntPtr)(object)p).MethodToken 
                MethodToken = p
            };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    // http://sourceforge.net/p/jsc/code/HEAD/tree/core/ScriptCoreLib/JavaScript/BCLImplementation/System/Activator.cs

    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Activator.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Activator.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Activator.cs

    // http://referencesource.microsoft.com/#mscorlib/system/activator.cs
    // https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/activator.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Activator.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Activator.cs

    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Activator.cs
    // https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Activator.cs
    // https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Activator.cs

    [Script(Implements = typeof(global::System.Activator))]
    internal class __Activator
    {
        // tested by
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestAndroidOrderByThenGroupBy\ApplicationWebService.cs

        public static object CreateInstance(Type type)
        {
            __Type t = type;
            var o = default(object);

            try
            {
                o = t.InternalTypeDescription.newInstance();
            }
            catch // (csharp.ThrowableException e)
            {
                throw; // new csharp.RuntimeException(e.ToString());
            }

            return o;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
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

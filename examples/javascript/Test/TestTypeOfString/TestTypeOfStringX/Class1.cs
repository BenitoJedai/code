using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestTypeOfStringX
{
    [Script(Implements = typeof(global::System.String), InternalConstructor = true)]
    internal class __String
    {

    }

    [Script(Implements = typeof(global::System.Type))]
    internal class __Type 
    {
        public static Type GetTypeFromHandle(RuntimeTypeHandle TypeHandle)
        {
            return null;
        }
    }

    [Script(Implements = typeof(global::System.RuntimeTypeHandle))]
    internal sealed class __RuntimeTypeHandle
    {
        public __RuntimeTypeHandle()
        {

        }

        // special method invoked on typeof(Type) statement
        public __RuntimeTypeHandle(IntPtr e)
        {
        }

    }

    public class Class1
    {
        public Class1()
        {
            var x = typeof(string);
        }
    }
}

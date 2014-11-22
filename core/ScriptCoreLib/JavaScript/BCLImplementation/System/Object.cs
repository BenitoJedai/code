using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
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
        public static bool ReferenceEquals(object objA, object objB)
        {
            return Expando.ReferenceEquals(objA, objB);
        }

        // tested by?
        [Script(OptimizedCode = "return i.constructor.prototype;")]
        static IntPtr GetPrototype(object i)
        {
            return default(IntPtr);
        }


        [Script(DefineAsStatic = true)]
        new public Type GetType()
        {
            var x = new __RuntimeTypeHandle(
                GetPrototype(this)
                //(IntPtr) new DOM.IFunction("i", "return i.constructor.prototype;").apply(null, this)

            );

            return Type.GetTypeFromHandle(x);
        }

        public static bool Equals(object objA, object objB)
        {
            if (objA == objB)
            {
                return true;
            }
            if (objA != null)
                if (objB != null)
                    return objA.Equals(objB);

            return false;


        }






        public new virtual bool Equals(object obj)
        {
            return this == obj;
        }

        public new virtual int GetHashCode()
        {
            return default(int);
        }


        // X:\jsc.svn\examples\javascript\test\TestToString\TestToString\Application.cs
        public new virtual string ToString()
        {
            return default(string);
        }
    }

}

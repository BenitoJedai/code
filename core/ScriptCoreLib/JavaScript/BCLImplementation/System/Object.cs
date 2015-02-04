using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/object.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Object.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Object.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Object.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Object.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Object.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Object.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Object.cs


    [Script(Implements = typeof(global::System.Object))]
    internal class __Object
    {
        // http://www.hanselman.com/blog/GettingReadyForTheFutureWithTheMicrosoftNETPortabilityAnalyzer.aspx

        // http://channel9.msdn.com/Blogs/Charles/From-the-Archives-Erik-Meijer-and-Mark-Shields-Compiling-MSIL-to-JS
        // http://channel9.msdn.com/Forums/Coffeehouse/Volta-Dead-or-Alive

        // http://www.dotnetfoundation.org/netcore5
        // .NET Core has two major components. It includes a small runtime that is built from the same codebase as the .NET Framework CLR. 
        // The .NET Core runtime includes the same GC and JIT (RyuJIT), but doesn’t include features like Application Domains or Code Access Security. 
        // The runtime is delivered on NuGet, via the Microsoft.CoreCLR package.

        // http://ask.slashdot.org/story/14/12/20/2331202/ask-slashdot-is-an-open-source-net-up-to-the-job
        // Of late, .NET Native is an interesting piece of tech that precompiles .NET apps using VC++ compiler backend. 
        // When you are offering a system that can fire up nodes and destroy them dynamically and on-demand, its just not worth it to 
        // have to keep track of some piece of that being commercially licensed and all the restrictions you end up with on your freedom to fire up new nodes on demand

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

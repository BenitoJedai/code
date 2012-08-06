using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]

namespace TestResolveImplementation
{
    public class Class1
    {
        public void Foo(
            ScriptCoreLib.JavaScript.WebGL.WebGLObject obj,
            ScriptCoreLib.CLR.Foo foo)
        {
            obj.value = 5;
        }
    }

    //[Script]
    public interface IAssemblyReferenceToken :
        global::ScriptCoreLib.Android.IAssemblyReferenceToken,
        global::ScriptCoreLib.JavaScript.WebGL.IAssemblyReferenceToken
    {
    }
}

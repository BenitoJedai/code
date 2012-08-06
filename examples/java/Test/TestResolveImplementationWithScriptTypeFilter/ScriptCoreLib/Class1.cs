using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: 
    Script(IsCoreLib = true)
]

[assembly:
    ScriptTypeFilter(ScriptType.JavaScript, typeof(global::ScriptCoreLib.JavaScript.WebGL.WebGLObject)),
]

namespace ScriptCoreLib.CLR
{
    public class Foo
    {
    }
}

namespace ScriptCoreLib.JavaScript.WebGL
{
    [Script(IsNative = true)]
    public class WebGLObject
    {
        public int value;
    }

    [Script]
    public interface IAssemblyReferenceToken
    {
    }
}

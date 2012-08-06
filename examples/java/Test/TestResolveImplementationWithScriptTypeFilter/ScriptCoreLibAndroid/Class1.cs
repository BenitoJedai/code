using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: ScriptTypeFilter(ScriptType.Java)]
[assembly: Script(IsCoreLib = true)]

namespace ScriptCoreLib.CLR
{
    [Script(Implements = typeof(ScriptCoreLib.CLR.Foo))]
    public class __Foo
    {
    }
}

namespace ScriptCoreLib.Android
{
    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLObject))]
    public class __WebGLObject
    {
        public int value;
    }

    [Script]
    public interface IAssemblyReferenceToken
    {
    }
}


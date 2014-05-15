using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.ParameterInfo))]
    public class __ParameterInfo
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Reflection\ParameterInfo.cs
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Reflection\ParameterInfo.cs

        public virtual string Name
        {
            get
            {
                return "arg" + this.Position;
            }
        }

        public virtual int Position { get; set; }

        public virtual Type ParameterType { get; set; }

        public static implicit operator ParameterInfo(__ParameterInfo e)
        {
            return (ParameterInfo)(object)e;
        }
    }
}

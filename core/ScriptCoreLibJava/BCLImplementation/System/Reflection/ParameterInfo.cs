using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
    // http://referencesource.microsoft.com/#mscorlib/system/reflection/parameterinfo.cs

	[Script(Implements = typeof(ParameterInfo))]
	public class __ParameterInfo
	{
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Reflection\ParameterInfo.cs
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Reflection\ParameterInfo.cs

        public virtual string Name
        {
            get
            {
                // JVM does not seem to help us here..
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared
{
	[global::System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor , Inherited = false, AllowMultiple = true)]
	public class TypeOfByNameOverrideAttribute : Attribute
	{
		public Type Target;

		public TypeOfByNameOverrideAttribute(Type Target)
		{
			this.Target = Target;
		}

		public TypeOfByNameOverrideAttribute()
		{

		}
	}
}

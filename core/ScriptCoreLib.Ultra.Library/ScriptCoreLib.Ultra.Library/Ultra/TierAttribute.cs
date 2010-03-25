using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra
{
	[global::System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	public sealed class TierAttribute : Attribute
	{
		public TierEnum Value { get; private set; }
		public Type[] Targets { get; private set; }

		public TierAttribute(TierEnum Value, params Type[] Targets)
		{
			this.Value = Value;
			this.Targets = Targets;
		}

		public bool RequestTierSwitchAtCaller;
	}


}

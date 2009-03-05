using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJavaCard
{
	public sealed class APDUInstructionAttribute : Attribute
	{
		public sbyte INS;

		public APDUInstructionAttribute()
		{
			INS = -1;
		}

		public APDUInstructionAttribute(sbyte INS)
		{
			this.INS = INS;
		}
	}
}

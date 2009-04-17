using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJavaCard
{
	public sealed class APDUInstructionAttribute : Attribute
	{
		public byte INS;

		public APDUInstructionAttribute()
		{
			INS = 0;
		}

		public APDUInstructionAttribute(byte INS)
		{
			this.INS = INS;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJavaCard
{
	public sealed class APDUInstructionAttribute : Attribute
	{
		public byte INS;

		public Type DataParameters;

		public APDUInstructionAttribute()
		{
			INS = 0;
		}

		public APDUInstructionAttribute(byte INS)
		{
			this.INS = INS;
		}

		public Type InputParameterType;
		public Type OutputParameterType;

		public override string ToString()
		{
			return "" + INS;
		}

		public APDUInstructionAttribute ToINS(byte p)
		{
			return new APDUInstructionAttribute(p) { InputParameterType = InputParameterType, OutputParameterType = OutputParameterType };
		}
	}
}

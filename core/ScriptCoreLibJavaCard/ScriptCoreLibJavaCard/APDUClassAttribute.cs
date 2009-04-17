using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJavaCard
{
	public sealed class APDUClassAttribute : Attribute
	{
		public byte CLA;

		public APDUClassAttribute(byte CLA)
		{
			this.CLA = CLA;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJavaCard
{
	public sealed class APDUClassAttribute : Attribute
	{
		public sbyte CLA;

		public APDUClassAttribute(sbyte CLA)
		{
			this.CLA = CLA;
		}
	}
}

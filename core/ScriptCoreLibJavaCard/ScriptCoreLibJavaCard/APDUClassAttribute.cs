using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJavaCard
{
	public class APDUClassAttribute : Attribute
	{
		public byte CLA;

		public APDUClassAttribute(byte CLA)
		{
			this.CLA = CLA;
		}

		/// <summary>
		/// If set to true, the code generator will auto assign instruction codes. 
		/// This is only useful if the actual instruction numbers are being 
		/// defined by the current implementation and not an external specification.
		/// </summary>
		public bool AutoAssignInstructions;
	}
}

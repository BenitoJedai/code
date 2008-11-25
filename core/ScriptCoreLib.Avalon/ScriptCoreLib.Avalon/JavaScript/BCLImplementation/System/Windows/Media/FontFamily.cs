using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.FontFamily))]
	internal class __FontFamily
	{
		public readonly string InternalFamilyName;

		public __FontFamily(string familyName)
		{
			this.InternalFamilyName = familyName;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.Microsoft.VisualBasic.CompilerServices
{
	[Script(Implements = typeof(global::Microsoft.VisualBasic.CompilerServices.Conversions))]
	internal class __Conversions
	{
		public static string ToString(DateTime Value)
		{
			return Value.ToString();
		}
	}
}

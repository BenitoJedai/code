using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.PseudoExpressions
{
	public class PseudoConstantExpression
	{
		public object Value;

		public static implicit operator PseudoConstantExpression(string Value)
		{
			return new PseudoConstantExpression { Value = Value };
		}
	}
}

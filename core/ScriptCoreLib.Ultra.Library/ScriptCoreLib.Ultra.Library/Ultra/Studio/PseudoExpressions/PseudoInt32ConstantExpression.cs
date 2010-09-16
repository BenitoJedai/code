using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.PseudoExpressions
{
    public class PseudoInt32ConstantExpression : PseudoNumericConstantExpression
	{
		public int Value;

		public static implicit operator PseudoInt32ConstantExpression(int Value)
		{
			return new PseudoInt32ConstantExpression { Value = Value };
		}
	}
}

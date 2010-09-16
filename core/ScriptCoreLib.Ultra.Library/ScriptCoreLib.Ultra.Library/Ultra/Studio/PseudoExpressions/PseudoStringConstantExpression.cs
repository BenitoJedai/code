using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.PseudoExpressions
{
    public class PseudoStringConstantExpression : PseudoConstantExpression
	{
		public string Value;

		public static implicit operator PseudoStringConstantExpression(string Value)
		{
			return new PseudoStringConstantExpression { Value = Value };
		}
	}
}

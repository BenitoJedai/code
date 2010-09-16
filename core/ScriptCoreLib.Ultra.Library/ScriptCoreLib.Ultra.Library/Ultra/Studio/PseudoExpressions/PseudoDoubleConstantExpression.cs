using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.PseudoExpressions
{
    public class PseudoDoubleConstantExpression : PseudoNumericConstantExpression
	{
		public double Value;

        public static implicit operator PseudoDoubleConstantExpression(double Value)
		{
            return new PseudoDoubleConstantExpression { Value = Value };
		}
	}
}

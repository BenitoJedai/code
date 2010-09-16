using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.PseudoExpressions
{
    public class PseudoNumericConstantExpression : PseudoConstantExpression
    {
        public static  implicit operator PseudoNumericConstantExpression (int e)
        {
            return (PseudoInt32ConstantExpression)e;
        }

        public static implicit operator PseudoNumericConstantExpression(double e)
        {
            return (PseudoDoubleConstantExpression)e;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.Ultra.Studio.PseudoExpressions
{
	public class PseudoArrayExpression
	{
		public SolutionProjectLanguageType ElementType;

        // ahh. now java will soon support List<T> too :)
		public readonly ArrayList Items = new ArrayList();
	}
}

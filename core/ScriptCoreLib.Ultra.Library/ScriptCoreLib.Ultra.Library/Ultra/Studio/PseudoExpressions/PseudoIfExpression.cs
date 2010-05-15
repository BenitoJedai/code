using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.PseudoExpressions
{
	public class PseudoIfExpression
	{
		public object Expression;

		public SolutionProjectLanguageCode TrueCase;
		public SolutionProjectLanguageCode FalseCase;
	}
}

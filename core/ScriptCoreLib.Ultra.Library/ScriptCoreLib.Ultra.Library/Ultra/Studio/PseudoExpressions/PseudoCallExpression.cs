using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.PseudoExpressions
{
	public class PseudoCallExpression
	{

		public SolutionFileComment Comment;

		public object Object;

		public SolutionProjectLanguageMethod Method;

		public object[] ParameterExpressions;

		public bool IsAttributeContext;

		public static implicit operator Uri(PseudoCallExpression that)
		{
			if (that.Comment == null)
				return null;

			return that.Comment.Link;
		}
	}
}

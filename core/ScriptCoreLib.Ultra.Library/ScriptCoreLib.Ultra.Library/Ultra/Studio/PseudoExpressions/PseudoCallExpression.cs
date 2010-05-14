using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Studio.PseudoExpressions
{
	public class PseudoCallExpression
	{

		public SolutionFileComment Comment;

		public object Object;

		public SolutionProjectLanguageMethod Method;

		public object[] ParameterExpressions = new object[0];


		public bool IsAttributeContext;

		public static implicit operator Uri(PseudoCallExpression that)
		{
			if (that.Comment == null)
				return null;

			return that.Comment.Link;
		}

		/// <summary>
		/// Visual Basic can inline xml. When this field is set, 
		/// this expression is equal to the XLinq field.
		/// </summary>
		public XElement XLinq;

	}
}

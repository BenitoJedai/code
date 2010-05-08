using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.StockAttributes
{
	public class StockAttributeGeneralInformation : SolutionProjectLanguageAttribute
	{
		public StockAttributeGeneralInformation(SolutionProjectLanguageType Type, PseudoConstantExpression Constant)
		{
			this.Type = Type;
			this.Parameters = new[] { Constant };

		}
	}
}

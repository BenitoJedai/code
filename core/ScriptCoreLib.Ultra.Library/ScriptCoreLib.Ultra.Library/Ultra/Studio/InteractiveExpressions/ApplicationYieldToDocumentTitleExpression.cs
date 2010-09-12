using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;

namespace ScriptCoreLib.Ultra.Studio.InteractiveExpressions
{
	public class ApplicationYieldToDocumentTitleExpression : PseudoCallExpression
	{
		public ApplicationYieldToDocumentTitleExpression(SolutionBuilderInteractive Interactive)
		{
            this.Comment = "Show the server message as document title";
       
			this.Method = new KnownStockTypes.ScriptCoreLib.JavaScript.Extensions.JavaScriptStringExtensions.ToDocumentTitle();


			this.ParameterExpressions = new[] {
				Interactive.YieldMethod_doc
			};
		}
	}

}

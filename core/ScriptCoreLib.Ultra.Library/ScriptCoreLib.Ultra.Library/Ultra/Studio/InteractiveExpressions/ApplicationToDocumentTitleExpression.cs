using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;

namespace ScriptCoreLib.Ultra.Studio.InteractiveExpressions
{
	public class ApplicationToDocumentTitleExpression : PseudoCallExpression
	{
		public PseudoStringConstantExpression Title { get; private set; }

		public ApplicationToDocumentTitleExpression()
		{
            //this.Comment = (InteractiveComment)"Update document title";



			this.Method = new KnownStockTypes.ScriptCoreLib.JavaScript.Extensions.JavaScriptStringExtensions.ToDocumentTitle();


			this.Title = new PseudoStringConstantExpression { Value = "Hello world" };

			this.ParameterExpressions = new[] {
				this.Title
			};
		}
	}

}

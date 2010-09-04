using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.StockPages;
using ScriptCoreLib.Ultra.Studio.StockData;

namespace ScriptCoreLib.Ultra.Studio.InteractiveExpressions
{
	public class ApplicationCallWebMethodExpression : PseudoCallExpression
	{

		public ApplicationCallWebMethodExpression(SolutionBuilderInteractive Interactive)
		{
            this.Comment = "Send data from JavaScript to the server tier";

            // why isn't this showing up?
            this.GetObject = () => Interactive.Application_service;

			this.Method = Interactive.ApplicationWebServiceType.WebMethod2;


			// promote to stockmethod?
			var YieldMethod = new SolutionProjectLanguageMethod
			{
				Code = new SolutionProjectLanguageCode
				{
					Interactive.ApplicationYieldToDocumentTitle
				}
			};

			YieldMethod.Parameters.Add(Interactive.YieldMethod_doc);


			this.ParameterExpressions = new object[] {
				(PseudoStringConstantExpression) "A string from JavaScript.",
				YieldMethod
			};
		}
	}
}

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
		public readonly InteractiveComment InteractiveComment = "Send xml to server";

		public ApplicationCallWebMethodExpression(SolutionBuilderInteractive Interactive)
		{
			var XElement = new SolutionProjectLanguageType
			{
				Namespace = "System.Xml.Linq",
				Name = "XElement"
			};


			InteractiveComment.Click +=
				delegate
				{
					if (this.Method == null)
					{
						this.Method = Interactive.ApplicationWebServiceType.WebMethod2;
						this.InteractiveComment.Comment = "Send xml to server";
					}
					else
					{
						this.Method = null;
						this.InteractiveComment.Comment = "You could send xml to server";
					}
				};

			this.Comment = InteractiveComment;

			this.Object = new PseudoCallExpression
			{

				Method = new SolutionProjectLanguageMethod
				{
					Name = SolutionProjectLanguageMethod.ConstructorName,

					DeclaringType = Interactive.ApplicationWebServiceType,
					ReturnType = Interactive.ApplicationWebServiceType
				},

				ParameterExpressions = new object[] {
				}
			};

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
				StockWebMethod2Data.Element,
				YieldMethod
			};
		}
	}
}

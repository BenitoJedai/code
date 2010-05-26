using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio.StockMethods
{
	public class StockMethodWebMethod : SolutionProjectLanguageMethod
	{
		public StockMethodWebMethod(SolutionBuilderInteractive Interactive)
		{
			var _XElement = new SolutionProjectLanguageType
			{
				Namespace = "System.Xml.Linq",
				Name = "XElement"
			};

			// note: this method will run under javascript

			#region Parameters e y
			var _e = new SolutionProjectLanguageArgument
			{
				Type = _XElement,

				Name = "e",
				Summary = "A parameter from javascript"
			};


			var _y_Type = new SolutionProjectLanguageType
			{
				Namespace = "System",
				Name = "Action",
			}.With(
				k =>
				{
					k.Arguments.Add(
						new SolutionProjectLanguageArgument
						{
							Type = _XElement
						}
					);
				}
			);

			var _y = new SolutionProjectLanguageArgument
			{
				Type = _y_Type,

				Name = "y",
				Summary = "A callback to javascript"
			};
			#endregion

			this.Name = "WebMethod2";
			this.Summary = "This Method is a javascript callable method.";

			this.Code = new SolutionProjectLanguageCode
			{
                //Interactive.WebMethod2,

				"Send it to the caller.",
				new PseudoCallExpression
				{
					Object = _y.Name,

					Method =  new SolutionProjectLanguageMethod { Name = "Invoke" },

					ParameterExpressions = new [] {
						_e.Name
					}
				}
			};


			this.Parameters.Add(_e);

			this.Parameters.Add(_y);
		}

	}
}

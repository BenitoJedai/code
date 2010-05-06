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
		public StockMethodWebMethod(string Name)
		{
			// note: this method will run under javascript

			#region Parameters e y
			var _e = new SolutionProjectLanguageArgument
			{
				Type = new SolutionProjectLanguageType
				{
					Name = "XElement"
				},

				Name = "e",
				Summary = "A parameter from javascript"
			};

			var _y = new SolutionProjectLanguageArgument
			{
				Type = new SolutionProjectLanguageType
				{
					Name = "Action",
				}.With(
					k =>
					{
						k.Arguments.Add(
							new SolutionProjectLanguageArgument
							{
								Type = new SolutionProjectLanguageType
								{
									Name = "XElement"
								}
							}
						);
					}
				),

				Name = "y",
				Summary = "A callback to javascript"
			};
			#endregion

			this.Name = Name;
			this.Summary = "Method " + Name + " is a javascript callable method.";

			this.Code = new SolutionProjectLanguageCode
				{
					"Prepare the yield value for " + Name,
					new PseudoCallExpression
					{
						Object = new PseudoCallExpression
						{
							Object = _e.Name,

							Method =  new SolutionProjectLanguageMethod { Name = "Element" },

							Parameters = new [] {
								new PseudoConstantExpression { Value = "Data" }
							}
						},

						Method =  new SolutionProjectLanguageMethod { Name = "ReplaceWith" },

						Parameters = new [] {
							new PseudoConstantExpression { Value = "Method " + Name + " from the web server" }
						}
					},

					"Send it to the caller.",
					new PseudoCallExpression
					{
						Object = _y.Name,

						Method =  new SolutionProjectLanguageMethod { Name = "Invoke" },

						Parameters = new [] {
							_e.Name
						}
					}
				};


			this.Parameters.Add(_e);

			this.Parameters.Add(_y);
		}

	}
}

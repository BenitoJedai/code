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

						Method =  new SolutionProjectLanguageMethod { 
							DeclaringType = _XElement,
							Name = "Element" 
						},

						Parameters = new [] {
							new PseudoConstantExpression { Value = "Data" }
						}
					},

					Method =  new SolutionProjectLanguageMethod { 
						DeclaringType = _XElement,
						Name = "ReplaceWith" },

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;
using System.Linq.Expressions;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio
{
	partial class SolutionBuilder
	{
		private static SolutionProjectLanguageMethod WebMethod2(string Name)
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

			return new SolutionProjectLanguageMethod
			{
				Name = Name,
				Summary = "Method " + Name + " is a javascript callable method.",

				Code = new SolutionProjectLanguageCode
				{
					"Prepare the yield value for " + Name,
					new PseudoCallExpression
					{
						Object = new PseudoCallExpression
						{
							Object = _e.Name,

							Method = "Element",

							Parameters = new [] {
								new PseudoConstantExpression { Value = "Data" }
							}
						},

						Method = "ReplaceWith",

						Parameters = new [] {
							new PseudoConstantExpression { Value = "Method " + Name + " from the web server" }
						}
					},

					"Send it to the caller.",
					new PseudoCallExpression
					{
						Object = _y.Name,

						Method = "Invoke",

						Parameters = new [] {
							_e.Name
						}
					}
				}

			}.With(
				Method =>
				{
					Method.Parameters.Add(_e);

					Method.Parameters.Add(_y);
				}
			);
		}


	}
}

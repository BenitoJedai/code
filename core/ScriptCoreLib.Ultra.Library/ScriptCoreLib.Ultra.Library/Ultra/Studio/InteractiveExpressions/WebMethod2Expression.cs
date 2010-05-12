using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio.InteractiveExpressions
{
	public class WebMethod2Expression : PseudoCallExpression
	{
		public PseudoConstantExpression Title { get; private set; }

		public WebMethod2Expression()
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


			this.Comment = (InteractiveComment)"Send something back from WebMethod2";

			this.Object = new PseudoCallExpression
			{
				Object = _e.Name,

				Method = new SolutionProjectLanguageMethod
				{
					DeclaringType = _XElement,
					Name = "Element"
				},

				ParameterExpressions = new[] {
					new PseudoCallExpression {
						
						Method = new SolutionProjectLanguageMethod
						{
							DeclaringType = new SolutionProjectLanguageType
							{
								Namespace = "System.Xml.Linq",
								Name = "XName"
							},
							IsStatic = true,
							Name = SolutionProjectLanguageMethod.op_Implicit
						},
						ParameterExpressions = new []
						{
							new PseudoConstantExpression { 
								Value = 
									"Data" 
							}
						}
					}
					
				}
			};

			this.Method = new SolutionProjectLanguageMethod
			{
				DeclaringType = _XElement,
				Name = "ReplaceAll"
			};

			this.Title = new PseudoConstantExpression { Value = "Data from the web server" };

			ParameterExpressions = new[] {
				this.Title
			};
		}
	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Studio.StockTypes;

namespace ScriptCoreLib.Ultra.Studio.InteractiveExpressions
{
	public class WebMethod2Expression : PseudoCallExpression
	{
		public PseudoStringConstantExpression Title { get; private set; }

		public WebMethod2Expression()
		{
            // PHP does not yet support XLinq
            // Java does not yet support Generics

		

			// note: this method will run under javascript

			#region Parameters e y
			var _e = new SolutionProjectLanguageArgument
			{
                Type = new KnownStockTypes.System.String(),

				Name = "e",
				Summary = "A parameter from javascript. JSC supports string data type for all platforms."
			};

			var _y = new SolutionProjectLanguageArgument
			{
                Type = new KnownStockTypes.ScriptCoreLib.Delegates.StringAction(),

				Name = "y",
				Summary = "A callback to javascript. In the future all platforms will allow Action<XElementConvertable> delegates."
			};
			#endregion


			this.Comment = (InteractiveComment)"Send something back from WebMethod2";

			this.Object = new PseudoCallExpression
			{
				Object = _e.Name,

				Method = new SolutionProjectLanguageMethod
				{
					DeclaringType = new KnownStockTypes.System.Xml.Linq.XElement(),
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
							new PseudoStringConstantExpression { 
								Value = 
									"Data" 
							}
						}
					}
					
				}
			};

			this.Method = new SolutionProjectLanguageMethod
			{
                DeclaringType = new KnownStockTypes.System.Xml.Linq.XElement(),
				Name = "ReplaceAll"
			};

			this.Title = new PseudoStringConstantExpression { Value = "Data from the web server" };

			ParameterExpressions = new[] {
				this.Title
			};
		}
	}

}

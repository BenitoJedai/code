using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.StockTypes
{
	public class StockAppletType : SolutionProjectLanguageType
	{
		public StockAppletType(string Namespace, string Name)
		{
			this.Namespace = Namespace;
			this.Name = Name;

			this.BaseType = new SolutionProjectLanguageType
			{
				Namespace = "java.applet",
				Name = "Applet"
			};

			this.IsSealed = true;

			var init = new SolutionProjectLanguageMethod
			{
				DeclaringType = this,
				Name = "init",
				IsOverride = true,
				Code = new SolutionProjectLanguageCode
				{
					new PseudoCallExpression
					{
						Object = new PseudoBaseExpression(),
						Method = new SolutionProjectLanguageMethod
						{
							DeclaringType = BaseType,
							Name = "resize",
						},
						ParameterExpressions = new []
						{
							(PseudoInt32ConstantExpression)400,
							(PseudoInt32ConstantExpression)300,
						}
					}
				}
			};

			this.Methods.Add(init);
		}
	}
}

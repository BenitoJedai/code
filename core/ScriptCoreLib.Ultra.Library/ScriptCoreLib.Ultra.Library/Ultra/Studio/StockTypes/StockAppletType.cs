﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.StockTypes
{
	public class StockAppletType : SolutionProjectLanguageType
	{
		public readonly SolutionProjectLanguageMethod init;

		public StockAppletType(string Namespace, string Name)
		{
			this.Namespace = Namespace;
			this.Name = Name;

			this.BaseType = new KnownStockTypes.java.applet.Applet();

			this.IsSealed = true;

			this.init = new SolutionProjectLanguageMethod
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

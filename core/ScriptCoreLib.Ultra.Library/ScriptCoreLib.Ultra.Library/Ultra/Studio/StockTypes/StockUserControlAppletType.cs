using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.StockTypes
{
	public class StockUserControlAppletType : StockAppletType
	{
		public StockUserControlAppletType(string Namespace, string Name, StockUserControlType Content)
			: base(Namespace, Name)
		{
			this.init.Code = new SolutionProjectLanguageCode
			{
				new PseudoCallExpression
				{
					Method = new SolutionProjectLanguageMethod 
					{
						Name = "EnableVisualStyles",
						IsExtensionMethod = true,
						DeclaringType = new SolutionProjectLanguageType
						{
							Namespace = "ScriptCoreLib.Java.Extensions",
							Name = "WindowsFormsExtensions"
						}
					
					},
					ParameterExpressions = new []
					{
						new PseudoThisExpression()
					}
				},
				new PseudoCallExpression
				{
					Method = new SolutionProjectLanguageMethod 
					{
						Name = "ReplaceContentWith",
						IsExtensionMethod = true,
						DeclaringType = new SolutionProjectLanguageType
						{
							Namespace = "ScriptCoreLib.Java.Extensions",
							Name = "WindowsFormsExtensions"
						}
					
					},
					ParameterExpressions = new object []
					{
						new PseudoThisExpression(),
						Content.GetConstructorExpression()
					}
				}
			};
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.StockExpressions
{
	public class StockReplaceWithNewAppletExpression : PseudoCallExpression
	{
		public StockReplaceWithNewAppletExpression(string Namespace, string Name)
		{
			var page_Page1 =
				new PseudoCallExpression
				{
					// Application(page)
					Object = "page",

					Method =
						new SolutionProjectLanguageMethod
						{
							IsProperty = true,
							Name = "get_" + Name,
							ReturnType = new SolutionProjectLanguageType
							{
								Name = "IHTMLImage"
							}
						}
				};

			var new_Page1 =
				new PseudoCallExpression
				{
					// Application(page)

					Method =
						new SolutionProjectLanguageMethod
						{
							Name = SolutionProjectLanguageMethod.ConstructorName,
							DeclaringType = new SolutionProjectLanguageType
							{
								Namespace = Namespace,
								Name = Name
							}
						}
				};

			this.Comment = "Initialize " + Name + " by replacing the placeholder";

			this.Method =
				new SolutionProjectLanguageMethod
				{
					IsStatic = true,
					IsExtensionMethod = true,
					Name = "ReplaceWith",
					ReturnType = new SolutionProjectLanguageType
					{
						Name = "AppletExtensions"
					}
				};

			this.ParameterExpressions = new[]
			{
				page_Page1,
				new_Page1
			};
		}
	}
}

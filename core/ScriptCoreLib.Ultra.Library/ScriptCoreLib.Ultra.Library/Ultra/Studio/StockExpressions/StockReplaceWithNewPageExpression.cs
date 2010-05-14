using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.StockExpressions
{
	public class StockReplaceWithNewPageExpression : PseudoCallExpression
	{
		public StockReplaceWithNewPageExpression(string Name)
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
								Name = Name
							}
						}
				};

			var new_Page1_Container =
				new PseudoCallExpression
				{
					// Application(page)
					Object = new_Page1,

					Method =
						new SolutionProjectLanguageMethod
						{
							IsProperty = true,
							Name = "get_Container",
							ReturnType = new SolutionProjectLanguageType
							{
								Name = "IHTMLDiv"
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
						Name = "INodeExtensions"
					}
				};

			this.ParameterExpressions = new[]
			{
				page_Page1,
				new_Page1_Container
			};
		}
	}
}

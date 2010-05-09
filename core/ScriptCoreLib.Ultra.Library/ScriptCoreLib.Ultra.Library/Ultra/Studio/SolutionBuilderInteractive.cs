using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.Languages;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionBuilderInteractive
	{
		public ApplicationToDocumentTitleExpression ApplicationToDocumentTitle =
			new ApplicationToDocumentTitleExpression();

		public InteractiveComment
			ToVisualBasicLanguage = "Convert to Visual Basic project",
			ToVisualCSharpLanguage = "Convert to Visual CSharp project";

		public SolutionBuilderInteractive()
		{
			ToVisualBasicLanguage.IsActiveFilter =
				Context =>
				{
					return !(Context.Language is VisualBasicLanguage);
				};

			ToVisualCSharpLanguage.IsActiveFilter =
				Context =>
				{
					return !(Context.Language is VisualCSharpLanguage);
				};
		}

		public class InteractiveComment : SolutionFileComment
		{
			public static implicit operator InteractiveComment(string Comment)
			{
				return new InteractiveComment
				{
					Comment = Comment,
					Link = new Uri("http://do.jsc-solutions.net/#" + Comment.Replace(" ", "-")),
					MarginBottom = 1
				};
			}
		}


		public class ApplicationToDocumentTitleExpression : PseudoCallExpression
		{
			public PseudoConstantExpression Title { get; private set; }

			public ApplicationToDocumentTitleExpression()
			{
				this.Comment = (InteractiveComment)"Update document title";



				this.Method = new SolutionProjectLanguageMethod
				{
					IsExtensionMethod = true,
					Name = "ToDocumentTitle",
					DeclaringType = new SolutionProjectLanguageType
					{
						Namespace = "ScriptCoreLib.JavaScript.Extensions",
						Name = "JavaScriptStringExtensions"
					}
				};

				this.Title = new PseudoConstantExpression { Value = "Hello world" };

				this.ParameterExpressions = new[] {
					this.Title
				};
			}
		}
	}
}

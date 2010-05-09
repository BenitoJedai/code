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
			ToVisualBasicLanguage = "View as Visual Basic project",
			ToVisualCSharpLanguage = "View as Visual CSharp project",
			ToVisualFSharpLanguage = "View as Visual FSharp project";

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

			ToVisualFSharpLanguage.IsActiveFilter =
				Context =>
				{
					return !(Context.Language is VisualFSharpLanguage);
				};
		}

		public class InteractiveComment : SolutionFileComment
		{
			public static implicit operator InteractiveComment(string Comment)
			{
				return new InteractiveComment
				{
					Comment = Comment,
					// we should redirec to wiki from there if the user
					// actually navigates there :)
					Link = new Uri("http://do.jsc-solutions.net/" + Comment.Replace(" ", "-")),
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

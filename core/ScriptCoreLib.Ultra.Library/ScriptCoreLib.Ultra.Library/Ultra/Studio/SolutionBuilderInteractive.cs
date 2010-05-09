using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.Languages;
using ScriptCoreLib.Ultra.Studio.InteractiveExpressions;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionBuilderInteractive
	{
		public ApplicationToDocumentTitleExpression ApplicationToDocumentTitle =
			new ApplicationToDocumentTitleExpression();

		public WebMethod2Expression WebMethod2 =
			new WebMethod2Expression();

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

	

	}
}

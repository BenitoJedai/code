using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.Languages;
using ScriptCoreLib.Ultra.Studio.InteractiveExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionBuilderInteractive
	{
		public StockApplicationWebServiceType ApplicationWebServiceType;

		public ApplicationCallWebMethodExpression ApplicationCallWebMethod;

		public ApplicationToDocumentTitleExpression ApplicationToDocumentTitle =
			new ApplicationToDocumentTitleExpression();

		public ApplicationYieldToDocumentTitleExpression ApplicationYieldToDocumentTitle;


		public WebMethod2Expression WebMethod2 =
			new WebMethod2Expression();

		public InteractiveComment
			ToVisualBasicLanguage = "View as Visual Basic project",
			ToVisualCSharpLanguage = "View as Visual CSharp project",
			ToVisualFSharpLanguage = "View as Visual FSharp project";

		public SolutionProjectLanguageArgument YieldMethod_doc = new SolutionProjectLanguageArgument
		{
			Name = "doc",
			Type = new StockXElementType()
		};

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

			this.ApplicationWebServiceType = new StockApplicationWebServiceType(this);


			this.ApplicationYieldToDocumentTitle = new ApplicationYieldToDocumentTitleExpression(this);

			
			this.ApplicationCallWebMethod = new ApplicationCallWebMethodExpression(this);

		}

		public InteractiveComment[] Comments
		{
			get
			{
				return new InteractiveComment[]
				{
					this.ApplicationCallWebMethod.InteractiveComment,
					this.ApplicationYieldToDocumentTitle.InteractiveComment
				};
			}
		}

	}
}

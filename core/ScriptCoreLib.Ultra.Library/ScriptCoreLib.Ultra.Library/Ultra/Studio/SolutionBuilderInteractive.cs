using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.Languages;
using ScriptCoreLib.Ultra.Studio.InteractiveExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionBuilderInteractive
	{
		// jsc shall ignore img.src and background as it is a placeholder
		// will not download the referenced source
		// will not generate types for this image
		public const string DataTypeAttribute = "data-jsc-type";


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
			Name = "value",
            Type = new SolutionProjectLanguageType.System.String()
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
                    //this.ApplicationCallWebMethod.InteractiveComment,
                    //this.ApplicationYieldToDocumentTitle.InteractiveComment
				};
			}
		}



		public event Action<Action<SolutionProjectHTMLFile>> GenerateHTMLFiles;
		public void RaiseGenerateHTMLFiles(Action<SolutionProjectHTMLFile> e)
		{
			if (this.GenerateHTMLFiles != null)
				this.GenerateHTMLFiles(e);
		}

		public event Action<Action<SolutionProjectLanguageType>> GenerateTypes;
		public void RaiseGenerateTypes(Action<SolutionProjectLanguageType> e)
		{
			if (this.GenerateTypes != null)
				this.GenerateTypes(e);
		}


		public event Action<Action<PseudoCallExpression>> GenerateApplicationExpressions;
		public void RaiseGenerateApplicationExpressions(Action<PseudoCallExpression> e)
		{
			if (this.GenerateApplicationExpressions != null)
				this.GenerateApplicationExpressions(e);
		}


        public SolutionProjectLanguageField Application_service;

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.Languages;
using ScriptCoreLib.Ultra.Studio.InteractiveExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.Studio.StockMethods;

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

	

		public SolutionProjectLanguageArgument YieldMethod_doc = new SolutionProjectLanguageArgument
		{
			Name = "value",
            Type = new KnownStockTypes.System.String()
		};

        public SolutionProjectLanguageType ApplicationType;
        public SolutionProjectLanguageType ProgramType;
        public StockMethodMain ProgramType_MainMethod;

		public SolutionBuilderInteractive()
		{
            Initialize();



			this.ApplicationYieldToDocumentTitle = new ApplicationYieldToDocumentTitleExpression(this);

			
			this.ApplicationCallWebMethod = new ApplicationCallWebMethodExpression(this);
		}

        public void Initialize()
        {
            this.ApplicationWebServiceType = new StockApplicationWebServiceType(this);

            this.ApplicationType = new SolutionProjectLanguageType
            {
                IsSealed = true,

                Name = "Application",
                Summary = "This type will run as JavaScript.",

            };

            this.Application_service = new SolutionProjectLanguageField
            {
                FieldType = ApplicationWebServiceType,
                FieldConstructor = ApplicationWebServiceType.GetDefaultConstructor(),
                Name = "service",
                IsReadOnly = true
            };

            // we are adding a field. does it show up in the source code later?
            // SolutionProjectLanguage.WriteType makes it happen!
            this.ApplicationType.Fields.Add(this.Application_service);


            this.ProgramType = new SolutionProjectLanguageType
            {
                IsStatic = true,
                Name = "Program",
                Summary = "You can debug your application by hitting F5.",
                DependentUpon = ApplicationType
            };

            this.ProgramType_MainMethod = new StockMethodMain(ApplicationType);
            this.ProgramType.Methods.Add(this.ProgramType_MainMethod);
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

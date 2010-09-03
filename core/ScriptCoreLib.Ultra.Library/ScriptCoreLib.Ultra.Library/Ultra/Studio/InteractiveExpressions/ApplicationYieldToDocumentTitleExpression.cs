using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.InteractiveExpressions
{
	public class ApplicationYieldToDocumentTitleExpression : PseudoCallExpression
	{
		public ApplicationYieldToDocumentTitleExpression(SolutionBuilderInteractive Interactive)
		{
            this.Comment = "Show the server message as document title";
       
			this.Method = new SolutionProjectLanguageMethod
			{
				IsExtensionMethod = true,
				Name = "ToDocumentTitle",
				DeclaringType = new SolutionProjectLanguageType
				{
					Namespace = "ScriptCoreLib.JavaScript.Extensions",
					Name = "JavaScriptStringExtensions"
				},
				ReturnType = new SolutionProjectLanguageType.System.String()
			};

			this.ParameterExpressions = new[] {
				Interactive.YieldMethod_doc
			};
		}
	}

}

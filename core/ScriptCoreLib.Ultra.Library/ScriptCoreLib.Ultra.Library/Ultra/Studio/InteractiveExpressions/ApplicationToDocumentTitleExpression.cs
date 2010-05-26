using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.InteractiveExpressions
{
	public class ApplicationToDocumentTitleExpression : PseudoCallExpression
	{
		public PseudoStringConstantExpression Title { get; private set; }

		public ApplicationToDocumentTitleExpression()
		{
            //this.Comment = (InteractiveComment)"Update document title";



			this.Method = new SolutionProjectLanguageMethod
			{
				IsExtensionMethod = true,
				Name = "ToDocumentTitle",
				DeclaringType = new SolutionProjectLanguageType
				{
					Namespace = "ScriptCoreLib.JavaScript.Extensions",
					Name = "JavaScriptStringExtensions"
				},
				ReturnType = new SolutionProjectLanguageType
				{
					Namespace = "System",
					Name = "String"
				}
			};

			this.Title = new PseudoStringConstantExpression { Value = "Hello world" };

			this.ParameterExpressions = new[] {
				this.Title
			};
		}
	}

}

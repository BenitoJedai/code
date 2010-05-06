using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.StockMethods
{
	public class StockMethodApplication : SolutionProjectLanguageMethod
	{
		public StockMethodApplication(SolutionProjectLanguageType DeclaringType)
		{
			// note: this method will run under javascript

			#region Parameters args
			var _page = new SolutionProjectLanguageArgument
			{
				Type = new SolutionProjectLanguageType
				{
					Name = "IApplicationPage"
				},

				Name = "page",
				Summary = "HTML document rendered by the web server which can now be enhanced."
			};

			#endregion

			this.Name = SolutionProjectLanguageMethod.ConstructorName;
			this.Summary = "This is a javascript application.";
			this.Code = new SolutionProjectLanguageCode
			{
				"Hello world",
			
			};
			this.DeclaringType = DeclaringType;
			this.Parameters.Add(_page);
		}

	}
}

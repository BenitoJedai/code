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
					Name = "IDefaultPage"
				},

				Name = "page",
				Summary = "HTML document rendered by the web server which can now be enhanced."
			};

			#endregion

			this.Name = SolutionProjectLanguageMethod.ConstructorName;
			this.Summary = "This is a javascript application.";
			this.Code = new SolutionProjectLanguageCode
			{
				new SolutionFileComment
				{
					Comment = "Change the title",
					Link = new Uri("http://do.jsc-solutions.net/#Change-the-title")
				},

				"Native.Document.title = \"Hello world\";",

					new SolutionFileComment
				{
					Comment = "Undo: Change the title",
					Link = new Uri("http://do.jsc-solutions.net/#Undo-Change-the-title")
				},


				new SolutionFileComment
				{
					Comment = "Add HTML via XElement",
					Link = new Uri("http://do.jsc-solutions.net/#Add-HTML-via-XElement")
				},


				"Hello world",
				"Native.Document.Title = new WebService().GetTitle()",
			};
			this.DeclaringType = DeclaringType;
			this.Parameters.Add(_page);
		}

	}
}

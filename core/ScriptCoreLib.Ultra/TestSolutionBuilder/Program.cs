using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio;
using System.IO;
using ScriptCoreLib.Ultra.Studio.Languages;
using ScriptCoreLib.Ultra.Studio.StockPages;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.StockExpressions;

namespace TestSolutionBuilder
{
	class Program
	{
		static void Main(string[] args)
		{
			var sln = new SolutionBuilder
			{
				Name = "VisualCSharpProject1",
				//Language = new VisualFSharpLanguage()
			};


			sln.Interactive.GenerateApplicationExpressions +=
				Add =>
				{
					//page.PageContainer.ReplaceWith(

					Add(
						new StockReplaceWithNewPageExpression("Page1")
					);

				};

			sln.Interactive.GenerateHTMLFiles +=
				Add =>
				{
					var Content = new XElement(StockPageDefault.Page);

					Content.Element("head").Element("title").Value = "Page1";

					Add(
						new SolutionProjectHTMLFile
						{
							Name = "Design/Page1.htm",
							Content = Content
						}
					);
				};

			sln.WriteToConsole();
		}
	}
}

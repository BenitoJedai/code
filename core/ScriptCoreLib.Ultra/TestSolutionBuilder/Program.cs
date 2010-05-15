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
using ScriptCoreLib.Ultra.Studio.StockTypes;

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

					//Add(
					//    new StockReplaceWithNewPageExpression("Page1")
					//);


					//Add(
					//    new StockReplaceWithNewUserControlExpression(sln.Name + ".Components", "UserControl1")
					//);
				};

			sln.Interactive.GenerateTypes +=
				Add =>
				{
					var Namespace = sln.Name + ".Components";
					//var Name = "UserControl1";


					Add(
						new StockAppletType(Namespace, "Applet1")
					);
				};

			//sln.Interactive.GenerateHTMLFiles +=
			//    Add =>
			//    {
			//        var Content = new XElement(StockPageDefault.Page);

			//        Content.Element("head").Element("title").Value = "Page1";

			//        Add(
			//            new SolutionProjectHTMLFile
			//            {
			//                Name = "Design/Page1.htm",
			//                Content = Content
			//            }
			//        );
			//    };

			sln.WriteToConsole();
		}
	}
}

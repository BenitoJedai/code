using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using System;
using CakeCuttingProblem.Library;


namespace CakeCuttingProblemDocument.js
{
	[Script, ScriptApplicationEntryPoint]
	public class CakeCuttingProblemDocument
	{

		public CakeCuttingProblemDocument()
		{
			Func<string, Action<string>> WithColor =
				color =>
					text =>
					{
						var div = new IHTMLDiv();

						div.innerText = text;
						div.style.color = color;

						div.AttachToDocument();

					};



			DemoSituation.Demo(
				WithColor("red"),
				WithColor("green"),
				WithColor("blue"),
				WithColor("yellow")
			);


		}


		static CakeCuttingProblemDocument()
		{
			typeof(CakeCuttingProblemDocument).SpawnTo(i => new CakeCuttingProblemDocument());
		}

	}

}

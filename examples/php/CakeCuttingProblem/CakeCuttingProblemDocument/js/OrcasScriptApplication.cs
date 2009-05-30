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
			Native.Document.body.style.overflow = IStyle.OverflowEnum.auto;

			Func<string, Action<string>> WithColor =
				color =>
					text =>
					{
						var div = new IHTMLDiv();

						div.innerText = text;
						div.style.color = color;

						div.AttachToDocument();

					};



			var h = DemoSituation.Demo(
				WithColor("red"),
				WithColor("green"),
				WithColor("blue"),
				WithColor("yellow")
			);

			{
				var t = new IHTMLTable().AttachToDocument();

				t.style.border = "1px solid gray";

				var b = t.AddBody();

				var header = b.AddRow();

				header.AddColumn();

				h.Menu.ForEach(m => header.AddColumn(m));
			
				h.Clients.ForEach(
					(client, i) =>
					{
						var r = b.AddRow();

						r.AddColumn(client.Name);

						var sum = 0;
						client.PreferenceList.ForEach(
							(p, j) =>
							{
								var xx = r.AddColumn("$" + p);

						
								sum += p;
							}
						);
						
					}
				);
			}

			{
				var t = new IHTMLTable().AttachToDocument();

				t.style.border = "1px solid gray";

				var b = t.AddBody();

				var header = b.AddRow();

				header.AddColumn();

				h.Menu.ForEach(m => header.AddColumn(m));
				header.AddColumn("summa");
				header.AddColumn("soov");
				header.AddColumn("vahe");
				header.AddColumn("kompensatsioon");

				h.Clients.ForEach(
					(client, i) =>
					{
						var r = b.AddRow();

						r.AddColumn(client.Name);

						var sum = 0;
						client.PreferenceList.ForEach(
							(p, j) =>
							{
								var xx = r.AddColumn("$" + p);

								if (h.Decide_Compensation[j].ClientIndex == i)
									xx.style.color = "blue";

								sum += p;
							}
						);
						r.AddColumn("$" + sum);
						r.AddColumn("$" + Math.Round((double)sum / h.Clients.Length));

						r.AddColumn("$" + h.Decide_Results[i].Difference);
						r.AddColumn("$" + h.Decide_Results[i].Compensation);
					}
				);
			}
		}


		static CakeCuttingProblemDocument()
		{
			typeof(CakeCuttingProblemDocument).SpawnTo(i => new CakeCuttingProblemDocument());
		}

	}

}

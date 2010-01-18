using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using System;


namespace ReferencingWebSource.js
{
	[Script, ScriptApplicationEntryPoint]
	public class ReferencingWebSource
	{
		public ReferencingWebSource()
		{
			Native.Document.body.innerHTML = Pages.MyApp1.HTML;

			var done = false;

			Pages.MyApp1.Elements.Button1.onclick +=
				e =>
				{
					e.PreventDefault();

					if (done)
						return;

					done = true;

					{
						var news = new IHTMLDiv();

						var x = new MySnippetProject.Class1_();

						news.innerText = new SomeNamespaceForJAVA.Class1__().About() + " - " + x.Invoke("x");

						Pages.MyApp1.Elements.MyPicture.parentNode.appendChild(news);

					}
					{
						var c = new IHTMLDiv();

						c.innerHTML = Pages.Page1.HTML;

						Pages.MyApp1.Elements.MyPicture.parentNode.appendChild(c);

						Pages.Page1.Elements.Button2.onclick +=
							delegate
							{
								Native.Window.alert(Pages.Page1.Elements.TextArea1.value);
							};

					}
				};

			
		}


		static ReferencingWebSource()
		{
			typeof(ReferencingWebSource).SpawnTo(i => new ReferencingWebSource());
		}

	}

}

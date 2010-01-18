using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;


namespace ReferencingWebSource.js
{
	[Script, ScriptApplicationEntryPoint]
	public class ReferencingWebSource
	{
		public ReferencingWebSource()
		{
			Native.Document.body.innerHTML = "hello world<button id='a1'>hi</button>";

			var a1 = (IHTMLButton)Native.Document.getElementById("a1");

			a1.onclick +=
				delegate
				{
					var x = new MySnippetProject.Class1_();


					a1.innerText = new SomeNamespaceForJAVA.Class1__().About() + " - " + x.Invoke("x");
				};

			//try
			//{
			//    IStyleSheet.Default.AddRule("span",
			//        r =>
			//        {
			//            r.style.fontWeight = "bold";
			//            r.style.textDecoration = "underline";

			//        }
			//    );
			//}
			//catch
			//{
			//}

			//var btn = new IHTMLButton("Hello World!").AttachToDocument();

			//var counter = new IHTMLSpan().AttachTo(btn);

			//counter.style.margin = "1em";

			//var i = 0;

			//btn.onclick += ev =>
			//    {
			//        i++;

			//        counter.innerText = "(" + i + ")";

			//        counter.style.color = Color.FromRGB(
			//            0xff.Random(),
			//            0xff.Random(),
			//            0xff.Random()
			//        );
			//    };

			//new IHTMLBreak().AttachToDocument();
			//new IHTMLImage(Assets.Path + "/cal.png").AttachToDocument();
			//new IHTMLBreak().AttachToDocument();
			//new IHTMLImage(Assets.Path + "/Preview.png").AttachToDocument();
		}


		static ReferencingWebSource()
		{
			typeof(ReferencingWebSource).SpawnTo(i => new ReferencingWebSource());
		}

	}

}

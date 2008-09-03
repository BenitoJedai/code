using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;


namespace OrcasScriptApplication.js
{
	[Script, ScriptApplicationEntryPoint]
	public class OrcasScriptApplication
	{

		[Script]
		class BaseType
		{
		}

		[Script]
		class OtherType
		{
		}


		[Script]
		class SubType : BaseType
		{
		}

		public OrcasScriptApplication()
		{
			CheckIsOperator();

			IStyleSheet.Default.AddRule("span",
				r =>
				{
					r.style.fontWeight = "bold";
					r.style.textDecoration = "underline";

				}
			);

			var btn = new IHTMLButton("Hello World!").AttachToDocument();

			var counter = new IHTMLSpan().AttachTo(btn);

			counter.style.margin = "1em";

			var i = 0;

			btn.onclick += ev =>
				{
					i++;

					counter.innerText = "(" + i + ")";

					counter.style.color = Color.FromRGB(
						0xff.Random(),
						0xff.Random(),
						0xff.Random()
					);
				};

			new IHTMLBreak().AttachToDocument();
			new IHTMLImage(Assets.Path + "/cal.png").AttachToDocument();
			new IHTMLBreak().AttachToDocument();
			new IHTMLImage(Assets.Path + "/Preview.png").AttachToDocument();
		}

		private static void CheckIsOperator()
		{
			object x = new SubType();

			if (!(x is BaseType))
				throw new System.Exception("not BaseType");

			if (x is OtherType)
				throw new System.Exception("is OtherType");
		}

		static OrcasScriptApplication()
		{
			typeof(OrcasScriptApplication).SpawnTo(i => new OrcasScriptApplication());
		}

	}

}

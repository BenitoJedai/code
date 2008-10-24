using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace ScriptApplication.source.js.Controls
{
	[Script, ScriptApplicationEntryPoint]
	public class OrcasPHPScriptApplicationDocument
	{
		AvalonExampleGallery.JavaScript.AvalonExampleGalleryDocument Reference;

		IHTMLDiv Control = new IHTMLDiv();

		public IStyle Style { get { return Control.style; } }

		public OrcasPHPScriptApplicationDocument(IHTMLElement e)
		{
			Control.AttachToDocument();


			Control.innerHTML = "hello world (javascript)";

			Control.onmouseover += delegate { Style.color = Color.Blue; };
			Control.onmouseout += delegate { Style.color = Color.None; };

			Style.cursor = IStyle.CursorEnum.pointer;

			var btn = IHTMLButton.Create("go!",
					delegate
					{
						Control.innerHTML = "you clicked me!";

					}
				);



		}

		static OrcasPHPScriptApplicationDocument()
		{
			typeof(OrcasPHPScriptApplicationDocument).SpawnTo(i => new OrcasPHPScriptApplicationDocument(i));
		}

	}


}

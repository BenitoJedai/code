using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using WindowsFormsApplication1;


namespace WindowsFormsApplication1Document.js
{
	[Script, ScriptApplicationEntryPoint]
	public class WindowsFormsApplication1Document
	{

		public WindowsFormsApplication1Document()
		{
			new Form1().ShowAt(200, 100);
		}


		static WindowsFormsApplication1Document()
		{
			typeof(WindowsFormsApplication1Document).SpawnTo(i => new WindowsFormsApplication1Document());
		}

	}

}

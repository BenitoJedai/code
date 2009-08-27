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
		// based on http://zproxy.wordpress.com/2007/11/04/windowsforms-vectors-in-dhtml/#comments

		public WindowsFormsApplication1Document()
		{
			Program.Main();
			//new Form1().ShowAt(200, 100);
		}


		static WindowsFormsApplication1Document()
		{
			typeof(WindowsFormsApplication1Document).Spawn(); // .SpawnTo(i => new WindowsFormsApplication1Document());
		}

	}

}

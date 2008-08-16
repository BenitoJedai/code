using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;


namespace BrowserAvalonExample.js
{
	[Script, ScriptApplicationEntryPoint]
	public class BrowserAvalonExample
	{
		public BrowserAvalonExample()
		{
			// wpf here
		}

		static BrowserAvalonExample()
		{
			typeof(BrowserAvalonExample).SpawnTo(i => new BrowserAvalonExample());
		}

	}

}

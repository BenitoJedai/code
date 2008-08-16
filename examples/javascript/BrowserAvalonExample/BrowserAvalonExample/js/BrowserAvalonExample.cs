using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using BrowserAvalonExample.Code;


namespace BrowserAvalonExample.js
{
	[Script, ScriptApplicationEntryPoint]
	public class BrowserAvalonExample
	{
		public BrowserAvalonExample()
		{
			// wpf here
			AvalonExtensions.AttachToContainer(new MyCanvas(), Native.Document.body);

		}

		static BrowserAvalonExample()
		{
			typeof(BrowserAvalonExample).SpawnTo(i => new BrowserAvalonExample());
		}

	}

}

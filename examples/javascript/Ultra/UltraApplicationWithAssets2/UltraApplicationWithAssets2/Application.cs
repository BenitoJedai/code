using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using UltraApplicationWithAssets2.HTML.Audio.FromAssets;
using System.ComponentModel;

namespace UltraApplicationWithAssets2
{

	[Description("UltraApplicationWithAssets2. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{

		public Application(IHTMLElement e)
		{
			Native.Document.title = "UltraApplicationWithAssets2";



			new MyCanvas().AttachToContainer(Native.Document.body);


		}


	}


}

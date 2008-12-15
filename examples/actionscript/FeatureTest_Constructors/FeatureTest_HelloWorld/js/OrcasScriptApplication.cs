using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;


namespace FeatureTest_HelloWorld.js
{
	[Script, ScriptApplicationEntryPoint]
	public class FeatureTest_HelloWorld
	{

		public FeatureTest_HelloWorld()
		{
			var btn = new IHTMLButton("Hello World!").AttachToDocument();

			btn.onclick += 
				delegate
				{
				
					btn.style.color = Color.Blue;
				};
		}

		static FeatureTest_HelloWorld()
		{
			typeof(FeatureTest_HelloWorld).SpawnTo(i => new FeatureTest_HelloWorld());
		}

	}

}

using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;

namespace FeatureTest_FlashHelloWorld.ActionScript
{
	[Script, ScriptApplicationEntryPoint]
	[SWF]
	public class FeatureTest_FlashHelloWorld : Sprite
	{
		public FeatureTest_FlashHelloWorld()
		{
			var t = new TextField
			{
				text = "hello world"
			}.AttachTo(this);

			t.click +=
				delegate
				{
					t.setTextFormat(
						new TextFormat { color = 0xff }
					);
				};
		}
	}


}
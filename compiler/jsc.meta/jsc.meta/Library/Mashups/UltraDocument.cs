using System;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;

namespace jsc.meta.Library.Mashups
{
	class UltraDocument
	{
		class MyApplet : java.applet.Applet
		{
			public override void init()
			{
				// java applet
			}
		}

		[SWF]
		class MyFlash : Sprite
		{
			public MyFlash()
			{
				// flash object 
			}
		}

		public UltraDocument()
		{
			// javascript code

			var j = new MyApplet();
			Native.Document.body.appendChild((INode)(object)j);

			var f = new MyFlash();
			Native.Document.body.appendChild((INode)(object)j);
		}
	}
}

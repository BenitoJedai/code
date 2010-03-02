using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace OrcasUltraApplication.Advanced
{
	public sealed class UltraSprite : Sprite
	{
		public const int DefaultWidth = 500;
		public const int DefaultHeight = 400;

		public UltraSprite()
		{
			// creating the flash object 
			// + stratus
			// + alchemy

			// funny :) i have forgotten how to write anything
			// on flash API ... too much WPF API?
			var r = new Sprite();

			r.graphics.beginFill(0x7070);
			r.graphics.drawRect(8, 8, 64, 64);


			r.AttachTo(this);
		}


	}

	public static class UltraSpriteIntegration
	{
		public static void CreateSprite(this UltraApplication a)
		{
			var x = new IHTMLButton("create UltraApplet ");

			x.AttachToDocument();

			x.onclick +=
				delegate
				{
					var o = new UltraApplet();

					o.AttachAppletToDocument();
				};
		}
	}
}

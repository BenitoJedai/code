using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System.Diagnostics;

namespace UltraApplicationWithAssets1.Advanced
{
	public class UltraSpriteBase : Sprite
	{
		public const int DefaultWidth = 500;
		public const int DefaultHeight = 400;

		public UltraSpriteBase()
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


}

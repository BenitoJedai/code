using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Diagnostics;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;
using ScriptCoreLib.JavaScript.Remoting.Extensions;
using ScriptCoreLib.JavaScript.Remoting.DOM;
using ScriptCoreLib.ActionScript.flash.geom;

namespace UltraApplicationWithFlash
{
	public sealed partial class UltraSprite : Sprite
	{
		public const int DefaultWidth = 400;
		public const int DefaultHeight = 200;

		

		public UltraSprite()
		{
			{
				var r = new Sprite();


				r.graphics.beginFill(0x7000);
				r.graphics.drawRect(8, 8, 64, 32);

				r.AttachTo(this);

			}

			{
				var r = new Sprite();

				var fillType = GradientType.LINEAR;
				var colors = new uint[] { 0xFF0000, 0xFF0000 };
				var alphas = new double[] { 1, 0 };
				var ratios = new int[] { 0x00, 0xFF };
				var matr = new Matrix();
				matr.createGradientBox(DefaultWidth / 2, DefaultHeight, 0, 0, 0);
				var spreadMethod = SpreadMethod.PAD;
				this.graphics.beginGradientFill(fillType, colors, alphas, ratios, matr, spreadMethod);
				this.graphics.drawRect(0, 0, DefaultWidth / 2, DefaultHeight);

				r.AttachTo(this);
			}
		}



	
	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class FixedBorderCanvas : Sprite
	{
		public readonly Sprite CanvasOverlay;

		public readonly Sprite InfoOverlay;

		public readonly Shape BorderOverlay;

		public FixedBorderCanvas(int DefaultWidth, int DefaultHeight)
		{
	
			CanvasOverlay = new Sprite();
			// canvas is not attached

			InfoOverlay = new Sprite().AttachTo(this);
			BorderOverlay = new Shape().AttachTo(this);

			#region mask
			var CanvasMask = new Shape();

			CanvasMask.graphics.beginFill(0x00ffffff);
			CanvasMask.graphics.drawRect(0, 0, DefaultWidth, DefaultHeight);

			CanvasMask.AttachTo(this);

			CanvasOverlay.mask = CanvasMask;
			#endregion


			BorderOverlay.graphics.lineStyle(1, Colors.Green, 1);
			BorderOverlay.graphics.drawRect(0, 0, DefaultWidth - 1, DefaultHeight - 1);
		}
	}
}

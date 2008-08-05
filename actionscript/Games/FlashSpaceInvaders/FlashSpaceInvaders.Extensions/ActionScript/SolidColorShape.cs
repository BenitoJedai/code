using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class SolidColorShape : Shape
	{
		public readonly int Size;
		public readonly uint Color;

		public SolidColorShape(int size, uint color)
		{
			this.Size = size;
			this.Color = color;

			this.graphics.beginFill(color);
			this.graphics.drawRect(-size / 2, -size / 2, size, size);
		}
	}
}

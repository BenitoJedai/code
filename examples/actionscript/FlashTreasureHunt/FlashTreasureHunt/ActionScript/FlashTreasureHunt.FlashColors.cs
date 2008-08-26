using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	partial class FlashTreasureHunt
	{
		public void FlashColors(uint color)
		{
			new Bitmap(new BitmapData(DefaultWidth, DefaultHeight, false, color))
			{
				scaleX = DefaultScale,
				scaleY = DefaultScale
			}.AttachTo(this).FadeOutAndOrphanize(1000 / 15, 0.2);
		}
	}
}
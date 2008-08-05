using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript.Extensions;

using FlashSpaceInvaders.ActionScript.Extensions;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public static class Animations
	{
		public static Func<int, int, Sprite> Spawn_UFO =
			(x, y) =>
				new Sprite { x = x, y = y }.AnimateAt(
					new BitmapAsset[]
                        {
                            Assets.ufo_1.ToBitmapAsset(),
                        }
				, 500);

		public static Func<int, int, Sprite> Spawn_BigGun =
			(x, y) =>
				new Sprite { x = x, y = y }.AnimateAt(
					new BitmapAsset[]
                        {
                            Assets.biggun_1.ToBitmapAsset(),
                        }
				, 500);
	}
}

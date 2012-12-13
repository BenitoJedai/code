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
		public static Sprite Spawn_UFO(double x, double y)
		{
			return new Sprite { x = x, y = y }.AnimateAt(
				new BitmapAsset[]
                        {
                            Assets.ufo_1.ToBitmapAsset(),
                        }
			, 500);
		}

		public static Sprite Spawn_BigGun(double x, double y)
		{
			return 
				new Sprite { x = x, y = y }.AnimateAt(
					new BitmapAsset[]
                        {
                            Assets.biggun_1.ToBitmapAsset(),
                        }
				, 500);
		}


		public static Sprite Spawn_A(double x, double y)
		{
			return 
				new Sprite { x = x, y = y }.AnimateAt(
					new BitmapAsset[]
							{
								Assets.aenemy_1.ToBitmapAsset(),
								Assets.aenemy_2.ToBitmapAsset()
							}
				, 500);
		}

		public static Sprite Spawn_B(double x, double y)
		{
			return 
				new Sprite { x = x, y = y }.AnimateAt(
					new BitmapAsset[]
							{
								Assets.benemy_1.ToBitmapAsset(),
								Assets.benemy_2.ToBitmapAsset()
							}
				, 500);
		}

		public static Sprite Spawn_C(double x, double y)
		{
			return 
				new Sprite { x = x, y = y }.AnimateAt(
					new BitmapAsset[]
							{
								Assets.cenemy_1.ToBitmapAsset(),
								Assets.cenemy_2.ToBitmapAsset()
							}
				, 500);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.RayCaster;
using System.Collections.Specialized;
using ScriptCoreLib.ActionScript.flash.utils;




namespace RayCaster6.ActionScript
{


	partial class RayCaster6
	{
		bool DrawMinimapEnabled;

		private void DrawMinimap()
		{
			if (!DrawMinimapEnabled)
				return;

			var _WallMap = EgoView.Map.WallMap;
			var posX = EgoView.ViewPositionX;
			var posY = EgoView.ViewPositionY;
			var rayDirLeft = EgoView.ViewDirectionLeftBorder;
			var rayDirRight = EgoView.ViewDirectionRightBorder;


			int isize = 2;

			var minimap = new BitmapData(isize * (_WallMap.Size + 2), isize * (_WallMap.Size + 2), true, 0x0);
			var minimap_bmp = new Bitmap(minimap);


			for (int ix = 0; ix < _WallMap.Size; ix++)
				for (int iy = 0; iy < _WallMap.Size; iy++)
				{
					if (_WallMap[ix, iy] > 0)
						minimap.fillRect(new Rectangle((ix + 1) * isize, (iy + 1) * isize, isize, isize), 0x7f00ff00);

				}

			//minimap.applyFilter(minimap, minimap.rect, new Point(), new GlowFilter(0x00ff00));


			minimap.drawLine(0xffffffff,
					(posX + 1) * isize,
					(posY + 1) * isize,
					(posX + 1 + Math.Cos(rayDirLeft) * 8) * isize,
					(posY + 1 + Math.Sin(rayDirLeft) * 8) * isize
					);

			minimap.drawLine(0xffffffff,
				(posX + 1) * isize,
				(posY + 1) * isize,
				(posX + 1 + Math.Cos(rayDirRight) * 8) * isize,
				(posY + 1 + Math.Sin(rayDirRight) * 8) * isize
				);

			//Console.WriteLine("left: " + rayDirLeft);
			//Console.WriteLine("right: " + rayDirLeft);

			foreach (var ss in EgoView.SpritesFromPointOfView)
			{
				uint color = 0xff00ffff;



				if (!ss.ViewInfo.IsInView)
					color = 0xff000000;


				minimap.fillRect(new Rectangle(
						(ss.Sprite.Position.x + 0.5) * isize,
						(ss.Sprite.Position.y + 0.5) * isize,
						isize,
						isize), color);

				var _x = (ss.Sprite.Position.x + 1) * isize;
				var _y = (ss.Sprite.Position.y + 1) * isize;


				minimap.drawLine(
						0xffffffff,
						_x,
						_y,
						_x + Math.Cos(ss.Sprite.Direction) * isize * 4,
						_y + Math.Sin(ss.Sprite.Direction) * isize * 4
				);

			}

			minimap.fillRect(new Rectangle((posX + 0.5) * isize, (posY + 0.5) * isize, isize, isize), 0xffff0000);



			EgoView.Buffer.draw(minimap);
		}
	}
}
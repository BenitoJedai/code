using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashTreasureHunt.ActionScript.ThreeD
{
	partial class ViewEngine
	{
		//bool DrawMinimapEnabled = true;


		private void DrawMinimap()
		{
			//if (!DrawMinimapEnabled)
			//    return;

			var EgoView = this;

			var _WallMap = EgoView.Map.WallMap;
			var posX = EgoView.ViewPositionX;
			var posY = EgoView.ViewPositionY;
			var rayDirLeft = EgoView.ViewDirectionLeftBorder;
			var rayDirRight = EgoView.ViewDirectionRightBorder;


			int isize = 3;

			var minimap = new BitmapData(isize * (_WallMap.Size + 2), isize * (_WallMap.Size + 2), true, 0x0);
			var minimap_bmp = new Bitmap(minimap);


			for (int ix = 0; ix < _WallMap.Size; ix++)
				for (int iy = 0; iy < _WallMap.Size; iy++)
				{
					if (_WallMap[ix, iy] == 0)
						minimap.fillRect(new Rectangle((ix + 1) * isize, (iy + 1) * isize, isize, isize), 0x4f00ff00);

				}

			//minimap.applyFilter(minimap, minimap.rect, new Point(), new GlowFilter(0x00ff00));

			//Console.WriteLine("left: " + rayDirLeft);
			//Console.WriteLine("right: " + rayDirLeft);

			foreach (var _ss in EgoView.SpritesFromPointOfView)
			{
				var ss = _ss;

				uint color = 0x9f008000;


				var extended = _ss.Sprite as SpriteInfoExtended;

				if (extended != null)
				{
					color = extended.MinimapColor;
				}


				minimap.fillRect(new Rectangle(
						(ss.Sprite.Position.x + 0.5) * isize,
						(ss.Sprite.Position.y + 0.5) * isize,
						isize,
						isize), color);

				var _x = (ss.Sprite.Position.x + 1) * isize;
				var _y = (ss.Sprite.Position.y + 1) * isize;


			
			}

			minimap.drawLine(0xffffffff,
				(posX + 1) * isize,
				(posY + 1) * isize,
				(posX + 1 + Math.Cos(rayDirLeft) * isize) * isize,
				(posY + 1 + Math.Sin(rayDirLeft) * isize) * isize
				);

			minimap.drawLine(0xffffffff,
				(posX + 1) * isize,
				(posY + 1) * isize,
				(posX + 1 + Math.Cos(rayDirRight) * isize) * isize,
				(posY + 1 + Math.Sin(rayDirRight) * isize) * isize
				);


			minimap.fillRect(new Rectangle((posX + 0.5) * isize, (posY + 0.5) * isize, isize, isize), 0xffff0000);



			EgoView.Buffer.draw(minimap);
		}

	}
}

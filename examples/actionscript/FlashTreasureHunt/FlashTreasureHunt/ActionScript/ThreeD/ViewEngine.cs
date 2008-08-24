using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashTreasureHunt.ActionScript.ThreeD
{
	[Script]
	public class ViewEngine : ViewEngineBase
	{
		int HorizonStep = 4;

		int HorizonGradientCount;


		uint[] HorizonGradientUpper;

		uint[] HorizonGradientLower;

		const double PlayerRadiusMargin = FlashTreasureHunt.PlayerRadiusMargin;

		public ViewEngine(int w, int h)
			: base(w, h)
		{
			Func<byte, byte, int, Func<int, uint>> f =
				(g0, g1, max) =>
				{
					return
						i =>
						{
							var c = (g1 + (g0 - g1) * i / max).Min(255).Max(0);

							return (uint)((c << 16) + (c << 8) + c);
						};
				};

			HorizonGradientCount = h / 2 / HorizonStep + 1;

			HorizonGradientUpper = Enumerable.Range(0, HorizonGradientCount).Select(
				f(0x30, 0x50, HorizonGradientCount)
			).ToArray();

			HorizonGradientLower = Enumerable.Range(0, HorizonGradientCount).Select(
				f(0x90, 0x50, HorizonGradientCount)
			).ToArray();

		}

		public override void RenderHorizon()
		{
			var r = new Rectangle { width = _ViewWidth };

			Action<int, int, uint> fill =
				(y, h, c) =>
				{
					r.y = y;
					r.height = h;

					buffer.fillRect(r, c);
				};

			//fill(0, _ViewHeight / 3, 0x404040);
			//fill(_ViewHeight / 3, _ViewHeight / 3, 0x202020);
			//fill(_ViewHeight * 2 / 3, _ViewHeight / 3, 0x808080);

			for (int i = 0; i < HorizonGradientCount; i++)
				fill(i * HorizonStep, HorizonStep, HorizonGradientUpper[i]);

			for (int i = 0; i < HorizonGradientCount; i++)
				fill(i * HorizonStep + _ViewHeight / 2, HorizonStep, HorizonGradientLower[i]);
		}

		public Point ViewPositionLock { get; set; }

		public readonly List<SpriteInfoExtended> BlockingSprites = new List<SpriteInfoExtended>();


		public override void ClipViewPosition(Point p)
		{
			if (ViewPositionLock != null)
			{
				p.x = ViewPositionLock.x;
				p.y = ViewPositionLock.y;

				return;
			}

			var Walls = this.Map.WallMap;

			if (Walls == null)
				return;


			#region bump on blocking sprites
			foreach (var v in BlockingSprites)
			{
				var z = Point.distance(p, v.Position);

				if (z < v.Range)
				{
					// pump us out of that sprite

					var a = (p - v.Position).GetRotation();

					p.x = v.Position.x + Math.Cos(a) * v.Range;
					p.y = v.Position.y + Math.Sin(a) * v.Range;
				}

			}
			#endregion

			var fPlayerX = p.x;
			var fPlayerY = p.y;


			var c = new PointInt32
			{
				X = (int)Math.Floor(p.x),
				Y = (int)Math.Floor(p.y)
			};

			var TILE_SIZE = 1.0;

			var PositionInWall =
				 new Point
				 {
					 x = fPlayerX % TILE_SIZE,
					 y = fPlayerY % TILE_SIZE
				 };


			var TileTop = Walls[c.X, c.Y - 1];
			var TileLeft = Walls[c.X - 1, c.Y];
			var TileRight = Walls[c.X + 1, c.Y];
			var TileBottom = Walls[c.X, c.Y + 1];


			var CurrentMapTile = Walls[c.X, c.Y];

			if (CurrentMapTile != 0)
			{
				// we are inside a wall
				// push us out of there

				#region Dia
				var A = PositionInWall.x > PositionInWall.y;
				var B = PositionInWall.x > (TILE_SIZE - PositionInWall.y);

				var DiaClipLeft = !A && !B;
				var DiaClipRight = A && B;
				var DiaClipTop = A && !B;
				var DiaClipBottom = !A && B;
				#endregion

				#region Alt
				var C = PositionInWall.x > (TILE_SIZE / 2);
				var D = PositionInWall.y > (TILE_SIZE / 2);

				var AltClipTopLeft = !C && !D;
				var AltClipBottomLeft = !C && D;
				var AltClipTopRight = C && !D;
				var AltClipBottomRight = C && D;
				#endregion


				var ClipLeft = DiaClipLeft;

				if (TileTop != 0) ClipLeft |= AltClipTopLeft;
				if (TileBottom != 0) ClipLeft |= AltClipBottomLeft;

				var ClipRight = DiaClipRight;

				if (TileTop != 0) ClipRight |= AltClipTopRight;
				if (TileBottom != 0) ClipRight |= AltClipBottomRight;

				var ClipTop = DiaClipTop;

				if (TileLeft != 0) ClipTop |= AltClipTopLeft;
				if (TileRight != 0) ClipTop |= AltClipTopRight;

				var ClipBottom = DiaClipBottom;

				if (TileLeft != 0) ClipBottom |= AltClipBottomLeft;
				if (TileRight != 0) ClipBottom |= AltClipBottomRight;



				if (ClipLeft)
					fPlayerX = fPlayerX.Min(c.X * TILE_SIZE - PlayerRadiusMargin);
				else if (ClipRight)
					fPlayerX = fPlayerX.Max((c.X + 1) * TILE_SIZE + PlayerRadiusMargin);

				if (ClipTop)
					fPlayerY = fPlayerY.Min(c.Y * TILE_SIZE - PlayerRadiusMargin);
				else if (ClipRight)
					fPlayerY = fPlayerY.Max((c.Y + 1) * TILE_SIZE + PlayerRadiusMargin);
			}
			else
			{
				// fix corners
				//
				//   *        L
				//    F      *

				if (TileLeft != 0)
					fPlayerX = fPlayerX.Max(c.X * TILE_SIZE + PlayerRadiusMargin);
				if (TileRight != 0)
					fPlayerX = fPlayerX.Min((c.X + 1) * TILE_SIZE - PlayerRadiusMargin);

				if (TileTop != 0)
					fPlayerY = fPlayerY.Max(c.Y * TILE_SIZE + PlayerRadiusMargin);
				if (TileBottom != 0)
					fPlayerY = fPlayerY.Min((c.Y + 1) * TILE_SIZE - PlayerRadiusMargin);

			}

			p.x = fPlayerX;
			p.y = fPlayerY;



		}
	}
}

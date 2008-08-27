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
		public readonly List<SpriteInfoExtended> BlockingSprites = new List<SpriteInfoExtended>();

		public bool EnableBlockingSprites = true;

		public Point ViewPositionLock { get; set; }


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

			Func<Point, double, bool> RadialBump =
				(vp, vr) =>
				{

					var z = p.GetDistance(vp);

					if (z < vr)
					{
						// pump us out of that sprite

						var a = (p - vp).GetRotation();

						p.x = vp.x + Math.Cos(a) * vr;
						p.y = vp.y + Math.Sin(a) * vr;

						return true;
					}
					else
					{
						return false;
					}
				};


			#region bump on blocking sprites
			if (EnableBlockingSprites)
				foreach (var v in BlockingSprites)
				{
					RadialBump(v.Position, v.Range);
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

		
			BumpCounter++;

			var p_top_left = new Point(c.X, c.Y);
			var p_top_right = new Point(c.X + 1, c.Y);
			var p_bottom_right = new Point(c.X + 1, c.Y + 1);
			var p_bottom_left = new Point(c.X, c.Y + 1);

			var z_top_left = p.GetDistance(p_top_left);
			var z_top_right = p.GetDistance(p_top_right);
			var z_bottom_right = p.GetDistance(p_bottom_right);
			var z_bottom_left = p.GetDistance(p_bottom_left);

			var is_top_left = z_top_left < PlayerRadiusMargin;
			var is_top_right = z_top_right < PlayerRadiusMargin;
			var is_bottom_right = z_bottom_right < PlayerRadiusMargin;
			var is_bottom_left = z_bottom_left < PlayerRadiusMargin;

			// this assumes there are always corners.
			// if we add larger rooms we need to adjust this here

			//WriteLine(BumpCounter + " " + new { is_top_left, is_top_right, is_bottom_right, is_bottom_left });

			if (is_top_left)
				if (!RadialBump(p_top_left, PlayerRadiusMargin))
					WriteLine("fail p_top_left");

			if (is_top_right)
				if (!RadialBump(p_top_right, PlayerRadiusMargin))
					WriteLine("fail p_top_right");

			if (is_bottom_right)
				if (!RadialBump(p_bottom_right, PlayerRadiusMargin))
					WriteLine("fail p_bottom_right");

			if (is_bottom_left)
				if (!RadialBump(p_bottom_left, PlayerRadiusMargin))
					WriteLine("fail bottom_left");
		}

		int BumpCounter = 0;

		public Action<string> WriteLine;
	}
}

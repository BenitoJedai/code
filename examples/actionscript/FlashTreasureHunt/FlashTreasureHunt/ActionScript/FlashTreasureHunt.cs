using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using System.Linq;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.Archive.Extensions;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Shared.Maze;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.flash.filters;
using FlashTreasureHunt.ActionScript.ThreeD;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script]
	public partial class FlashTreasureHunt : Sprite
	{
		// http://www.users.globalnet.co.uk/~brlowe/index.html
		// http://www.wolfenstein3d.co.uk/index.htm
		// http://winwolf3d.dugtrio17.com/index.php
		// http://www.3drealms.com/wolf3d/

		// codename: Hundefelsen 4C

		// http://en.wikipedia.org/wiki/Castle_Wolfenstein
		// http://www.mobygames.com/game/wolfenstein-3d/trivia

		// http://www1.linkclub.or.jp/~clubey/wolfenmania.html
		// http://www.geocities.com/TimesSquare/3744/index2.html
		// http://www.users.globalnet.co.uk/~brlowe/utilities.htm
		// http://www.geocities.com/TimesSquare/Cavern/4087/
		// http://www.glenrhodes.com/wolf/myRay.html
		// http://nihilogic.dk/labs/wolf/sounds/
		// http://www.lostinactionscript.com/blog/index.php/2007/10/13/flash-you-tube-api/
		// http://www.digital-ist-besser.de/
		// http://www.fredheintz.com/sitefred/main.html

		// shareware:
		// Computer software developed for the public domain, which can be used or copied without infringing copyright. ...
		// tr.wou.edu/ntac/documents/fact_sheets/glossary.htm

		// todo: add teleport


		BlockMaze maze;
		ViewEngine EgoView;

		SoundChannel music;

		bool EndLevelMode = false;

		SpriteInfoExtended TheGoldStack;

		Sprite HudContainer;

		public double GetGoodDirection(Point p)
		{
			var u = new PointInt32 { X = p.x.Floor(), Y = p.y.Floor() };

			if (EgoView.Map.WallMap[u.X + 1, u.Y] == 0)
				return 0.DegreesToRadians();
			else if (EgoView.Map.WallMap[u.X - 1, u.Y] == 0)
				return 180.DegreesToRadians();
			else if (EgoView.Map.WallMap[u.X, u.Y + 1] == 0)
				return 90.DegreesToRadians();
			else if (EgoView.Map.WallMap[u.X, u.Y - 1] == 0)
				return 270.DegreesToRadians();

			return 0;
		}

		void ResetEgoPosition()
		{
			var PossibleStarters = EgoView.Map.WorldMap.Entries.Where(i => i.Value == 0).Where(i => i.YIndex < (maze.Height / 2)).Randomize();

			var StartPoint = PossibleStarters.First();

			EgoView.ViewPosition = new Point { x = StartPoint.XIndex + 0.5, y = StartPoint.YIndex + 0.5 };

			// what direction shall we be looking at?

			EgoView.ViewDirection = GetGoodDirection(EgoView.ViewPosition);
		}

		Func<SpriteInfoExtended> CreateGuard;

		Bitmap getpsyched;

		public FlashTreasureHunt()
		{
			Initialize();
		}


		private void Initialize()
		{
			this.music = Assets.Default.Sounds.music.play(0, 9999);

			Assets.Default.Sounds.gutentag.play();



			EgoView = new ViewEngine(DefaultWidth, DefaultHeight)
			{
				FloorAndCeilingVisible = false,
				RenderLowQualityWalls = false,
				ViewDirection = 0,
				ViewPosition = new Point()
			};

			EgoView.Image.scaleX = DefaultScale;
			EgoView.Image.scaleY = DefaultScale;
			EgoView.Image.alpha = 0;
			EgoView.Image.AttachTo(this);

			// show fps
			new TextField { textColor = 0xff0000, x = DefaultControlWidth / 2 }.AttachTo(this).Do(t => EgoView.FramesPerSecondChanged += () => t.text = "fps: " + EgoView.FramesPerSecond);

			EgoView.FramesPerSecondChanged +=
				delegate
				{
					if (EgoView.FramesPerSecond < 14)
						if (!EgoView.RenderLowQualityWalls)
							EgoView.RenderLowQualityWalls = true;

					if (EgoView.FramesPerSecond > 22)
						if (EgoView.RenderLowQualityWalls)
							EgoView.RenderLowQualityWalls = false;
				};


			this.HudContainer = new Sprite().AttachTo(this);
			this.HudContainer.alpha = 0;

			getpsyched = Assets.Default.getpsyched.AttachTo(this);

			getpsyched.scaleX = 2;
			getpsyched.scaleY = 2;
			getpsyched.MoveTo((DefaultControlWidth - getpsyched.width) / 2, (DefaultControlHeight - getpsyched.height) / 2);


			InitializeWriteLine();


			Assets.Default.dude5.ToBitmapArray(
				Bitmaps =>
				{
					var Spawn = default(Func<SpriteInfoExtended>);

					#region figure out
					if (Bitmaps == null)
						throw new Exception("No bitmaps");

					Func<Texture64[], Texture64[]> Reorder8 =
						p =>
							Enumerable.ToArray(
								from i in Enumerable.Range(0, 8)
								select p[(i + 6) % 8]
							);

					var BitmapStream = Bitmaps.Select(i => (Texture64)i).GetEnumerator();

					Func<Texture64[]> Next8 =
						delegate
						{
							// keeping compiler happy with full delegate form

							if (BitmapStream == null)
								throw new Exception("BitmapStream is null");

							return Reorder8(BitmapStream.Take(8));
						};


					var Stand = Next8();


					//if (Bitmaps.Length == 8)
					//{
					//    Spawn = () => CreateWalkingDummy(Stand);
					//}
					//else
					//{
					var Walk = new[]
                        {
                            Next8(),
                            Next8(),
                            Next8(),
                            Next8(),
                        };

					var Death = new List<Texture64>
					{
						BitmapStream.Take(),
						BitmapStream.Take(),
						BitmapStream.Take(),
						BitmapStream.Take(),
						BitmapStream.Take()
					};


					var Hit = new[] { BitmapStream.Take() };

					// funny ordering huh?
					Death.Add(BitmapStream.Take());

					Spawn = () => CreateWalkingDummy(Stand, Walk, Hit, Death.ToArray());

					//}

					#endregion

					CreateGuard = Spawn;

					InitializeMap();


				}
			);
		}




	}
}
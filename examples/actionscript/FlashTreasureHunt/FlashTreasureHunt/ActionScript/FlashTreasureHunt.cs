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

		// shareware:
		// Computer software developed for the public domain, which can be used or copied without infringing copyright. ...
		// tr.wou.edu/ntac/documents/fact_sheets/glossary.htm

		// todo: add teleport
		// todo: add random start points
		// todo: add ammo

		BlockMaze maze;
		ViewEngine EgoView;

		SoundChannel music;

		bool EndLevelMode = false;

		SpriteInfoExtended TheGoldStack;

		Sprite HudContainer;

		void ResetEgoPosition()
		{
			var PossibleStarters = EgoView.Map.WorldMap.Entries.Where(i => i.Value == 0).Where(i => i.YIndex < (maze.Height / 2)).Randomize();

			var StartPoint = PossibleStarters.First();

			EgoView.ViewPosition = new Point { x = StartPoint.XIndex + 0.5, y = StartPoint.YIndex + 0.5 };

			// what direction shall we be looking at?

			if (EgoView.Map.WallMap[StartPoint.XIndex + 1, StartPoint.YIndex] == 0)
				EgoView.ViewDirection = 0.DegreesToRadians();
			else if (EgoView.Map.WallMap[StartPoint.XIndex - 1, StartPoint.YIndex] == 0)
				EgoView.ViewDirection = 180.DegreesToRadians();
			else if (EgoView.Map.WallMap[StartPoint.XIndex , StartPoint.YIndex + 1] == 0)
				EgoView.ViewDirection = 90.DegreesToRadians();
			else if (EgoView.Map.WallMap[StartPoint.XIndex, StartPoint.YIndex - 1] == 0)
				EgoView.ViewDirection = 270.DegreesToRadians();
		}

		Func<SpriteInfoExtended> CreateGuard;

		Bitmap getpsyched;

		public FlashTreasureHunt()
		{
			this.music = Assets.Default.music.play(0, 9999);

			Assets.Default.gutentag.play();



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
            new TextField().AttachTo(this).Do(t => EgoView.FramesPerSecondChanged += () => t.text = "fps: " + EgoView.FramesPerSecond);

            EgoView.FramesPerSecondChanged +=
                delegate
                {
                    if (EgoView.FramesPerSecond < 15)
                        if (!EgoView.RenderLowQualityWalls)
                            EgoView.RenderLowQualityWalls = true;
                };


			this.HudContainer = new Sprite().AttachTo(this);
			this.HudContainer.alpha = 0;

			getpsyched = Assets.Default.getpsyched.AttachTo(this);

			getpsyched.scaleX = 2;
			getpsyched.scaleY = 2;
			getpsyched.MoveTo((DefaultControlWidth - getpsyched.width) / 2, (DefaultControlHeight - getpsyched.height) / 2);



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


					if (Bitmaps.Length == 8)
					{
						Spawn = () => CreateWalkingDummy(Stand);
					}
					else
					{
						var Walk = new[]
                        {
                            Next8(),
                            Next8(),
                            Next8(),
                            Next8(),
                        };

						Spawn = () => CreateWalkingDummy(Stand, Walk);
					}
					#endregion

					CreateGuard = Spawn;

					InitializeMap();


				}
			);



		}


		public SpriteInfoExtended CreateWalkingDummy(Texture64[] Stand, params Texture64[][] Walk)
		{
			var tt = default(Timer);
			var s = default(SpriteInfoExtended);

			Action start =
				delegate
				{
					s.WalkingAnimationRunning = true;

					if (Walk.Length > 0)
						tt = (200).AtInterval(
							t =>
							{
								s.Frames = Walk[t.currentCount % Walk.Length];
							}
						);
				};

			Action stop =
				delegate
				{
					s.WalkingAnimationRunning = false;

					if (tt != null)
						tt.stop();

					s.Frames = Stand;
				};


			s = new SpriteInfoExtended
			{
				Position = new Point { x = EgoView.ViewPositionX, y = EgoView.ViewPositionY },
				Frames = Stand,
				Direction = EgoView.ViewDirection,
				StartWalkingAnimation = start,
				StopWalkingAnimation = stop

			}.AddTo(EgoView.Sprites);



			return s;
		}



		public SpriteInfoExtended CreateDummy(Texture64 Stand)
		{
			return CreateWalkingDummy(new[] { Stand });

		}

	}
}
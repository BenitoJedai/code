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

		BlockMaze maze;
		ViewEngine EgoView;

		SoundChannel music;

		bool EndLevelMode = false;

		SpriteInfoExtended TheGoldStack;

		Sprite HudContainer;

		void ResetEgoPosition()
		{
			EgoView.ViewPosition = new Point { x = 1.25, y = 1.25 };
			EgoView.ViewDirection = (45).DegreesToRadians();
		}

		Func<SpriteInfoExtended> CreateGuard;

		public FlashTreasureHunt()
		{
			this.music = Assets.Default.music.play(0, 9999);




			EgoView = new ViewEngine(DefaultWidth, DefaultHeight)
			{
				FloorAndCeilingVisible = false,
				RenderLowQualityWalls = false
			};

			EgoView.Image.scaleX = DefaultScale;
			EgoView.Image.scaleY = DefaultScale;
			EgoView.Image.AttachTo(this);

			this.HudContainer = new Sprite().AttachTo(this);

			ResetEgoPosition();

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
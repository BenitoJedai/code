﻿using ScriptCoreLib;
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
using ScriptCoreLib.ActionScript.flash.ui;

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
		// http://www.permadi.com/tutorial/raycast/rayc1.html

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
		// http://www.newgrounds.com/portal/view/412065
		// http://www.2flashgames.com/f/f-Wolfenstein-3D-Headshot-5297.htm

		// shareware:
		// Computer software developed for the public domain, which can be used or copied without infringing copyright. ...
		// tr.wou.edu/ntac/documents/fact_sheets/glossary.htm

		// todo: 
		// fix dead coplayers showing up alive
		// fix compass reveal when not killing a coplayer by ego
		// dummy guards?
		// fix score sharing
		// add button to clear portal renders
		// move game goal to a newly revealed tile
		// add movement smoothing
		// add damage emitter info

		BlockMaze maze;
		public ViewEngine EgoView;

		public SoundChannel music;

		bool EndLevelMode = false;

		public SpriteInfoExtended TheGoldStack;

		public Sprite HudContainer;

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

		public void ResetEgoPosition()
		{
			var PossibleStarters = EgoView.Map.WorldMap.Entries.Where(i => i.Value == 0).Where(i => i.YIndex < (maze.Height / 2)).Randomize();

			var StartPoint = PossibleStarters.First();

			EgoView.ViewPosition = new Point { x = StartPoint.XIndex + 0.5, y = StartPoint.YIndex + 0.5 };

			// what direction shall we be looking at?

			EgoView.ViewDirection = GetGoodDirection(EgoView.ViewPosition);
		}


		public Bitmap getpsyched;

		public FlashTreasureHunt()
		{


			// users can override this method
			this.ReadyWithLoadingCurrentLevel = this.ReadyWithLoadingCurrentLevelDirect;

			this.ReadyForNextLevel =
				AlmostDone =>
				{
					this.WriteLine("default ReadyForNextLevel");

					getpsyched.FadeOut(AlmostDone);
				};


			this.InvokeWhenStageIsReady(Initialize);
		}


		private void Initialize()
		{
			this.stage.fullScreenSourceRect = new Rectangle
			{
				width = DefaultControlWidth,
				height = DefaultControlHeight
			};

			#region fullscreen support
			bool NextFullscreenMode = true;

			this.stage.fullScreen +=
				e =>
				{
					NextFullscreenMode = !e.fullScreen;

					// hide chat while fullscreen zoom
					this.GetStageChild().Siblings().ForEach(k => k.visible = !e.fullScreen);
				};
			
		
			this.contextMenu = new ContextMenuEx
			{
				{ "Fullscreen", 
					delegate
					{
						this.stage.SetFullscreen(NextFullscreenMode);
					}
				}
			};
			#endregion



			this.music = Assets.Default.Music.music.play(0, 9999, new SoundTransform(0.5));




			EgoView = new ViewEngine(DefaultWidth, DefaultHeight)
			{
				FloorAndCeilingVisible = false,
				RenderLowQualityWalls = false,
				ViewDirection = 0,
				ViewPosition = new Point(0, 0)
			};

			EgoView.Image.scaleX = DefaultScale;
			EgoView.Image.scaleY = DefaultScale;
			EgoView.Image.alpha = 0;
			EgoView.Image.AttachTo(this);

			EgoView.ViewPositionChanged +=
				delegate
				{
					this.NextViewPosition = EgoView.ViewPosition;
				};

			EgoView.ViewDirectionChanged +=
				delegate
				{
					this.NextViewDirection = EgoView.ViewDirection;
				};
		
			EgoView.FramesPerSecondChanged +=
				delegate
				{
					if (EgoView.FramesPerSecond < 14)
						if (!EgoView.RenderLowQualityWalls)
							EgoView.RenderLowQualityWalls = true;

					if (EgoView.FramesPerSecond > 20)
						if (EgoView.RenderLowQualityWalls)
							EgoView.RenderLowQualityWalls = false;
				};


			this.HudContainer = new Sprite().AttachTo(this);
			this.HudContainer.alpha = 0;

			getpsyched = Assets.Default.getpsyched.AttachTo(this);

			getpsyched.scaleX = 2;
			getpsyched.scaleY = 2;
			getpsyched.MoveTo((DefaultControlWidth - getpsyched.width) / 2, (DefaultControlHeight - getpsyched.height) / 2);
			getpsyched.alpha = 0;

			InitializeWriteLine();

			this.WriteLine("init: getpsyched");

			//WriteLine("with minimap");


			getpsyched.FadeIn(
				delegate
				{
					Assets.Default.Sounds.gutentag.play();

					this.WriteLine("init: gutentag");


					Assets.Default.dude5.ToBitmapArray(
						//(cur, max) =>
						//{
						//    this.WriteLine("init: dude5 " + cur + "/" + max);

						//},
						CachedGuardTextures,
						Bitmaps =>
						{
							CachedGuardTextures = Bitmaps;

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


							var Shooting = new List<Texture64>
							{
								BitmapStream.Take(),
								BitmapStream.Take(),
								BitmapStream.Take()
							};

							Spawn = () => CreateWalkingDummy(Stand, Walk, Hit, Death.ToArray(), Shooting.ToArray());


							#endregion

							CreateGuard = Spawn;


							this.WriteLine("init: dude5");


							InitializeMap();


						}
					);
				}
			);

		}




	}
}
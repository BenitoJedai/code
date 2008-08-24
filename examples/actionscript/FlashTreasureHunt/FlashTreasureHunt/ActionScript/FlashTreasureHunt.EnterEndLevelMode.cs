using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using System.Linq;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.filters;

namespace FlashTreasureHunt.ActionScript
{
	partial class FlashTreasureHunt
	{

		private void EnterEndLevelMode()
		{
			var ScoreContainer = new Sprite().AttachTo(this);

			ScoreContainer.alpha = 0.8;

			var scroll = Assets.Default.scroll.AttachTo(ScoreContainer);
			var scroll_scale = DefaultControlHeight / scroll.height;

			scroll.scaleX = scroll_scale;
			scroll.scaleY = scroll_scale;

			scroll.MoveTo(DefaultControlWidth - scroll.width, 0);
			scroll.filters = new BitmapFilter[] { new DropShadowFilter() };

			new Bitmap(EgoView.Buffer.clone())
			{
				scaleX = DefaultScale,
				scaleY = DefaultScale
			}.AttachTo(this).FadeOutAndOrphanize(1000 / 24, 0.1);

			music.stop();

			EndLevelMode = true;
			MovementEnabled = false;

			var music_endlevel = Assets.Default.Sounds.music_endlevel.play(1);


			this.EgoView.Image.filters = new BitmapFilter[] {
				Filters.GrayScaleFilter,
			};

			this.EgoView.ViewPositionLock = TheGoldStack.Position;
			this.EgoView.ViewPosition = TheGoldStack.Position;

			var FrozenLook = (45 + 180);

			var p = new PointInt32
			{
				X = (int)Math.Floor(TheGoldStack.Position.x),
				Y = (int)Math.Floor(TheGoldStack.Position.y),
			};

			// where should we look actually?
			if (EgoView.Map.WallMap[p.X - 1, p.Y] != 0)
				FrozenLook = (90 + 180);

			if (EgoView.Map.WallMap[p.X, p.Y - 1] != 0)
				FrozenLook = (0 + 180);

			this.EgoView.ViewDirection = FrozenLook.DegreesToRadians();



			1500.AtDelayDo(
				delegate
				{
					HudContainer.FadeOut(1000 / 15, 0.2,
						delegate
						{
							CompassContainer.alpha = 0;
						}
					);


			
					// level ends for all

					// list current scores


					1000.Chain(
						delegate
						{
							Assets.Default.Sounds.gunshot.play();

							new TextField
							{
								defaultTextFormat = new TextFormat
								{
									size = 36,
								},
								text = "Level " + CurrentLevel + " Complete",

								textColor = 0xFFC526,
								autoSize = TextFieldAutoSize.LEFT,
								filters = new[] { new GlowFilter(0xC1931D) }
							}.AttachTo(ScoreContainer).MoveTo(scroll.x + 40, scroll.y + 64);

						}
					).Chain(
						delegate
						{
							Assets.Default.Sounds.gunshot.play();

							new TextField
							{
								defaultTextFormat = new TextFormat
								{
									size = 33,
								},
								text = "Blazkowicz - " + CurrentLevelScore + "$",

								textColor = 0xFFC526,
								autoSize = TextFieldAutoSize.LEFT,
								filters = new[] { new GlowFilter(0xC1931D) }
							}.AttachTo(ScoreContainer).MoveTo(scroll.x + 48, scroll.y + 96 + 33 * 1);

						}
					).Chain(
						delegate
						{
							Assets.Default.Sounds.gunshot.play();
							new TextField
							{
								defaultTextFormat = new TextFormat
								{
									size = 30,
								},
								text = "Player 2 - 1200$",

								textColor = 0xbebebe,
								autoSize = TextFieldAutoSize.LEFT,
								filters = new[] { new GlowFilter(0x909090) }
							}.AttachTo(ScoreContainer).MoveTo(scroll.x + 48, scroll.y + 96 + 33 * 2);

						}
					).Chain(
						delegate
						{
							Assets.Default.Sounds.gunshot.play();
							new TextField
							{
								defaultTextFormat = new TextFormat
								{
									size = 30,
								},
								text = "Player 3 - 1800$",

								textColor = 0xbebebe,
								autoSize = TextFieldAutoSize.LEFT,
								filters = new[] { new GlowFilter(0x909090) }
							}.AttachTo(ScoreContainer).MoveTo(scroll.x + 48, scroll.y + 96 + 33 * 3);
						}
					).Do();


					music_endlevel.soundComplete +=
						delegate
						{
							// we are ready to continue...
							// are other players?

							ScoreContainer.FadeOut(1000 / 15, 0.1,
								delegate
								{
									ScoreContainer.Orphanize();

									ReadyForNextLevel();
								}
							);
						};
				}
			);

		}

		public int CurrentLevel = 1;

		public virtual void ReadyForNextLevel()
		{
			CurrentLevel++;

			this.EgoView.Image.FadeOut(
				delegate
				{
					this.EgoView.Sprites.Clear();
					this.GoldSprites.Clear();
					this.AmmoSprites.Clear();

					// each level starts counting from zero
					GoldTotalCollected = 0;

					//MazeSize = (MazeSizeMin + CurrentLevel / 2).Min(MazeSizeMax);
					MazeSize = (MazeSizeMin + CurrentLevel).Min(MazeSizeMax);

					CreateMapFromMaze();

					AddIngameEntities();

					TheGoldStack.IsTaken = false;
					TheGoldStack.Position.To(maze.Width - 1.5, maze.Height - 1.5);


					GoldSprites.Add(TheGoldStack);

					WaitForCollectingHalfTheTreasureToRevealEndGoal();


					ResetPortals();

					music = Assets.Default.Sounds.music.play(0, 9999);

					this.EgoView.Image.filters = null;
					this.EgoView.ViewPositionLock = null;

					EndLevelMode = false;
					MovementEnabled = true;

					ResetEgoPosition();

					this.EgoView.Image.FadeIn();
					this.HudContainer.FadeIn();
				}
			);


		}
	}
}
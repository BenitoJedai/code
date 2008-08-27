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
using ScriptCoreLib.ActionScript.flash.events;

namespace FlashTreasureHunt.ActionScript
{
	partial class FlashTreasureHunt
	{

		public event Action Sync_EnterEndLevelMode;

		public void EnterEndLevelMode()
		{
			if (EndLevelMode)
				return;

			this.WriteLine("init: EnterEndLevelMode");

			if (Sync_EnterEndLevelMode != null)
				Sync_EnterEndLevelMode();

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
			MovementEnabled_IsInGame = false;

			var music_endlevel = Assets.Default.Music.music_endlevel.play(1);

			this.WriteLine("init: music_endlevel");

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

			var ReadyToContinue = default(Action);

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
									size = 33,
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
									size = 30,
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
									size = 27,
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
									size = 27,
								},
								text = "Player 3 - 1800$",

								textColor = 0xbebebe,
								autoSize = TextFieldAutoSize.LEFT,
								filters = new[] { new GlowFilter(0x909090) }
							}.AttachTo(ScoreContainer).MoveTo(scroll.x + 48, scroll.y + 96 + 33 * 3);
						}
					).Do();

					
					var onClick = default(Action<MouseEvent>);
					var onKeyUp = default(Action<KeyboardEvent>);

					ReadyToContinue =
						delegate
						{
							if (ReadyToContinue == null)
								return;

							ReadyToContinue = null;

							music_endlevel.stop();

							this.WriteLine("stop: music_endlevel");

							ScoreContainer.FadeOut(
								delegate
								{
									ScoreContainer.Orphanize();

									PrepareToCallReadyForNextLevel();
								}
							);

							stage.keyUp -= onKeyUp;
							stage.click -= onClick;

						};

					#region exit this menu
					music_endlevel.soundComplete +=
						delegate
						{
							// we are ready to continue...
							// are other players?

							if (ReadyToContinue != null)
								ReadyToContinue();

						};


					onClick =
						delegate
						{
							if (!MovementEnabled_IsFocused)
								return;

							if (ReadyToContinue != null)
								ReadyToContinue();
						};

					onKeyUp =
						delegate
						{
							if (!MovementEnabled_IsFocused)
								return;

							if (ReadyToContinue != null)
								ReadyToContinue();
						};
					#endregion

					stage.click += onClick;
					stage.keyUp += onKeyUp;

					// should add click / any key to dismiss this menu
				}
			);

		}

		public event Action BeforeReadyForNextLevel;

		private void PrepareToCallReadyForNextLevel()
		{
			this.WriteLine("init: PrepareToCallReadyForNextLevel - was it expected?");

			EgoView.Image.FadeOut(delegate
			{
				getpsyched.FadeIn(delegate
				{
					LoadNextLevel(ReadyForNextLevel);

				
				});
			});
		}

		public int CurrentLevel = 1;



		public Action<Action> ReadyForNextLevel;


		public void LoadNextLevel(Action<Action> AlmostDone)
		{
			CurrentLevel++;

			this.EgoView.Image.FadeOut(
				delegate
				{
					RemoveAllEntities();

					// each level starts counting from zero
					GoldTotalCollected = 0;

					//MazeSize = (MazeSizeMin + CurrentLevel / 2).Min(MazeSizeMax);
					MazeSize = (MazeSizeMin + CurrentLevel / MazeDelayResize).Min(MazeSizeMax);

					this.WriteLine("mazesize: " + MazeSize);

					CreateMapFromMaze();

					AddIngameEntities(
						delegate
						{
							TheGoldStack.IsTaken = false;
							TheGoldStack.Position.To(maze.Width - 1.3, maze.Height - 1.3);
							GoldSprites.Add(TheGoldStack);

							//this.WriteLine("goal is at " + new { TheGoldStack.Position.x, TheGoldStack.Position.y });


							WaitForCollectingHalfTheTreasureToRevealEndGoal();

							ResetPortals();

							music = Assets.Default.Music.music.play(0, 9999);

							this.EgoView.Image.filters = null;
							this.EgoView.ViewPositionLock = null;

							EndLevelMode = false;
							MovementEnabled_IsInGame = true;

							ResetEgoPosition();

							AlmostDone(
								delegate
								{
									this.EgoView.Image.FadeIn();
									this.HudContainer.FadeIn();
								}
							);
						}
					);


				}
			);
		}

		public void RemoveAllEntities()
		{
			this.EgoView.BlockingSprites.Clear();
			this.EgoView.Sprites.Clear();

			this.GoldSprites.Clear();
			this.AmmoSprites.Clear();
			this.NonblockSprites.Clear();

			this.DualPortals.Clear();
			this.Portals.Clear();
		}
	}
}
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

		public Action EnterEndLevelMode_ReadyToContinue;

		public void EnterEndLevelMode()
		{
			if (EndLevelMode)
				return;

			this.WriteLine("init: EnterEndLevelMode");

			if (Sync_EnterEndLevelMode != null)
				Sync_EnterEndLevelMode();

			var ScoreContainer = new Sprite().AttachTo(this);

			ScoreContainer.alpha = 0.8;

			Bitmap scroll = Assets.Default.scroll.AttachTo(ScoreContainer);
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

			if (music != null)
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

			HudContainer.FadeOut(1000 / 15, 0.2,
						delegate
						{
							CompassContainer.alpha = 0;
						}
					);


			var onClick = default(Action<MouseEvent>);
			var onKeyUp = default(Action<KeyboardEvent>);


			#region EnterEndLevelMode_ReadyToContinue
			EnterEndLevelMode_ReadyToContinue =
				delegate
				{
					if (EnterEndLevelMode_ReadyToContinue == null)
					{
						this.WriteLine("EnterEndLevelMode_ReadyToContinue already disabled?");

						return;
					}

					this.WriteLine("EnterEndLevelMode_ReadyToContinue is now disabled!");

					EnterEndLevelMode_ReadyToContinue = null;

					music_endlevel.stop();


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
			#endregion



			1500.AtDelayDo(
				delegate
				{




					// level ends for all

					// list current scores

					ShowScoreTable(ScoreContainer, scroll);




					#region exit this menu
					music_endlevel.soundComplete +=
						delegate
						{
							// we are ready to continue...
							// are other players?

							if (EnterEndLevelMode_ReadyToContinue != null)
								EnterEndLevelMode_ReadyToContinue();

						};


					onClick =
						delegate
						{
							if (!MovementEnabled_IsFocused)
								return;

							if (EnterEndLevelMode_ReadyToContinue != null)
								EnterEndLevelMode_ReadyToContinue();
						};

					onKeyUp =
						delegate
						{
							if (!MovementEnabled_IsFocused)
								return;

							if (EnterEndLevelMode_ReadyToContinue != null)
								EnterEndLevelMode_ReadyToContinue();
						};
					#endregion

					stage.click += onClick;
					stage.keyUp += onKeyUp;

					// should add click / any key to dismiss this menu
				}
			);

		}

		const int HeaderOffset = 70;
		const int MarginLeft = 48;

		const int Spacing = 50;

		const int ScoreLeft = 90;
		const int KillsLeft = 200;

		const int DelayBetweenEntries = 900;

		const int FrameRate_HideEntry = 1000 / 20;

		[Script]
		public class ScoreTag
		{
			public string Name;

			public int Score;
			public int Kills;
		}

		public Func<ScoreTag[]> VirtualGetScoreValues;

		public ScoreTag[] GetScoreValues()
		{
			if (VirtualGetScoreValues != null)
				return VirtualGetScoreValues();

			return new[]
			{
				new ScoreTag { Name = "Blazkowicz", Score = CurrentLevelScore, Kills = 7 },
				new ScoreTag { Name = "ken1", Score = 3, Kills = 7 },
				new ScoreTag { Name = "ken2", Score = 3, Kills = 7 },
				new ScoreTag { Name = "ken3", Score = 3, Kills = 7 },
				new ScoreTag { Name = "zen", Score = 22, Kills = 7 },
				new ScoreTag { Name = "yyy", Score = 224, Kills = 7 },
			};
		}
		private void ShowScoreTable(Sprite ScoreContainer, Bitmap scroll)
		{

			var ContainerForEntries = new Sprite { x = scroll.x, y = scroll.y }.AttachTo(ScoreContainer);

			var Entries = new Queue<Sprite>();

			Action RemoveFirst =
				delegate
				{
					Entries.Dequeue().FadeOut();

					var Steps = DelayBetweenEntries / FrameRate_HideEntry;

					(FrameRate_HideEntry / 2).AtInterval(
						t =>
						{
							if (t.currentCount == Steps)
							{
								t.stop();

								return;
							}

							ContainerForEntries.y -= Spacing / Steps;
						}
					);
				};

			Action ConditionalRemoveFirst =
				delegate
				{
					if (Entries.Count > 4)
						RemoveFirst();
				};

			#region chain
			DelayBetweenEntries.Chain(
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
				GetScoreValues().Select<ScoreTag, Action>(
					(k, i) => delegate
					{
						Entries.Enqueue(ShowScoreTable_Entry(ContainerForEntries, k.Name, k.Score, k.Kills, i + 1));
						ConditionalRemoveFirst();
					}
				)
			).Do();
			#endregion
		}

		private Sprite ShowScoreTable_Entry(Sprite _ScoreContainer, string Text, int Score, int Kills, int Index)
		{
			var ScoreContainer = new Sprite().AttachTo(_ScoreContainer);

			Assets.Default.Sounds.gunshot.play();

			new TextField
			{
				defaultTextFormat = new TextFormat
				{
					size = 24,
				},
				text = Text,
				textColor = 0x0000ff,
				autoSize = TextFieldAutoSize.LEFT,
				filters = new[] { new GlowFilter(0xffffff) }
			}.AttachTo(ScoreContainer).MoveTo(MarginLeft, HeaderOffset + Spacing * Index);



			new TextField
			{
				defaultTextFormat = new TextFormat
				{
					size = 20,
				},
				autoSize = TextFieldAutoSize.RIGHT,
				text = "",

				textColor = 0xFFC526,
				filters = new[] { new GlowFilter(0xC1931D) }
			}.AttachTo(ScoreContainer).MoveTo(MarginLeft + ScoreLeft, HeaderOffset + Spacing * (Index + .5)).text = Score + "$";

			new TextField
			{
				defaultTextFormat = new TextFormat
				{
					size = 20,
				},
				autoSize = TextFieldAutoSize.RIGHT,
				text = "",

				textColor = 0xa00000,
				filters = new[] { new GlowFilter(0xff0000) }
			}.AttachTo(ScoreContainer).MoveTo(MarginLeft + KillsLeft, HeaderOffset + Spacing * (Index + .5)).text = Kills + " kills";

			return ScoreContainer;
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


			this.GuardSprites.Clear();
		}
	}
}
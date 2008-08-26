using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using System.Linq;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Archive.Extensions;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.Shared.Maze;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashTreasureHunt.ActionScript
{
	partial class FlashTreasureHunt
	{
		const uint graywall = 0xff0000;
		const uint graywall_achtung = 0xff0001;
		const uint graywall_verboten = 0xff0002;

		const uint woodwall = 0x7F3300;
		const uint woodwall_books = 0x7F33F0;
		const uint woodwall_achtung = 0x7F3301;
		const uint woodwall_verboten = 0x7F3302;


		const uint bluewall = 0x0000ff;
		const uint greenwall = 0x00ff00;

		// items for pickup
		List<SpriteInfoExtended> AmmoSprites = new List<SpriteInfoExtended>();
		List<SpriteInfoExtended> GoldSprites = new List<SpriteInfoExtended>();

		// used to make BJ smile
		int GoldTakenCounter = 0;

		Dictionary<string, Bitmap> StuffDictionary;

		private void InitializeMap()
		{
			#region fill map
			Assets.Default.stuff.ToBitmapDictionary(
				f =>
				{
					StuffDictionary = f;


					CreateMapFromMaze();




					Func<string, Texture64> t =
						texname => f[texname + ".png"];

					Func<string, string, Texture64> mix =
						(a, b) =>
						{
							var ia = f[a + ".png"];
							var ib = f[b + ".png"];

							var u = new Bitmap(ia.bitmapData.clone());



							u.bitmapData.draw(ib);
							return u;
						};



					#region game goal
					TheGoldStack = CreateDummy(f["life.png"]);

					TheGoldStack.RemoveFrom(EgoView.Sprites);

					WaitForCollectingHalfTheTreasureToRevealEndGoal();

					TheGoldStack.Position.To(maze.Width - 1.25, maze.Height - 1.25);
					TheGoldStack.Range = 0.5;
					TheGoldStack.ItemTaken +=
						delegate
						{
							if (EndLevelMode)
								return;

							Assets.Default.Sounds.yeah.play();


							// show stats

							EnterEndLevelMode();
						};
					GoldSprites.Add(TheGoldStack);


					#endregion


					EgoView.Map.Textures = new Dictionary<uint, Texture64>
                        {
                            {graywall_achtung, mix("graywall", "achtung")},
                            {graywall_verboten, mix("graywall", "verboten")},
                            {graywall, t("graywall")},


							{woodwall_achtung, mix("woodwall", "achtung")},
                            {woodwall_verboten, mix("woodwall", "verboten")},
                            {woodwall, t("woodwall")},
                            {woodwall_books, t("woodwall_books")},


                            {bluewall, t("bluewall")},
                            {greenwall, t("greenwall")},
                        };

					// EgoView.RenderScene();

					InitializeWeaponOverlay(f);

					#region heads

					Assets.Default.head.Items.OrderBy(k => k.FileName).Select(k => k.Data).ToImages(
						heads =>
						{
							var head = default(Bitmap);

							1000.AtInterval(
								tt =>
								{
									if (head != null)
										head.Orphanize();

									if (heads.Length > 0)
									{
										if (GoldTakenCounter > 0)
										{
											GoldTakenCounter--;
											head = heads.Last();
										}
										else
											head = heads.AtModulus(tt.currentCount % 3);

										head.filters = new[] { new DropShadowFilter() };
										head.scaleX = 2;
										head.scaleY = 2;
										head.MoveTo(4, DefaultControlHeight - head.height - 4).AttachTo(HudContainer);
									}
								}
							);
						}
					);


					#endregion

					InitializeCompass();
					InitializeKeyboard();

					

					AttachMovementInput(EgoView, true, false);

					#region focus logic
					this.focusIn +=
						e =>
						{
							this.MovementEnabled_IsFocused = true;
							//WriteLine("focusIn");

							this.filters = null;

						};

					this.focusOut +=
						delegate
						{
							this.MovementEnabled_IsFocused = false;
							this.filters = new[] { Filters.GrayScaleFilter };
							//WriteLine("focusOut");
						};


					this.focusRect = null;
					this.mouseChildren = false;
					this.tabEnabled = true;
					#endregion
					
					//this.stage.focus = this;

					ResetEgoPosition();

					AddPortals();

					AddIngameEntities(
						delegate
						{
							stage.enterFrame +=
								e =>
								{
									if (EgoView.Image.alpha > 0)
										EgoView.RenderScene();
								};

							ReadyWithLoadingCurrentLevel();
						}
					);
				}
			);
			#endregion
		}

		public Action ReadyWithLoadingCurrentLevel;


		public void ReadyWithLoadingCurrentLevelDirect()
		{
			getpsyched.FadeOut(
			   delegate
			   {
				   this.EgoView.Image.FadeIn(
					   delegate
					   {
						   1000.AtDelayDo(
							   delegate
							   {
								   this.HudContainer.FadeIn();
							   }
						   );
					   }
				   );

			   }

		   );
		}




		private void CreateMapFromMaze()
		{
			var Map = new Texture32();


			#region safe map
			for (int i = 0; i < 32; i++)
				for (int j = 0; j < 32; j++)
				{
					Map[i, j] = bluewall;
				}
			#endregion

			maze = new BlockMaze(new MazeGenerator(MazeSize, MazeSize, null));

			#region write walls to map
			var wall_counter = 0;

			for (int x = 1; x < maze.Width - 1; x++)
				for (int y = 1; y < maze.Height - 1; y++)
				{
					if (maze.Walls[x][y])
					{
						wall_counter++;

						var variant = graywall;

						if (y > maze.Height / 2)
						{
							variant = woodwall;

							if (wall_counter % 7 == 0)
								variant = woodwall_books;
							if (wall_counter % 11 == 0)
								variant = woodwall_achtung;
							else if (wall_counter % 13 == 0)
								variant = woodwall_verboten;
						}
						else
						{
							variant = graywall;

							if (wall_counter % 8 == 0)
								variant = graywall_achtung;
							else if (wall_counter % 9 == 0)
								variant = graywall_verboten;

						}

						Map[x, y] = variant;

					}
					else
						Map[x, y] = 0;
				}
			#endregion


			#region maze is smaller than 31
			for (int x = 1; x < maze.Width - 1; x++)
			{
				Map[x, maze.Height - 1] = greenwall;
			}

			for (int y = 1; y < maze.Height - 1; y++)
			{
				Map[maze.Width - 1, y] = greenwall;
			}
			#endregion


			EgoView.Map.WorldMap = Map;
		}





	}
}
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

		private void InitializeMap()
		{
			#region fill map
			Assets.Default.stuff.ToBitmapDictionary(
				f =>
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


					Action<IEnumerator<Texture64.Entry>, Texture64, Action<SpriteInfoExtended>> AddSpriteByTexture =
							  (SpaceForStuff, tex, handler) =>
							  {
								  var p = SpaceForStuff.TakeOrDefault();

								  if (p == null)
									  return;

								  CreateDummy(tex).Do(handler).Position.To(p.XIndex + 0.5, p.YIndex + 0.5);

							  };



					var FreeSpaceForStuff = EgoView.Map.WorldMap.Entries.Where(i => i.Value == 0).Randomize().GetEnumerator();

					CreateGuards(FreeSpaceForStuff);


					var GoldTakenCounter = 0;

					#region gold

					var GoldSprites = new List<SpriteInfo>();

					#region nonblock
					Action AddNonBlockingItems =
						delegate
						{



							Assets.Default.nonblock.ToBitmapArray(
								sprites =>
								{
									for (int i = 0; i < 7; i++)
										foreach (var s in sprites)
										{
											// compiler bug: get a delegate to BCL class
											//AddSpriteByTexture(FreeSpaceForStuff, s, GoldSprites.Add);

											AddSpriteByTexture(FreeSpaceForStuff, s,
												k =>
												{
													k.Range = 0.5;
													//GoldSprites.Add(k);
												}
											);

										}
								}
							);


						};
					#endregion

					Assets.Default.gold.ToBitmapArray(
					   sprites =>
					   {

						   for (int i = 0; i < 6; i++)
							   foreach (var _s in sprites)
							   {
								   var s = _s;

								   // compiler bug: get a delegate to BCL class
								   //AddSpriteByTexture(FreeSpaceForStuff, s, GoldSprites.Add);

								   AddSpriteByTexture(FreeSpaceForStuff, s,
									   k =>
									   {
										   k.Range = 0.5;
										   k.ItemTaken +=
											   delegate
											   {
												   Assets.Default.treasure.play();
											   };

										   GoldSprites.Add(k);
									   }
								   );

							   }

						   var LastPosition = new Point();

						   EgoView.ViewPositionChanged +=
							   delegate
							   {
								   // only check for items each 0.5 distance travelled
								   if ((EgoView.ViewPosition - LastPosition).length < 0.5)
									   return;

								   Action Later = null;
								   Action ItemTaken = null;


								   foreach (var Item in EgoView.SpritesFromPointOfView)
								   {
									   var Item_Sprite = Item.Sprite as SpriteInfoExtended;

									   if (Item_Sprite != null)
										   if (!Item_Sprite.IsTaken)
											   if (Item.Distance < Item_Sprite.Range)
											   {
												   if (GoldSprites.Contains(Item_Sprite))
												   {
													   // ding-ding-ding!
													   Item_Sprite.IsTaken = true;

													   new Bitmap(new BitmapData(DefaultWidth, DefaultHeight, false, 0xffff00))
													   {
														   scaleX = DefaultScale,
														   scaleY = DefaultScale
													   }.AttachTo(this).FadeOutAndOrphanize(1000 / 24, 0.2);



													   if (Item_Sprite != null)
														   if (Item_Sprite.ItemTaken != null)
															   ItemTaken += () => Item_Sprite.ItemTaken();

													   GoldTakenCounter = (GoldTakenCounter + 1).Min(1);

													   Later +=
														   delegate
														   {
															   EgoView.Sprites.Remove(Item_Sprite);
															   GoldSprites.Remove(Item_Sprite);
														   };
												   }
											   }
								   }

								   if (Later != null)
									   Later();

								   LastPosition = EgoView.ViewPosition;

								   if (ItemTaken != null)
									   ItemTaken();
							   };

						   AddNonBlockingItems();

					   }
					);
					#endregion

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
					TheGoldStack.Position.To(maze.Width - 1.5, maze.Height - 1.5);
					TheGoldStack.Range = 0.5;
					TheGoldStack.ItemTaken +=
						delegate
						{
							if (EndLevelMode)
								return;


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

					#region hand
					var hand = f["hand.png"];
					const int handsize = 4;

					var hand_x = (DefaultControlWidth - hand.width * handsize) / 2;
					var hand_y = DefaultControlHeight - hand.height * handsize;
					hand.x = hand_x;
					hand.y = hand_y;
					hand.scaleX = handsize;
					hand.scaleY = handsize;
					hand.AttachTo(HudContainer);

					(1000 / 24).AtInterval(
						tt =>
						{
							hand.x = hand_x + Math.Cos(tt.currentCount * 0.2) * 6;
							hand.y = hand_y + Math.Abs(Math.Sin(tt.currentCount * 0.2)) * 4;
						}
					);
					#endregion

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


					ResetEgoPosition();

					stage.enterFrame +=
						e =>
						{
							//if (EndLevelMode)
							//    return;

							EgoView.RenderScene();
						};

					getpsyched.FadeOutAndOrphanize(1000 / 15, 0.1);

					this.EgoView.Image.FadeIn(
						delegate
						{
							1500.AtDelayDo(
								delegate
								{
									this.HudContainer.FadeIn();
								}
							);
						}
					);
				}
			);
			#endregion
		}




	}
}
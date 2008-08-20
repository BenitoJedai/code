using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using System.Linq;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Shared.Maze;
using ScriptCoreLib.Shared.Lambda;

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

		BlockMaze maze;
		ViewEngineBase EgoView;

		public FlashTreasureHunt()
		{

			EgoView = new ViewEngineBase(DefaultWidth, DefaultHeight)
			{
				FloorAndCeilingVisible = false,
				RenderLowQualityWalls = true,

				ViewPosition = new Point { x = 1.25, y = 1.25 },
				ViewDirection = (45 + 180).DegreesToRadians(),

			};

			EgoView.Image.scaleX = DefaultScale;
			EgoView.Image.scaleY = DefaultScale;

			EgoView.Image.AttachTo(this);

			var hud = Assets.Default.hud;

			hud.y = DefaultControlHeight - hud.height * 2;
			hud.scaleX = 2;
			hud.scaleY = 2;
			hud.AttachTo(this);

			Action<Bitmap[]> BitmapsLoadedAction =
				Bitmaps =>
				{
					var Spawn = default(Func<SpriteInfo>);

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


					Assets.Default.treasure.play();

				};


			Assets.Default.dude5
				.Where(f => f.FileName.EndsWith(".png"))
				.ToBitmapArray(BitmapsLoadedAction);

			Assets.Default.stuff.ToBitmapDictionary(
				f =>
				{
					const uint graywall = 0xff0000;
					const uint bluewall = 0x0000ff;
					const uint greenwall = 0x00ff00;
					const uint woodwall = 0x7F3300;

					var Map = new Texture32();


					#region safe map
					for (int i = 0; i < 32; i++)
						for (int j = 0; j < 32; j++)
						{
							Map[i, j] = bluewall;
						}
					#endregion

					maze = new BlockMaze(new MazeGenerator(14, 14, null));


					for (int x = 1; x < maze.Width - 1; x++)
						for (int y = 1; y < maze.Height - 1; y++)
						{
							if (maze.Walls[x][y])
							{
								if (y > maze.Height / 2)
									Map[x, y] = woodwall;
								else
									Map[x, y] = graywall;
							}
							else
								Map[x, y] = 0;
						}

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

					Action<IEnumerator<Texture64.Entry>, Texture64, Action<SpriteInfo>> AddSpriteByTexture =
							  (SpaceForStuff, tex, handler) => SpaceForStuff.Take().Do(p => CreateDummy(tex).Do(handler).Position.To(p.XIndex + 0.5, p.YIndex + 0.5));


					var FreeSpaceForStuff = EgoView.Map.WorldMap.Entries.Where(i => i.Value == 0).Randomize().GetEnumerator();

					#region gold

					var GoldSprites = new List<SpriteInfo>();

					Assets.Default.gold.ToBitmapArray(
					   sprites =>
					   {


						   foreach (var s in sprites)
						   {
							   for (int i = 0; i < 20; i++)
							   {
								   // compiler bug: get a delegate to BCL class
								   //AddSpriteByTexture(FreeSpaceForStuff, s, GoldSprites.Add);

								   AddSpriteByTexture(FreeSpaceForStuff, s,
									   k =>
									   {
										   k.Range = 0.5;
										   GoldSprites.Add(k);
									   }
								   );

							   }
						   }

						   var LastPosition = new Point();

						   EgoView.ViewPositionChanged +=
							   delegate
							   {
								   // only check for items each 0.5 distance travelled
								   if ((EgoView.ViewPosition - LastPosition).length < 0.5)
									   return;

								   Action Later = delegate { };


								   foreach (var Item in EgoView.SpritesFromPointOfView)
								   {
									   var Item_Sprite = Item.Sprite;

									   if (Item.Distance < Item_Sprite.Range)
									   {
										   if (GoldSprites.Contains(Item_Sprite))
										   {
											   // ding-ding-ding!

											   new Bitmap(new BitmapData(DefaultWidth, DefaultHeight, false, 0xffff00))
											   {
												   scaleX = DefaultScale,
												   scaleY = DefaultScale
											   }.AttachTo(this).FadeOutAndOrphanize(1000 / 24, 0.2);

											   Assets.Default.treasure.play();

											   Later += () => EgoView.Sprites.Remove(Item_Sprite);
										   }
									   }
								   }

								   Later();

								   LastPosition = EgoView.ViewPosition;
							   };


					   }
					);
					#endregion


					Func<string, Texture64> t =
						texname => f[texname + ".png"];

					#region game goal
					var TheGoldStack = CreateDummy(f["life.png"]);
					TheGoldStack.Position.To(maze.Width - 1.5, maze.Height - 1.5);
					TheGoldStack.Range = 0.5;
					GoldSprites.Add(TheGoldStack);
					#endregion


					EgoView.Map.Textures = new Dictionary<uint, Texture64>
                        {
                            {graywall, t("graywall")},
                            {bluewall, t("bluewall")},
                            {greenwall, t("greenwall")},
                            {woodwall, t("woodwall")},
                        };

					EgoView.RenderScene();

					stage.enterFrame +=
						e =>
						{
							EgoView.RenderScene();
						};

					InitializeCompass();
					InitializeKeyboard();
				}
			);
		}

		public SpriteInfo CreateWalkingDummy(Texture64[] Stand, params Texture64[][] Walk)
		{
			var s = new SpriteInfo
			{
				Position = new Point { x = EgoView.ViewPositionX, y = EgoView.ViewPositionY },
				Frames = Stand,
				Direction = EgoView.ViewDirection
			}.AddTo(EgoView.Sprites);

			if (Walk.Length > 0)
				(200).AtInterval(
					t =>
					{
						s.Frames = Walk[t.currentCount % Walk.Length];
					}
				);

			return s;
		}

		public SpriteInfo CreateDummy(Texture64 Stand)
		{
			return CreateWalkingDummy(new[] { Stand });

		}

	}
}
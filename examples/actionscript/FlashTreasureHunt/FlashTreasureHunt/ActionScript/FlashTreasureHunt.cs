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

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script]
	public partial class FlashTreasureHunt : Sprite
	{

		ViewEngineBase EgoView;

		public FlashTreasureHunt()
		{

			EgoView = new ViewEngineBase(DefaultWidth, DefaultHeight)
			{
				FloorAndCeilingVisible = false,

				ViewPosition = new Point { x = 1.5, y = 1.5 },
				ViewDirection = 90.DegreesToRadians(),

			};

			EgoView.Image.scaleX = DefaultScale;
			EgoView.Image.scaleY = DefaultScale;

			EgoView.Image.AttachTo(this);

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


					var Map = new Texture32();

					#region safe map
					for (int i = 0; i < 32; i++)
					{
						Map[i, 0] = bluewall;
						Map[0, i] = bluewall;
						Map[i, 31] = bluewall;
						Map[31, i] = bluewall;
					}
					#endregion

					var maze = new BlockMaze(new MazeGenerator(9, 9, null));

					for (int x = 1; x < maze.Width - 1; x++)
					{
						Map[x, maze.Height - 1] = bluewall;
					}

					for (int y = 1; y < maze.Height - 1; y++)
					{
						Map[maze.Width - 1, y] = bluewall;
					}

					for (int x = 1; x < maze.Width - 1; x++)
						for (int y = 1; y < maze.Height - 1; y++)
						{
							if (maze.Walls[x][y])
								Map[x, y] = graywall;
						}

					EgoView.Map.WorldMap = Map;

					Func<string, Texture64> t =
						texname => f[texname + ".png"];

					EgoView.Map.Textures = new Dictionary<uint, Texture64>
                        {
                            {graywall, t("graywall")},
                            {bluewall, t("bluewall")},
                            {0x00ff00, t("greenwall")},
                            {0x7F3300, t("woodwall")},
                        };

					EgoView.RenderScene();

					stage.enterFrame +=
						e =>
						{
							EgoView.RenderScene();
						};

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
	}
}
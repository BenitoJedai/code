using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.Linq;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.Shared.Lambda;
using Mahjong.Shared;
using ScriptCoreLib.ActionScript;

namespace Mahjong.ActionScript
{
	[Script]
	class LoadedAsset
	{
		public BitmapAsset Image;

		public RankAsset Asset;

		public static implicit operator LoadedAsset(RankAsset e)
		{
			return new LoadedAsset { Asset = e, Image = e.ToImage() };
		}
	}


	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	[SWF(width = 800, height = 600)]
	public class MahjongGame : Sprite
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public MahjongGame()
		{
			var s = new Asset.Settings();
			var last = default(VisibleTile);

			#region CreateTile
			Func<int, int, LoadedAsset, VisibleTile> CreateTile =
				(x, y, i) =>
				{
					var a = new VisibleTile(i, s);


					a.Background.AttachTo(stage);
					a.Background.MoveTo(x, y);

					a.Background.click += delegate
					{
						var next = a;

						if (last != null)
						{
							if (last.IsMatch(a))
							{
								System.Console.WriteLine("match!");

								last.Hide();
								a.Hide();

								next = null;
							}
						}

						System.Console.WriteLine("click: " + a.Info.Asset.Suit + " / " + a.Info.Asset.Rank);

						last = next;
					};

					a.Background.mouseOver +=
						delegate { a.Background.alpha = 0.8; };


					a.Background.mouseOut +=
						delegate { a.Background.alpha = 1; };

					return a;
				};
			#endregion



			Action<int, IEnumerable<RankAsset>> CreateTiles =
				(y, a) =>
				{
					int c = 0;



					foreach (var v in a.AsEnumerable())
					{


						c++;

						CreateTile((s.OuterWidth + 2) * c, (s.OuterHeight + 2) * y, (LoadedAsset)v);
					}
				};

			var stuff = Asset.Bamboo.
							Concat(Asset.Characters).
							Concat(Asset.Dots).
							Concat(Asset.Dragons).
							Concat(Asset.Flowers).
							Concat(Asset.Seasons).
							Concat(Asset.Winds);




			CreateTiles(1, Asset.Bamboo);
			CreateTiles(2, Asset.Dots);
			CreateTiles(3, Asset.Characters);
			CreateTiles(4, Asset.Winds);
			CreateTiles(5, Asset.Dragons);
			CreateTiles(6, Asset.Seasons);
			CreateTiles(7, Asset.Flowers);
			CreateTiles(8, stuff.Randomize());
			CreateTiles(9, stuff.Randomize());


			var stack1 = stuff.Randomize();


			CreateTile(220, 220, stack1.ElementAt(0));
		}
	}
}
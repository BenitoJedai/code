using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.CSharp.Extensions;
using Mahjong.Shared;
using System.Windows.Media;
using System.Media;

namespace Mahjong.Code
{
	[Script]
	public class MyCanvas : Canvas
	{
		// http://cynagames.com/

		public const double DefaultScale = 1;

		public const int DefaultWidth = 600;
		public const int DefaultHeight = 500;

		public const int DefaultScaledWidth = (int)((double)DefaultWidth * DefaultScale);
		public const int DefaultScaledHeight = (int)((double)DefaultHeight * DefaultScale);

		// Set if the platform supports playing embeddded sounds
		// WPF does not (2008 09)

		public readonly FutureAction<string> PlaySoundFuture;
		public readonly Action<string> PlaySound;

		public MyCanvas()
		{


			this.PlaySoundFuture = new FutureAction<string>();
			this.PlaySound = this.PlaySoundFuture;

			this.Width = DefaultScaledWidth;
			this.Height = DefaultScaledHeight;

			var Sounds = new
			{
				birds = PlaySoundFuture["birds"],
				flag = PlaySoundFuture["flag"],
				reveal = PlaySoundFuture["reveal"]
			};

			Sounds.birds();

			#region background
			new Image
			{
				Source = "assets/Mahjong.Assets/china.jpg".ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = DefaultScaledWidth,
				Height = DefaultScaledHeight
			}.AttachTo(this);

			new Image
			{
				Source = "assets/Mahjong.Assets/shadow.png".ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = DefaultScaledWidth,
				Height = DefaultScaledHeight
			}.AttachTo(this);
			#endregion

			var MyLayout = new VisibleLayout(
				new VisibleLayout.SettingsInfo
				{
					Scale = DefaultScale,
					ScaledWidth = DefaultScaledWidth,
					ScaledHeight = DefaultScaledHeight
				}
			);

			MyLayout.Container.AttachTo(this);

			var Comment = new TextBox
			{
				Text = "",
				IsReadOnly = true
			}.MoveTo(4, 4).AttachTo(this);

			var stuff = AbstractAsset.Bamboo. // 9x
			Concat(AbstractAsset.Characters). // 9x
			Concat(AbstractAsset.Dots). // 9x

			Concat(AbstractAsset.Dragons). // 2
			Concat(AbstractAsset.Flowers). // 4
			Concat(AbstractAsset.Seasons). // 4
			Concat(AbstractAsset.Winds); // 3

			MyLayout.LayoutChanging +=
				delegate
				{
					Console.WriteLine("LayoutChanging ...");

					// this occurs only after current load is complete
					MyLayout.LayoutProgress.Continue(
						delegate
						{
							Console.WriteLine("Layout loaded!");
						}
					);
					// we should suffle the ranks

					#region attach interactive
					foreach (var v in MyLayout.Tiles)
					{
						// any image
						v.RankImage = stuff.Random();

						v.Tile.Continue(
							(VisibleTile tt) =>
							{
								#region interactive logic
								tt.Control.MouseEnter +=
									delegate
									{
										tt.YellowFilter.Opacity = 0.5;

									};

				

								tt.Control.MouseLeave +=
									delegate
									{
										tt.YellowFilter.Opacity = 0;
									};

								// while loading the k.Tile.Value is null for siblings
								MyLayout.LayoutProgress.Continue(
									delegate
									{
										tt.Control.MouseEnter +=
											delegate
											{
												tt.Entry.Siblings.ForEach(k => k.Tile.Value.GreenFilter.Opacity = 0.5);
											};

										tt.Control.MouseLeave +=
											delegate
											{
												tt.Entry.Siblings.ForEach(k => k.Tile.Value.GreenFilter.Opacity = 0);
											};
									}
								);

								//tt.Control.MouseLeftButtonUp +=
								//    delegate
								//    {
								//        //tt.YellowFilter.Opacity = 0;
								//        tt.GreenFilter.Opacity = 0.5;
								//        Sounds.flag();
								//    };
								#endregion

							}
						);
					}
					#endregion

				};

			// this occurs always
			MyLayout.LayoutChanged +=
				delegate
				{
					// done loading new layout
					Comment.Text = MyLayout.Layout.Comment;

					Sounds.reveal();
				};

			Assets.Default.FileNames.Random(k => k.EndsWith(".lay")).ToStringAsset(
				DataString =>
				{
					MyLayout.Layout = new Layout(DataString);
				}
			);


		}
	}
}

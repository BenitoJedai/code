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

		public const int DefaultWidth = 480;
		public const int DefaultHeight = 480;

		public const int DefaultScaledWidth = (int)((double)DefaultWidth * DefaultScale);
		public const int DefaultScaledHeight = (int)((double)DefaultHeight * DefaultScale);

		// Set if the platform supports playing embeddded sounds
		// WPF does not (2008 09)

		public readonly FutureAction<string> PlaySoundFuture;
		public readonly Action<string> PlaySound;

		public MyCanvas()
		{
			this.PlaySoundFuture =  new FutureAction<string>();
			this.PlaySound = this.PlaySoundFuture;

			this.Width = DefaultScaledWidth;
			this.Height = DefaultScaledHeight;

			var Sounds = new
			{
				birds = PlaySoundFuture["birds"],
				flag = PlaySoundFuture["flag"]
			};

			Sounds.birds();
			
			new Image
			{
				Source = "assets/Mahjong.Assets/china.jpg".ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = DefaultScaledWidth,
				Height = DefaultScaledHeight
			}.AttachTo(this);

			var stuff = Asset.Bamboo.
					Concat(Asset.Characters).
					Concat(Asset.Dots).
					Concat(Asset.Dragons).
					Concat(Asset.Flowers).
					Concat(Asset.Seasons).
					Concat(Asset.Winds);

			var random = stuff.Concat(stuff).Randomize().ToArray();

			var s = new Asset.Settings { Scale = DefaultScale };

			const int TilesPerLine = 12;

			for (int i = 0; i < random.Length; i++)
			{
				var tt = new VisibleTile(s, random[i]);
				
				tt.Control.AttachTo(this).MoveTo(
					32 + (s.ScaledInnerWidth + 4) * (TilesPerLine - (i % TilesPerLine)), 
					32 + (s.ScaledInnerHeight + 4) * Convert.ToInt32(i / TilesPerLine));

				tt.Control.MouseLeftButtonUp +=
					delegate
					{
						Sounds.flag();
					};
			}


		}
	}
}

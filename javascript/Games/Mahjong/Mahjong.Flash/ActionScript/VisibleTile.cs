using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mahjong.Shared;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;

namespace Mahjong.ActionScript
{
	[Script]
	class VisibleTile
	{
		public readonly LoadedAsset Info;
		public readonly Asset.Settings Settings;

		public readonly Sprite Background = new Sprite();
		public readonly Sprite Display = new Sprite();

		public VisibleTile(LoadedAsset Info, Asset.Settings Settings)
		{
			this.Settings = Settings;
			this.Info = Info;

			this.Display.MoveTo(
				Settings.OuterWidth - Settings.InnerWidth - 1, 1); //, Settings.InnerWidth, Settings.InnerHeight);

			Info.Image.AttachTo(this.Display);


			Settings.BackgroundTile.ToImage().AttachTo(this.Background);
			this.Display.AttachTo(this.Background);

		}

		public bool IsMatch(VisibleTile a)
		{
			if (a == null)
				return false;

			// rule 1 - rank and suit must match

			if (a.Info.Asset.Rank != this.Info.Asset.Rank)
				return false;

			if (a.Info.Asset.Suit != this.Info.Asset.Suit)
				return false;

			return true;
		}

		public void Hide()
		{
			this.Background.Orphanize();
		}
	}
}

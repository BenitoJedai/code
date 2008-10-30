using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
	[Script]
	public class CardStack : ISupportsContainer
	{
		public CardDeck CurrentDeck;
		public Canvas Container { get; set; }

		public CardStack()
		{
			this.Container = new Canvas
			{
				Width = CardInfo.Width,
				Height = CardInfo.Height
			};

			new Image
			{
				Source = CardInfo.GetImagePath(KnownAssets.Path.DefaultCards, false, 0, "spider.empty", false).ToSource(),

				Width = CardInfo.Width,
				Height = CardInfo.Height
			}.AttachTo(Container);
		}


	}
}

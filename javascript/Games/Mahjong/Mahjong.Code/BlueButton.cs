using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Media;
using System.Windows;
using ScriptCoreLib.Shared.Avalon.TextButton;

namespace Mahjong.Code
{
	[Script]
	public class BlueButton : TextButtonControl
	{
		public BlueButton()
		{
			this.Width = 120;
			this.Height = 24;
			this.Foreground = Brushes.White;
			this.TextAlignment = TextAlignment.Center;

			this.Background.Fill = Brushes.Blue;
			this.Background.Opacity = 0.5;

			this.MouseEnter +=
				delegate
				{
					this.Background.Opacity = 1;
				};


			this.MouseLeave +=
				delegate
				{
					this.Background.Opacity = 0.5;
				};
		}
	}
}

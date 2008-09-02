using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FlashAvalonQueryExample.Shared
{
	[Script]
	public class MyCanvas : Canvas
	{
		public const int DefaultWidth = 600;
		public const int DefaultHeight = 400;


		public MyCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			#region Gradient
			for (int i = 0; i < DefaultHeight; i += 4)
			{
				new Rectangle
				{
					Fill = ((uint)(0xff00007F + Convert.ToInt32(128 * i / DefaultHeight))).ToSolidColorBrush(),
					Width = DefaultWidth,
					Height = 5,
				}.MoveTo(0, i).AttachTo(this);
			}
			#endregion

			// http://www.google.com/s2/favicons?domain=wordpress.com
			var KnownDomains = "google.com, wordpress.com, sf.net, mochiads.com, nonoba.com, newgrounds.com, youtube.com";

			var Color_Inactive = 0xffc0c0c0.ToSolidColorBrush();
			var Color_Active = 0xffffffff.ToSolidColorBrush();

			var KnownDomainsInputHeight = 120;

			var KnownDomainsInput = new TextBox
			{
				AcceptsReturn = true,
				FontSize = 15,
				Text = KnownDomains,
				BorderThickness = new Thickness(0),
				//Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Color_Inactive,
				Width = 400,
				Height = KnownDomainsInputHeight,
				TextWrapping = TextWrapping.Wrap
			}.MoveTo(32, 32).AttachTo(this);

			Action<TextBox> ApplyActiveColor =
				e =>
				{
					e.GotFocus +=
						delegate
						{
							e.Background = Color_Active;
						};


					e.LostFocus +=
						delegate
						{
							e.Background = Color_Inactive;
						};
				};

			ApplyActiveColor(KnownDomainsInput);

			var Filter = new TextBox
			{
				FontSize = 15,
				Text = ".com",
				BorderThickness = new Thickness(0),
				//Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Color_Inactive,
				Width = 400,
				Height = 22,
			}.MoveTo(32, 32 + KnownDomainsInputHeight + 4).AttachTo(this);

			ApplyActiveColor(Filter);

		}
	}
}

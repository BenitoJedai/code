using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
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

			var FilterInputHeight = 22;

			var FilterInput = new TextBox
			{
				FontSize = 15,
				Text = ".com",
				BorderThickness = new Thickness(0),
				//Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Color_Inactive,
				Width = 400,
				Height = FilterInputHeight,
			}.MoveTo(32, 32 + KnownDomainsInputHeight + 4).AttachTo(this);

			ApplyActiveColor(FilterInput);

			var ResultOutputHeight = 120;
			var ResultOutput = new TextBox
			{
				AcceptsReturn = true,
				FontSize = 15,
				Text = "?",
				BorderThickness = new Thickness(0),
				//Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Color_Inactive,
				Width = 400,
				Height = ResultOutputHeight,
				IsReadOnly = true
				
			}.MoveTo(32, 32 + KnownDomainsInputHeight + 4 + FilterInputHeight + 4).AttachTo(this);

			ApplyActiveColor(ResultOutput);

			Action AnyInputChanged =
				delegate
				{
					ResultOutput.Text = "chars: " + KnownDomainsInput.Text.Length;

					var query = from k in KnownDomainsInput.Text.Split(',')
								let t = k.Trim()
								where t.LooksLikeValidCName()
								where t.Contains(FilterInput.Text)
								orderby t
								select t;

					//var Entries = KnownDomainsInput.Text.Split(',').Select(t => t.Trim()).Where(k => k.LooksLikeValidCName()).ToArray();

					ResultOutput.AppendText(" entries: " + query.Count() + Environment.NewLine);

					query.ForEach(
						(k, i) =>
						{
							ResultOutput.AppendText((i + 1) + ". " + k + Environment.NewLine);
						}
					);
					// we need to validate CNAMEs



				};

			#region attach AnyInputChanged
			KnownDomainsInput.TextChanged +=
				delegate
				{
					AnyInputChanged();
				};

			FilterInput.TextChanged +=
				delegate
				{
					AnyInputChanged();
				};

			AnyInputChanged();

			#endregion

		}
	}
}

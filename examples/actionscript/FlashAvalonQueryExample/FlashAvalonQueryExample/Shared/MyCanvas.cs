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
using System.Windows.Media.Imaging;

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

			var FaciconService = "http://www.google.com/s2/favicons?domain=";

			// http://www.google.com/s2/favicons?domain=wordpress.com
			var KnownDomains = "google.com, wordpress.com, sf.net, mochiads.com, nonoba.com, newgrounds.com, youtube.com";

			var Color_Inactive = 0xffc0c0c0.ToSolidColorBrush();
			var Color_Active = 0xffffffff.ToSolidColorBrush();
			var Color_Error = 0xffffff80.ToSolidColorBrush();

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

			//var ResultOutputHeight = 120;
			//var ResultOutput = new TextBox
			//{
			//    AcceptsReturn = true,
			//    FontSize = 15,
			//    Text = "?",
			//    BorderThickness = new Thickness(0),
			//    //Foreground = 0xffffffff.ToSolidColorBrush(),
			//    Background = Color_Inactive,
			//    Width = 400,
			//    Height = ResultOutputHeight,
			//    IsReadOnly = true

			//}.MoveTo(32, 32 + KnownDomainsInputHeight + 4 + FilterInputHeight + 4).AttachTo(this);

			//ApplyActiveColor(ResultOutput);

			var AnyInputChangedBefore = new List<Action>();

			Action AnyInputChanged =
				delegate
				{
					AnyInputChangedBefore.Do();
					AnyInputChangedBefore.Clear();

					//ResultOutput.Text = "chars: " + KnownDomainsInput.Text.Length;

					var query = from k in KnownDomainsInput.Text.Split(',')
								let t = k.Trim()
								where t.LooksLikeValidCName()
								where t.Contains(FilterInput.Text)
								orderby t
								select t;

					//var Entries = KnownDomainsInput.Text.Split(',').Select(t => t.Trim()).Where(k => k.LooksLikeValidCName()).ToArray();

					//ResultOutput.AppendTextLine(" entries: " + query.Count());

					query.ForEach(
						(k, i) =>
						{
							var src = FaciconService + k;

							var y = 32 + KnownDomainsInputHeight + 4 + FilterInputHeight + 4 + i * 22;

							var bg = new Rectangle
							{
								Fill = Color_Active,
								Width = 400,
								Height = 22

							}
							.MoveTo(32, y).AttachTo(this);

							
							//var img = new Image
							//{
								
							//}
							//.MoveTo(32 + 2, y + 2).AttachTo(this);

						
							var text = new TextBox
							{
								IsReadOnly = true,
								Text = (i + 1) + ". " + k,
								Width = 400 - 32,
								BorderThickness = new Thickness(0),
								Background = Color_Inactive
							}.MoveTo(32 + 32, y + 2).AttachTo(this);

				
						

							//var src_uri = new BitmapImage(new Uri(src));

							//src_uri.DownloadCompleted +=
							//    delegate
							//    {
							//        text.Background = Color_Active;
							//    };

							//src_uri.DownloadFailed +=
							//    delegate
							//    {
							//        text.Background = Color_Error;
							//    };


							//img.Source = src_uri;

							AnyInputChangedBefore.Add(() => bg.Orphanize());
							//AnyInputChangedBefore.Add(() => img.Orphanize());
							AnyInputChangedBefore.Add(() => text.Orphanize());
							//ResultOutput.AppendTextLine((i + 1) + ". " + k + " " + src);
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

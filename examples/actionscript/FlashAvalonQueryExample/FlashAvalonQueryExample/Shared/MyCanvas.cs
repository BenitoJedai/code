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
		public const int DefaultHeight = 600;


		public MyCanvas()
		{
			CheckIsPanel();

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

			//var FaciconService = "http://www.google.com/s2/favicons?domain=";

			// http://www.google.com/s2/favicons?domain=wordpress.com
			var KnownDomains = "63/155262375_104aee1bb0, 3183/2825405970_a1469cd673, 2336/2454679206_de5176b827";

			Func<Brush> Color_Inactive = () => 0xffc0c0c0.ToSolidColorBrush();
			Func<Brush> Color_Active = () => 0xffffffff.ToSolidColorBrush();
			Func<Brush> Color_Error = () => 0xffffff80.ToSolidColorBrush();

			var KnownDomainsInputHeight = 120;

			var KnownDomainsInput = new TextBox
			{
				AcceptsReturn = true,
				FontSize = 15,
				Text = KnownDomains,
				BorderThickness = new Thickness(0),
				//Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Color_Inactive(),
				Width = 400,
				Height = KnownDomainsInputHeight,
				TextWrapping = TextWrapping.Wrap
			}.MoveTo(32, 32).AttachTo(this);

			KnownDomainsInput.Orphanize();
			KnownDomainsInput.AttachTo(this);

			Action<TextBox> ApplyActiveColor =
				e =>
				{
					e.GotFocus +=
						delegate
						{
							e.Background = Color_Active();
						};


					e.LostFocus +=
						delegate
						{
							e.Background = Color_Inactive();
						};
				};

			ApplyActiveColor(KnownDomainsInput);

			var FilterInputHeight = 22;

			var FilterInput = new TextBox
			{
				FontSize = 15,
				Text = "63",
				BorderThickness = new Thickness(0),
				//Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Color_Inactive(),
				Width = 400,
				Height = FilterInputHeight,
			}.MoveTo(32, 32 + KnownDomainsInputHeight + 4).AttachTo(this);

			ApplyActiveColor(FilterInput);


			var AnyInputChangedBefore = new List<Action>();

			Action AnyInputChanged =
				delegate
				{
					AnyInputChangedBefore.Do();
					AnyInputChangedBefore.Clear();

					//ResultOutput.Text = "chars: " + KnownDomainsInput.Text.Length;

					var query = from k in KnownDomainsInput.Text.Split(',')
								let t = k.Trim()
								//where t.LooksLikeValidCName()
								where t.Contains(FilterInput.Text)
								orderby t
								select t;

					//var Entries = KnownDomainsInput.Text.Split(',').Select(t => t.Trim()).Where(k => k.LooksLikeValidCName()).ToArray();

					//ResultOutput.AppendTextLine(" entries: " + query.Count());

					query.ForEach(
						(k, i) =>
						{
							// http://static.flickr.com/63/155262375_104aee1bb0.jpg
							var src = "http://static.flickr.com/" + k + "_s.jpg";

							var y = 32 + KnownDomainsInputHeight + 4 + FilterInputHeight + 4 + i * 78;

							var bg = new Rectangle
							{
								Fill = Color_Inactive(),
								Width = 400,
								Height = 24

							}
							.MoveTo(32, y).AttachTo(this);


							var img = new Image
							{
								Width = 75,
								Height = 75
							}
							.MoveTo(32 + 2, y + 2).AttachTo(this);


							var text = new TextBox
							{
								IsReadOnly = true,
								Text = (i + 1) + ". " + src,
								Width = 400 - 80,
								Height = 20,
								BorderThickness = new Thickness(0),
								Foreground = 0xff0000ff.ToSolidColorBrush(),
								Background = 0xffffffff.ToSolidColorBrush()
							}.MoveTo(32 + 80, y + 2).AttachTo(this);




							var src_uri = new BitmapImage(new Uri(src));

							src_uri.DownloadCompleted +=
								delegate
								{
									text.Foreground = 0xff000000.ToSolidColorBrush();
									//bg.Fill = Color_Active();
								};

							src_uri.DownloadFailed +=
								delegate
								{
									text.Text += " failed...";
									text.Foreground = 0xffff0000.ToSolidColorBrush();
									//bg.Fill = Color_Error();
								};


							img.Source = src_uri;


							AnyInputChangedBefore.Add(() => bg.Orphanize());
							AnyInputChangedBefore.Add(() => img.Orphanize());
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

		private void CheckIsPanel()
		{
			object e = this;

			var p = e as Panel;

			if (p == null)
				throw new Exception("this not a Panel?");
		}
	}
}

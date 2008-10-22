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
	public class AvalonQueryExampleCanvas : Canvas
	{
		public const int DefaultWidth = 600;
		public const int DefaultHeight = 600;


		public AvalonQueryExampleCanvas()
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

			new Image
			{
				Source = "assets/FlashAvalonQueryExample/labels.png".ToSource()
			}.MoveTo(0, 0).AttachTo(this);

			//var FaciconService = "http://www.google.com/s2/favicons?domain=";

			// http://www.google.com/s2/favicons?domain=wordpress.com
			var KnownDomains = "63/155262375_104aee1bb0, 3183/2825405970_a1469cd673, 2336/2454679206_de5176b827, 3178/2825551196_6548ff54b9";
			var KnownFilter = "31";

			Func<Brush> Color_Inactive = () => 0xffc0c0c0.ToSolidColorBrush();
			Func<Brush> Color_Active = () => 0xffffffff.ToSolidColorBrush();
			Func<Brush> Color_Error = () => 0xffffff80.ToSolidColorBrush();

			var KnownDomainsInputHeight = 100;

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
				Text = KnownFilter,
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

					var query = from k in KnownDomainsInput.Text.Split(',')
								let t = k.Trim()
								where t.Contains(FilterInput.Text)
								orderby t
								select t;


					query.ForEach(
						(k, i) =>
						{
							// http://static.flickr.com/63/155262375_104aee1bb0.jpg
							var src = "http://static.flickr.com/" + k + "_s.jpg";

							var y = 32 + KnownDomainsInputHeight + 4 + FilterInputHeight + 4 + i * 80;

							var bg = new Rectangle
							{
								Fill = Color_Inactive(),
								Width = 400,
								Height = 26

							}
							.MoveTo(32, y + 20).AttachTo(this);

							var img_shadow = new Rectangle
							{
								Fill = 0xff000000.ToSolidColorBrush(),
								Opacity = 0.3,
								Width = 75,
								Height = 75
							}
							.MoveTo(32 + 2 + 3, y + 2 + 3).AttachTo(this);

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
								Width = 400 - 88,
								Height = 20,
								BorderThickness = new Thickness(0),
								Foreground = 0xff0000ff.ToSolidColorBrush(),
								Background = 0xffffffff.ToSolidColorBrush()
							}.MoveTo(32 + 84, y + 22).AttachTo(this);




							var src_uri = new BitmapImage();

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

							src_uri.BeginInit();
							src_uri.UriSource = new Uri(src);
							src_uri.EndInit();


							img.Source = src_uri;

							text.Background = Color_Inactive();
							bg.Opacity = 0.3;

							text.GotFocus +=
								delegate
								{
									text.Background = Color_Active();
									bg.Opacity = 1;
								};

							text.LostFocus +=
								delegate
								{
									text.Background = Color_Inactive();
									bg.Opacity = 0.3;
								};

							AnyInputChangedBefore.Add(
								delegate
								{
									bg.Orphanize();
									img.Orphanize();
									text.Orphanize();
									img_shadow.Orphanize();
								}
							);

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

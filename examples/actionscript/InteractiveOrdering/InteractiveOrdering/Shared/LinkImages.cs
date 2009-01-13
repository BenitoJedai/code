using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Input;

namespace InteractiveOrdering.Shared
{
	[Script]
	public class LinkImages : IEnumerable<LinkImages.LinkImage>
	{
		public string Text;

		[Script]
		public class LinkImage : ISupportsContainer
		{
			public Canvas Container { get; set; }

			public const int ImageWidth = 400;
			public const int ImageHeight = 300;

			public readonly Image Image;
			public readonly Rectangle Overlay;

			public LinkImage SizeTo(double Zoom)
			{
				var w = ImageWidth * Zoom;
				var h = ImageHeight * Zoom;

				this.Container.Width = w;
				this.Container.Height = h;

				this.Image.Width = w;
				this.Image.Height = h;

				this.Overlay.Width = w;
				this.Overlay.Height = h;

				return this;
			}

			public bool ClickEnabled;

			public event Action Click;
			public LinkImage(ImageSource src, Uri href)
			{
				this.Container = new Canvas
				{
					Width = ImageWidth,
					Height = ImageHeight
				};

				this.Image = new Image
				{
					Stretch = Stretch.Fill,
					Width = ImageWidth,
					Height = ImageHeight,
					Source = src,
				}.AttachTo(this);

				this.Overlay = new Rectangle
				{
					Fill = Brushes.White,
					Width = ImageWidth,
					Height = ImageHeight,
					Opacity = 0,
					Cursor = Cursors.Hand
				}.AttachTo(this);

				this.Overlay.MouseLeftButtonUp +=
					delegate
					{
						if (ClickEnabled)
						{
							if (Click != null)
								Click();

						}
						else
						{
							href.NavigateTo(this.Container);
						}
					};
			}
		}

		public readonly BindingList<LinkImage> Images = new BindingList<LinkImage>();

		public event Action<LinkImages, LinkImage> Click;

		string InternalPath;
		public string Path
		{
			get { return InternalPath; }
			set
			{
				InternalPath = value;

				Images.Clear();

				if (!string.IsNullOrEmpty( InternalPath))
				{
					(InternalPath + "/links.txt").ToStringAsset(
						data =>
						{
							using (var s = new StringReader(data))
							{
								s.ReadLines(
									(Link, Index) =>
									{
										var href = new Uri(Link);

										var i = new LinkImage(
												(InternalPath + "/" + Index + ".jpg").ToSource(),
												href
											);

										i.Click +=
											delegate
											{
												if (this.Click != null)
													this.Click(this, i);

											};

										this.Images.Add(
											i
										);
									}
								);
								
							}

							if (Loaded != null)
								Loaded();
						}
					);
				}
			}
		}

		public event Action Loaded;

		#region IEnumerable<LinkImage> Members

		public IEnumerator<LinkImages.LinkImage> GetEnumerator()
		{
			return this.Images.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.Images.GetEnumerator();
		}

		#endregion
	}
}

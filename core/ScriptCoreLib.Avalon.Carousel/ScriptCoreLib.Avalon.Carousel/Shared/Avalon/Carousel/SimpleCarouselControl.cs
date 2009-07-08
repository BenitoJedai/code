using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;

namespace ScriptCoreLib.Shared.Avalon.Carousel
{
	[Script]
	public class SimpleCarouselControl : ISupportsContainer
	{
		public Canvas Container { get; set; }
		public Canvas Overlay { get; set; }


		[Script]
		public class Entry
		{
			public double o;
			public double cy;
			public Canvas pc;
			public Image ps;
			public Rectangle Overlay;
			public Action Tick;

			public EntryInfo Info;
		}

		[Script]
		public class EntryInfo
		{
			public ImageSource Source;
			public double Position;

			public Action Click;

			public Action MouseEnter;
			public Action MouseLeave;

			public string Text;
		}

		public readonly Action<EntryInfo> AddEntry;

		public readonly DispatcherTimer Timer;

		public void Hide()
		{
			this.Container.Hide();
			this.Overlay.Hide();
		}

		public void Show()
		{
			this.Container.Show();
			this.Overlay.Show();
		}

		public TextBox Caption { get; set; }

		public event Action Idle;

		public event Action<EntryInfo> Hover;

		public SimpleCarouselControl(int DefaultWidth, int DefaultHeight)
		{
			this.Container = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			};

			var ContentContainer = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			}.AttachTo(this.Container);

			this.Caption = new TextBox
			{
				Width = DefaultWidth,
				Height = 32,
				TextAlignment = TextAlignment.Center,
				Foreground = Brushes.White,
				BorderThickness = new Thickness(0),
				Background = Brushes.Transparent,
				IsReadOnly = true
			}.MoveTo(0, (DefaultHeight - 32) / 2).AttachTo(this.Container);

			this.Overlay = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight,
			};

			var OverlayFill = new Rectangle
			{
				Width = DefaultWidth,
				Height = DefaultHeight,
				Fill = Brushes.Red,
				Opacity = 0
			}.AttachTo(this.Overlay);

			var a = new List<Entry>();

			var OverlayReorderingEnabled = true;

			#region Timer
			this.Timer = (1000 / 30).AtInterval(
				delegate
				{
					a.ForEach(k => k.Tick());

					a.OrderBy(k => k.cy).ForEach(
						k =>
						{

							if (OverlayReorderingEnabled)
							{
								// in javascript reordering an element under mouse
								// will leave you without the leave event
								// we could sort other nodes around it tho

								k.Overlay.Orphanize();
								k.Overlay.AttachTo(this.Overlay);
							}

							k.pc.Orphanize();
							k.pc.AttachTo(this.Container);

						}
					);
				}
			);
			#endregion


			var s = 0.01;

			this.AddEntry =
				e =>
				{
					var pc_Width = 166 + 9;
					var pc_Height = 90 + 9 * 2;

					var pc = new Canvas
					{
						//Background = Brushes.Green,
						Width = pc_Width,
						Height = pc_Height
					}.AttachTo(ContentContainer);

					const string Assets = "assets/ScriptCoreLib.Avalon.Carousel";

					var p = new Image
					{
						Width = 166,
						Height = 90,
						Stretch = Stretch.Fill,
						Source = (Assets + "/PreviewShadow.png").ToSource()
					}.AttachTo(pc);


					var ps = new Image
					{
						Width = 138,
						Height = 108,
						Stretch = Stretch.Fill,
						Source = (Assets + "/PreviewSelection.png").ToSource()
					}.AttachTo(pc);

					var pi = new Image
					{
						Width = 120,
						Height = 90,
						Stretch = Stretch.Fill,
						Source = e.Source,
						Cursor = Cursors.Hand
					}.AttachTo(pc);

					var Overlay_Width = 120;
					var Overlay_Height = 90;

					var Overlay = new Rectangle
					{
						Fill = Brushes.Black,
						Width = Overlay_Width,
						Height = Overlay_Height,
						Cursor = Cursors.Hand,
						Opacity = 0
					}.AttachTo(this.Overlay);

					ps.Hide();

					Overlay.MouseLeftButtonUp +=
						delegate
						{
							if (e.Click != null)
								e.Click();

						};

					var IsHot = false;

					Overlay.MouseLeave +=
						delegate
						{
							OverlayReorderingEnabled = true;

							if (e.Text != null)
								this.Caption.Text = "";

							if (Idle != null)
								Idle();

						};

					Overlay.MouseEnter +=
						delegate
						{
							OverlayReorderingEnabled = false;

							if (e.MouseEnter != null)
								e.MouseEnter();

							if (Hover != null)
								Hover(e);

							if (e.Text != null)
								this.Caption.Text = e.Text;

							IsHot = true;
							s = 0.004;

							p.Opacity = 1;
							pi.Opacity = 1;

							a.ForEach(
								k =>
								{
									if (k.ps == ps)
									{
										ps.Show();
									}
									else
									{
										k.ps.Hide();
									}



								}
							);

						};

					var MyEntry = new Entry
						{
							o = 1,
							cy = 0,
							pc = (Canvas)pc,
							ps = (Image)ps,
							Overlay = (Rectangle)Overlay,
							Info = e
						};

					//var o = new Boxed<double>();
					//var _cy = new Boxed<double>();

					Overlay.MouseLeave +=
						delegate
						{
							if (e.MouseLeave != null)
								e.MouseLeave();

							IsHot = false;
							p.Opacity = MyEntry.o;
							pi.Opacity = MyEntry.o;


							s = 0.01;

							ps.Hide();
						};





					#region Tick
					MyEntry.Tick =
						delegate
						{

							var x = Math.Cos(e.Position);
							var y = Math.Sin(e.Position);

							var z = (y + 3) / 4;

							MyEntry.o = z;

							if (!IsHot)
							{
								pi.Opacity = MyEntry.o;
								//p.Opacity = MyEntry.o;
							}

							e.Position += s * z;

							pc.Width = pc_Width * z;
							pc.Height = pc_Height * z;

							Overlay.Width = Overlay_Width * z;
							Overlay.Height = Overlay_Height * z;

							p.Width = 166 * z;
							p.Height = 90 * z;


							pi.Width = 120 * z;
							pi.Height = 90 * z;

							ps.Width = 138 * z;
							ps.Height = 108 * z;

							var cx = x * (DefaultWidth - 160) / 2 + DefaultWidth / 2;
							var cy = y * (DefaultHeight / 2) / 2 + DefaultHeight / 2;

							MyEntry.cy = cy;

							pc.MoveTo(
								cx - pc.Width / 2,
								cy - pc.Height / 2
							);

							p.MoveTo(
								z * 9,
								z * 9
							);

							pi.MoveTo(
								z * 9,
								z * 9
							);


							ps.MoveTo(
								0,
								0
							);

							Overlay.MoveTo(
								cx - pc.Width / 2 + 9 * z,
								cy - pc.Height / 2 + 9 * z
							);
						};
					#endregion


					a.Add(
						MyEntry
					);

					MyEntry.Tick();
				};

		}
	}
}

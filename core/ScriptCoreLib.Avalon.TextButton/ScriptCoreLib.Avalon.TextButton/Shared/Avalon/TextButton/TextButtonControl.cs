using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Input;

namespace ScriptCoreLib.Shared.Avalon.TextButton
{
	[Script]
	public class TextButtonControl
	{
		public readonly Rectangle Background;

		public readonly TextBox Content;

		public readonly Rectangle Overlay;

		public readonly Canvas Container;

		public TextButtonControl()
		{
			this.Container = new Canvas
			{

			};

			this.Padding = 4;

			this.Background = new Rectangle
			{
				Fill = Brushes.Transparent,
			}.AttachTo(this.Container).MoveTo(0, 0);

			this.Content = new TextBox
			{
				Background = Brushes.Transparent,
				BorderThickness = new Thickness(0),
				IsReadOnly = true
			}.AttachTo(this.Container).MoveTo(0, 0);

			this.Overlay = new Rectangle
			{
				Cursor = Cursors.Hand,
				Fill = Brushes.White,
				Opacity = 0
			}.AttachTo(this.Container).MoveTo(0, 0);
		}

		public Brush Foreground
		{
			set
			{
				this.Content.Foreground = value;
			}
		}

		public string Text
		{
			set
			{
				this.Content.Text = value;
			}
		}

		public TextAlignment TextAlignment
		{
			set
			{
				this.Content.TextAlignment = value;
			}
		}

		public double Width
		{
			set
			{
				this.Container.Width = value;
				this.Background.Width = value;
				this.Content.Width = value - Padding * 2;
				Canvas.SetLeft(this.Content, Padding);
				this.Overlay.Width = value;
			}
		}

		public double Height
		{
			set
			{
				this.Container.Height = value;
				this.Background.Height = value;
				this.Content.Height = value - Padding * 2;
				Canvas.SetTop(this.Content, Padding);
				this.Overlay.Height = value;
			}
		}

		public double Padding { get; set; }

		public event Action MouseEnter
		{
			add
			{
				this.Overlay.MouseEnter += delegate { value(); };
			}
			remove
			{
				throw new NotSupportedException();
			}
		}

		public event Action MouseLeave
		{
			add
			{
				this.Overlay.MouseLeave += delegate { value(); };
			}
			remove
			{
				throw new NotSupportedException();
			}
		}

		public event Action Click
		{
			add
			{
				this.Overlay.MouseLeftButtonUp += delegate { value(); };
			}
			remove
			{
				throw new NotSupportedException();
			}
		}
	}
}

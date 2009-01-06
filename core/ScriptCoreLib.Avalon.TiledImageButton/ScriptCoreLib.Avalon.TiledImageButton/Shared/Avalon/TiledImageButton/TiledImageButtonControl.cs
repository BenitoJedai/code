using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace ScriptCoreLib.Shared.Avalon.TiledImageButton
{

	[Script]
	public class TiledImageButtonControl
	{
		public readonly Canvas Container;
		public readonly Image Tiles;
		public readonly Rectangle Overlay;
		public readonly ImageButtonStates States;

		[Script]
		public class StateSelector
		{
			public Func<ImageTileSelector, Action> AsDisabled;
			public Func<ImageTileSelector, Action> AsEnabled;
			public Func<ImageTileSelector, Action> AsHot;
			public Func<ImageTileSelector, Action> AsPressed;
		}

		public TiledImageButtonControl(ImageSource Source, int Width, int Height, StateSelector StateSelector)
		{
			this.Container = new Canvas
			{
				Width = Width,
				Height = Height,

				Clip = new RectangleGeometry
				{
					Rect = new Rect { X = 0, Y = 0, Width = Width, Height = Height }
				}

			};

			this.Tiles = new Image
			{
				Stretch = Stretch.Fill,
				Source = Source,

			}.AttachTo(this.Container);

			var s = new ImageTileSelector(this.Tiles, Width, Height);

			this.Overlay = new Rectangle
			{
				Fill = Brushes.White,
				Width = Width,
				Height = Height,
				Opacity = 0
			}.AttachTo(this.Container);

			this.States = new ImageButtonStates(this.Overlay,
				StateSelector.AsDisabled(s),
				StateSelector.AsEnabled(s),
				StateSelector.AsHot(s),
				StateSelector.AsPressed(s)
			);
		}

		public bool Enabled
		{
			get
			{
				return this.States.Enabled;
			}
			set
			{
				this.States.Enabled = value;
			}
		}
	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;
using Mahjong.Shared;
using System.Windows.Shapes;
using System.Windows;
using ScriptCoreLib.Shared.Lambda;

namespace Mahjong.Code
{
	[Script]
	public class VisibleTile
	{
		public readonly Canvas Control = new Canvas();

		public readonly Image RankImage;

		public readonly Image BlackFilter;

		public readonly Image YellowFilter;
		public readonly Image GreenFilter;
		public readonly Image RedFilter;

		public VisibleLayout.Entry Entry;


		public Rectangle Overlay;

		public Future LayoutProgress;

		public readonly AbstractAsset.Settings Settings;

		public UIElement InteractiveControl
		{
			get
			{
				if (Overlay == null)
					return Control;

				return Overlay;
			}
		}

		public event Action MouseEnter
		{
			add
			{
				InteractiveControl.MouseEnter += delegate { value(); };
			}
			remove
			{
				throw new NotSupportedException();
			}
		}

		public event Action MouseLeaveWhenLayoutLoaded
		{
			add
			{
				this.LayoutProgress.Continue(
					delegate
					{
						InteractiveControl.MouseLeave += delegate { value(); };
					}
				);
			}
			remove
			{
				throw new NotSupportedException();
			}
		}

		public event Func<Action> MouseEnterLeaveWhenLayoutLoaded
		{
			add
			{
				this.LayoutProgress.Continue(
					delegate
					{
						var Leave = new Future();

						InteractiveControl.MouseLeave +=
							delegate
							{
								var x = Leave;
								Leave = new Future();
								x.Signal();
							};

						InteractiveControl.MouseEnter += 
							delegate 
							{ 
								Leave.Continue(value()); 

							};
					}
				);

			}
			remove
			{
				throw new NotSupportedException();
			}
		}

		public event Action MouseEnterWhenLayoutLoaded
		{
			add
			{
				this.LayoutProgress.Continue(
					delegate
					{
						InteractiveControl.MouseEnter += delegate { value(); };
					}
				);
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
				InteractiveControl.MouseLeave += delegate { value(); };
			}
			remove
			{
				throw new NotSupportedException();
			}
		}

		public event Action ClickWhenLayoutLoaded
		{
			add
			{
				this.LayoutProgress.Continue(
					delegate
					{
						InteractiveControl.MouseLeftButtonUp += delegate { value(); };
					}
				);
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
				InteractiveControl.MouseLeftButtonUp += delegate { value(); };
			}
			remove
			{
				throw new NotSupportedException();
			}
		}

		public VisibleTile(AbstractAsset.Settings s, RankAsset r)
		{
			this.Settings = s;


			Control.Width = s.OuterWidth;
			Control.Height = s.OuterHeight;

			new Image
			{
				Source = s.BackgroundTileShadow.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledShadowWidth,
				Height = s.ScaledShadowHeight
			}.AttachTo(Control).MoveTo(-4 * s.Scale, -4 * s.Scale);

			new Image
			{
				Source = s.BackgroundTile.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledOuterWidth,
				Height = s.ScaledOuterHeight
			}.AttachTo(Control);


			RankImage = new Image
			{
				Source = r.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledInnerWidth,
				Height = s.ScaledInnerHeight
			}.AttachTo(Control).MoveTo((s.ScaledOuterWidth - s.ScaledInnerWidth - 1), 1 * s.Scale);



			BlackFilter = new Image
			{
				Source = s.BackgroundTileBlack.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledOuterWidth,
				Height = s.ScaledOuterHeight
			}.AttachTo(Control);

			YellowFilter = new Image
			{
				Source = s.BackgroundTileYellow.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledShadowWidth,
				Height = s.ScaledShadowHeight,
				Opacity = 0
			}.AttachTo(Control).MoveTo(-4 * s.Scale, -4 * s.Scale);



			GreenFilter = new Image
			{
				Source = s.BackgroundTileGreen.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledShadowWidth,
				Height = s.ScaledShadowHeight,
				Opacity = 0
			}.AttachTo(Control).MoveTo(-4 * s.Scale, -4 * s.Scale);

			RedFilter = new Image
			{
				Source = s.BackgroundTileRed.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledShadowWidth,
				Height = s.ScaledShadowHeight,
				Opacity = 0
			}.AttachTo(Control).MoveTo(-4 * s.Scale, -4 * s.Scale);

		}


	}

}

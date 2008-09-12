using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;

namespace ScriptCoreLib.Shared.Avalon.TiledImageButton
{
	[Script]
	public class AeroNavigationBar
	{
		public readonly Canvas Container;
		public readonly Image Background;
		public readonly TiledImageButtonControl ButtonGoBack;
		public readonly TiledImageButtonControl ButtonGoForward;

		public event Action GoBack;
		public event Action GoForward;

		public AeroNavigationBar()
		{
			Container = new Canvas { Width = 27 * 2 + 6, Height = 27 + 4 };

			Background = new Image
			{
				Source = "assets/ScriptCoreLib.Avalon.TiledImageButton/backMenuPic.png".ToSource(),
				Clip = new RectangleGeometry
				{
					Rect = new Rect { X = 0, Y = 0, Width = 27 * 2 + 6, Height = 27 + 4 }
				}
			}.MoveTo(-1, 0).AttachTo(Container);

			ButtonGoBack = new TiledImageButtonControl(
				"assets/ScriptCoreLib.Avalon.TiledImageButton/back-forward-large.png".ToSource(),
				 27, 27,
				 new TiledImageButtonControl.StateSelector
				 {
					 AsDisabled = s => s[0, 0],
					 AsEnabled = s => s[0, 1],
					 AsHot = s => s[0, 2],
					 AsPressed = s => s[0, 3]
				 }
			 );

			ButtonGoBack.Overlay.MouseLeftButtonUp +=
				delegate
				{
					if (!this.ButtonGoBack.Enabled)
						return;

					if (this.GoBack != null)
						this.GoBack();
				};

			ButtonGoBack.Container.AttachTo(Container);

			ButtonGoForward = new TiledImageButtonControl(
				"assets/ScriptCoreLib.Avalon.TiledImageButton/back-forward-large.png".ToSource(),
				27, 27,
				 new TiledImageButtonControl.StateSelector
				 {
					 AsDisabled = s => s[1, 0],
					 AsEnabled = s => s[1, 1],
					 AsHot = s => s[1, 2],
					 AsPressed = s => s[1, 3]
				 }
			);

			ButtonGoForward.Overlay.MouseLeftButtonUp +=
				delegate
				{
					if (!this.ButtonGoForward.Enabled)
						return;

					if (this.GoForward != null)
						this.GoForward();
				};

			ButtonGoForward.Enabled = false;
			ButtonGoForward.Container.MoveTo(27, 0).AttachTo(Container);
		}
	}

}

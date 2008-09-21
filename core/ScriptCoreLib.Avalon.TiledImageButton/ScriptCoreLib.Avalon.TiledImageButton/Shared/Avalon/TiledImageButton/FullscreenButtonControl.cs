using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace ScriptCoreLib.Shared.Avalon.TiledImageButton
{
	[Script]
	public class FullscreenButtonControl
	{
		public readonly Canvas Container;


		public readonly TiledImageButtonControl ButtonGoFullscreen;


		public event Action GoFullscreen;

		public const int Width = 24;
		public const int Height = 24;

		public FullscreenButtonControl()
		{
			Container = new Canvas { Width = Width, Height = Height };

			ButtonGoFullscreen = new TiledImageButtonControl(
				"assets/ScriptCoreLib.Avalon.TiledImageButton/fullscreen.png".ToSource(),
				 Width, Height,
				 new TiledImageButtonControl.StateSelector
				 {
					 AsDisabled = s => s[0, 2],
					 AsEnabled = s => s[0, 0],
					 AsHot = s => s[0, 1],
					 AsPressed = s => s[0, 4]
				 }
			);
			ButtonGoFullscreen.Container.AttachTo(Container);


			ButtonGoFullscreen.Overlay.MouseLeftButtonUp +=
				delegate
				{
					if (!this.ButtonGoFullscreen.Enabled)
						return;

					if (this.GoFullscreen != null)
						this.GoFullscreen();
				};
		}
	}
}

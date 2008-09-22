using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace ScriptCoreLib.Shared.Avalon.TiledImageButton
{
	[Script]
	public class HelpButtonControl
	{
		public readonly Canvas Container;


		public readonly TiledImageButtonControl ButtonHelp;


		public event Action Help;

		public const int Width = 24;
		public const int Height = 24;

		public HelpButtonControl()
		{
			Container = new Canvas { Width = Width, Height = Height };

			ButtonHelp = new TiledImageButtonControl(
				"assets/ScriptCoreLib.Avalon.TiledImageButton/help.png".ToSource(),
				 Width, Height,
				 new TiledImageButtonControl.StateSelector
				 {
					 AsDisabled = s => s[0, 2],
					 AsEnabled = s => s[0, 0],
					 AsHot = s => s[0, 1],
					 AsPressed = s => s[0, 3]
				 }
			);
			ButtonHelp.Container.AttachTo(Container);


			ButtonHelp.Overlay.MouseLeftButtonUp +=
				delegate
				{
					if (!this.ButtonHelp.Enabled)
						return;

					if (this.Help != null)
						this.Help();
				};
		}
	}
}

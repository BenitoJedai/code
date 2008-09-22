using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace ScriptCoreLib.Shared.Avalon.TiledImageButton
{
	[Script]
	public class ConsoleButtonControl
	{
		public readonly Canvas Container;


		public readonly TiledImageButtonControl ButtonConsole;


		public event Action Console;

		public const int Width = 24;
		public const int Height = 24;

		public ConsoleButtonControl()
		{
			Container = new Canvas { Width = Width, Height = Height };

			ButtonConsole = new TiledImageButtonControl(
				"assets/ScriptCoreLib.Avalon.TiledImageButton/console.png".ToSource(),
				 Width, Height,
				 new TiledImageButtonControl.StateSelector
				 {
					 AsDisabled = s => s[0, 2],
					 AsEnabled = s => s[0, 0],
					 AsHot = s => s[0, 1],
					 AsPressed = s => s[0, 3]
				 }
			);
			ButtonConsole.Container.AttachTo(Container);


			ButtonConsole.Overlay.MouseLeftButtonUp +=
				delegate
				{
					if (!this.ButtonConsole.Enabled)
						return;

					if (this.Console != null)
						this.Console();
				};
		}
	}
}

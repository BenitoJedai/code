using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;

namespace ScriptCoreLib.Shared.Avalon.TiledImageButton
{
	[Script]
	public class ImageTileSelector
	{
		public readonly Image Image;
		public readonly int Width;
		public readonly int Height;

		public ImageTileSelector(Image i, int Width, int Height)
		{
			this.Image = i;
			this.Width = Width;
			this.Height = Height;
		}

		public Action this[int x, int y]
		{
			get
			{
				return delegate
				{
					Image.MoveTo(-x * Width, -y * Height);
				};
			}
		}
	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace ScriptCoreLib.Shared.Avalon.Controls
{
	[Script]
	public class TiledBackgroundImage : ISupportsContainer
	{
		public Canvas Container { get; set; }


		public TiledBackgroundImage(ImageSource src, int width, int height, int x, int y)
		{


			this.Container = new Canvas
			{
				Width = width * x,
				Height = height * y
			};

			for (int i = 0; i < x; i++)
				for (int j = 0; j < y; j++)
				{
					new Image
					{
						Source = src,
						Width = width,
						Height = height
					}.MoveTo(i * width, j * height).AttachTo(this.Container);
				}
		}
	}

}

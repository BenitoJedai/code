using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace UltraApplicationWithAssets2
{
	public class MyCanvas : Canvas
	{
		public MyCanvas()
		{
			this.Width = 100;
			this.Height = 100;

			var r = new Rectangle
			{
				Width = 100,
				Height = 100,

				Fill = Brushes.Red
			};

			r.AttachTo(this);

			r.MouseLeftButtonUp +=
				delegate
				{
					r.Fill = Brushes.Green;
				};
		}
	}
}

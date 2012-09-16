using AvalonThemeHospitalScene.Avalon.Images.SpriteSheet;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace AvalonThemeHospitalScene
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
		{
			this.Width = 400;
			this.Height = 200;


			this.Background = Brushes.Gray;


			Action<Image, int, int, int, int> AddWithOffset =
				(i, x, y, xoffset, yoffset) =>
				{
					i.AttachTo(this).MoveTo(64 * x + 32 * (y % 2) - 32 + xoffset, 16 * y - 16 + yoffset);
				};

			Action<Image, int, int> Add =
				(i, x, y) =>
				{

					AddWithOffset(i, x, y, 0, 0);
				};


			Action<int, int> G = (x, y) =>
				{

					Add(new Grass(), x, y);
				};

			Action<int, int> F = (x, y) =>
				{
					Add(new FloorLight(), x, y);
				};

			Action<int, int> A = (x, y) =>
				{
					AddWithOffset(new Wall1(), x, y, 
						32,
						FloorLight.ImageDefaultHeight - Wall1.ImageDefaultHeight - 16);
					F(x, y);
				};

			Action<int, int> B = (x, y) =>
				{
					AddWithOffset(new Wall2(), x, y,
						32,
						FloorLight.ImageDefaultHeight - Wall1.ImageDefaultHeight - 16);
					F(x, y);
				};

			Action<int, int> C = (x, y) =>
			{
				AddWithOffset(new WallC(), x, y,
					32,
					FloorLight.ImageDefaultHeight - Wall1.ImageDefaultHeight - 16);
				F(x, y);
			};


			var Map = new Action<int, int>[]
			{
				G, G, G, G, G, G, G, G,
				G, G, G, G, G, G, G, G,
				C, G, G, G, G, G, G, G,
				C, G, G, G, G, G, G, G,
				F, A, G, G, G, G, G, G,
				F, B, G, G, G, G, G, G,
				F, F, C, G, G, G, G, G,
				F, F, C, G, G, G, G, G,
				F, F, F, A, G, G, G, G,
				F, F, F, B, G, G, G, G,
				F, F, F, F, C, G, G, G,
				F, F, F, F, C, G, G, G,
				F, F, F, F, F, A, G, G,
				F, F, F, F, F, B, G, G,
				F, F, F, F, F, F, C, G,
				F, F, F, F, F, F, C, G,
			};

			var MapWidth = 8;
			var MapHeight = Map.Length / MapWidth;

			for (int iy = 0; iy < MapHeight; iy++)
				for (int ix = 0; ix < MapWidth; ix++)
				{

					Map[ix + iy * MapWidth](ix, iy);
				}


			//this.SnapsToDevicePixels = true;
		}


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace InteractiveOrdering.Shared
{
	[Script]
	public class InteractiveOrderingCanvas : Canvas
	{
		public const int DefaultWidth = 640;
		public const int DefaultHeight = 480;

		//1.	kuvab võrreldavate piltide kogumid, lubab määrata mitut (näiteks 6)  pilti lineaarselt võrrelda (näiteks kole vs ilusam).
		//2.	võimaldab muuta saaty arvskaalat (1/9,1/7,1/5,1/3,1,3,5,7,9)
		//3.	kuvatakse iga erineva pildipaari kohta küsimus nende erinevusest saaty skaalal.
		//4.	luuakse saaty maatriks tabel, ja arvutatakse nende kaalud
		//5.	kuvatakse suurima veaga element ning pakutakse võimalust hinnangu muutmiseks
		//6.	kuvatakse lõplik otsus ja piltide järjestus

		public InteractiveOrderingCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			this.ClipToBounds = true;

			Colors.White.ToGradient(Colors.Blue, DefaultHeight / 4).Select(
				(c, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 4,
					}.MoveTo(0, i * 4).AttachTo(this)
			).ToArray();



		}
	}
}

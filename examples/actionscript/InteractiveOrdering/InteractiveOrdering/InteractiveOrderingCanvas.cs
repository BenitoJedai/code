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
using ScriptCoreLib.Shared.Lambda;

namespace InteractiveOrdering.Shared
{
	[Script]
	public class InteractiveOrderingCanvas : Canvas
	{
		public const int DefaultWidth = 800;
		public const int DefaultHeight = 600;

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

			var ChooseImageSet = new TextBox
			{
				Background = Brushes.Transparent,
				BorderThickness = new Thickness(0),
				Text = "Choose an image set to compare them!",
				IsReadOnly = true,
				FontFamily =  new FontFamily("Verdana"),
				FontSize = 32
			}.AttachTo(this).MoveTo(8, 8);


			var DataSet1 = new LinkImages { Text = "Cars" };
			var DataSet2 = new LinkImages { Text = "Bikes" };

			DataSet1.Loaded +=
				delegate
				{
					DataSet1.Images.Randomize().ForEach(
						(k, i) =>
						{
							k.ClickEnabled = true;
							k.SizeTo(0.5).
								AttachContainerTo(this).
								MoveContainerTo(32 + i * 4, 132 + i * 3);
						}
					);
				};

			DataSet1.Path = KnownAssets.Path.DataSet1;

			DataSet2.Loaded +=
				delegate
				{
					DataSet2.Images.Randomize().ForEach(
						(k, i) =>
						{
							k.ClickEnabled = true;
							k.SizeTo(0.5).
								AttachContainerTo(this).
								MoveContainerTo(332 + i * 4, 132 + i * 3);
						}
					);
				};

			DataSet2.Path = KnownAssets.Path.DataSet2;


			DataSet1.Click +=
				delegate
				{
					ChooseImageSet.Text = "You chose " + DataSet1.Text + "!";
				};

			DataSet2.Click +=
				delegate
				{
					ChooseImageSet.Text = "You chose " + DataSet2.Text + "!";
				};
		}
	}
}

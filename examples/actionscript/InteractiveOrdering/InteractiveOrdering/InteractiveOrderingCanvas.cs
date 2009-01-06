using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.TiledImageButton;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Avalon.TextButton;

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

		readonly TextBox Title;

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

			var History = new AeroNavigationBar();

			History.AttachContainerTo(this).MoveContainerTo(8, 8);

			this.Title = new TextBox
			{
				AcceptsReturn = true,
				Width = DefaultWidth - (16 + History.Width),
				Height = 96,
				TextWrapping = TextWrapping.Wrap,
				Background = Brushes.Transparent,
				BorderThickness = new Thickness(0),
				Text = "...",
				IsReadOnly = true,
				FontFamily = new FontFamily("Verdana"),
				FontSize = 32
			}.AttachTo(this).MoveTo(16 + History.Width, 8);

			var DataSet1 = new LinkImages { Text = "Cars" };
			var DataSet2 = new LinkImages { Text = "Bikes" };

			#region wait for all to load
			var DataSets = new[]
			{
				DataSet1,
				DataSet2
			};

			DataSets.ForEach(
				k =>
					k.Loaded +=
						delegate
						{
							if (DataSets.All(q => q.Images.Any()))
								Step1_ChooseImageSet(History.History, DataSets);

						}
			);
			#endregion

			DataSet1.Path = KnownAssets.Path.DataSet1;
			DataSet2.Path = KnownAssets.Path.DataSet2;






		}

		int Step1_ChooseImageSet_Counter;

		public void Step1_ChooseImageSet(
			AeroNavigationBar.HistoryInfo History,
			LinkImages[] Source)
		{



			History.AddFrame(
				delegate
				{
					Console.WriteLine("enter step1");

					this.Title.Text = "Choose an image set to compare them!";

					Action<LinkImages, LinkImages.LinkImage> Handler = null;

					Handler =
						(Images, Image) =>
						{
							//Console.WriteLine("Step1_ChooseImageSet handler");

							//Step2_ChooseCount(History, Images, Redo, Undo);

							Step2_ChooseCount(History, Images);
						};


					Source.Randomize().ForEach(
						(q, qi) =>
						{
							q.Randomize().ForEach(
								(k, i) =>
								{
									k.ClickEnabled = true;
									k.SizeTo(0.5).
										AttachContainerTo(this).
										MoveContainerTo(300 * qi + 82 + i * 4, 132 + i * 3);
								}
							);

							q.Click += Handler;
						}
					);

					return delegate
					{
						Console.WriteLine("exit step1");

						this.Title.Text = "...";

						Source.ForEach(
							q =>
							{
								q.Click -= Handler;
								q.ForEach(k => k.OrphanizeContainer());
							}
						);
					};
				}
			);







		}

		int Step2_ChooseCount_Counter = 0;
		public void Step2_ChooseCount(
			AeroNavigationBar.HistoryInfo History,
			LinkImages Source
			)
		{
			History.AddFrame(
				delegate
				{

					this.Title.Text = "How many images are you willing to compare from '" + Source.Text + "'";

					Action<int> Handler =
						Number =>
						{
							var Selection = Source.Select((k, i) => new { k, i }).Where(k => k.i < Number).ToArray(k => k.k);

							Step3_Compare(History);
						};

					var Options = Enumerable.Range(2, Source.Images.Count - 1).Select(
						(Number, Index) =>
						{
							var o7 = new TextButtonControl
							{
								Text = "I can only handle " + Number + "  images from '" + Source.Text + "'",
								Width = 400,
								Height = 40,
							}.AttachContainerTo(this).MoveContainerTo(100, 100 + 40 * Index);

							o7.Content.FontSize = 20;

							var o7bg = o7.Background.ToAnimatedOpacity();


							o7.Background.Fill = Brushes.White;
							o7bg.Opacity = 0;

							o7.Overlay.MouseEnter +=
								delegate { o7bg.Opacity = 1; };


							o7.Overlay.MouseLeave +=
								delegate { o7bg.Opacity = 0; };


							o7.Click +=
								delegate
								{
									Handler(Number);
								};

							return o7;
						}
					).ToArray();

					return delegate
					{
						this.Title.Text = "...";
						Options.ForEach(k => k.OrphanizeContainer());
					};
				}
			);
		}



		public void Step3_Compare(AeroNavigationBar.HistoryInfo History)
		{
			History.AddFrame(
				delegate
				{
					this.Title.Text = "Compare!";

					return delegate
					{
						this.Title.Text = "...";

					};
				}
			);
		}
	}
}

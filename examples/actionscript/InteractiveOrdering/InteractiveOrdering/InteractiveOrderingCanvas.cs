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

		public void Step2_ChooseCount(
			AeroNavigationBar.HistoryInfo History,
			LinkImages Source
			)
		{
			History.AddFrame(
				delegate
				{
					this.Title.Text = "How many images are you willing to compare from '" + Source.Text + "'";

					var DefinedValues = new[] 
					{
						new ComparisionValue { Name = "extremly better", Value = 9 },
						new ComparisionValue { Name = "much better", Value =7 },
						new ComparisionValue { Name = "better", Value =5 },
						new ComparisionValue { Name = "slightly better", Value =3 },
						new ComparisionValue { Name = "equal", Value =1 },
						new ComparisionValue { Name = "slightly worse", Value =1.0 / 3.0 },
						new ComparisionValue { Name = "worse", Value =1.0 / 5.0 },
						new ComparisionValue { Name = "much worse", Value =1.0 / 7.0 },
						new ComparisionValue { Name = "extremly worse", Value =9 },
					};

					Action<int> Handler =
						Number =>
						{
							var Selection = Source.Select((k, i) => new { k, i }).Where(k => k.i < Number).ToArray(k => k.k);

							var Matrix = Enumerable.Range(0, Selection.Length).SelectMany(
								x => Enumerable.Range(0, Selection.Length).Select(
									y =>
									{
										var n = new ComparisionInfo
										{
											X = x,
											Y = y,
										};

										if (n.X == n.Y)
										{
											n.Value = DefinedValues.Single(k => k.Value == 1);
										}

										if (n.X > n.Y)
											n.WaitingForUser = true;

										return n;
									}
								)
							).Randomize().ToArray();

							// we need to have the matrix now

							Step3_Compare(History, Selection, Matrix, DefinedValues);


						};

					#region Options
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
					#endregion

					return delegate
					{
						this.Title.Text = "...";
						Options.ForEach(k => k.OrphanizeContainer());
					};
				}
			);
		}

		[Script]
		public class ComparisionInfo
		{
			public int X;
			public int Y;

			public ComparisionValue Value;

			public bool WaitingForUser;

			public override string ToString()
			{
				return new { X, Y, Value, WaitingForUser }.ToString();
			}
		}

		[Script]
		public class ComparisionValue
		{
			public string Name;

			public double Value;

			public override string ToString()
			{
				return new { Name, Value }.ToString();
			}
		}

		public void Step3_Compare(
			AeroNavigationBar.HistoryInfo History,
			LinkImages.LinkImage[] Source,
			ComparisionInfo[] Comparision,
			ComparisionValue[] Values)
		{
			History.AddFrame(
				delegate
				{
					var Current = Comparision.Where(k => k.WaitingForUser && k.Value == null).FirstOrDefault();

					var MatrixButton = new TextButtonControl
					{
						Text = ">> Show the matrix",
						Width = 400,
						Height = 40,
					}.AttachContainerTo(this).MoveContainerTo(350, 100 + 40 * 10);

					MatrixButton.Content.FontSize = 20;

					var MatrixButton_bg = MatrixButton.Background.ToAnimatedOpacity();


					MatrixButton.Background.Fill = Brushes.White;
					MatrixButton_bg.Opacity = 0;

					MatrixButton.Overlay.MouseEnter +=
						delegate { MatrixButton_bg.Opacity = 1; };


					MatrixButton.Overlay.MouseLeave +=
						delegate { MatrixButton_bg.Opacity = 0; };


					MatrixButton.Click +=
						delegate
						{
							Step4_ShowMatrix(History, Source, Comparision, Values);
						};

					if (Current == null)
					{
						this.Title.Text = "You are done!";

						


						return delegate
						{
							this.Title.Text = "...";

							MatrixButton.OrphanizeContainer();
						};
					}
					else
					{
						var More = Comparision.Count(k => k.WaitingForUser && k.Value == null);

						this.Title.Text = "Compare images #" + (1 + Current.X) + " above and #" + (1 + Current.Y) + " below. You have " + More + " images to compare...";


						var X = Source[Current.X];
						var Y = Source[Current.Y];

						X.ClickEnabled = false;
						X.SizeTo(0.5).MoveContainerTo(100, 100).AttachContainerTo(this);

						Y.ClickEnabled = false;
						Y.SizeTo(0.5).MoveContainerTo(100, 300).AttachContainerTo(this);

						#region Options
						var Options = Values.Select(
							(Value, Index) =>
							{
								var o7 = new TextButtonControl
								{
									Text = "above is " + Value.Name + " than below",
									Width = 400,
									Height = 40,
								}.AttachContainerTo(this).MoveContainerTo(350, 100 + 40 * Index);

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
										var NewComparision = Comparision.ToArray(
											o =>
											{
												var n = new ComparisionInfo
												{
													WaitingForUser = o.WaitingForUser,
													Value = o.Value,
													X = o.X,
													Y = o.Y
												};

												if (o == Current)
												{
													n.Value = Value;
												}

												return n;
											}
										);

										Step3_Compare(History, Source, NewComparision, Values);
									};

								return o7;
							}
						).ToArray();
						#endregion

						return delegate
						{
							this.Title.Text = "...";

							X.OrphanizeContainer();
							Y.OrphanizeContainer();

							Options.ForEach(k => k.OrphanizeContainer());
							MatrixButton.OrphanizeContainer();
						};
					}



				}
			);
		}


		public void Step4_ShowMatrix(
			AeroNavigationBar.HistoryInfo History,
			LinkImages.LinkImage[] Source,
			ComparisionInfo[] Comparision,
			ComparisionValue[] Values)
		{
			History.AddFrame(
				delegate
				{
					var More = Comparision.Count(k => k.WaitingForUser && k.Value == null);

					this.Title.Text = "The Matrix. You have " + More + " images to compare...";


					return delegate
					{
						this.Title.Text = "...";
					};
				}
			);
		}
	}
}

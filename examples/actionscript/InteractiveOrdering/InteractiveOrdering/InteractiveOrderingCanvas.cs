using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.TextButton;
using ScriptCoreLib.Shared.Avalon.TiledImageButton;
using ScriptCoreLib.Shared.Lambda;
using System.Windows.Input;
using System.Collections.Generic;

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
			DefaultDataSets = new[]
			{
				DataSet1,
				DataSet2
			};

			DefaultDataSets.ForEach(
				k =>
					k.Loaded +=
						delegate
						{
							if (DefaultDataSets.All(q => q.Images.Any()))
								Step1_ChooseImageSet(History.History, DefaultDataSets);

						}
			);
			#endregion

			DataSet1.Path = KnownAssets.Path.DataSet1;
			DataSet2.Path = KnownAssets.Path.DataSet2;

			{

				var GoBackButton = new TextButtonControl
				{
					Text = "[Back]",
					Width = 50,
					Height = 30,
				}.AttachContainerTo(this).MoveContainerTo(30, 100 + 40 * 11);


				var GoBackButtonBG = GoBackButton.Background.ToAnimatedOpacity();


				GoBackButton.Background.Fill = Brushes.White;
				GoBackButtonBG.Opacity = 0;

				GoBackButton.Overlay.MouseEnter +=
					delegate { GoBackButtonBG.Opacity = 1; };


				GoBackButton.Overlay.MouseLeave +=
					delegate { GoBackButtonBG.Opacity = 0; };


				GoBackButton.Click +=
					delegate
					{
						if (History.History.GoBack.Any())
							History.History.GoBack.Pop()();
					};
			}

			{

				var GoBackButton = new TextButtonControl
				{
					Text = "[Forward]",
					Width = 66,
					Height = 30,
				}.AttachContainerTo(this).MoveContainerTo(80, 100 + 40 * 11);


				var GoBackButtonBG = GoBackButton.Background.ToAnimatedOpacity();


				GoBackButton.Background.Fill = Brushes.White;
				GoBackButtonBG.Opacity = 0;

				GoBackButton.Overlay.MouseEnter +=
					delegate { GoBackButtonBG.Opacity = 1; };


				GoBackButton.Overlay.MouseLeave +=
					delegate { GoBackButtonBG.Opacity = 0; };


				GoBackButton.Click +=
					delegate
					{
						if (History.History.GoForward.Any())
							History.History.GoForward.Pop()();
					};
			}

		}

		public LinkImages[] DefaultDataSets;


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

					var UserDefinedValues_extremly_better = new ComparisionValue { Name = "extremly better", Value = 9 };
					var UserDefinedValues_much_better = new ComparisionValue { Name = "much better", Value = 7 };
					var UserDefinedValues_better = new ComparisionValue { Name = "better", Value = 5 };
					var UserDefinedValues_slightly_better = new ComparisionValue { Name = "slightly better", Value = 3 };

					var DefinedValues = new[] 
					{
						UserDefinedValues_extremly_better,
						UserDefinedValues_much_better,
						UserDefinedValues_better,
						UserDefinedValues_slightly_better,
						new ComparisionValue { Name = "equal", Value =1 },
						new ComparisionValue { Name = "slightly worse", InverseOf = UserDefinedValues_slightly_better },
						new ComparisionValue { Name = "worse", InverseOf = UserDefinedValues_better },
						new ComparisionValue { Name = "much worse", InverseOf = UserDefinedValues_much_better },
						new ComparisionValue { Name = "extremly worse", InverseOf = UserDefinedValues_extremly_better },
					};

					Action<int> Handler =
						XNumber =>
						{
							var Selection = Source.Select((k, i) => new { k, i }).Where(k => k.i < XNumber).ToArray(k => k.k);

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

							if (XNumber == 6)
							{
								Action<int, int, double> set =
									(x, y, value) =>
									{
										Matrix.Single(k => k.X == x && k.Y == y).Value = DefinedValues.Single(k => k.GetCurrentValue() == value);
										Matrix.Single(k => k.X == y && k.Y == x).Value = DefinedValues.Single(k => k.GetCurrentValue() == 1.0 / value);
									};

								set(0, 1, 3);
								set(2, 0, 3);
								set(3, 0, 5);
								set(0, 4, 3);
								set(5, 0, 3);


								set(2, 1, 7);
								set(3, 1, 9);
								set(4, 1, 1);
								set(5, 1, 7);

								set(3, 2, 3);
								set(2, 4, 5);
								set(5, 2, 1);

								set(3, 4, 9);
								set(3, 5, 3);

								set(5, 4, 9);

							}

							// we need to have the matrix now

							Step3_Compare(History, Selection, Matrix, DefinedValues);


						};

					#region Options
					var Options = Enumerable.Range(3, Source.Images.Count - 2).Select(
						(XNumber, Index) =>
						{
							var o7 = new TextButtonControl
							{
								Text = "I can only handle " + XNumber + "  images from '" + Source.Text + "'",
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
									Handler(XNumber);
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

			public string ToVersusString()
			{
				return "#" + (X + 1) + " vs #" + (Y + 1);
			}

			public double GetCurrentValue()
			{
				if (Value == null)
					throw new Exception();

				return Value.GetCurrentValue();

			}
		}

		[Script]
		public class ComparisionValue
		{
			public string Name;

			public double Value;

			public ComparisionValue InverseOf;

			public double GetCurrentValue()
			{
				if (this.InverseOf == null)
					return this.Value;

				return 1.0 / this.InverseOf.Value;
			}

			public override string ToString()
			{
				if (InverseOf == null)
					return Value.ToString();

				return "1 / " + InverseOf.Value.ToString();
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

						var MistakeMatrixButton = new TextButtonControl
						{
							Text = ">> Show the mistake matrix",
							Width = 400,
							Height = 40,
						}.AttachContainerTo(this).MoveContainerTo(350, 100 + 40 * 9);

						MistakeMatrixButton.Content.FontSize = 20;

						var MistakeMatrixButton_bg = MistakeMatrixButton.Background.ToAnimatedOpacity();


						MistakeMatrixButton.Background.Fill = Brushes.White;
						MistakeMatrixButton_bg.Opacity = 0;

						MistakeMatrixButton.Overlay.MouseEnter +=
							delegate { MistakeMatrixButton_bg.Opacity = 1; };


						MistakeMatrixButton.Overlay.MouseLeave +=
							delegate { MistakeMatrixButton_bg.Opacity = 0; };


						MistakeMatrixButton.Click +=
							delegate
							{
								Step5_ShowMistakeMatrix(History, Source, Comparision, Values);
							};

						var RestartButton = new TextButtonControl
						{
							Text = ">> Restart",
							Width = 400,
							Height = 40,
						}.AttachContainerTo(this).MoveContainerTo(350, 100 + 40 * 11);

						RestartButton.Content.FontSize = 20;

						var RestartButton_bg = RestartButton.Background.ToAnimatedOpacity();


						RestartButton.Background.Fill = Brushes.White;
						RestartButton_bg.Opacity = 0;

						RestartButton.Overlay.MouseEnter +=
							delegate { RestartButton_bg.Opacity = 1; };


						RestartButton.Overlay.MouseLeave +=
							delegate { RestartButton_bg.Opacity = 0; };


						RestartButton.Click +=
							delegate
							{
								Step1_ChooseImageSet(History, DefaultDataSets);
							};


						// step 1 - each row gets a geomean and is seen as a new column
						var GeomeanColumn = Enumerable.Range(0, Source.Length).ToArray(
							i => Comparision.Where(k => k.Y == i).Geomean(k => k.GetCurrentValue())
						);

						// step 2 - geomean gets a sum
						var GeomeanColumnSum = GeomeanColumn.Sum();

						// step 3 - each column gets a sum
						//var SumRow = Enumerable.Range(0, Source.Length).ToArray(
						//    i => Comparision.Where(k => k.X == i).Sum(k => k.GetCurrentValue())
						//);

						// step 4 - calculate the weights for each row
						var GeomeanWeightColumn = GeomeanColumn.ToArray(k => k / GeomeanColumnSum);

						// step 5 - calculate max selfvalue
						//var MaxSelfValue = SumRow.MatrixMultiplication(GeomeanWeightColumn);


						var Sorted = GeomeanWeightColumn.
							Select((weight, i) => new { weight = 1 - weight, i, Source = Source[i] }).
							OrderBy(k => k.weight).Select((k, i) => new { k.weight, i, k.Source }).ToArray();

						var DisposeSorted = new List<Action>();


						foreach (var v in Sorted)
						{
							var k = v;

							var zoom = (0.5 + v.weight * 0.5) / 2.0;

							Console.WriteLine(new { v.i, zoom, v.weight }.ToString());

							v.Source.ClickEnabled = false;
							v.Source.SizeTo(zoom);

							var k_x = 500 + Convert.ToInt32(-30 * v.i * zoom) * v.i;
							var k_y = 100 + Convert.ToInt32(70 * v.i * zoom);

							v.Source.MoveContainerTo(k_x, k_y);
							v.Source.AttachContainerTo(this);


							var k_Text = new TextBox
							{
								Background = Brushes.Black,
								Width = 60,
								Height = 22,
								Foreground = Brushes.Yellow,
								BorderThickness = new Thickness(0),
								Text = "" + v.weight,
								IsReadOnly = true
							};


							bool MouseEnterDisabled = false;
							MouseEventHandler MouseEnter =
								delegate
								{
									// cannot remove event from MouseEnter yet
									if (MouseEnterDisabled)
										return;

									k.Source.BringContainerToFront();
									k_Text.BringToFront();
								};

							k.Source.Overlay.MouseEnter += MouseEnter;

							k_Text.MoveTo(k_x - 30, k_y - 11).AttachTo(this);

							DisposeSorted.Add(
								delegate
								{
									k.Source.OrphanizeContainer();

									k_Text.Orphanize();

									MouseEnterDisabled = true;
								}
							);
						}

						MatrixButton.BringContainerToFront();
						MistakeMatrixButton.BringContainerToFront();


						return delegate
						{
							this.Title.Text = "...";

							DisposeSorted.ToArray().ForEach(h => h());


							MatrixButton.OrphanizeContainer();
							MistakeMatrixButton.OrphanizeContainer();
							RestartButton.OrphanizeContainer();
						};


					}
					else
					{
						var More = Comparision.Count(k => k.WaitingForUser && k.Value == null);

						this.Title.Text = "Compare images #" + (1 + Current.X) + " above and #" + (1 + Current.Y) + " below. You have " + More + " image pairs to compare...";


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
									Text = "above is " + Value.Name + " than below (" + Value.ToString() + ")",
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

												if (o.Y == Current.X)
													if (o.X == Current.Y)
													{
														if (Value.InverseOf != null)
															n.Value = Value.InverseOf;
														else
														{
															var Inverse = Values.SingleOrDefault(k => k.InverseOf == Value);

															if (Inverse == null)
																n.Value = Value;
															else
																n.Value = Inverse;
														}
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

					this.Title.Text = "The Matrix. You have " + More + " image pairs to compare...";

					#region headers
					var o = Source.Select<LinkImages.LinkImage, Action>(
						(k, i) =>
						{
							k.SizeTo(0.15);
							k.AttachContainerTo(this);
							k.MoveContainerTo(60, 150 + i * 60);

							var kx = new TextButtonControl
							{
								Text = "#" + (1 + i),
								Width = 40,
								Height = 32
							};

							kx.AttachContainerTo(this);
							kx.MoveContainerTo(130, 160 + i * 60);
							kx.Background.Fill = Brushes.White;
							kx.Background.Opacity = 0.3;

							var ky = new TextButtonControl
							{
								Text = "#" + (1 + i),
								Width = 40,
								Height = 32
							};

							ky.AttachContainerTo(this);
							ky.MoveContainerTo(200 + i * 60, 100);
							ky.Background.Fill = Brushes.White;
							ky.Background.Opacity = 0.3;

							var kxr = new Rectangle
							{
								Fill = Brushes.Black,
								Width = Source.Length * 60 + 140,
								Height = 1
							};

							kxr.AttachTo(this);
							kxr.MoveTo(60, 200 + i * 60);


							var kyr = new Rectangle
							{
								Fill = Brushes.Black,
								Height = Source.Length * 60 + 60,
								Width = 1
							};

							kyr.AttachTo(this);
							kyr.MoveTo(250 + i * 60, 100);

							return delegate
							{
								k.OrphanizeContainer();
								kx.OrphanizeContainer();
								ky.OrphanizeContainer();
								kxr.Orphanize();
								kyr.Orphanize();
							};
						}
					).ToArray();
					#endregion

					#region values

					var v = Comparision.Select<ComparisionInfo, Action>(
						k =>
						{
							var kt = new TextButtonControl
							{
								Text = "",
								Width = 40,
								Height = 32
							};

							kt.AttachContainerTo(this);
							kt.MoveContainerTo(200 + k.X * 60, 160 + k.Y * 60);

							kt.Background.Fill = Brushes.White;
							kt.Background.Opacity = 0.3;

							if (k.Value == null)
							{
								if (k.WaitingForUser)
								{
									kt.Background.Fill = Brushes.Yellow;
									kt.Background.Opacity = 0.5;
								}
							}
							else
							{
								kt.Text = k.Value.ToString();

								if (k.Value.Value == 1)
								{
									kt.Background.Fill = Brushes.Cyan;
									kt.Background.Opacity = 0.5;
								}
							}

							return delegate
							{
								kt.OrphanizeContainer();
							};
						}
					).ToArray();

					#endregion

					return delegate
					{
						this.Title.Text = "...";

						o.ForEach(h => h());
						v.ForEach(h => h());
					};
				}
			);
		}

		public void Step5_ShowMistakeMatrix(
			AeroNavigationBar.HistoryInfo History,
			LinkImages.LinkImage[] Source,
			ComparisionInfo[] Comparision,
			ComparisionValue[] Values)
		{
			History.AddFrame(
				delegate
				{
					var More = Comparision.Count(k => k.WaitingForUser && k.Value == null);



					#region headers
					var o = Source.Select<LinkImages.LinkImage, Action>(
						(k, i) =>
						{
							k.SizeTo(0.15);
							k.AttachContainerTo(this);
							k.MoveContainerTo(60, 150 + i * 60);

							var kx = new TextButtonControl
							{
								Text = "#" + (1 + i),
								Width = 40,
								Height = 32
							};

							kx.AttachContainerTo(this);
							kx.MoveContainerTo(130, 160 + i * 60);
							kx.Background.Fill = Brushes.White;
							kx.Background.Opacity = 0.3;

							var ky = new TextButtonControl
							{
								Text = "#" + (1 + i),
								Width = 40,
								Height = 32
							};

							ky.AttachContainerTo(this);
							ky.MoveContainerTo(200 + i * 60, 100);
							ky.Background.Fill = Brushes.White;
							ky.Background.Opacity = 0.3;

							var kxr = new Rectangle
							{
								Fill = Brushes.Black,
								Width = Source.Length * 60 + 140,
								Height = 1
							};

							kxr.AttachTo(this);
							kxr.MoveTo(60, 200 + i * 60);


							var kyr = new Rectangle
							{
								Fill = Brushes.Black,
								Height = Source.Length * 60 + 60,
								Width = 1
							};

							kyr.AttachTo(this);
							kyr.MoveTo(250 + i * 60, 100);

							return delegate
							{
								k.OrphanizeContainer();
								kx.OrphanizeContainer();
								ky.OrphanizeContainer();
								kxr.Orphanize();
								kyr.Orphanize();
							};
						}
					).ToArray();
					#endregion

					#region values

					var Mistakes = Comparision.Where(q => q.WaitingForUser).ToArray(
						q =>
						{

							var x_cells =
								Comparision.Where(k => k.X == q.Y).OrderBy(k => k.Y).ToArray();

							var x_product =
								x_cells.Product(k => k.GetCurrentValue());

							var y_cells =
								Comparision.Where(k => k.Y == q.X).OrderBy(k => k.X).ToArray();

							var y_product =
								y_cells.Product(k => k.GetCurrentValue());


							var z = Math.Pow(q.GetCurrentValue(), Source.Length);

							return new { q, Mistake = 1.0 / Math.Pow(x_product * y_product * z, 1.0 / (Source.Length - 2)) };

							/* 

							 1/POWER(PRODUCT(R4C2:R9C2)*PRODUCT(R9C2:R9C7)*POWER(R[-46]C;veerge);1/(veerge-2))
							 1/POWER(x_product*y_product*POWER(R[-46]C;veerge);1/(veerge-2))
							 1/POWER(x_product*y_product*z;1/(veerge-2))

							 */
						}
					).OrderBy(
						ContextMistakes =>
						{
							var Mistakes_Max = ContextMistakes.Max(k => k.Mistake);
							var Mistakes_Min = ContextMistakes.Min(k => k.Mistake);

							var Mistake_Value = Mistakes_Min;

							if (Mistakes_Max * Mistakes_Min > 1.0)
								Mistake_Value = Mistakes_Max;

							return ContextMistakes.First(k => k.Mistake == Mistake_Value);
						}
					).ToArray();

					var Gradient = Colors.Red.ToGradient(Colors.Blue, Mistakes.Length).ToArray();



					Title.Text = "Biggest mistake was made at " + Mistakes.First().q.ToVersusString() + ". Click on a cell to recompare.";


					var v = Mistakes.Select(
						(k, k_index) =>
						{
							var kt = new TextButtonControl
							{
								Text = "",
								Width = 60 - 4,
								Height = 32
							};

							kt.AttachContainerTo(this);
							kt.MoveContainerTo(192 + k.q.X * 60, 160 + k.q.Y * 60);

							kt.Background.Fill = new SolidColorBrush(Gradient[k_index]);


							kt.Text = k.Mistake.ToString();

							kt.Click +=
								delegate
								{
									var NewComparision = Comparision.ToArray(
										oo =>
										{
											var n = new ComparisionInfo
											{
												WaitingForUser = oo.WaitingForUser,
												Value = oo.Value,
												X = oo.X,
												Y = oo.Y
											};

											if (oo == k.q)
											{
												n.Value = null;
											}



											return n;
										}
									);


									Step3_Compare(History, Source, NewComparision, Values);
								};

							return new Action(
								delegate
								{
									kt.OrphanizeContainer();
								}
							);
						}
					).ToArray();

					#endregion

					return delegate
					{
						this.Title.Text = "...";

						o.ForEach(h => h());
						v.ForEach(h => h());
					};
				}
			);
		}
	}
}

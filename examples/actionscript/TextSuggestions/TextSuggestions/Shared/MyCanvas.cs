using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;

namespace TextSuggestions.Shared
{
	[Script]
	public class MyCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		public MyCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			#region Gradient
			for (int i = 0; i < DefaultHeight; i += 4)
			{
				var green = Convert.ToInt32(128 * i / DefaultHeight) << 8;

				new Rectangle
				{
					Fill = ((uint)(0xff007F00 + green)).ToSolidColorBrush(),
					Width = DefaultWidth,
					Height = 4,
				}.MoveTo(0, i).AttachTo(this);
			}
			#endregion

			var t = new TextBox
			{
				Width = 200,
				Height = 22,
				Text = "we",
				BorderThickness = new Thickness(0)

			}.MoveTo(32, 32).AttachTo(this);

			var t_Unfocus = new TextBox
			{
				Width = 200,
				Height = 22,
				Text = "powered by jsc",
				BorderThickness = new Thickness(0)

			}.MoveTo(32, 4).AttachTo(this);

			var DataInput =
				@"
Action
Adventure
Animation
Biography
Comedy
Crime
Documentary
Drama
Family
Fantasy
Film-Noir
History
Horror
Independent
Music
Musical
Mystery
Romance
Sci-Fi
Short
Sport
Thriller
TV mini-series
War
Western
	".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(k => k.Trim()).WhereNot(string.IsNullOrEmpty);

			
			Func<string, IEnumerable<string>> Interpolate =
				k => Enumerable.Range(1, 2).Select(i => k + " " + i).ConcatSingle(k)
					//.Concat(DataInput.Select(u => k + " vs " + u))
					;


			var Data = DataInput.SelectMany(Interpolate);




	

			var MaxResults = 7;
			var Results = new List<TextBox>();
			var ResultsLayers = new List<Rectangle>();
			var DataSelectedLastTimeDefault = new string[0];
			var DataSelectedLastTime = DataSelectedLastTimeDefault;

			Action ClearResults =
				delegate
				{
					Results.AsEnumerable().Orphanize();
					Results.Clear();

					ResultsLayers.AsEnumerable().Orphanize();
					ResultsLayers.Clear();

					DataSelectedLastTime = DataSelectedLastTimeDefault;
				};

			var FriendlyFocusChange = false;

			Action<TextBox> ApplyFocusKeys =
				e =>
				{
					e.KeyUp +=
						(sender, ev) =>
						{

							if (ev.Key == Key.Down)
							{
								FriendlyFocusChange = true;

								if (Results.Count > 0)
									if (e == t)
										Results.First().Focus();
									else if (e == Results.Last())
										t.Focus();
									else
										Results.Next(k => k == e).Focus();

								FriendlyFocusChange = false;

							}
							else if (ev.Key == Key.Up)
							{
								FriendlyFocusChange = true;

								if (Results.Count > 0)
									if (e == t)
										Results.Last().Focus();
									else if (e == Results.First())
										t.Focus();
									else
										Results.Previous(k => k == e).Focus();

								FriendlyFocusChange = false;
							}
						};
				};

			Action CancelExitDefault = delegate { };
			Action CancelExit = CancelExitDefault;



			Action StartExit =
				delegate
				{
					if (!FriendlyFocusChange)
					{
						CancelExit = 100.AtDelay(
							delegate
							{
								CancelExit = CancelExitDefault;

								ClearResults();
							}
						).Stop;

					}
				};

			var Update = default(Action);

			var t_HasFocus = false;

			t.GotFocus +=
				delegate
				{
					CancelExit();

					t_HasFocus = true;

					t.Background = Brushes.White;
					t.Foreground = Brushes.Black;

					Console.WriteLine("t got focus");


					if (!FriendlyFocusChange)
					{
						Update();
					}


				};

			t.LostFocus +=
				(sender, ev) =>
				{

					t_HasFocus = false;

					t.Background = Brushes.Black;
					t.Foreground = Brushes.White;


					// to whome we lost focus?
					Console.WriteLine("t lost focus");

					StartExit();
				};



			Update =
				delegate
				{
					var Filter = t.Text.ToLower();
					var DataSelected = GetDataSelected(Data, MaxResults, Filter);

					if (DataSelectedLastTime.Length == DataSelected.Length)
					{
						var skip = true;

						for (int i = 0; i < DataSelected.Length; i++)
						{
							if (DataSelected[i] != DataSelectedLastTime[i])
							{
								skip = false;
								break;
							}
						}

						if (skip)
							return;
					}

					ClearResults();

					DataSelected.ForEach(
						(Entry, Index) =>
						{
							Action SelectEntry =
								delegate
								{
									Console.WriteLine("select " + Entry);
									t.Text = Entry;

									t_Unfocus.Focus();
								};

							var r_Margin = 4;
							var r_Below = new Rectangle
							{
								Fill = Brushes.White,
								Width = 200 + r_Margin * 2,
								Height = 24 + r_Margin * 2,
							}.MoveTo(64 - r_Margin, 64 + Index * 30 - r_Margin).AttachTo(this);

							ResultsLayers.Add(r_Below);

							var r = new TextBox
							{
								Text = Entry,
								Width = 200,
								Height = 24,
								IsReadOnly = true,
								BorderThickness = new Thickness(0),
								Background = Brushes.Transparent
							}.MoveTo(64, 64 + Index * 30).AttachTo(this);

							var r_Above = new Rectangle
							{
								Fill = Brushes.Red,
								Opacity = 0,
								Cursor = Cursors.Hand,
								Width = 200 + r_Margin * 2,
								Height = 24 + r_Margin * 2,
							}.MoveTo(64 - r_Margin, 64 + Index * 30 - r_Margin).AttachTo(this);

							ResultsLayers.Add(r_Above);

							var HasFocus = false;
							var HasMouse = false;

							r.GotFocus +=
								delegate
								{
									if (t_HasFocus)
										return;

									HasFocus = true;

									CancelExit();

									Console.WriteLine("r got focus - " + Entry);

									r_Below.Fill = Brushes.Blue;
									r.Foreground = Brushes.White;

									if (!FriendlyFocusChange)
									{

										SelectEntry();
									}



								};

							r.LostFocus +=
								delegate
								{
									HasFocus = false;

									if (!HasMouse)
									{
										r_Below.Fill = Brushes.Yellow;
										r.Foreground = Brushes.Black;
									}

									StartExit();
								};

							r_Above.MouseLeftButtonDown +=
								delegate
								{
									CancelExit();
									r.Focus();
								};

							r_Above.MouseEnter +=
								delegate
								{
									HasMouse = true;

									if (HasFocus)
										return;

									r_Below.Fill = Brushes.Blue;
									r.Foreground = Brushes.White;
								};

							r_Above.MouseLeave +=
								delegate
								{
									HasMouse = false;

									if (HasFocus)
										return;

									r_Below.Fill = Brushes.White;
									r.Foreground = Brushes.Black;
								};


							r.KeyUp +=
								(sender, ev) =>
								{
									if (ev.Key == Key.Left)
									{
										FriendlyFocusChange = true;
										if (r == Results.First())
											t.Focus();
										else
											Results.First().Focus();
										FriendlyFocusChange = false;

									}
									else if (ev.Key == Key.Right)
									{
										FriendlyFocusChange = true;
										if (r == Results.Last())
											t.Focus();
										else
											Results.Last().Focus();
										FriendlyFocusChange = false;

									}
									else if (ev.Key == Key.Enter)
									{
										SelectEntry();
									}
									else
									{
										FriendlyFocusChange = true;
										t.Focus();
										FriendlyFocusChange = false;

									}
								};


							ApplyFocusKeys(r);

							Results.Add(r);
						}
					);
				};



			ApplyFocusKeys(t);



			t.Background = Brushes.Black;
			t.Foreground = Brushes.White;


			t_Unfocus.GotFocus +=
						delegate
						{
							t_Unfocus.Background = Brushes.White;
							t_Unfocus.Foreground = Brushes.Black;
						};

			t_Unfocus.LostFocus +=
				delegate
				{
					t_Unfocus.Background = Brushes.Black;
					t_Unfocus.Foreground = Brushes.White;
				};

			t_Unfocus.Background = Brushes.Black;
			t_Unfocus.Foreground = Brushes.White;

			Action CancelUpdateDelayDefault = delegate { };
			Action CancelUpdateDelay = CancelUpdateDelayDefault;

			t.KeyUp +=
				(sender, ev) =>
				{
					if (ev.Key == Key.Up)
						return;

					if (ev.Key == Key.Down)
						return;

					if (ev.Key == Key.Left)
						return;

					if (ev.Key == Key.Right)
						return;

					if (ev.Key == Key.Enter)
					{
						t_Unfocus.Focus();
						return;
					}

					CancelUpdateDelayDefault();
					CancelUpdateDelay = 300.AtDelay(
						delegate
						{
							Update();
							CancelUpdateDelay = CancelUpdateDelayDefault;
						}
					).Stop;
				};


		}

		Dictionary<string, string[]> GetDataSelected_Cache = new Dictionary<string, string[]>();


		private string[] GetDataSelected(IEnumerable<string> Data, int MaxResults, string Filter)
		{
			if (GetDataSelected_Cache.ContainsKey(Filter))
				return GetDataSelected_Cache[Filter];

			var Filters = Filter.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

			var DataSelectedSource = from k in Data
									 let Subject = k.ToLower()
									 where Filter != Subject
									 let match =
										Filters.Aggregate(0,
											(seed, entry) =>
											{
												if (Subject.Contains(entry))
													return seed + 1;

												return seed;
											}
										 )
									 orderby match descending, k
									 select k;
			var DataSelectedArray = DataSelectedSource.ToArray();
			var DataSelected = DataSelectedSource.Take(DataSelectedArray.Length.Min(MaxResults)).ToArray();

			GetDataSelected_Cache[Filter] = DataSelected;

			return DataSelected;
		}
	}
}

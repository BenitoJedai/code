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
				new Rectangle
				{
					Fill = ((uint)(0xff00007F + Convert.ToInt32(128 * i / DefaultHeight))).ToSolidColorBrush(),
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

			Func<string, IEnumerable<string>> Interpolate =
				k => Enumerable.Range(1, 2).Select(i => k + " " + i).ConcatSingle(k);

			var Data =
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
	".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(k => k.Trim()).WhereNot(string.IsNullOrEmpty).SelectMany(Interpolate);




			//var Anwsers = new TextBox
			//{
			//    BorderThickness = new Thickness(0),
			//    AcceptsReturn = true,
			//    Width = 200,
			//    Height = 200
			//}.MoveTo(32, 64).AttachTo(this);

			var MaxResults = 7;
			var Results = new List<TextBox>();
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
						CancelExit = 1.AtDelay(
							delegate
							{
								CancelExit = CancelExitDefault;

								Results.AsEnumerable().Orphanize();
								Results.Clear();
							}
						).Stop;

					}
				};

			var Update = default(Action);
			
			Update =
				delegate
				{
					var Filters = t.Text.ToLower().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

					var DataSelectedSource = from k in Data
											 let Subject = k.ToLower()
											 let match = 
												Filters.Aggregate(0,
													(seed, entry)  =>
													{
														if (Subject.Contains(entry))
															return seed + 1;

														return seed;
													}
												 )
											 orderby match descending, k
											 select k;

					// we need to rebuild results


					//Anwsers.Text = DataSelectedSource.ConcatToLines();

					// TakeWhile is not yet supported...

					var DataSelectedArray = DataSelectedSource.ToArray();
					var DataSelected = DataSelectedSource.Take(DataSelectedArray.Length.Min(MaxResults));


					Results.AsEnumerable().Orphanize();
					Results.Clear();

					DataSelected.ForEach(
						(Entry, Index) =>
						{
							Action SelectEntry =
								delegate
								{
									t.Text = Entry;

									FriendlyFocusChange = true;
									t.Focus();
									FriendlyFocusChange = false;

									Update();
								};

							var r = new TextBox
							{
								Text = Entry,
								Width = 200,
								Height = 24,
								IsReadOnly = true,
								BorderThickness = new Thickness(0)
							}.MoveTo(64, 64 + Index * 30).AttachTo(this);

							r.GotFocus +=
								delegate
								{
									CancelExit();

									Console.WriteLine("r got focus - " + Entry);

									r.Background = Brushes.Blue;
									r.Foreground = Brushes.White;

									if (!FriendlyFocusChange)
									{

										SelectEntry();
									}



								};

							r.LostFocus +=
								delegate
								{
									r.Background = Brushes.White;
									r.Foreground = Brushes.Black;

									StartExit();
								};

							r.KeyUp +=
								(sender, ev) =>
								{
									if (ev.Key == Key.Left)
									{
										Results.First().Focus();
									}
									else if (ev.Key == Key.Right)
									{
										Results.Last().Focus();
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

			t.GotFocus +=
				delegate
				{
					t.Background = Brushes.White;
					t.Foreground = Brushes.Black;

					if (!FriendlyFocusChange)
					{
						Update();
					}
				};

			t.LostFocus +=
				(sender, ev) =>
				{
					t.Background = Brushes.Black;
					t.Foreground = Brushes.White;


					// to whome we lost focus?
					Console.WriteLine("t lost focus");

					StartExit();
				};

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

					Update();
				};


		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ScriptCoreLib.Shared.Avalon.TextSuggestions
{
	[Script]
	public partial class TextSuggestionsControl
	{
		string[] InternalSuggestions;
		public string[] Suggestions
		{
			get
			{
				return InternalSuggestions;
			}
			set
			{
				InternalSuggestions = value;

				this.GetDataSelected_Cache.Clear();
			}
		}

		public readonly TextBox Input;
		public readonly int MaxResults;
		public readonly UIElement Unfocus;

		public Brush InactiveResultForeground = Brushes.Black;
		public Brush InactiveResultBackground = Brushes.White;

		public Brush ActiveResultForeground = Brushes.White;
		public Brush ActiveResultBackground = Brushes.Blue;

		public int Margin = 4;

		public int Spacing = 8;

		public int Delay = 300;

		public TextSuggestionsControl(TextBox Input, int MaxResults, UIElement Unfocus, Canvas Container)
		{
			this.Input = Input;
			this.MaxResults = MaxResults;
			this.Unfocus = Unfocus;

			var Results = new List<TextBox>();
			var ResultsLayers = new List<Rectangle>();
			var DataSelectedLastTimeDefault = new Match[0];
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

			Action<UIElement> SetFriendlyFocus =
				e =>
				{
					FriendlyFocusChange = true;
					e.Focus();
					FriendlyFocusChange = false;
				};

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
									if (e == Input)
										Results.First().Focus();
									else if (e == Results.Last())
										Input.Focus();
									else
										Results.Next(k => k == e).Focus();

								FriendlyFocusChange = false;

							}
							else if (ev.Key == Key.Up)
							{
								FriendlyFocusChange = true;

								if (Results.Count > 0)
									if (e == Input)
										Results.Last().Focus();
									else if (e == Results.First())
										Input.Focus();
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

								if (this.Exit != null)
									this.Exit();
							}
						).Stop;

					}
				};

			var Update = default(Action);

			var t_HasFocus = false;

			Input.GotFocus +=
				delegate
				{
					CancelExit();

					t_HasFocus = true;

					//Input.Background = Brushes.White;
					//Input.Foreground = Brushes.Black;



					if (!FriendlyFocusChange)
					{
						Update();
					}

					if (this.Enter != null)
						this.Enter();
				};

			Input.LostFocus +=
				(sender, ev) =>
				{

					t_HasFocus = false;

					//Input.Background = Brushes.Black;
					//Input.Foreground = Brushes.White;

					StartExit();
				};



			Update =
				delegate
				{
					var Filter = Input.Text.ToLower();
					var DataSelected = GetDataSelected(Filter);

					if (DataSelectedLastTime.Length == DataSelected.Length)
					{
						var skip = true;

						for (int i = 0; i < DataSelected.Length; i++)
						{
							if (DataSelected[i].Data != DataSelectedLastTime[i].Data)
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
						(Match Entry, int Index) =>
						{
							Action SelectEntry =
								delegate
								{
									Input.Text = Entry.Data;

									Unfocus.Focus();
								};

							var r_Margin = 6;

							var x = Canvas.GetLeft(Input);
							var y = Canvas.GetTop(Input) + Input.Height + r_Margin * 2;

							var r_Below = new Rectangle
							{
								Fill = Brushes.White,
								Width = Input.Width,
								Height = Input.Height + r_Margin * 2,
							}.MoveTo(x, y + Index * 30 - r_Margin).AttachTo(Container);

							ResultsLayers.Add(r_Below);

							var r = new TextBox
							{
								Text = Entry.Data,
								Width = Input.Width - r_Margin * 2,
								Height = Input.Height,
								IsReadOnly = true,
								BorderThickness = new Thickness(0),
								Background = Brushes.Transparent,
								TextAlignment = Input.TextAlignment
							}.MoveTo(x + r_Margin, y + Index * 30).AttachTo(Container);

							var r_Above = new Rectangle
							{
								Fill = Brushes.Red,
								Opacity = 0,
								Cursor = Cursors.Hand,
								Width = Input.Width,
								Height = Input.Height + r_Margin * 2,
							}.MoveTo(x, y + Index * 30 - r_Margin).AttachTo(Container);

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

									r_Below.Fill = this.ActiveResultBackground;
									r.Foreground = this.ActiveResultForeground;

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
										r_Below.Fill = this.InactiveResultBackground;
										r.Foreground = this.InactiveResultForeground;
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

									r_Below.Fill = this.ActiveResultBackground;
									r.Foreground = this.ActiveResultForeground;
								};

							r_Above.MouseLeave +=
								delegate
								{
									HasMouse = false;

									if (HasFocus)
										return;

									r_Below.Fill = this.InactiveResultBackground;
									r.Foreground = this.InactiveResultForeground;
								};


							r.KeyUp +=
								(sender, ev) =>
								{
									if (ev.Key == Key.Left)
									{
										if (r == Results.First())
											SetFriendlyFocus(Input);
										else
											SetFriendlyFocus(Results.First());

									}
									else if (ev.Key == Key.Right)
									{
										if (r == Results.Last())
											SetFriendlyFocus(Input);
										else
											SetFriendlyFocus(Results.Last());

									}
									else if (ev.Key == Key.Enter)
									{
										SelectEntry();
									}
									else
									{
										SetFriendlyFocus(Input);

									}
								};


							ApplyFocusKeys(r);

							Results.Add(r);
						}
					);
				};



			ApplyFocusKeys(Input);

			#region Search Exit by Enter
			Action CancelUpdateDelayDefault = delegate { };
			Action CancelUpdateDelay = CancelUpdateDelayDefault;

			Input.KeyUp +=
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
						Unfocus.Focus();
						return;
					}

					CancelUpdateDelayDefault();
					CancelUpdateDelay = this.Delay.AtDelay(
						delegate
						{
							Update();
							CancelUpdateDelay = CancelUpdateDelayDefault;
						}
					).Stop;
				};
			#endregion

		}


	}
}

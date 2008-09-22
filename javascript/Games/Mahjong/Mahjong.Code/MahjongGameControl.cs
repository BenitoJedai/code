using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Mahjong.Shared;
using ScriptCoreLib;
using ScriptCoreLib.CSharp.Extensions;
using ScriptCoreLib.Shared.Avalon.Cursors;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.TextSuggestions;
using ScriptCoreLib.Shared.Avalon.TiledImageButton;
using ScriptCoreLib.Shared.Lambda;

namespace Mahjong.Code
{
	[Script]
	public partial class MahjongGameControl : Canvas
	{
		// http://cynagames.com/

		public const double DefaultScale = 1;

		public const int DefaultWidth = 600;
		public const int DefaultHeight = 500;

		public const int DefaultScaledWidth = (int)((double)DefaultWidth * DefaultScale);
		public const int DefaultScaledHeight = (int)((double)DefaultHeight * DefaultScale);

		// Set if the platform supports playing embeddded sounds
		// WPF does not (2008 09)

		public readonly FutureAction<string> PlaySoundFuture;
		//public readonly Action<string> PlaySound;

		public event Action<int, int> Sync_MouseMove;
		public event Action Sync_GoBack;
		public event Action Sync_GoForward;
		public event Action Sync_MapReloaded;


		public readonly Action<string> DiagnosticsWriteLine;

		public readonly TextBox DiagnosticsText;

		public readonly Canvas CoPlayerMouseContainer;

		public const int CommentMargin = 8;
		public readonly TextBox CommentForUnfocusing;
		public readonly TextBox Comment;
		public readonly TextSuggestionsControl CommentSuggestions;

		public readonly VisibleLayout MyLayout;

		public readonly Canvas DiagnosticsContainer = new Canvas();

		public readonly AeroNavigationBar Navbar;


		public bool AllowPlayerToChooseLayouts = true;

		public readonly FullscreenButtonControl FullscreenButton;
		public readonly HelpButtonControl HelpButton;

		public Action PlaySoundClick;

		public MahjongGameControl()
		{
			this.PlaySoundFuture = new FutureAction<string>();
			//this.PlaySound = this.PlaySoundFuture;

			this.Width = DefaultScaledWidth;
			this.Height = DefaultScaledHeight;

			// http://www.partnersinrhyme.com/pir/free_music_loops.shtml
			// http://www.soundsnap.com/taxonomy/term/62

			var Sounds = new
			{
				click = PlaySoundFuture["click"],
				birds = PlaySoundFuture["birds"],
				flag = PlaySoundFuture["flag"],
				buzzer = PlaySoundFuture["buzzer"],
				reveal = PlaySoundFuture["reveal"],
				treasure = PlaySoundFuture["treasure"],
			};

			PlaySoundClick = Sounds.click;

			Sounds.birds();

			#region background
			new Image
			{
				Source = "assets/Mahjong.Assets/china.jpg".ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = DefaultScaledWidth,
				Height = DefaultScaledHeight
			}.AttachTo(this);

			new Image
			{
				Source = "assets/Mahjong.Assets/shadow.png".ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = DefaultScaledWidth,
				Height = DefaultScaledHeight
			}.AttachTo(this);
			#endregion

			this.MyLayout = new VisibleLayout(
				new VisibleLayout.SettingsInfo
				{
					Scale = DefaultScale,
					ScaledWidth = DefaultScaledWidth,
					ScaledHeight = DefaultScaledHeight,
					CreateOverlay = true,
				}
			);



			MyLayout.Container.AttachTo(this);

			#region DiagnosticsWriteLine

			DiagnosticsContainer.AttachTo(this);

			var DiagnosticsBackground = new Rectangle
			{
				Fill = Brushes.Black,
				Width = DefaultScaledWidth,
				Height = DefaultScaledHeight / 2,
				Opacity = 0.5
			}.MoveTo(0, DefaultScaledHeight / 4).AttachTo(DiagnosticsContainer);


			this.DiagnosticsText = new TextBox
			{
				AcceptsReturn = true,
				Width = DefaultScaledWidth,
				Height = DefaultScaledHeight / 2,
				IsReadOnly = true,
				Background = Brushes.Transparent,
				Foreground = Brushes.White,
				BorderThickness = new Thickness(0)

			}.MoveTo(0, DefaultScaledHeight / 4).AttachTo(DiagnosticsContainer);

			var DiagnosticsHistory = new Queue<string>();

			this.DiagnosticsWriteLine =
				text =>
				{
					Console.WriteLine(text);

					DiagnosticsHistory.Enqueue(text);

					while (DiagnosticsHistory.Count > 12)
						DiagnosticsHistory.Dequeue();

					DiagnosticsText.Text = string.Concat(DiagnosticsHistory.Select(u => u + Environment.NewLine).ToArray());
				};
			#endregion


			this.CoPlayerMouseContainer = new Canvas
			{
				Width = DefaultScaledWidth,
				Height = DefaultScaledHeight
			}.AttachTo(this);


			this.Score = 0;


			this.MouseMove +=
				(sender, e) =>
				{
					var p = e.GetPosition(this);

					if (Sync_MouseMove != null)
						Sync_MouseMove(Convert.ToInt32(p.X), Convert.ToInt32(p.Y));
				};

			MyLayout.Overlay.AttachTo(this);

			#region Layout Selection
			// http://www.stripegenerator.com
			var Stripes = new Image
			{
				Source = "assets/Mahjong.Assets/stripes.png".ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = DefaultScaledWidth,
				Height = DefaultScaledHeight,
				Visibility = Visibility.Hidden
			}.AttachTo(this);


			this.CommentForUnfocusing = new TextBox
			{
				Width = DefaultScaledWidth - CommentMargin * 2 - 64,
				Height = 24,
				Background = Brushes.Transparent,
				Foreground = Brushes.White,
				BorderThickness = new Thickness(0),
				Text = "Mahjong Multiplayer",
				TextAlignment = TextAlignment.Left,
				IsReadOnly = true,
			}.MoveTo(CommentMargin + 64, CommentMargin).AttachTo(this);



			this.Comment = new TextBox
			{
				Width = DefaultScaledWidth - CommentMargin * 2,
				Height = 24,
				Background = Brushes.Transparent,
				Foreground = Brushes.White,
				BorderThickness = new Thickness(0),
				Text = "Loading...",
				TextAlignment = TextAlignment.Right,
				IsReadOnly = true,
			}.MoveTo(CommentMargin, CommentMargin).AttachTo(this);

			this.CommentSuggestions = new TextSuggestionsControl(Comment, 7, CommentForUnfocusing, this)
			{
				Suggestions = new string[0],
				InactiveResultBackground = Brushes.Transparent,
				Margin = 2,
				Enabled = false,
				UnfocusWhenDisabled = false
			};

			CommentSuggestions.Activate +=
				(bg, text) =>
				{
					bg.Opacity = 0.6;
				};

			CommentSuggestions.Enter +=
				delegate
				{
					Comment.Foreground = Brushes.Black;
					CommentForUnfocusing.Foreground = Brushes.Gray;
					Stripes.Visibility = Visibility.Visible;
				};

			CommentSuggestions.Exit +=
				delegate
				{
					Comment.Foreground = Brushes.White;
					CommentForUnfocusing.Foreground = Brushes.White;
					Stripes.Visibility = Visibility.Hidden;

					// we need to reload a map now...
				};

			Stripes.MouseLeftButtonUp +=
				delegate
				{
					CommentForUnfocusing.Focus();
				};
			#endregion


			this.FullscreenButton = new FullscreenButtonControl();

			FullscreenButton.Container.AttachTo(this).MoveTo(
								CommentMargin,
				DefaultScaledHeight - CommentMargin - FullscreenButtonControl.Height - CommentMargin - HelpButtonControl.Height
			);

			FullscreenButton.ButtonGoFullscreen.Enabled = false;

			this.HelpButton = new HelpButtonControl();

			HelpButton.Container.AttachTo(this).MoveTo(
				//CommentMargin + HelpButtonControl.Width + CommentMargin, 
				//DefaultScaledHeight - CommentMargin - HelpButtonControl.Height
				CommentMargin,
				DefaultScaledHeight - CommentMargin - HelpButtonControl.Height
			);

			HelpButton.ButtonHelp.Enabled = false;

			HelpButton.Help +=
				delegate
				{
					// now we should flash valid moves


					var ValidMoves =
						Enumerable.ToArray(
							from tt in this.MyLayout.Tiles
							where !tt.BlockingSiblings.Any()
							let m = this.MyLayout.Tiles.WhereNot(k => k.BlockingSiblings.Any()).Where(k => k.Tile.Value.IsPairable(tt.Tile.Value)).ToArray()
							where m.Length > 0
							select new { tt, m }
						);

					if (ValidMoves.Length == 0)
					{
						Sounds.buzzer();
					}
					else
					{
						PlaySoundClick();

						var p = ValidMoves.Random();

						MyLayout.FlashGreen(p.m.ConcatSingle(p.tt).Select(k => k.Tile.Value).ToArray());

						Score -= 9;
					}
				};

			MyLayout.LayoutDestroyed +=
				delegate
				{
					HelpButton.ButtonHelp.Enabled = false;

					Console.WriteLine("LayoutDestroyed ...");

					//Comment.IsReadOnly = true;
					//CommentSuggestions.Enabled = false;
				};

			#region MyLayout.LayoutChanging
			MyLayout.LayoutChanging +=
				delegate
				{
					Console.WriteLine("LayoutChanging ..." + MyLayout.Layout.Source);

					Comment.Text = MyLayout.Layout.Comment;

					// this occurs only after current load is complete
					MyLayout.LayoutProgress.Continue(
						delegate
						{
							Console.WriteLine("Layout loaded! " + MyLayout.Layout.Source);
						}
					);
					// we should suffle the ranks

					var SelectedTile = default(VisibleTile);

					foreach (var v in MyLayout.Tiles)
					{
						// any image

						v.Tile.Continue(
							(VisibleTile tt) =>
							{
								// ApplyDiagnostics(tt);

								ApplyDiagnosticsForPairs(MyLayout, tt);


								tt.MouseEnterWhenLayoutLoaded +=
									delegate
									{
										// we need to lock this tile
										// otherwise two network clients might take the same
										// tile yet get out of sync score
										//Console.WriteLine("lock: " + tt.Entry.RankImage.ToString());
									};

								tt.ClickWhenLayoutLoaded +=
									delegate
									{
										//tt.YellowFilter.Opacity = 0;

										Console.WriteLine(tt.Entry.RankImage.ToString());

										if (tt.Entry.BlockingSiblings.Any())
										{
											SelectedTile = null;
											Sounds.buzzer();
										}
										else
										{
											if (tt.IsPairable(SelectedTile))
											{
												TryToRemovePair(SelectedTile, tt, Sounds.buzzer);


												SelectedTile = null;
												Sounds.treasure();
											}
											else
											{
												SelectedTile = tt;
												Sounds.click();
											}
										}
									};

							}
						);
					}

				};
			#endregion

			// this occurs always
			MyLayout.LayoutChanged +=
				delegate
				{
					// done loading new layout

					Sounds.reveal();

					HelpButton.ButtonHelp.Enabled = true;

					Layouts.AllLoaded.Continue(
						delegate
						{
							if (AllowPlayerToChooseLayouts)
							{
								Comment.IsReadOnly = false;
								CommentSuggestions.Enabled = true;
							}



						}
					);

				};



			#region back/forward buttons
			this.Navbar = new AeroNavigationBar();

			Navbar.ButtonGoBack.Enabled = false;
			Navbar.ButtonGoForward.Enabled = false;

			Navbar.Container.AttachTo(this).MoveTo(CommentMargin, 2);

			MyLayout.GoBackAvailable += () => Navbar.ButtonGoBack.Enabled = true;
			MyLayout.GoBackUnavailable += () => Navbar.ButtonGoBack.Enabled = false;
			Navbar.GoBack +=
				() => Synchronized(
					delegate
					{
						if (MyLayout.GoBackHistory.Count == 0)
							return;

						MyLayout.GoBack();

						Score -= 2;

						if (this.Sync_GoBack != null)
							this.Sync_GoBack();
					}
				);

			MyLayout.GoForwardAvailable += () => Navbar.ButtonGoForward.Enabled = true;
			MyLayout.GoForwardUnavailable += () => Navbar.ButtonGoForward.Enabled = false;
			Navbar.GoForward +=
					() => Synchronized(
						delegate
						{
							if (MyLayout.GoForwardHistory.Count == 0)
								return;

							MyLayout.GoForward();

							Score -= 2;

							if (this.Sync_GoForward != null)
								this.Sync_GoForward();
						}
					);
			#endregion

			MyLayout.GoBackCompleted += this.PlaySoundClick;
			MyLayout.GoForwardCompleted += this.PlaySoundClick;

			this.Layouts = new LayoutsFuture(
				// new string [0]
				Mahjong.Shared.Assets.Default.FileNames.Where(k => k.EndsWith(".lay")).Randomize().ToArray()
			);

			Layouts.Progress +=
				(Index, Count) =>
				{
					Comment.Text = "Loading layout " + Index + " of " + Count;
				};

			WhatToDoWhenFirstLayoutIsLoaded();

			#region layouts loaded
			Layouts.AllLoaded.Continue(
				ByComment =>
				{
					if (ByComment.Count == 0)
					{
						Comment.Text = "No layouts were loaded!";
						return;
					}
					else
					{
						if (MyLayout.LayoutProgress == null)
							Comment.Text = "There are " + ByComment.Count + " layouts";
						else
						{
							if (MyLayout.LayoutProgress.CanSignal)
								Comment.Text = MyLayout.Layout.Comment;
							else
								Comment.Text = "There are " + ByComment.Count + " layouts";

							MyLayout.LayoutProgress.Continue(
								delegate
								{
									Comment.Text = MyLayout.Layout.Comment;
								}
							);
						}


					}

					CommentSuggestions.Suggestions = ByComment.Keys.ToArray();

					CommentSuggestions.Select +=
						NewLayoutComment =>
						{
							// Do not reload the same layout
							if (MyLayout.Layout != null)
								if (NewLayoutComment == MyLayout.Layout.Comment)
									return;

							if (Layouts.ByComment.ContainsKey(NewLayoutComment))
							{
								TryToChangeLayout(NewLayoutComment);
							}
							else
							{
								// that layout is not available
								Sounds.buzzer();
							}
						};

					if (AllowPlayerToChooseLayouts)
					{
						CommentForUnfocusing.Focus();
					}
				}
			);
			#endregion

			MyLayout.ReadyForNextLayout +=
				IsLocalPlayer =>
				{
					DiagnosticsWriteLine("congrats!");


					if (IsLocalPlayer)
					{
						// small layouts do not count
						if (MyLayout.Tiles.Length > 8)
						{
							Score += MyLayout.Tiles.Length;

							if (Sync_LocalPlayerCompletedLayout != null)
								Sync_LocalPlayerCompletedLayout();
						}

						Layouts.AllLoaded.Continue(
							k => SynchronizedChangeLayout(k.Random().Value)
						);
					}
				};
		}


		public event Action Sync_LocalPlayerCompletedLayout;

		public event Action<Action<Action>> Sync_SynchronizedAsync;

		void Synchronized(Action h)
		{
			SynchronizedAsync(
				Done =>
				{
					h();

					Done();
				}
			);
		}

		public void SynchronizedAsync(Action<Action> h)
		{
			// using a virtual method will allow us to provide a default
			// implementation for singleplayer mode

			if (Sync_SynchronizedAsync == null)
			{
				h(
					delegate
					{
						// release the virtual lock that we do not have
					}
				);

				return;
			}

			Sync_SynchronizedAsync(h);
		}

		public event Action<int, int> Sync_RemovePair;

		private void TryToRemovePair(VisibleTile a, VisibleTile b, Action fail)
		{
			// we need to lock the game for this action
			// create a new lock
			// aquire it

			// make tiles semitransparent to indicate we are syncing
			a.Control.Opacity = 0.5;
			b.Control.Opacity = 0.5;

			var m = this.MyLayout.Layout;

			Synchronized(
				delegate
				{
					// maybe we loaded a new map in the meanwhile?
					if (m == this.MyLayout.Layout)
					{
						// revert from pending state
						a.Control.Opacity = 1;
						b.Control.Opacity = 1;

						// does the tile still exist?
						// maybe a coplayer stole it?

						if (a.Visible)
							if (b.Visible)
							{

								MyLayout.Remove(a, b);

								if (Sync_RemovePair != null)
									Sync_RemovePair(a.Entry.index, b.Entry.index);

								if (this.ShowMatchingTiles)
									Score++;
								else
									Score += 10;

								return;
							}
					}

					DiagnosticsWriteLine("whooops");
					fail();
				}
			);
		}

		public virtual void WhatToDoWhenFirstLayoutIsLoaded()
		{
			Layouts.FirstLoaded.Continue(
				value =>
				{
					MyLayout.Layout = value;
				}
			);
		}

		public bool ShowMatchingTiles = true;

		private void ApplyDiagnosticsForPairs(VisibleLayout MyLayout, VisibleTile tt)
		{
			tt.MouseEnterLeaveWhenLayoutLoaded +=
				delegate
				{
					tt.BlackFilter.Visibility = Visibility.Hidden;

					return delegate
					{
						tt.BlackFilter.Visibility = Visibility.Visible;
					};
				};

			tt.MouseEnterLeaveWhenLayoutLoaded +=
				delegate
				{
					if (tt.Entry.BlockingSiblings.Any())
					{


						var SetOpacity = tt.Entry.BlockingSiblings.Select(
							k =>
								new Action<double>(
									Opacity => k.Tile.Value.YellowFilter.Opacity = Opacity
								)
							).Combine();

						tt.RedFilter.Opacity = 0.5;
						SetOpacity(0.5);

						return delegate
						{
							tt.RedFilter.Opacity = 0;
							SetOpacity(0);
						};
					}
					else
					{
						tt.GreenFilter.Opacity = 0.5;

						if (this.ShowMatchingTiles)
						{
							var a = MyLayout.Tiles.WhereNot(k => k.BlockingSiblings.Any()).Where(k => k.Tile.Value.IsPairable(tt)).ToArray();

							foreach (var v in a)
							{
								v.Tile.Value.YellowFilter.Opacity = 0.5;
							}

							return delegate
							{
								tt.GreenFilter.Opacity = 0;

								foreach (var v in a)
								{
									v.Tile.Value.YellowFilter.Opacity = 0;
								}
							};
						}
						else
						{
							return delegate
							{
								tt.GreenFilter.Opacity = 0;
							};
						}
					}
				};
		}

		private static void ApplyDiagnostics(VisibleTile tt)
		{
			tt.MouseEnter +=
				delegate
				{
					tt.GreenFilter.Opacity = 0.5;
					tt.BlackFilter.Visibility = Visibility.Hidden;
				};



			tt.MouseLeave +=
				delegate
				{
					tt.GreenFilter.Opacity = 0;
					tt.BlackFilter.Visibility = Visibility.Visible;
				};

			// while loading the k.Tile.Value is null for siblings

			tt.MouseEnterWhenLayoutLoaded +=
				delegate
				{
					tt.Entry.Siblings.ForEach(k => k.Tile.Value.YellowFilter.Opacity = 0.5);
					tt.Entry.BlockingSiblings.ForEach(k => k.Tile.Value.RedFilter.Opacity = 0.5);
				};

			tt.MouseLeaveWhenLayoutLoaded +=
				delegate
				{
					tt.Entry.Siblings.ForEach(k => k.Tile.Value.YellowFilter.Opacity = 0);
					tt.Entry.BlockingSiblings.ForEach(k => k.Tile.Value.RedFilter.Opacity = 0);
				};
		}


		public readonly LayoutsFuture Layouts;

	}
}

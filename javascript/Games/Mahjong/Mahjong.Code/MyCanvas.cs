using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.CSharp.Extensions;
using Mahjong.Shared;
using System.Windows.Media;
using System.Media;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows;
using ScriptCoreLib.Shared.Avalon.TextSuggestions;

namespace Mahjong.Code
{
	[Script]
	public class MyCanvas : Canvas
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
		public readonly Action<string> PlaySound;

		public MyCanvas()
		{
			this.PlaySoundFuture = new FutureAction<string>();
			this.PlaySound = this.PlaySoundFuture;

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
				reveal = PlaySoundFuture["reveal"]
			};

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

			var MyLayout = new VisibleLayout(
				new VisibleLayout.SettingsInfo
				{
					Scale = DefaultScale,
					ScaledWidth = DefaultScaledWidth,
					ScaledHeight = DefaultScaledHeight
				}
			);

			MyLayout.Container.AttachTo(this);

			// http://www.stripegenerator.com
			var Stripes = new Image
			{
				Source = "assets/Mahjong.Assets/stripes.png".ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = DefaultScaledWidth,
				Height = DefaultScaledHeight,
				Visibility = Visibility.Hidden
			}.AttachTo(this);

			var CommentMargin = 8;
			var CommentForUnfocusing = new TextBox
			{
				Width = DefaultScaledWidth - CommentMargin * 2,
				Height = 24,
				Background = Brushes.Transparent,
				Foreground = Brushes.White,
				BorderThickness = new Thickness(0),
				Text = "Mahjong Multiplayer",
				TextAlignment = TextAlignment.Left,
				IsReadOnly = true,
			}.MoveTo(CommentMargin, CommentMargin).AttachTo(this);

			var Comment = new TextBox
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

			var CommentSuggestions = new TextSuggestionsControl(Comment, 6, CommentForUnfocusing, this)
			{
				Suggestions = new string [0],
				InactiveResultBackground = Brushes.Transparent,
				Margin = 2
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


			MyLayout.LayoutChanging +=
				delegate
				{
					Console.WriteLine("LayoutChanging ...");

					// this occurs only after current load is complete
					MyLayout.LayoutProgress.Continue(
						delegate
						{
							Console.WriteLine("Layout loaded!");
						}
					);
					// we should suffle the ranks

				

					foreach (var v in MyLayout.Tiles)
					{
						// any image

						v.Tile.Continue(
							(VisibleTile tt) =>
							{
								#region interactive logic
								tt.Control.MouseEnter +=
									delegate
									{
										tt.GreenFilter.Opacity = 0.5;
										tt.BlackFilter.Visibility = Visibility.Hidden;


									};

				

								tt.Control.MouseLeave +=
									delegate
									{
										tt.GreenFilter.Opacity = 0;
										tt.BlackFilter.Visibility = Visibility.Visible;
									};

								// while loading the k.Tile.Value is null for siblings
								MyLayout.LayoutProgress.Continue(
									delegate
									{
										tt.Control.MouseEnter +=
											delegate
											{
												tt.Entry.Siblings.ForEach(k => k.Tile.Value.YellowFilter.Opacity = 0.5);
												tt.Entry.BlockingSiblings.ForEach(k => k.Tile.Value.RedFilter.Opacity = 0.5);
											};

										tt.Control.MouseLeave +=
											delegate
											{
												tt.Entry.Siblings.ForEach(k => k.Tile.Value.YellowFilter.Opacity = 0);
												tt.Entry.BlockingSiblings.ForEach(k => k.Tile.Value.RedFilter.Opacity = 0);
											};
									}
								);

								tt.Control.MouseLeftButtonUp +=
									delegate
									{
										//tt.YellowFilter.Opacity = 0;

										Console.WriteLine(tt.Entry.RankImage.ToString());

										if (tt.Entry.BlockingSiblings.Any())
											Sounds.buzzer();
										else
											Sounds.click();

									};
								#endregion

							}
						);
					}

				};

			// this occurs always
			MyLayout.LayoutChanged +=
				delegate
				{
					// done loading new layout
					Comment.Text = MyLayout.Layout.Comment;

					Sounds.reveal();
				};

			var button1 = new TextBox
			{
				Foreground = Brushes.White,
				Background = Brushes.Transparent,
				Text = "Next Layout »",
				IsReadOnly = true,
				Width = 120,
				Height = 24,
				BorderThickness = new Thickness(0),
				TextAlignment = TextAlignment.Center
			}.MoveTo(8, DefaultScaledHeight - 32).AttachTo(this);

			var button1_overlay = new Rectangle
			{
				Width = 120,
				Height = 24,
				Fill = Brushes.Blue,
				Opacity = 0.0,
				Cursor = Cursors.Hand
			}.MoveTo(8, DefaultScaledHeight - 32).AttachTo(this);

			button1_overlay.MouseEnter +=
				delegate
				{
					button1.Foreground = Brushes.Blue;
				};

			button1_overlay.MouseLeave +=
				delegate
				{
					button1.Foreground = Brushes.White;
				};

	

			Layouts = new LayoutsFuture(Assets.Default.FileNames.Where(k => k.EndsWith(".lay")).Randomize().ToArray());

			Layouts.FirstLoaded.Continue(
				value => MyLayout.Layout = value
			);

			Layouts.AllLoaded.Continue(
				ByComment =>
				{
					CommentSuggestions.Suggestions = ByComment.Keys.ToArray();
					Comment.IsReadOnly = false;
				}
			);
		}

		[Script]
		public class LayoutsFuture
		{
			public readonly Future<Layout> FirstLoaded = new Future<Layout>();
			public readonly Future<Dictionary<string, Layout>> AllLoaded = new Future<Dictionary<string, Layout>>();

			public readonly Dictionary<string, Layout> ByComment = new Dictionary<string, Layout>();

			public LayoutsFuture(string[] Files)
			{
				Files.ForEach(
					(string File, Action SignalNext) =>
					{
						File.ToStringAsset(
							DataString =>
							{
								var e = new Layout(DataString);

								ByComment[e.Comment] = e;

								if (FirstLoaded.CanSignal)
									FirstLoaded.Value = e;

								1.AtDelay(SignalNext);
							}
						);
					}
					
				)(
					delegate
					{
						AllLoaded.Value = ByComment;
					}
				);
			}
		}

		public readonly LayoutsFuture Layouts;
		
	}
}

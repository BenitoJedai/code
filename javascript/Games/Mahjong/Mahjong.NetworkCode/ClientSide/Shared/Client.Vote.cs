using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Mahjong.Code;
using Mahjong.NetworkCode.Shared;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using ScriptCoreLib.Shared.Avalon.TextButton;

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	partial class Client
	{
		[Script]
		class VoteDialog
		{
			public readonly Canvas Container;

			public readonly TextButtonControl OkButton;
			public readonly TextButtonControl CancelButton;

			public Brush OkHighlight = Brushes.Green;

			public VoteDialog(string who, string what)
			{
				Container = new Canvas
				{
					Width = MahjongGameControl.DefaultScaledWidth,
					Height = MahjongGameControl.DefaultScaledHeight
				};

				new Rectangle
				{
					Fill = Brushes.Black,
					Width = MahjongGameControl.DefaultScaledWidth,
					Height = MahjongGameControl.DefaultScaledHeight,
					Opacity = 0.8
				}.AttachTo(Container);

				new TextBox
				{
					Background = Brushes.Transparent,
					Foreground = Brushes.White,
					Width = MahjongGameControl.DefaultScaledWidth,
					Height = 72,
					TextAlignment = TextAlignment.Center,
					BorderThickness = new Thickness(0),
					Text = who + " would like to",
					FontSize = 36,
					IsReadOnly = true
				}.AttachTo(Container).MoveTo(0, 64);

				new TextBox
				{
					Background = Brushes.Transparent,
					Foreground = Brushes.White,
					Width = MahjongGameControl.DefaultScaledWidth,
					Height = 72,
					TextAlignment = TextAlignment.Center,
					BorderThickness = new Thickness(0),
					Text = what,
					FontSize = 20,
					IsReadOnly = true
				}.AttachTo(Container).MoveTo(0, 140);

				var ok = new TextButtonControl
				{
					Width = 130,
					Height = 64,
					TextAlignment = TextAlignment.Center,
					Text = "Okay!",
					Foreground = Brushes.White,
				};

				this.OkButton = ok;

				ok.Content.FontSize = 32;

				ok.MouseEnter +=
					delegate
					{
						ok.Background.Fill = OkHighlight;
						ok.Background.Opacity = 0.5;
					};

				ok.MouseLeave +=
					delegate
					{
						ok.Background.Fill = Brushes.Transparent;
					};


				ok.Container.AttachTo(Container).MoveTo(130, 200);


				var cancel = new TextButtonControl
				{
					Width = 130,
					Height = 64,
					TextAlignment = TextAlignment.Center,
					Text = "No!",
					Foreground = Brushes.White,
				};

				this.CancelButton = cancel;



				cancel.Content.FontSize = 32;

				cancel.MouseEnter +=
					delegate
					{
						cancel.Background.Fill = Brushes.Red;
						cancel.Background.Opacity = 0.5;
					};

				cancel.MouseLeave +=
					delegate
					{
						cancel.Background.Fill = Brushes.Transparent;
					};


				cancel.Container.AttachTo(Container).MoveTo(340, 200);

				HideButtons =
					delegate
					{
						ok.Container.Visibility = Visibility.Hidden;
						cancel.Container.Visibility = Visibility.Hidden;
					};

				ok.Click +=
					delegate
					{
						if (Ok != null)
							Ok();
					};

				cancel.Click +=
					delegate
					{
						if (Cancel != null)
							Cancel();
					};
			}

			public event Action Ok;
			public event Action Cancel;

			public readonly Action HideButtons;
		}

		public void InitializeVote()
		{
			#region vote

			this.Map.Sync_Vote =
				(Message, Continue, Abort) =>
				{
					if (this.Identity.Value.vote == 0)
					{
						Continue();

						return;
					}

					if (this.CoPlayers.List.Count == 0)
					{
						Continue();

						return;
					}

					this.SynchronizedAsync(
						Done =>
						{
							var dialog = new VoteDialog("You", Message);

							dialog.Container.AttachTo(this.Map);

							dialog.OkHighlight = Brushes.Yellow;
							dialog.OkButton.Text = "0%";
							dialog.CancelButton.Text = "Cancel";

							this.Messages.VoteRequest(Message);

							Action Return = delegate
							{
								this.Messages.VoteAbort();

								dialog.Container.Orphanize();
								Done();
							};

							var SingalsDisabled = false;

							Action SignalContinue = new Future(
								delegate
								{
									if (SingalsDisabled)
										return;

									SingalsDisabled = true;

									Return();
									
									Continue();
								}
							).Signal;

							Action SignalAbort = new Future(
								delegate
								{
									if (SingalsDisabled)
										return;

									SingalsDisabled = true;

									Return();
									
									Abort();
								}
							).Signal;

							dialog.Cancel += SignalAbort;
							

							var VoteResponse = default(Action<Communication.RemoteEvents.UserVoteResponseArguments>);
							var VotesInFavor = 0;
							var VotesInTotal = 0;

							VoteResponse =
								VoteResponseArgs =>
								{
									VotesInTotal++;

									if (VoteResponseArgs.value == 1)
									{
										VotesInFavor++;

										var Percentage = (100 * VotesInFavor * 2 / this.CoPlayers.List.Count).Min(100).Max(0);

										dialog.OkButton.Text = Percentage + "%";

										if (Percentage == 100)
										{
											SignalContinue();

											return;
										}
									}

									if (VotesInTotal >= this.CoPlayers.List.Count)
										SignalAbort();
								};

							this.Events.UserVoteResponse += VoteResponse;

							Return +=
								delegate
								{
									this.Events.UserVoteResponse -= VoteResponse;
								};

							15000.AtDelay(
								delegate
								{
									SignalAbort();
								}
							);
						}
					);
				};



			#endregion

			this.Events.UserVoteRequest +=
				e =>
				{
					var c = CoPlayers[e.user];
					if (!this.UserLock_ByRemote.IsAcquired)
						throw new Exception("UserVoteRequest needs a lock");

					var dialog = new VoteDialog(c.Name, e.text);

					dialog.Container.AttachTo(this.Map);

					dialog.Ok +=
						delegate
						{
							dialog.HideButtons();

							c.ToPlayer.UserVoteResponse(1);
						};

					dialog.Cancel +=
						delegate
						{
							dialog.HideButtons();

							c.ToPlayer.UserVoteResponse(0);
						};

					var Abort = default(Action<Communication.RemoteEvents.UserVoteAbortArguments>);

					Abort =
						AbortArgs =>
						{
							if (!this.UserLock_ByRemote.IsAcquired)
								throw new Exception("UserVoteAbort needs a lock");

							dialog.Container.Orphanize();


							this.Events.UserVoteAbort -= Abort;
						};

					this.Events.UserVoteAbort += Abort;
				};
		}
	}
}

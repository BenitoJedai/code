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

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	partial class Client
	{
		[Script]
		class VoteDialog 
		{
			public readonly Canvas Container;

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
			}
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

					this.SynchronizedAsync(
						Done =>
						{
							var dialog = new VoteDialog("You", Message);

							dialog.Container.AttachTo(this.Map);
							

							this.Messages.VoteRequest(Message);

							15000.AtDelay(
								delegate
								{
									this.Messages.VoteAbort();

									dialog.Container.Orphanize();
									Done();
									Abort();
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

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
using System.Windows.Media;
using System.Windows;

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	partial class Client
	{

		public MahjongGameControl Map;

		[Script]
		public class MahjongGameControlForNetwork : MahjongGameControl
		{
			public override void WhatToDoWhenFirstLayoutIsLoaded()
			{
				// nothing 
			}


		}

		public void InitializeMap()
		{
			if (this.Map != null)
				throw new NotSupportedException();

			this.Map = new MahjongGameControlForNetwork();
			this.Map.AttachTo(Element);

			#region MouseMove
			// we need to use a treshold and throttle too frequent updates
			var MouseMove = NumericOmitter.Of(
				(x, y) =>
				{
					this.Messages.MouseMove(x, y);

					//this.Map.DiagnosticsWriteLine("write: " + new { x, y }.ToString());
				}
			);

			this.Map.Sync_MouseMove += MouseMove;
			#endregion

			#region DisplayLockLocal
			var DisplayLockLocal = new TextBox
			{
				Text = "local lock",
				Width = 200,
				Height = 20,
				Background = Brushes.Transparent,
				BorderThickness = new Thickness(0),
				Foreground = Brushes.Green
			}.AttachTo(this.Map.CoPlayerMouseContainer).MoveTo(8, 64);

			this.UserLock_ByLocal.Acquired +=
				delegate
				{
					DisplayLockLocal.Foreground = Brushes.Red;
				};

			this.UserLock_ByLocal.Pending +=
				delegate
				{
					DisplayLockLocal.Foreground = Brushes.Yellow;
				};

			this.UserLock_ByLocal.Released +=
				delegate
				{
					DisplayLockLocal.Foreground = Brushes.Green;
				};
			#endregion


			#region DisplayLockRemote
			var DisplayLockRemote = new TextBox
			{
				Text = "Remote lock",
				Width = 200,
				Height = 20,
				Background = Brushes.Transparent,
				BorderThickness = new Thickness(0),
				Foreground = Brushes.Green
			}.AttachTo(this.Map.CoPlayerMouseContainer).MoveTo(8, 64 + 20);

			this.UserLock_ByRemote.Acquired +=
				delegate
				{
					DisplayLockRemote.Foreground = Brushes.Red;
				};

			this.UserLock_ByRemote.Pending +=
				delegate
				{
					DisplayLockRemote.Foreground = Brushes.Yellow;
				};

			this.UserLock_ByRemote.Released +=
				delegate
				{
					DisplayLockRemote.Foreground = Brushes.Green;
				};
			#endregion

			this.Map.Sync_RemovePair +=
				(a, b) =>
				{
					// do we really have focus?

					this.Map.DiagnosticsWriteLine("Sync_RemovePair");

					this.Messages.RemovePair(a, b);
				};

			#region Sync_Synchronized
			this.Map.Sync_Synchronized +=
				h =>
				{

					this.Map.DiagnosticsWriteLine("Sync_Synchronized");

					this.UserLock_ByLocal[this.UserLock_ByRemote](
						delegate
						{
							this.Map.DiagnosticsWriteLine("Sync_Synchronized (no local lock)");



							var a = this.CoPlayers.List.ToArray(k => k.Value);
							var n = 0;
							var lock_id = 700;

							Action DoneUsingThisLock =
								delegate
								{
									this.Map.DiagnosticsWriteLine("Sync_Synchronized ready (will release lock later)");

									h();

									// we got the lock now continue
									3000.AtDelay(
										delegate
										{
											this.Map.DiagnosticsWriteLine("Sync_Synchronized Releasing Locks");

											this.UserLock_ByLocal.Release();

											// release all locks
											foreach (var vv in a)
											{
												vv.ToPlayer.UserLockExit(lock_id);
											}
										}
									);
								};

							if (a.Length == 0)
							{
								DoneUsingThisLock();

								return;
							}

							#region enter locks
							foreach (var v in a)
							{
								var c = v;

								var LockValidate = default(Action<int>);

								LockValidate =
									id =>
									{
										if (id != lock_id)
											return;

										c.LockValidate -= LockValidate;

										n++;

										if (n != a.Length)
											return;


										DoneUsingThisLock();
									};

								c.LockValidate += LockValidate;


								c.ToPlayer.UserLockEnter(lock_id);
							}
							#endregion


						}
					);

					// ask everyone for lock
					// if anyone else is also looking for a lock, wait some and then deny it 
					// do our stuff
					// release the lock
				};
			#endregion

		}
	}
}

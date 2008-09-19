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
using System.Collections;

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

			Action<string> DiagnosticsWriteLine = text => this.Map.DiagnosticsWriteLine(text);

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
					DiagnosticsWriteLine("UserLock_ByLocal.Acquired");
					DisplayLockLocal.Foreground = Brushes.Red;
				};

			this.UserLock_ByLocal.Pending +=
				delegate
				{
					DiagnosticsWriteLine("UserLock_ByLocal.Pending");
					DisplayLockLocal.Foreground = Brushes.Yellow;
				};

			this.UserLock_ByLocal.Released +=
				delegate
				{
					DiagnosticsWriteLine("UserLock_ByLocal.Released");
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
					DiagnosticsWriteLine("UserLock_ByRemote.Acquired");
					DisplayLockRemote.Foreground = Brushes.Red;
				};

			this.UserLock_ByRemote.Pending +=
				delegate
				{
					DiagnosticsWriteLine("UserLock_ByRemote.Pending");
					DisplayLockRemote.Foreground = Brushes.Yellow;
				};

			this.UserLock_ByRemote.Released +=
				delegate
				{
					DiagnosticsWriteLine("UserLock_ByRemote.Released");
					DisplayLockRemote.Foreground = Brushes.Green;
				};
			#endregion

			this.Map.Sync_RemovePair +=
				(a, b) =>
				{
					this.Messages.RemovePair(a, b);
				};

			this.Map.Sync_GoBack += this.Messages.GoBack;
			this.Map.Sync_GoForward += this.Messages.GoForward;


			var SynchronizedCache = new Queue<Action<Action>>();

			#region Synchronized
			this.SynchronizedAsync =
				h =>
				{

					// whatif we are already trying to sync
					if (this.UserLock_ByLocal.IsAcquired)
					{
						this.Map.DiagnosticsWriteLine("Synchronized to be continued");

						SynchronizedCache.Enqueue(h);

						return;
					}
					else
					{
						this.Map.DiagnosticsWriteLine("Synchronized");
					}

					this.UserLock_ByLocal[this.UserLock_ByRemote](
						delegate
						{
							// 
							var a = this.CoPlayers.List.ToArray(k => k.Value);
							var n = 0;
							var lock_id = 700;

							var DoneUsingThisLockTimeout = default(Action);

							var AllDoneForNow = default(Action);

							AllDoneForNow =
								delegate
								{
									// we got the lock now continue
									SynchronizedLingerTime.AtDelay(
										delegate
										{
											if (SynchronizedCache.Count > 0)
											{
												DiagnosticsWriteLine("Synchronized continued");

												var p = SynchronizedCache.Dequeue();

												p(AllDoneForNow);

												return;
											}

											// release all locks
											foreach (var vv in a)
											{
												vv.ToPlayer.UserLockExit(lock_id);
											}

											this.UserLock_ByLocal.Release();

										}
									);
								};

							#region DoneUsingThisLock
							Action DoneUsingThisLock =
								delegate
								{
									DoneUsingThisLockTimeout = null;

									h(AllDoneForNow);


									
								};
							#endregion

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

							DoneUsingThisLockTimeout = 5000.AtDelay(
								delegate
								{
									// if we did got the lock then we are not deadlocked
									if (DoneUsingThisLockTimeout == null)
										return;


									// we should clear our lock and retry later
									DiagnosticsWriteLine("Deadlock");
								}
							).Stop;
						}
					);

					// ask everyone for lock
					// if anyone else is also looking for a lock, wait some and then deny it 
					// do our stuff
					// release the lock
				};
			#endregion

			this.Map.Sync_SynchronizedAsync += this.SynchronizedAsync;


			this.Map.Sync_MapReloaded +=
				delegate
				{
					this.Messages.MapReload(SerializeMap());
				};
		}
	}
}

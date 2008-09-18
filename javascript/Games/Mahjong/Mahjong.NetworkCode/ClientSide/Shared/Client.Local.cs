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

					this.UserLockEnter_ByRemote.Continue(
						delegate
						{
							this.Map.DiagnosticsWriteLine("Sync_Synchronized (no remote lock)");

							this.UserLockEnter_ByLocal.Acquire(
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

													this.UserLockEnter_ByLocal.Release();

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

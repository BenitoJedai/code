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

		public void InitializeSynchronize()
		{
			Action<string> DiagnosticsWriteLine = text => this.Map.DiagnosticsWriteLine(text);
			#region DisplayLockRemote
			var DisplayLockRemote = new TextBox
			{
				Text = "Remote lock",
				Width = 200,
				Height = 20,
				Background = Brushes.Transparent,
				BorderThickness = new Thickness(0),
				Foreground = Brushes.Green
			}.AttachTo(this.Map.DiagnosticsContainer).MoveTo(8, 64 + 20);

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

			#region Synchronized
			this.SynchronizedAsync =
				h =>
				{

					//// whatif we are already trying to sync
					//if (this.UserLock_ByLocal.IsAcquired)
					//{
					//    this.Map.DiagnosticsWriteLine("Synchronized to be continued");

					//    SynchronizedCache.Enqueue(h);

					//    return;
					//}
					//else
					//{
					//    //this.Map.DiagnosticsWriteLine("Synchronized");
					//}

					this.UserLock_Singelton.Acquire(
						delegate
						{
							this.UserLock_ByRemote.Continue(
								delegate
								{
									LagBeforeGoingForALock.AtDelay(
										delegate
										{
											#region actions under lock
											var a = this.CoPlayers.List.ToArray(k => k.Value);

											//var DoneUsingThisLockTimeout = default(Action);

											var AllDoneForNow = default(Action);

											AllDoneForNow =
												delegate
												{
													// we got the lock now continue
													SynchronizedLingerTime.AtDelay(
														delegate
														{


															// release all locks
															foreach (var vv in a)
															{
																vv.ToPlayer.UserLockExit(lock_id);
															}

															this.UserLock_ByRemote.Release();
															this.UserLock_Singelton.Release();

														}
													);
												};

											#region DoneUsingThisLock
											Action DoneUsingThisLock =
												delegate
												{
													//DoneUsingThisLockTimeout = null;

													this.UserLock_ByRemote.Acquire(
														delegate
														{
															LagBeforeUsingAcuiredLock.AtDelay(
																delegate
																{
																	h(AllDoneForNow);
																}
															);
														}
													);



												};
											#endregion

											if (a.Length == 0)
											{
												DoneUsingThisLock();

												return;
											}


											GetRemoteLocks(a, DoneUsingThisLock);


											#endregion

										}
									);
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

			this.Map.Sync_SynchronizedAsync += this.SynchronizedAsync;







			#region Lock management

			this.Events.UserLockEnter +=
				e =>
				{
					var c = CoPlayers[e.user];

					// whatif that user will send lock exit before we get that lock?
					var IsPrematureExit = false;

					var PrematureLockExit = default(Action<Communication.RemoteEvents.UserLockExitArguments>);

					PrematureLockExit =
							args =>
							{
								this.Map.DiagnosticsWriteLine("PrematureLockExit: " + c.Name);
								IsPrematureExit = true;
								this.Events.UserLockExit -= PrematureLockExit;
							};

					this.Events.UserLockExit += PrematureLockExit;

					//// are we trying to get a lock by ourselves?
					// we cannot give lock to the user while we are still loading the map
					this.UserLock_ByRemote[
						//this.UserLock_ByLocal,
						this.Map.MyLayout.LayoutProgress
					](
						delegate
						{
							if (IsPrematureExit)
							{
								this.Map.DiagnosticsWriteLine("IsPrematureExit: " + c.Name);
								this.UserLock_ByRemote.Release();

								return;
							}

							this.Events.UserLockExit -= PrematureLockExit;

							// what if this player leaves?
							c.ToPlayer.UserLockValidate(e.id);

							var LockOwnerLeft = default(Action<Communication.RemoteEvents.ServerPlayerLeftArguments>);
							var LockOwnerReleasedTheLockInstead = default(Action<Communication.RemoteEvents.UserLockExitArguments>);


							LockOwnerLeft =
								args =>
								{
									if (args.user != c.user)
										return;

									// he had the lock yet he left... we need to aviod deadlocks
									this.UserLock_ByRemote.Release();

									this.Events.ServerPlayerLeft -= LockOwnerLeft;
									this.Events.UserLockExit -= LockOwnerReleasedTheLockInstead;
								};


							LockOwnerReleasedTheLockInstead =
								args =>
								{
									if (args.user != c.user)
										return;

									this.UserLock_ByRemote.Release();

									this.Events.ServerPlayerLeft -= LockOwnerLeft;
									this.Events.UserLockExit -= LockOwnerReleasedTheLockInstead;
								};



							this.Events.ServerPlayerLeft += LockOwnerLeft;
							this.Events.UserLockExit += LockOwnerReleasedTheLockInstead;
						}
					);


				};

			this.Events.UserLockExit +=
				e =>
				{
					//if (!this.UserLock_ByRemote.IsAcquired)
					//    throw new Exception("UserLockExit needs a lock");
				};

			#region UserLockValidate
			this.Events.UserLockValidate +=
				e =>
				{
					var c = CoPlayers[e.user];

					//DiagnosticsWriteLine("UserLockValidate: " + c.Name);

					if (c.LockValidate != null)
						c.LockValidate(e.id);
				};
			#endregion
			#endregion

		}

		const int lock_id = 700;


		public void GetRemoteLocks(CoPlayer[] a, Action done)
		{
			var n = 0;

			var LocksThatAreReady = new List<CoPlayer>();

			var TakeLocksAndLookForDeadlocks = default(Action<CoPlayer[]>);

			TakeLocksAndLookForDeadlocks =
				a_ =>
				{

					foreach (var v in a_)
					{
						var c = v;

						var LockValidate = default(Action<int>);

						this.Map.DiagnosticsWriteLine("need lock from: " + c.user);


						LockValidate =
							id =>
							{
								if (id != lock_id)
									return;

								//LockValidateAborted = true;
								this.Map.DiagnosticsWriteLine("got lock from: " + c.user);

								c.LockValidate -= LockValidate;

								LocksThatAreReady.Add(c);

								n++;

								if (n != a.Length)
									return;

								LocksThatAreReady = null;
								done();
							};

						c.LockValidate += LockValidate;


						c.ToPlayer.UserLockEnter(lock_id);
					}

					DeadlockWatchTimeout.AtDelay(
						delegate
						{
							if (LocksThatAreReady == null)
								return;

							if (LocksThatAreReady.Count > 0)
							{
								// maybe we release our locks so we could release deadlocks and try again

								var LocksToBeTakenYetAgain = LocksThatAreReady.ToArray();
								LocksThatAreReady.Clear();

								foreach (var v in LocksToBeTakenYetAgain)
								{
									v.ToPlayer.UserLockExit(lock_id);
									n--;
								}

								this.Map.DiagnosticsWriteLine("continue after deadlock!");

								DeadlockWatchTimeoutResume.AtDelay(
									delegate
									{
										TakeLocksAndLookForDeadlocks(LocksToBeTakenYetAgain);
									}
								);
							}
							else
							{
								this.Map.DiagnosticsWriteLine("warning: network is probably down!");
							}
						}
					);
				};

			TakeLocksAndLookForDeadlocks(a);
		}
	}
}

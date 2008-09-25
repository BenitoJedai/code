using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mahjong.NetworkCode.Shared;
using ScriptCoreLib;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Lambda;

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	[Script]
	public partial class Client : VirtualClient
	{
		public readonly Canvas Element;


		public readonly FutureLock UserLock_Singelton = new FutureLock();
		public readonly FutureLock UserLock_ByRemote = new FutureLock();


		public Action<Action<Action>> SynchronizedAsync;
		public Action<Action> Synchronized;

		public const int SynchronizedLingerTime = 1;

		public const int LagBeforeRespondingToMapRequest = 1;
		public const int LagBeforeReadingMapResponse = 1;
		public const int LagBeforeGoingForALock = 1;
		public const int LagBeforeUsingAcuiredLock = 1;

		const int DeadlockWatchTimeout = 1000;
		const int DeadlockWatchTimeoutResume = 500;


		public Client()
		{
			Synchronized =
				h =>
				{
					SynchronizedAsync(
						Done =>
						{
							h();

							Done();
						}
					);
				};

			Element = new Canvas
			{
				Width = Mahjong.Code.MahjongGameControl.DefaultScaledWidth,
				Height = Mahjong.Code.MahjongGameControl.DefaultScaledHeight
			};

			var Initialized =
				new []
				{
					this.InitializeMapDone,
					this.InitializeEventsDone
				};

			Initialized.Continue(InitializeSynchronize);
			Initialized.Continue(InitializeVote);
		}
	}
}

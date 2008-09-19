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


		public readonly FutureLock UserLock_ByLocal = new FutureLock();
		public readonly FutureLock UserLock_ByRemote = new FutureLock();


		public Action<Action<Action>> SynchronizedAsync;
		public Action<Action> Synchronized;

		public const int SynchronizedLingerTime = 100;

		public const int LagBeforeRespondingToMapRequest = 5000;

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
		}
	}
}

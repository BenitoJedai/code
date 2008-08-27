﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using FlashTreasureHunt.Shared;
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashTreasureHunt.ActionScript
{
	partial class Client
	{
		public FlashTreasureHunt Map;


		public readonly TimeoutAction FirstMapLoader = new TimeoutAction();
		public readonly OrderedAction MapInitialized = new OrderedAction();

		public void InitializeMapOnce()
		{
			// this should be a ctor instead?

			this.Map = new FlashTreasureHunt();
			this.Map.ReadyWithLoadingCurrentLevel = this.ReadyWithLoadingCurrentLevel;
			this.Map.ReadyForNextLevel = this.ReadyForNextLevel;
					

			#region FirstMapLoader
			this.FirstMapLoader.SignalMissed +=
				delegate
				{
					this.Map.WriteLine("FirstMapLoader.SignalMissed - Noone sent us a map");
				};

			this.FirstMapLoader.SignalNotExpected +=
				delegate
				{
					this.Map.WriteLine("FirstMapLoader.SignalNotExpected - Already got it or it is too late");
				};

			this.FirstMapLoader.SignalWaisted +=
				delegate
				{
					this.Map.WriteLine("FirstMapLoader.SignalWaisted - We were really slow loading that map...");
				};

			this.FirstMapLoader.SignalWasExpected +=
				delegate
				{
					this.Map.WriteLine("FirstMapLoader.SignalWasExpected - we got map in time");
				};
			#endregion

			// pass sync events from singleplayer to server

			this.Map.Sync_TakeGold += this.Messages.TakeGold;
			this.Map.Sync_TakeAmmo += this.Messages.TakeAmmo;
			this.Map.Sync_FireWeapon += this.Messages.FireWeapon;

			this.Map.Sync_EnterEndLevelMode +=
				delegate
				{
					if (DisableEnterEndLevelMode)
						return;

					this.Map.WriteLine("send EnterEndLevelMode");

					this.Messages.EnterEndLevelMode();
				};

			this.Map.AttachTo(Element);


		}


	}
}

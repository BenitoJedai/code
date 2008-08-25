using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using FlashTreasureHunt.Shared;

namespace FlashTreasureHunt.ActionScript
{
	partial class Client
	{
		public FlashTreasureHuntForMultiPlayer Map;

		[Script]
		public class FlashTreasureHuntForMultiPlayer : FlashTreasureHunt
		{
			public FlashTreasureHuntForMultiPlayer(Action ReadyWithLoadingCurrentLevel)
			{
				this.BeforeReadyWithLoadingCurrentLevel = ReadyWithLoadingCurrentLevel;
			}

			Action BeforeReadyWithLoadingCurrentLevel;

			protected override void ReadyWithLoadingCurrentLevel()
			{
				BeforeReadyWithLoadingCurrentLevel();

				// base.ReadyWithLoadingCurrentLevel();
			}
		}

		public void InitializeMapOnce()
		{
			// this should be a ctor instead?

			this.Map = new FlashTreasureHuntForMultiPlayer(InitializeMap_When_ReadyWithLoadingCurrentLevel).AttachTo(Element);


		}

		public void InitializeMap_When_ReadyWithLoadingCurrentLevel()
		{
			this.Map.WriteLine("ready for multiplayer map");
		}

	}
}

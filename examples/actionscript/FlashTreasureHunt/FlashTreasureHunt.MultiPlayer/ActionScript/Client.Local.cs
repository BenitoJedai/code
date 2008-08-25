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
		public FlashTreasureHunt Map;

	
		public void InitializeMapOnce()
		{
			// this should be a ctor instead?

			this.Map =
				new FlashTreasureHunt
				{
					ReadyWithLoadingCurrentLevel =
						delegate
						{
							this.Map.WriteLine("ready for multiplayer map");

							this.Map.ReadyWithLoadingCurrentLevelDirect();

							// if we are the host, we will have the primary map
						}
				};

			this.Map.AttachTo(Element);


		}


	}
}

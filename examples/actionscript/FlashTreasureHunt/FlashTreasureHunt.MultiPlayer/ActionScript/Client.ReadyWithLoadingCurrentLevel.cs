using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashTreasureHunt.ActionScript
{
	partial class Client
	{
		public void ReadyWithLoadingCurrentLevel()
		{
			this.Map.WriteLine("init: ReadyWithLoadingCurrentLevel");

			MapInitialized.Signal();

			FirstMapLoader.ContinueWhenDone(
				this.Map.ReadyWithLoadingCurrentLevelDirect
			);

			// if we are the host, we will have the primary map
		}
	}

}

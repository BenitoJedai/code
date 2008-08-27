using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashTreasureHunt.ActionScript
{
	partial class Client
	{
		public TimeoutAction Sync_ReadyForNextLevel;

		public void ReadyForNextLevel()
		{
			// ...

			this.Map.WriteLine("whoever exited last level should now send a map!");


			//throw new Exception("trace - " + this.Map.WriteLineControl.text);

		}
	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashTreasureHunt.ActionScript
{
	partial class Client
	{

		public void ReadyForNextLevel(Action AlmostDone)
		{
			// ...

			this.Map.WriteLine("init: ReadyForNextLevel");
			
			// wait only five more secs
			//FirstMapLoader.ExtendedWait(5000);

			FirstMapLoader.ContinueWhenDone(
				delegate
				{
					this.Map.getpsyched.FadeOut(AlmostDone);
				}
			);
		
		}
	}

}

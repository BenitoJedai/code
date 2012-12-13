using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript.StarShips
{
	[Script]
	public class EnemyB : StarShip
	{
		public EnemyB()
		{
			this.Add(Animations.Spawn_B);

			this.ScorePoints = 2;

			this.Name = "Enemy B";

		}

		public override ScriptCoreLib.ActionScript.flash.media.Sound GetVirtualDeathSound()
		{
			return Sounds.invaderexplode;
		}
	}
}

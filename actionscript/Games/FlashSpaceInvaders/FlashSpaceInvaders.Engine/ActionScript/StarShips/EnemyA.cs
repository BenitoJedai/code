using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript.StarShips
{
	[Script]
	public class EnemyA : StarShip
	{
		public EnemyA()
		{
			this.Add(Animations.Spawn_A);

			this.ScorePoints = 4;

			this.Name = "Enemy A";
		}

		public override ScriptCoreLib.ActionScript.flash.media.Sound GetVirtualDeathSound()
		{
			return Sounds.invaderexplode;
		}
	}
}

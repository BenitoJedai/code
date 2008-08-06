using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript.StarShips
{
	[Script]
	public class EnemyC : StarShip
	{
		public EnemyC()
		{
			this.Add(Animations.Spawn_C);

			this.ScorePoints = 2;

			this.Name = "Enemy C";

		}

		public override ScriptCoreLib.ActionScript.flash.media.Sound GetVirtualDeathSound()
		{
			return Sounds.invaderexplode;
		}
	}
}

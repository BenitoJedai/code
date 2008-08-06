using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript.StarShips
{
	[Script]
	public class EnemyUFO : StarShip
	{
		public EnemyUFO()
		{
			this.Add(Animations.Spawn_UFO);

			this.ScorePoints = 10;

			this.Name = "Enemy UFO";

		}

		public override ScriptCoreLib.ActionScript.flash.media.Sound GetVirtualDeathSound()
		{
			return Sounds.mothershipexplode;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript.StarShips
{
	[Script]
	public class EnemyBigGun : StarShip
	{
		public EnemyBigGun()
		{
			this.Add(Animations.Spawn_BigGun);

			this.ScorePoints = 10;

			this.Name = "Enemy BigGun";


		}
	}
}

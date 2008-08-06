using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class BulletInfo : ParentRelation<SpriteWithMovement, PlayerShip>
	{
		public double Damage = 0.10;
		public double Multiplier = 1;

		public double TotalDamage
		{
			get
			{
				return Damage * Multiplier;
			}
		}

		public BulletInfo(ParentRelation<SpriteWithMovement, PlayerShip> e)
		{

			this.Element = e.Element;
			this.Parent = e.Parent;

			
		}

	}
}

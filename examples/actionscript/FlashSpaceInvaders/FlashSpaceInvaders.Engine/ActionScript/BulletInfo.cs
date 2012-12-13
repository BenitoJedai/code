﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class BulletInfo : ParentRelation<SpriteWithMovement, StarShip>
	{
		public bool Silent;

		public double Damage = 0.05;
		public double Multiplier = 1;

		public double TotalDamage
		{
			get
			{
				return Damage * Multiplier;
			}
		}

		public BulletInfo(ParentRelation<SpriteWithMovement, StarShip> e)
		{

			this.Element = e.Element;
			this.Parent = e.Parent;

			
		}

	}
}

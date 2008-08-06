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
		public BulletInfo()
			: this(null)
		{

		}

		public BulletInfo(ParentRelation<SpriteWithMovement, PlayerShip> e)
		{
			if (e != null)
			{
				this.Element = e.Element;
				this.Parent = e.Parent;
			}
		}

	}
}

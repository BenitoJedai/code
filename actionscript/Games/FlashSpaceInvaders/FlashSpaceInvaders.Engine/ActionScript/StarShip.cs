using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class StarShip : SpriteWithMovement, IFragileEntity
	{

		#region ITakeDamage Members

		public void TakeDamage(double damage)
		{
			this.alpha -= damage * 4;
		}

		#endregion

		#region IWithLocation Members

		public ScriptCoreLib.ActionScript.flash.geom.Point Location
		{
			get { return this.ToPoint(); }
		}

		#endregion

		#region IHitPoints Members

		public double HitPoints
		{
			get { return this.alpha; }
		}

		#endregion

		#region IHitRange Members

		public double HitRange
		{
			get { return 64; }
		}

		#endregion

		#region IName Members

		public string Name { get; set; }

		#endregion
	}
}

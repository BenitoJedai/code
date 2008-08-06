using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using FlashSpaceInvaders.ActionScript.FragileEntities;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class StarShip : SpriteWithMovement, IFragileEntity
	{
		public static implicit operator StarShip(Func<double, double, Sprite> ctor)
		{
			return new StarShip { ctor };
		}

		public void Add(Func<double, double, Sprite> ctor)
		{
			ctor(0, 0).AttachTo(this);
		}

		public readonly BooleanProperty IsAlive = true;


		#region ITakeDamage Members

		public void TakeDamage(double damage)
		{
			this.alpha -= damage * 4;

			if (this.alpha < 0.5)
				this.alpha = 0;

			IsAlive.Value = this.alpha > 0;
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
			get { return 32; }
		}

		#endregion

		#region IName Members

		public string Name { get; set; }

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlashSpaceInvaders.ActionScript.Extensions;
using FlashSpaceInvaders.ActionScript.FragileEntities;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Shared.Lambda;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class StarShip : SpriteWithMovement, IFragileEntity
	{
		public int ScorePoints { get; set; }

		public StarShip()
		{
			this.ScorePoints = 10;

			// this should be global to network session
		

		}

		public static implicit operator StarShip(Func<double, double, Sprite> ctor)
		{
			return new StarShip { ctor };
		}

		public void Add(Func<double, double, Sprite> ctor)
		{
			ctor(0, 0).AttachTo(this);
		}

		public readonly BooleanProperty IsAlive = true;

		public readonly BooleanProperty GodMode = false;

		#region ITakeDamage Members

		public void TakeDamage(double damage)
		{

			if (!GodMode)
			{
				this.alpha -= damage * 4;

				if (this.alpha < 0.5)
					this.alpha = 0;
			}

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

		#region IDeathSound Members

		public ScriptCoreLib.ActionScript.flash.media.Sound GetDeathSound()
		{
			return GetVirtualDeathSound();
		}

		#endregion

		public virtual ScriptCoreLib.ActionScript.flash.media.Sound GetVirtualDeathSound()
		{
			return Sounds.baseexplode;
		}

		public RoutedActionInfo<StarShip, int, Point, Point, double, Action<BulletInfo>> FireBullet;

		public BulletInfo FireBulletChained(int Multiplier, Point From, Point To, double Limit)
		{
			// fixme should return a Future instead
			var result = new Property<BulletInfo>();

			if (this.FireBullet != null)
				this.FireBullet.Chained(this, Multiplier, From, To, Limit, result);

			return result.Value;

			
		}

	}
}

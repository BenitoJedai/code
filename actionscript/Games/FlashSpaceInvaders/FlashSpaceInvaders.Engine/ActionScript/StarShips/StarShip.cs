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

		public BulletInfo FireBullet(int Multiplier, Point From, Point To, double Limit)
		{
			var bullet = new SpriteWithMovement();

			Multiplier = Multiplier.Max(1);

			for (int i = 1; i <= Multiplier; i++)
			{
				bullet.graphics.beginFill(Colors.Green);
				bullet.graphics.drawRect((i - Multiplier) * 2, -8, 1, 16);
			}


			bullet.StepMultiplier = 0.3;
			bullet.MaxStep = 24;

			if (From.y < To.y)
			{
				bullet.TeleportTo(From.x, From.y);
				bullet.TweenMoveTo(To.x + 0.00001, To.y);

				bullet.PositionChanged +=
					delegate
					{
						if (bullet.y > Limit)
							bullet.Orphanize();
					};
			}
			else
			{
				bullet.TeleportTo(From.x, From.y);
				bullet.TweenMoveTo(To.x + 0.00001, To.y);


				bullet.PositionChanged +=
					delegate
					{
						if (bullet.y < Limit)
							bullet.Orphanize();
					};
			}

			return new BulletInfo(bullet.WithParent(this)) { Multiplier = Multiplier };
		}

	}
}

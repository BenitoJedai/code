using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared.Lambda;

using FlashSpaceInvaders.ActionScript.Extensions;
using FlashSpaceInvaders.ActionScript.FragileEntities;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class PlayerShip
	{
		public readonly StarShip GoodEgo;
		public readonly StarShip EvilEgo;

		public readonly BooleanProperty EvilMode;

		public int GoodEgoY;
		public int EvilEgoY;

		public readonly int DefaultWidth;
		public readonly int DefaultHeight;

		public PlayerShip AddTo(List<IFragileEntity> c)
		{
			GoodEgo.AddTo(c);
			EvilEgo.AddTo(c);

			return this;
		}

		public PlayerShip(int DefaultWidth, int DefaultHeight)
		{
			this.DefaultWidth = DefaultWidth;
			this.DefaultHeight = DefaultHeight;

			this.GoodEgo = new StarShip { Animations.Spawn_BigGun(0, 0) };

			this.GoodEgoY = DefaultHeight - 20;
			this.EvilEgoY = 60;

			GoodEgo.y = GoodEgoY;

			this.EvilEgo = new StarShip { Animations.Spawn_UFO(0, 0) };

			EvilEgo.y = EvilEgoY;

			this.EvilMode = new BooleanProperty();












			this.GoodEgo.PositionChanged +=
				delegate
				{
					var EvilModePending = true;

					if (this.GoodEgo.x < DefaultWidth)
						if (this.GoodEgo.x > 0)
						{
							EvilModePending = false;
						}

					if (this.GoodEgo.MoveToTarget.Value.x > DefaultWidth / 2)
						EvilEgo.TeleportTo(this.GoodEgo.x - DefaultWidth, EvilEgoY);
					else
						EvilEgo.TeleportTo(this.GoodEgo.x + DefaultWidth, EvilEgoY);


					EvilMode.Value = EvilModePending;

					if (this.GoodEgo.x > DefaultWidth * 0.5)
					{
						this.GoodEgo.MoveToTarget.Value.x -= DefaultWidth * 2;
						this.GoodEgo.x -= DefaultWidth * 2;
					}

					if (this.GoodEgo.x < -DefaultWidth * 0.5)
					{
						this.GoodEgo.MoveToTarget.Value.x += DefaultWidth * 2;
						this.GoodEgo.x += DefaultWidth * 2;
					}
				};


			GoodEgo.MoveTo(DefaultWidth / 2, GoodEgoY);


			GoodEgo.MaxStep = 12;
			EvilEgo.MaxStep = 12;

		}

		public StarShip ActiveEgo
		{
			get
			{
				if (EvilMode)
					return EvilEgo;

				return GoodEgo;
			}
		}

		public BulletInfo FireBullet()
		{
			return FireBullet(1);
		}

		public BulletInfo FireBullet(int Multiplier)
		{
			var Ego = this;
			var bullet = new SpriteWithMovement();

			Multiplier = Multiplier.Max(1);

			for (int i = 1; i <= Multiplier; i++)
			{
				bullet.graphics.beginFill(Colors.Green);
				bullet.graphics.drawRect((i - Multiplier) * 2, -8, 1, 16);
			}


			bullet.StepMultiplier = 0.3;
			bullet.MaxStep = 24;

			if (Ego.EvilMode)
			{
				bullet.TeleportTo(Ego.EvilEgo.x, Ego.EvilEgo.y);
				bullet.MoveTo(Ego.EvilEgo.x + 0.00001, DefaultHeight);

				bullet.PositionChanged +=
					delegate
					{
						if (bullet.y > Ego.GoodEgoY)
							bullet.Orphanize();
					};
			}
			else
			{
				bullet.TeleportTo(Ego.GoodEgo.x, Ego.GoodEgo.y);
				bullet.MoveTo(Ego.GoodEgo.x + 0.00001, 0);


				bullet.PositionChanged +=
					delegate
					{
						if (bullet.y < Ego.EvilEgoY)
							bullet.Orphanize();
					};
			}

			return new BulletInfo(bullet.WithParent(this)) { Multiplier = Multiplier };
		}


		public double Wrapper(double x)
		{
			var Ego = this;

			if (Ego.EvilMode)
			{
				if (Ego.GoodEgo.x < DefaultWidth / 2)
					return x - DefaultWidth;
				else
					return x + DefaultWidth;
			}
			else
				return x;
		}

		string _Name = "";

		public string Name
		{
			get
			{
				return _Name;
			}

			set
			{
				_Name = value;

				GoodEgo.Name = _Name;
				EvilEgo.Name = "Evil " + _Name;
			}
		}
	}
}

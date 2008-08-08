using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared.Lambda;

using FlashSpaceInvaders.ActionScript.Extensions;
using FlashSpaceInvaders.ActionScript.FragileEntities;
using ScriptCoreLib.ActionScript.flash.geom;

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

		public readonly BooleanProperty GodMode = false;

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


			this.GodMode.LinkTo(this.GoodEgo.GodMode);
			this.GodMode.LinkTo(this.EvilEgo.GodMode);









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


			GoodEgo.TweenMoveTo(DefaultWidth / 2, GoodEgoY);


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

		public readonly Int32Property CurrentBulletMultiplier = 1;

		public BulletInfo FireBullet()
		{
			if (EvilMode)
				return ActiveEgo.FireBulletChained(CurrentBulletMultiplier, new Point(EvilEgo.x, EvilEgo.y), new Point(EvilEgo.x, DefaultHeight), GoodEgoY);
			else
				return ActiveEgo.FireBulletChained(CurrentBulletMultiplier, new Point(GoodEgo.x, GoodEgo.y), new Point(GoodEgo.x, 0), EvilEgoY);

		}

		

		public double Wrapper(double x, double y)
		{
			var Ego = this;

			var m = Ego.EvilMode;

			if (m && y > (DefaultHeight * 3 / 4))
				m = false;

			if (!m && y < (DefaultHeight * 1 / 4))
				m = true;


			if (m)
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

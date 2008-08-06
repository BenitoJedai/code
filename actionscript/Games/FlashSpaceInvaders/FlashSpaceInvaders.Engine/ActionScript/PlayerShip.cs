using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;

using FlashSpaceInvaders.ActionScript.Extensions;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class PlayerShip
	{
		public readonly SpriteWithMovement GoodEgo;
		public readonly SpriteWithMovement EvilEgo;

		public readonly BooleanProperty EvilMode;

		public int GoodEgoY;
		public int EvilEgoY;

		public readonly int DefaultWidth;
		public readonly int DefaultHeight;

		public PlayerShip(int DefaultWidth, int DefaultHeight)
		{
			this.DefaultWidth = DefaultWidth;
			this.DefaultHeight = DefaultHeight;

			this.GoodEgo = new SpriteWithMovement { Animations.Spawn_BigGun(0, 0) };

			this.GoodEgoY = DefaultHeight - 20;
			this.EvilEgoY = 60;

			GoodEgo.y = GoodEgoY;

			this.EvilEgo = new SpriteWithMovement { Animations.Spawn_UFO(0, 0) };

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

					if (this.GoodEgo.x > DefaultWidth * 2)
					{
						this.GoodEgo.MoveToTarget.Value.x -= DefaultWidth * 2;
						this.GoodEgo.x -= DefaultWidth * 2;
					}

					if (this.GoodEgo.x < -DefaultWidth)
					{
						this.GoodEgo.MoveToTarget.Value.x += DefaultWidth * 2;
						this.GoodEgo.x += DefaultWidth * 2;
					}
				};


			GoodEgo.MoveTo(DefaultWidth / 2, GoodEgoY);


			GoodEgo.MaxStep = 12;
			EvilEgo.MaxStep = 12;

		}

		public BulletInfo FireBullet()
		{
			var Ego = this;
			var bullet = new SpriteWithMovement();

			bullet.graphics.beginFill(Colors.Green);
			bullet.graphics.drawRect(0, -8, 1, 16);
			bullet.StepMultiplier = 0.3;

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

			return new BulletInfo( bullet.WithParent(this) );
		}


		public void SmartMoveTo(int x)
		{
			var Ego = this;

			if (Ego.EvilMode)
			{
				Ego.GoodEgo.MoveTo(x + DefaultWidth, Ego.GoodEgoY);
			}
			else
				Ego.GoodEgo.MoveTo(x, Ego.GoodEgoY);
		}
	}
}

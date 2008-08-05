using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

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

		public PlayerShip(int DefaultWidth, int DefaultHeight)
		{
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
	}
}

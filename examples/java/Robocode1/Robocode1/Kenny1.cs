using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using robocode;
using java.awt;
using ScriptCoreLib;

namespace Robocode1
{
	[Script]
	public class Kenny1 : Robot
	{
		public override void run()
		{
			this.setColors(
				new Color(0xFF0000),
				new Color(0x00FF00),
				new Color(0xFF0000)
			);

			while (true)
			{
				this.ahead(5);
				this.turnRight(15);
			}
		}

		public override void onScannedRobot(ScannedRobotEvent @event)
		{
			this.fire(0.5);
		}

		public override void onHitByBullet(HitByBulletEvent @event)
		{
			turnLeft(90 - @event.getBearing());
		}
	}
}

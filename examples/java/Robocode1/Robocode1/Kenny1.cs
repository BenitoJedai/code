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
		// notice:
		// Stop robocode simulation before build and then do a restart in robocode
		// You may have to remove your robot.database
		// You may have to import the jar before you can run it
		// Reflection seems to be disabled, so we cannot use delegates?
		// Should jsc be able to emit delegates as interfaces instead?

		public override void run()
		{

			this.setColors(
				new Color(0xFF0000),
				new Color(0x00FF00),
				new Color(0xFF0000)
			);


			this.setColors(
					new Color(0),
					new Color(0xffffff),
					new Color(0)
			);


			var h = this.getHeading();
			this.turnLeft(h);

			while (true)
			{
				StrategyApply();

				if (GotTarget)
					this.fire(0.5);
			}
		}

		private void StrategyApply()
		{
			if (GotTarget)
				return;

			this.ahead(10);

			if (GotTarget)
				return;

			this.turnRight(90);

			if (GotTarget)
				return;

			this.ahead(10);


			if (GotTarget)
				return;

			this.turnRight(90);

			if (GotTarget)
				return;


			this.back(10);


			if (GotTarget)
				return;

			this.turnLeft(90);

			if (GotTarget)
				return;

			this.back(10);


			if (GotTarget)
				return;

			this.turnLeft(90);
		}

		bool GotTarget;

		public override void onScannedRobot(ScannedRobotEvent @event)
		{
			GotTarget = true;

			this.fire(0.5);
		}

		public override void onHitByBullet(HitByBulletEvent @event)
		{
			GotTarget = false;

			turnLeft(90 - @event.getBearing());
		}
	}
}

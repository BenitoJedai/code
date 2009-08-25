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
	public class Kenny3 : Robot
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
				new Color(0xFFFF00),
				new Color(0xFF00FF)
			);



			while (true)
			{
				this.turnGunLeft(5);
			}
		}



		public override void onScannedRobot(ScannedRobotEvent @event)
		{
			this.fire(0.5);
			this.ahead(5);
			this.fire(0.5);
			this.fire(0.5);
		}

		public override void onHitByBullet(HitByBulletEvent @event)
		{
			// maybe we should be going right instead?
			var _turnLeft = 90 - @event.getBearing();
			var _turnRight = -(90 + @event.getBearing());

			Console.WriteLine("onHitByBullet.bearing: " + @event.getBearing());
			Console.WriteLine("onHitByBullet._turnLeft: " + _turnLeft);
			Console.WriteLine("onHitByBullet._turnRight: " + _turnRight);

			if (_turnLeft < _turnRight)
				this.turnLeft(_turnLeft);
			else
				this.turnLeft(_turnRight);

			this.ahead(40);

			if (_turnLeft < _turnRight)
				this.turnRight(15);
			else
				this.turnLeft(15);
			this.ahead(40);
		}

		public override void onHitWall(HitWallEvent @event)
		{
			this.turnRight(90 - @event.getBearing());
			this.back(10);
			this.turnLeft(180);
			this.ahead(40);
		}
	}
}

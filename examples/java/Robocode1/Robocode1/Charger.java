package kahro.robots;

import java.awt.Color;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

import robocode.DeathEvent;
import robocode.HitRobotEvent;
import robocode.MessageEvent;
import robocode.RobotDeathEvent;
import robocode.Rules;
import robocode.ScannedRobotEvent;
import robocode.TeamRobot;
import robocode.util.Utils;

public class Charger extends TeamRobot {

	private static final double CLOSE_RANGE = 150;
	private static final double CIRCLE_RADIUS = 120;

	private String name;
	private double distance;
	private Map<String, String> targets = new HashMap<String, String>();

	@Override
	public void run() {
		setBodyColor(new Color(0, 50, 0));
		setGunColor(new Color(0, 50, 0));
		setBulletColor(Color.RED);
		setRadarColor(Color.RED);
		setScanColor(new Color(0, 50, 0));

		setAdjustGunForRobotTurn(true);
		setAdjustRadarForGunTurn(true);

		while (true) {
			turnRadarRightRadians(Double.POSITIVE_INFINITY);
		}
	}

	@Override
	public void onScannedRobot(ScannedRobotEvent e) {
		// kui näen mitut vaenlast võtan alati lähima
		if (isValid(e.getName()) && (name == null || name.equals(e.getName()) || e.getDistance() < distance)) {
			try {
				broadcastMessage(e.getName());
			} catch (IOException ex) {
			}
			name = e.getName();
			distance = e.getDistance();

			// radar jälgib teist robotit
			double radarTurn = getHeadingRadians() + e.getBearingRadians() - getRadarHeadingRadians();
			setTurnRadarRightRadians(2 * Utils.normalRelativeAngle(radarTurn));

			// radar sõidab teise roboti ümber ringe
			BearingForCircle bfc = getValidBearingForCircle(e.getBearingRadians(), e.getDistance());
			setAhead(bfc.forward ? Double.POSITIVE_INFINITY : Double.NEGATIVE_INFINITY);
			setTurnRightRadians(bfc.turn);

			// keerab kahurit ja annab tuld
			setTurnGunRightRadians(getGunTurn(1, e.getBearingRadians(), e.getHeadingRadians(), e.getVelocity()));
			if (e.getDistance() < 1.5 * CLOSE_RANGE) {
				double power = 225 / e.getDistance();
				setTurnGunRightRadians(getGunTurn(power, e.getBearingRadians(), e.getHeadingRadians(), e.getVelocity()));
				if (getGunTurnRemaining() < Math.atan(10 / e.getDistance())) {
					setFire(power);
				}
			}
		}
	}

	@Override
	public void onRobotDeath(RobotDeathEvent e) {
		if (e.getName().equals(name)) {
			name = null;
			stop();
		}
		if (isTeammate(e.getName())) {
			targets.remove(e.getName());
		}
	}

	@Override
	public void onHitRobot(HitRobotEvent e) {
		if (!isTeammate(e.getName())) {
			try {
				broadcastMessage(e.getName());
			} catch (IOException ex) {
			}
			name = e.getName();
			distance = 0;
		}
	}

	@Override
	public void onDeath(DeathEvent e) {
		try {
			broadcastMessage(null);
		} catch (IOException ex) {
		}
	}

	@Override
	public void onMessageReceived(MessageEvent e) {
		if (e.getMessage() == null) {
			targets.remove(e.getSender());
			return;
		}

		String targetName = (String) e.getMessage();
		targets.put(e.getSender(), targetName);
		if (targetName.equals(name)) {
			name = null;
			stop();
		}
	}

	private BearingForCircle getValidBearingForCircle(double bearing, double distance) {
		BearingForCircle bfc = getBearingForCircle(bearing, distance, true);
		double x = getX() + bfc.distance * Math.sin(getHeadingRadians() + bfc.bearing);
		double y = getY() + bfc.distance * Math.cos(getHeadingRadians() + bfc.bearing);
		if (x <= 20 || x >= getBattleFieldWidth() - 20 || y <= 20 || y >= getBattleFieldHeight() - 20) {
			return getBearingForCircle(bearing, distance, false);
		}
		return bfc;
	}

	/**
	 * Suund vaenlase ümber ringile.
	 */
	private BearingForCircle getBearingForCircle(double bearing, double distance, boolean shorter) {
		BearingForCircle bfc = new BearingForCircle();
		bfc.forward = Math.abs(bearing) <= Math.PI / 2;// kas edaspidi on ikka kõige otsem
		if (distance < CLOSE_RANGE && getDistanceRemaining() != 0) {// lähivõitluses säilitame suuna
			bfc.forward = getDistanceRemaining() > 0 ? true : false;
		}
		if (wallCollsion()) {// Kui praegune suun sõidab seina, siis sõidame teistpidi
			bfc.forward = getDistanceRemaining() > 0 ? false : true;
		}
		if (!bfc.forward) {// Tagurpidi sõites on lähem teisel pool
			shorter = !shorter;
		}
		double angle = Math.PI / 2; // ringi sees
		if (distance > CIRCLE_RADIUS) {// ringist väljas
			angle = Math.asin(CIRCLE_RADIUS / distance);
			bfc.distance = Math.sqrt(distance * distance - CIRCLE_RADIUS * CIRCLE_RADIUS);
		} else {
			bfc.distance = Math.sqrt(CIRCLE_RADIUS * CIRCLE_RADIUS - distance * distance);
		}
		if (bearing < 0) {// pööre väheneb paremale
			bfc.bearing = shorter ? bearing + angle : bearing - angle;
		} else {// pööre väheneb vasakule
			bfc.bearing = shorter ? bearing - angle : bearing + angle;
		}
		bfc.turn = bfc.bearing;
		if (!bfc.forward) {// Tagurpidi sõites ninaotsa vastassuunda
			bfc.turn = bfc.turn < 0 ? bfc.turn + Math.PI : bfc.turn - Math.PI;
		}
		return bfc;
	}

	private boolean wallCollsion() {
		if (getDistanceRemaining() == 0) {
			return false;
		}
		double heading = getDistanceRemaining() > 0 ? getHeadingRadians() : getHeadingRadians() - Math.PI;
		double x = getX() + 20 * Math.sin(heading);
		double y = getY() + 20 * Math.cos(heading);
		if (x <= 20 || x >= getBattleFieldWidth() - 20 || y <= 20 || y >= getBattleFieldHeight() - 20) {
			return true;
		}
		return false;
	}

	private boolean isValid(String name) {
		return !isTeammate(name) && (name.equals(targets.get(getName())) || !targets.containsValue(name));
	}

	private double getGunTurn(double power, double bearing, double heading, double velosity) {
		double x = getX() + distance * Math.sin(getHeadingRadians() + bearing);
		double y = getY() + distance * Math.cos(getHeadingRadians() + bearing);

		double futureDistance = velosity * distance / Rules.getBulletSpeed(power);
		double futureX = x + futureDistance * Math.sin(heading);
		double futureY = y + futureDistance * Math.cos(heading);
		if (futureX <= 10) {
			futureDistance = Math.min(futureDistance, (10 - x) / Math.sin(heading));
		}
		if (futureX >= getBattleFieldWidth() - 10) {
			futureDistance = Math.min(futureDistance, (getBattleFieldWidth() - 10 - x) / Math.sin(heading));
		}
		if (futureY <= 10) {
			futureDistance = Math.min(futureDistance, (10 - y) / Math.cos(heading));
		}
		if (futureY >= getBattleFieldHeight() - 10) {
			futureDistance = Math.min(futureDistance, (getBattleFieldHeight() - 10 - y) / Math.cos(heading));
		}
		futureX = x + futureDistance * Math.sin(heading);
		futureY = y + futureDistance * Math.cos(heading);

		return Utils.normalRelativeAngle(absoluteBearing(getX(), getY(), futureX, futureY) - getGunHeadingRadians());
	}

	private double absoluteBearing(double x1, double y1, double x2, double y2) {
		double xo = x2 - x1;
		double yo = y2 - y1;
		double hyp = Math.sqrt(xo * xo + yo * yo);
		double arcSin = Math.asin(xo / hyp);
		double bearing = 0;

		if (xo > 0 && yo > 0) { // both pos: lower-Left
			bearing = arcSin;
		} else if (xo < 0 && yo > 0) { // x neg, y pos: lower-right
			bearing = 2 * Math.PI + arcSin; // arcsin is negative here, actuall 360 - ang
		} else if (xo > 0 && yo < 0) { // x pos, y neg: upper-left
			bearing = Math.PI - arcSin;
		} else if (xo < 0 && yo < 0) { // both neg: upper-right
			bearing = Math.PI - arcSin; // arcsin is negative here, actually 180 + ang
		}

		return bearing;
	}

	private class BearingForCircle {
		public boolean forward;
		public double turn;
		public double bearing;
		public double distance;
	}

}
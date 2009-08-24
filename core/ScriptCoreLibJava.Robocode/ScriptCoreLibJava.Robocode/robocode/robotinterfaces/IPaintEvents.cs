// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;

namespace robocode.robotinterfaces
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/IPaintEvents.html
	[Script(IsNative = true)]
	public interface IPaintEvents
	{
		/// <summary>
		/// This method is called every time the robot is painted.
		/// </summary>
		void onPaint(Graphics2D @g);

	}
}

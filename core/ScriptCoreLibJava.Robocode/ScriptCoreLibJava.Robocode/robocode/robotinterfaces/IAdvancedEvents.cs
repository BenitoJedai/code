// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode;

namespace robocode.robotinterfaces
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/IAdvancedEvents.html
	[Script(IsNative = true)]
	public interface IAdvancedEvents
	{
		/// <summary>
		/// This method is called when a custom condition is met.
		/// </summary>
		void onCustomEvent(CustomEvent @event);

		/// <summary>
		/// This method is called if the robot is using too much time between
		/// actions.
		/// </summary>
		void onSkippedTurn(SkippedTurnEvent @event);

	}
}

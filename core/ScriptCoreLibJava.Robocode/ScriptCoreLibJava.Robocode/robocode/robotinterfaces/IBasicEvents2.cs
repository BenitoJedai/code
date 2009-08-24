// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode;

namespace robocode.robotinterfaces
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/IBasicEvents2.html
	[Script(IsNative = true)]
	public interface IBasicEvents2
	{
		/// <summary>
		/// This method is called after end of the battle, even when the battle is aborted.
		/// </summary>
		void onBattleEnded(BattleEndedEvent @event);

	}
}

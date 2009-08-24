// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using robocode.control;

namespace robocode.control
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/RobocodeListener.html
	[Script(IsNative = true)]
	public interface RobocodeListener
	{
		/// <summary>
		/// <B>Deprecated.</B> <I>Since 1.6.2. Use the
		/// <A HREF="../../robocode/control/events/IBattleListener.html#onBattleFinished(robocode.control.events.BattleFinishedEvent)"><CODE>IBattleListener.onBattleFinished()</CODE></A> instead.
		/// <p/>
		/// This method is called when a battle has been aborted.</I>
		/// </summary>
		void battleAborted(BattleSpecification @battle);

		/// <summary>
		/// <B>Deprecated.</B> <I>Since 1.6.2. Use the
		/// <A HREF="../../robocode/control/events/IBattleListener.html#onBattleCompleted(robocode.control.events.BattleCompletedEvent)"><CODE>IBattleListener.onBattleCompleted()</CODE></A> instead.
		/// <p/>
		/// This method is called when a battle completes successfully.</I>
		/// </summary>
		void battleComplete(BattleSpecification @battle, RobotResults[] results);

		/// <summary>
		/// <B>Deprecated.</B> <I>Since 1.6.2. Use the
		/// <A HREF="../../robocode/control/events/IBattleListener.html#onBattleMessage(robocode.control.events.BattleMessageEvent)"><CODE>IBattleListener.onBattleMessage()</CODE></A> instead.
		/// <p/>
		/// This method is called when the game logs messages that is normally
		/// written out to the console.</I>
		/// </summary>
		void battleMessage(string @message);

	}
}

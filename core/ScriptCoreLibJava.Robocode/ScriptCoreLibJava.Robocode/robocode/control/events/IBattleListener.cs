// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode.control.events;

namespace robocode.control.events
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/events/IBattleListener.html
	[Script(IsNative = true)]
	public interface IBattleListener
	{
		/// <summary>
		/// This method is called when the battle has completed successfully and results are available.
		/// </summary>
		void onBattleCompleted(BattleCompletedEvent @event);

		/// <summary>
		/// This method is called when the game has sent an error message.
		/// </summary>
		void onBattleError(BattleErrorEvent @event);

		/// <summary>
		/// This method is called when the battle has finished.
		/// </summary>
		void onBattleFinished(BattleFinishedEvent @event);

		/// <summary>
		/// This method is called when the game has sent a new information message.
		/// </summary>
		void onBattleMessage(BattleMessageEvent @event);

		/// <summary>
		/// This method is called when the battle has been paused, either by the user or the game.
		/// </summary>
		void onBattlePaused(BattlePausedEvent @event);

		/// <summary>
		/// This method is called when the battle has been resumed (after having been paused).
		/// </summary>
		void onBattleResumed(BattleResumedEvent @event);

		/// <summary>
		/// This method is called when a new battle has started.
		/// </summary>
		void onBattleStarted(BattleStartedEvent @event);

		/// <summary>
		/// This method is called when the current round of a battle has ended.
		/// </summary>
		void onRoundEnded(RoundEndedEvent @event);

		/// <summary>
		/// This method is called when a new round in a battle has started.
		/// </summary>
		void onRoundStarted(RoundStartedEvent @event);

		/// <summary>
		/// This method is called when the current turn in a battle round is ended.
		/// </summary>
		void onTurnEnded(TurnEndedEvent @event);

		/// <summary>
		/// This method is called when a new turn in a battle round has started.
		/// </summary>
		void onTurnStarted(TurnStartedEvent @event);

	}
}

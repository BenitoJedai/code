using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Shared.Nonoba.Generic
{

    [Script]
    public abstract class ServerGameBase<RemoteEvents, RemoteMessages, Player>
            where Player : ServerPlayerBase<RemoteEvents, RemoteMessages>
    {

        public readonly List<Player> Users = new List<Player>();

        public virtual void UserJoined(Player player) { }
        public virtual void UserLeft(Player player) { }
        public virtual void GameStarted() { }
        public virtual void GameClosed() { }

		public ServerGameSettings Settings = new ServerGameSettings();

        public Func<Action, int, Action> AtInterval;
        public Func<Action, int, Action> AtDelay;

        #region GameState
        public GameStateEnum _GameState;

        public event Action GameStateChanged;

        public GameStateEnum GameState
        {
            get
            {
                return _GameState;
            }
            set
            {
                _GameState = value;

                if (GameStateChanged != null)
                    GameStateChanged();
            }
        }
        #endregion

        public enum GameStateEnum
        {
            WaitingForPlayers = 0,
            OpenGameInProgress = 1,
            ClosedGameInProgress = 2,
        }

        public Player AnyOtherUser(Player p)
        {
            var x = default(Player);

            foreach (var v in Users)
            {
                if (v.UserId != p.UserId)
                {
                    x = v;
                    break;
                }
            }

            return x;
        }
    }
}

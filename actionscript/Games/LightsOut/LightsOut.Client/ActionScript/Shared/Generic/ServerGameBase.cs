using System;
using System.Collections.Generic;
using System.Text;
#if !NoAttributes
using ScriptCoreLib;
#endif
namespace LightsOut.ActionScript.Shared.Generic
{

#if !NoAttributes
    [Script]
#endif
    public abstract class ServerGameBase<RemoteEvents, RemoteMessages, Player>
            where Player : ServerPlayerBase<RemoteEvents, RemoteMessages>
    {

        public readonly List<Player> Users = new List<Player>();

        public virtual void UserJoined(Player player) { }
        public virtual void UserLeft(Player player) { }
        public virtual void GameStarted() { }
        public virtual void GameClosed() { }


        public Func<Action, int, Action> AtInterval;
        public Func<Action, int, Action> AtDelay;

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

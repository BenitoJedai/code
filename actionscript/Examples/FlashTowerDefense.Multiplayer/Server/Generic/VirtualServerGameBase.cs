using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using Nonoba.GameLibrary;

namespace FlashTowerDefense.Server.Generic
{
    /// <summary>
    /// Each instance of this class represents one game.
    /// </summary>
    public abstract class VirtualServerGameBase<TSendArguments, TWithUserArgumentsRouter, TDispatchHelper, TRemoteEvents, TRemoteMessages, TVirtualGame, TVirtualPlayer, TGame, TPlayer> : NonobaGame<TPlayer>

        where TWithUserArgumentsRouter : Shared.IWithUserArgumentsRouter<TRemoteMessages>
        where TSendArguments : Shared.ISendArguments
        where TRemoteMessages : Shared.IRemoteMessages<TSendArguments>
        where TRemoteEvents : Shared.IRemoteEvents<TWithUserArgumentsRouter>
        where TDispatchHelper : Shared.IDispatchHelper
        where TPlayer : VirtualServerPlayerBase<TVirtualPlayer>, new()
        where TVirtualGame : Shared.Generic.ServerGameBase<TRemoteEvents, TRemoteMessages, TVirtualPlayer>
        where TVirtualPlayer : Shared.Generic.ServerPlayerBase<TRemoteEvents, TRemoteMessages>
    {
        public TVirtualGame Virtual;

        public Func<TWithUserArgumentsRouter> CreateTWithUserArgumentsRouter;
        public Func<TRemoteEvents> CreateTRemoteEvents;
        public Func<TRemoteMessages> CreateTRemoteMessages;
        public Func<TVirtualPlayer> CreateTVirtualPlayer;
        public Func<TVirtualGame> CreateTVirtualGame;
        public Func<TDispatchHelper> CreateTDispatchHelper;

   
        /// <summary>
        /// Game started is called *once* when an instance
        /// of your game is started. It's a good place to initialize
        /// your game.
        /// </summary>
        public override void GameStarted()
        {
            Virtual = CreateTVirtualGame();
            Virtual.AtDelay = (h, i) => this.ScheduleCallback(() => h(), i).Stop;
            Virtual.AtInterval = (h, i) => this.AddTimer(() => h(), i).Stop;


            // You can explicitly setup how many users are allowed in your game.
            MaxUsers = 8;

            Virtual.GameStarted();
        }


        public override void GameClosed()
        {
            Virtual.GameClosed();
            Virtual = null;
        }

        /// <summary>This message is called whenever a player sends a message into the game.</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void GotMessage(TPlayer player, Message m)
        {
            var e = /*(TRemoteMessages)*/int.Parse(m.Type);

            var d = CreateTDispatchHelper();

            d.GetLength = i => (int)m.Count;
            d.GetInt32 = m.GetInt;
            d.GetDouble = m.GetDouble;
            d.GetString = m.GetString;



            if (player.Virtual.FromPlayer.Dispatch(e, d))
                return;

            Console.WriteLine("Not on dispatch: " + m.Type);
        }



        /// <summary>When a user enters this game instance</summary>
        public override void UserJoined(TPlayer player)
        {
            player.Virtual = CreateTVirtualPlayer();

            player.Virtual.ToOthers = CreateTRemoteMessages();
            player.Virtual.ToOthers.Send = q => this.SendOthers(player.UserId, q.i, q.args);


            player.Virtual.ToPlayer = CreateTRemoteMessages();
            player.Virtual.ToOthers.Send = e => this.Send(player, e.i, e.args);

            player.Virtual.FromPlayer = CreateTRemoteEvents();
            player.Virtual.FromPlayer.Router = CreateTWithUserArgumentsRouter();
            player.Virtual.FromPlayer.Router.user = player.UserId;
            player.Virtual.FromPlayer.Router.Target = player.Virtual.ToOthers;


            player.Virtual.UserId = player.UserId;
            player.Virtual.Username = player.Username;
            

            Virtual.Users.Add(player.Virtual);

            Virtual.UserJoined(player.Virtual);





            // reset waves


            // we need to resync the players now
        }

        /// <summary>When a user leaves the game instance</summary>
        public override void UserLeft(TPlayer player)
        {
            this.Virtual.Users.Remove(player.Virtual);
            this.Virtual.UserLeft(player.Virtual);
            player.Virtual = null;



        }

        #region Send
        private void Send(TPlayer v, int type, params object[] e)
        {
            v.Send(((int)type).ToString(), e);
        }

        private void Send(int id, int type, params object[] e)
        {
            foreach (var v in Users)
            {
                if (v.UserId == id)
                    Send(v, type, e);
            }
        }

        private void SendOthers(int id, int type, params object[] e)
        {
            foreach (var v in Users)
            {
                if (v.UserId != id)
                    Send(v, type, e);
            }
        }


        private void Broadcast(int type, params object[] e)
        {
            Broadcast(((int)type).ToString(), e);
        }

        #endregion

    }
}

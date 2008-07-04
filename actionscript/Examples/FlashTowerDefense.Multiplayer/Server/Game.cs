using System;
using System.Collections.Generic;
using System.Text;
using Nonoba.GameLibrary;
using System.Drawing;
using FlashTowerDefense.Shared;
using System.Runtime.CompilerServices;

namespace FlashTowerDefense.Server
{
    class Game : Generic.VirtualServerGameBase<
        SharedClass1.RemoteMessages.SendArguments,
        SharedClass1.RemoteEvents.WithUserArgumentsRouter,
        SharedClass1.RemoteEvents.DispatchHelper,
        SharedClass1.RemoteEvents,
        SharedClass1.RemoteMessages,
        Shared.Game,
        Shared.Player,
        Game,
        Player>
    {

    }
#if XXX
    /// <summary>
    /// Each instance of this class represents one game.
    /// </summary>
    public class Game : NonobaGame<Player>
    {
        public Shared.Game Virtual;


        /// <summary>
        /// Game started is called *once* when an instance
        /// of your game is started. It's a good place to initialize
        /// your game.
        /// </summary>
        public override void GameStarted()
        {
            Virtual = new FlashTowerDefense.Shared.Game
            {
                AtDelay = (h, i) => this.ScheduleCallback(() => h(), i).Stop,
                AtInterval = (h, i) => this.AddTimer(() => h(), i).Stop,
            };

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
        public override void GotMessage(Player player, Message m)
        {
            var e = (SharedClass1.Messages)int.Parse(m.Type);

            if (player.Virtual.FromPlayer.Dispatch(e,
                    new SharedClass1.RemoteEvents.DispatchHelper
                    {
                        GetLength = i => (int)m.Count,
                        GetInt32 = m.GetInt,
                        GetDouble = m.GetDouble,
                        GetString = m.GetString
                    }
                ))
                return;

            Console.WriteLine("Not on dispatch: " + m.Type);
        }



        /// <summary>When a user enters this game instance</summary>
        public override void UserJoined(Player player)
        {
            player.Virtual = new FlashTowerDefense.Shared.Player
            {
                ToOthers =
                    new SharedClass1.RemoteMessages
                    {
                        Send = q => this.SendOthers(player.UserId, q.i, q.args)
                    },
                ToPlayer =
                    new SharedClass1.RemoteMessages
                    {
                        Send = e => this.Send(player, e.i, e.args)
                    },
                FromPlayer =
                    new SharedClass1.RemoteEvents
                    {
                        Router = new SharedClass1.RemoteEvents.WithUserArgumentsRouter
                        {
                            user = player.UserId,
                        }
                    },
                UserId = player.UserId,
                Username = player.Username
            };

            player.Virtual.FromPlayer.Router.Target = player.Virtual.ToOthers;

            Virtual.Users.Add(player.Virtual);

            Virtual.UserJoined(player.Virtual);





            // reset waves

            ScheduleCallback(
                delegate
                {
                    player.Virtual.ToPlayer.ServerMessage("Game will start shortly!");
                },
                25
            );


            // we need to resync the players now
        }

        /// <summary>When a user leaves the game instance</summary>
        public override void UserLeft(Player player)
        {
            this.Virtual.Users.Remove(player.Virtual);
            this.Virtual.UserLeft(player.Virtual);
            player.Virtual = null;



        }

        #region Send
        private void Send(Player v, Shared.SharedClass1.Messages type, params object[] e)
        {

            v.Send(((int)type).ToString(), e);
        }

        private void Send(int id, Shared.SharedClass1.Messages type, params object[] e)
        {
            foreach (var v in Users)
            {
                if (v.UserId == id)
                    Send(v, type, e);
            }
        }

        private void SendOthers(int id, Shared.SharedClass1.Messages type, params object[] e)
        {
            foreach (var v in Users)
            {
                if (v.UserId != id)
                    Send(v, type, e);
            }
        }


        private void Broadcast(Shared.SharedClass1.Messages type, params object[] e)
        {
            Broadcast(((int)type).ToString(), e);
        }

        #endregion

    }
#endif
}

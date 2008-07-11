using System;
using System.Collections.Generic;
using System.Text;
using Nonoba.GameLibrary;
using System.Runtime.CompilerServices;
using FlashMinesweeper.ActionScript.Shared;

namespace FlashMinesweeper.Server
{
    

    /// <summary>
    /// Each instance of this class represents one game.
    /// </summary>
    public class Game : NonobaGame<Player>
    {
        public FlashMinesweeper.ActionScript.Shared.Game Virtual;


        /// <summary>
        /// Game started is called *once* when an instance
        /// of your game is started. It's a good place to initialize
        /// your game.
        /// </summary>
        public override void GameStarted()
        {
            Virtual = new FlashMinesweeper.ActionScript.Shared.Game
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
            var e = /*(SharedClass1.Messages)*/int.Parse(m.Type);


            if (player.Virtual.FromPlayerDispatch.DispatchInt32(e,
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
            var FromPlayer =
                new SharedClass1.RemoteEvents
                {
                    Router = new SharedClass1.RemoteEvents.WithUserArgumentsRouter
                    {
                        user = player.UserId,
                    }
                };
            
            player.Virtual = new FlashMinesweeper.ActionScript.Shared.Player
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
                FromPlayer = FromPlayer,
                FromPlayerDispatch = FromPlayer,
                UserId = player.UserId,
                Username = player.Username,

                AddScore = player.SubmitRankingDelta,
                AwardAchievement = player.AwardAchievement,
            };

            FromPlayer.Router.Target = player.Virtual.ToOthers;

            Virtual.Users.Add(player.Virtual);

            Virtual.UserJoined(player.Virtual);


        }

        /// <summary>When a user leaves the game instance</summary>
        public override void UserLeft(Player player)
        {
            this.Virtual.Users.Remove(player.Virtual);
            this.Virtual.UserLeft(player.Virtual);
            player.Virtual = null;



        }

        #region Send
        private void Send(Player v, FlashMinesweeper.ActionScript.Shared.SharedClass1.Messages type, params object[] e)
        {

            v.Send(((int)type).ToString(), e);
        }

        private void Send(int id, FlashMinesweeper.ActionScript.Shared.SharedClass1.Messages type, params object[] e)
        {
            foreach (var v in Users)
            {
                if (v.UserId == id)
                    Send(v, type, e);
            }
        }

        private void SendOthers(int id, FlashMinesweeper.ActionScript.Shared.SharedClass1.Messages type, params object[] e)
        {
            foreach (var v in Users)
            {
                if (v.UserId != id)
                    Send(v, type, e);
            }
        }


        private void Broadcast(FlashMinesweeper.ActionScript.Shared.SharedClass1.Messages type, params object[] e)
        {
            Broadcast(((int)type).ToString(), e);
        }

        #endregion

    }
//#endif
}

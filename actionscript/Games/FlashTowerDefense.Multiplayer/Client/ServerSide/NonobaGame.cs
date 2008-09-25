using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using FlashTowerDefense.Shared;
using Nonoba.GameLibrary;
using ScriptCoreLib;

namespace FlashTowerDefense.ServerSide
{
	[Script]
	public class NonobaGameUser<TVirtualPlayer> : NonobaGameUser
	{
		public TVirtualPlayer Virtual { get; set; }

		public override Dictionary<string, string> GetDebugValues()
		{
			return new Dictionary<string, string> { };
		}
	}

    /// <summary>
    /// Each instance of this class represents one game.
    /// </summary>
	[Script]
	public partial class NonobaGame : NonobaGame<NonobaGameUser<VirtualPlayer>>
    {
        public Shared.VirtualGame Virtual;


        /// <summary>
        /// Game started is called *once* when an instance
        /// of your game is started. It's a good place to initialize
        /// your game.
        /// </summary>
        public override void GameStarted()
        {
            Virtual = new FlashTowerDefense.Shared.VirtualGame
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
		public override void GotMessage(NonobaGameUser<VirtualPlayer> player, Message m)
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
		public override void UserJoined(NonobaGameUser<VirtualPlayer> player)
        {
            var FromPlayer =
                new SharedClass1.RemoteEvents
                {
                    BroadcastRouter = new SharedClass1.RemoteEvents.WithUserArgumentsRouter_Broadcast
                    {
                        user = player.UserId,
                    }
                };

            player.Virtual = new FlashTowerDefense.Shared.VirtualPlayer
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
                AddScore = player.SubmitRankingDelta
            };

            FromPlayer.BroadcastRouter.Target = player.Virtual.ToOthers;

            Virtual.Users.Add(player.Virtual);

            Virtual.UserJoined(player.Virtual);


        }

        /// <summary>When a user leaves the game instance</summary>
		public override void UserLeft(NonobaGameUser<VirtualPlayer> player)
        {
            this.Virtual.Users.Remove(player.Virtual);
            this.Virtual.UserLeft(player.Virtual);
            player.Virtual = null;



        }

		#region Send
		private void Send(NonobaGameUser<VirtualPlayer> v, SharedClass1.Messages type, params object[] e)
		{
			var MessageType = ((int)type).ToString();


			v.Send(MessageType, e);
		}

		private void Send(int id, SharedClass1.Messages type, params object[] e)
		{
			foreach (var v in Users)
			{
				if (v.UserId == id)
					Send(v, type, e);
			}
		}

		private void SendOthers(int id, SharedClass1.Messages type, params object[] e)
		{
			foreach (var v in Users)
			{
				if (v.UserId != id)
					Send(v, type, e);
			}
		}


		#endregion


    }
//#endif
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using Nonoba.GameLibrary;
using LightsOut.ActionScript.Shared;

namespace LightsOut.Server
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

    [Script]
    public class NonobaGame : NonobaGame<NonobaGameUser<MyPlayer>>
    {
        public LightsOut.ActionScript.Shared.MyGame Virtual;


        public override void GameStarted()
        {
            Virtual = new LightsOut.ActionScript.Shared.MyGame
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


        public override void GotMessage(NonobaGameUser<MyPlayer> user, Message message)
        {
            var e = /*(SharedClass1.Messages)*/int.Parse(message.Type);

            var p = user.Virtual;

            if (p.FromPlayerDispatch.DispatchInt32(e,
                    new SharedClass1.RemoteEvents.DispatchHelper
                    {
                        GetLength = i => (int)message.Count,
                        GetInt32 = message.GetInt,
                        GetDouble = message.GetDouble,
                        GetString = message.GetString
                    }
                ))
                return;

            Console.WriteLine("Not on dispatch: " + message.Type);
        }

        public override void UserJoined(NonobaGameUser<MyPlayer> user)
        {
            var FromPlayer =
                  new SharedClass1.RemoteEvents
                  {
                      Router = new SharedClass1.RemoteEvents.WithUserArgumentsRouter
                      {
                          user = user.UserId,
                      }
                  };

            user.Virtual = new LightsOut.ActionScript.Shared.MyPlayer
            {
                ToOthers =
                    new SharedClass1.RemoteMessages
                    {
                        Send = q => this.SendOthers(user.UserId, q.i, q.args)
                    },
                ToPlayer =
                    new SharedClass1.RemoteMessages
                    {
                        Send = e => this.Send(user, e.i, e.args)
                    },
                FromPlayer = FromPlayer,
                FromPlayerDispatch = FromPlayer,
                UserId = user.UserId,
                Username = user.Username,

                AddScore = user.SubmitRankingDelta,
                AwardAchievement = user.AwardAchievement,
            };

            FromPlayer.Router.Target = user.Virtual.ToOthers;

            Virtual.Users.Add(user.Virtual);

            Virtual.UserJoined(user.Virtual);

        }

        public override void UserLeft(NonobaGameUser<MyPlayer> user)
        {
            this.Virtual.Users.Remove(user.Virtual);
            this.Virtual.UserLeft(user.Virtual);
            user.Virtual = null;

        }

        #region Send
        private void Send(NonobaGameUser<MyPlayer> v, LightsOut.ActionScript.Shared.SharedClass1.Messages type, params object[] e)
        {

            v.Send(((int)type).ToString(), e);
        }

        private void Send(int id, LightsOut.ActionScript.Shared.SharedClass1.Messages type, params object[] e)
        {
            foreach (var v in Users)
            {
                if (v.UserId == id)
                    Send(v, type, e);
            }
        }

        private void SendOthers(int id, LightsOut.ActionScript.Shared.SharedClass1.Messages type, params object[] e)
        {
            foreach (var v in Users)
            {
                if (v.UserId != id)
                    Send(v, type, e);
            }
        }


        #endregion

    }
}

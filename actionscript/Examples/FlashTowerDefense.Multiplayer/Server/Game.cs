using System;
using System.Collections.Generic;
using System.Text;
using Nonoba.GameLibrary;
using System.Drawing;
using FlashTowerDefense.Shared;

namespace FlashTowerDefense.Server
{
    /// <summary>
    /// Each instance of this class represents one game.
    /// 
    /// </summary>
    public class Game : NonobaGame<Player>
    {


        /// <summary>
        /// Game started is called *once* when an instance
        /// of your game is started. It's a good place to initialize
        /// your game.
        /// </summary>
        public override void GameStarted()
        {
            // You can explicitly setup how many users are allowed in your game.
            MaxUsers = 8;

            // You can schedule a onetime callback for later. 
            // In this case, we're sending out a onetime "delayedhello"
            // message 10000 milliseconds (10 seconds) after the 
            // game is started
            //ScheduleCallback(delegate
            //{
            //    Broadcast("delayedhello");
            //}, 10000);

            // You can setup timers to issue regular callbacks
            // in this case, the tick() method will be called
            // every 100th millisecond (10 times a second).
            // AddTimer(new TimerCallback(tick), 30000);

            AddTimer(CheckIfAllReady
                //delegate
                //{

                    //var a = new List<object>();
                //var r = new Random();

                    //for (int i = 0; i < 32; i++)
                //{
                //    a.Add(r.Next(100));
                //}
                //var z = a.ToArray();

                    //Broadcast(SharedClass1.Messages.ServerRandomNumbers, z);
                //}
            , 5000);
        }

        List<Player> PlayersWithActiveWarzone;

        private void CheckIfAllReady()
        {
            try
            {
                if (PlayersWithActiveWarzone == null)
                {
                    if (Users.Length > 1)
                    {
                        var Ready = new List<Player>();

                        foreach (var z in Users)
                        {
                            if (z.GameEventStatus == Player.GameEventStatusEnum.Ready)
                                Ready.Add(z);
                        }

                        if (Ready.Count == Users.Length)
                        {
                            foreach (var z in Ready)
                                z.GameEventStatus = Player.GameEventStatusEnum.Pending;

                            // multiple users are ready
                            PlayersWithActiveWarzone = Ready;
                        }
                    }
                }
                else
                {
                    var Cancelled = new List<Player>();

                    var a = new List<object>();
                    var r = new Random();

                    for (int i = 0; i < 32; i++)
                    {
                        a.Add(r.Next(100));
                    }
                    var z = a.ToArray();

                    foreach (var i in PlayersWithActiveWarzone)
                    {
                        if (i.GameEventStatus == Player.GameEventStatusEnum.Pending)
                        {
                            Send(i, SharedClass1.Messages.ServerRandomNumbers, z);
                        }
                        else if (i.GameEventStatus == Player.GameEventStatusEnum.Cancelled)
                        {
                            Cancelled.Add(i);
                        }
                    }

                    if (Cancelled.Count == PlayersWithActiveWarzone.Count)
                    {
                        // end of day for those guys
                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>Timer callback scheduled to be called 10 times a second in the AddTimer() call in GameStarted()</summary>
        //private void tick()
        //{

        //    Console.WriteLine("Use Console.WriteLine() for easy debugging");

        //    RefreshDebugView(); // update the visual debugging view
        //    //Broadcast("tick");
        //}

        /// <summary>This message is called whenever a player sends a message into the game.</summary>
        public override void GotMessage(Player player, Message m)
        {
            var e = (SharedClass1.Messages)int.Parse(m.Type);

            if (e == SharedClass1.Messages.EnterMachineGun)
                Broadcast(SharedClass1.Messages.UserEnterMachineGun, player.UserId);
            else if (e == SharedClass1.Messages.ExitMachineGun)
                Broadcast(SharedClass1.Messages.UserExitMachineGun, player.UserId);
            else if (e == SharedClass1.Messages.StartMachineGun)
                Broadcast(SharedClass1.Messages.UserStartMachineGun, player.Username);
            else if (e == SharedClass1.Messages.StopMachineGun)
                Broadcast(SharedClass1.Messages.UserStopMachineGun, player.Username);
            else if (e == SharedClass1.Messages.TeleportTo)
                Broadcast(SharedClass1.Messages.UserTeleportTo, player.UserId, m.GetInt(0), m.GetInt(1));
            else if (e == SharedClass1.Messages.WalkTo)
                Broadcast(SharedClass1.Messages.UserWalkTo, player.UserId, m.GetInt(0), m.GetInt(1));
            else if (e == SharedClass1.Messages.ToUserJoinedReply)
                Send(m.GetInt(0), SharedClass1.Messages.UserJoinedReply, player.Username, player.UserId);
            else if (e == SharedClass1.Messages.FiredShotgun)
                Broadcast(SharedClass1.Messages.UserFiredShotgun, player.UserId);
            else if (e == SharedClass1.Messages.ReadyForServerRandomNumbers)
                player.GameEventStatus = Player.GameEventStatusEnum.Ready;
            else if (e == SharedClass1.Messages.CancelServerRandomNumbers)
                player.GameEventStatus = Player.GameEventStatusEnum.Cancelled;
        }

        /// <summary>When a user enters this game instance</summary>
        public override void UserJoined(Player player)
        {
            //player.Send("welcometogame", Users.Length); // send a message with the amount users in the game

            Broadcast(SharedClass1.Messages.UserJoined, player.Username, player.UserId);


            //Send(player, SharedClass1.Messages.ServerRandomNumbers, z);
            // we need to resync the players now
        }

        /// <summary>When a user leaves the game instance</summary>
        public override void UserLeft(Player player)
        {
            Broadcast(SharedClass1.Messages.UserLeft, player.Username, player.UserId);
        }

        public void Send(Player v, Shared.SharedClass1.Messages type, params object[] e)
        {
            v.Send(((int)type).ToString(), e);
        }

        public void Send(int id, Shared.SharedClass1.Messages type, params object[] e)
        {
            foreach (var v in Users)
            {
                if (v.UserId == id)
                    Send(v, type, e);
            }
        }

        public void Broadcast(Shared.SharedClass1.Messages type, params object[] e)
        {
            Broadcast(((int)type).ToString(), e);
        }
        ///// <summary>
        ///// This method can be used to generate a visual debugging image, that
        ///// will be displayed in the Development Server. 
        ///// Call RefreshDebugView() to update image.
        ///// </summary>
        //public override Image GenerateDebugImage()
        //{
        //    // example code creating a 100x100 image and drawing the string "hello world" on it.
        //    Bitmap image = new Bitmap(100, 100);
        //    using (Graphics g = Graphics.FromImage(image))
        //    {
        //        g.FillRectangle(Brushes.DarkGray, 0, 0, 100, 100);
        //        g.DrawString("Hello World", new Font("verdana", 10F), Brushes.Black, 0, 0);
        //    }
        //    return image;
        //}
    }
}

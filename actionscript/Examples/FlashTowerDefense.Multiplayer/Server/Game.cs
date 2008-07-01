using System;
using System.Collections.Generic;
using System.Text;
using Nonoba.GameLibrary;
using System.Drawing;
using FlashTowerDefense.Shared;
using System.Runtime.CompilerServices;

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

            AddTimer(CheckIfAllReady, 2000);
            AddTimer(SendNextWave, 5000);
        }

        List<Player> PlayersWithActiveWarzone;

        public readonly int MinimumPlayersToActivateWarzone = 1;

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void CheckIfAllReady()
        {
            //if (PlayersWithActiveWarzone != null)
            //    return;

            //if (Users.Length < MinimumPlayersToActivateWarzone)
            //    return;

            //var Ready = new List<Player>();
            //var NextReadyCount = 0;

            //foreach (var z in Users)
            //{
            //    if (z.GameEventStatus == Player.GameEventStatusEnum.Ready)
            //        Ready.Add(z);

            //    if (z.GameEventStatus == Player.GameEventStatusEnum.Lagging)
            //        continue;

            //    NextReadyCount++;
            //}

            //if (NextReadyCount > 0)
            //{
            //    if (Ready.Count == NextReadyCount)
            //    {
            //        //Broadcast(SharedClass1.Messages.ServerMessage, "New wave!");

            //        foreach (var z in Ready)
            //            z.GameEventStatus = Player.GameEventStatusEnum.Pending;

            //        // multiple users are ready
            //        PlayersWithActiveWarzone = Ready;

            //        SetState(NonobaGameState.OpenGameInProgress);
            //    }
            //    else
            //    {

            //        //Broadcast(SharedClass1.Messages.ServerMessage, "All not ready!");
            //    }
            //}

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SendNextWave()
        {
            //if (PlayersWithActiveWarzone == null)
            //    return;

            ////Broadcast(SharedClass1.Messages.ServerMessage, "The wave is still active!");

            //var Cancelled = new List<Player>();

            var z = GenerateRandomNumbers();

            //foreach (var i in Users.ToArray())
            //{
            //    if (i.LastMessage.AddSeconds(5) < DateTime.Now)
            //    {
            //        i.GameEventStatus = Player.GameEventStatusEnum.Lagging;
            //        PlayersWithActiveWarzone.Remove(i);
            //        continue;
            //    }
            //}

            Console.WriteLine("Next Wave");

            foreach (var i in Users)
            {


                //if (i.GameEventStatus == Player.GameEventStatusEnum.Pending)
                //{
                    i.NetworkMessages.ServerRandomNumbers(z);
                    //Send(i, SharedClass1.Messages.ServerRandomNumbers, z);
                //}
                //else if (i.GameEventStatus == Player.GameEventStatusEnum.Cancelled)
                //{
                //    Cancelled.Add(i);
                //}
            }

            //if (Cancelled.Count == PlayersWithActiveWarzone.Count)
            //{
            //    // end of day for those guys
            //    PlayersWithActiveWarzone = null;
            //    SetState(NonobaGameState.WaitingForPlayers);
            //}

            
        }

        private double[] GenerateRandomNumbers()
        {
            var a = new List<double>();
            var r = new Random();

            for (int i = 0; i < 100; i++)
            {
                a.Add(r.NextDouble());
            }
            var z = a.ToArray();
            return z;
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
            var NetworkMessages_ToOthers = 
                new SharedClass1.RemoteMessages 
                { 
                    Send = q => this.SendOthers(player.UserId, q.i, q.args)
                };

            player.LastMessage = DateTime.Now;

            var e = (SharedClass1.Messages)int.Parse(m.Type);

            if (player.NetworkEvents.Dispatch(e,
                    new SharedClass1.RemoteEvents.DispatchHelper
                    {
                        GetLength = i => (int) m.Count,
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
            var ToOthers =
                 new SharedClass1.RemoteMessages
                 {
                     Send = q => this.SendOthers(player.UserId, q.i, q.args)
                 };

            player.NetworkMessages = 
                new SharedClass1.RemoteMessages
                {
                    Send = e => this.Send(player, e.i, e.args)
                };
            

            player.NetworkEvents = new SharedClass1.RemoteEvents();


            player.NetworkEvents.Ping += player.NetworkEvents.EmptyHandler;
            player.NetworkEvents.EnterMachineGun += e => ToOthers.UserEnterMachineGun(player.UserId);
            player.NetworkEvents.ExitMachineGun += e => ToOthers.UserExitMachineGun(player.UserId);
            player.NetworkEvents.StartMachineGun += e => ToOthers.UserStartMachineGun(player.UserId);
            player.NetworkEvents.StopMachineGun += e => ToOthers.UserStopMachineGun(player.UserId);

            player.NetworkEvents.ShowBulletsFlying += e => ToOthers.UserShowBulletsFlying(player.UserId, e.x, e.y, e.arc, e.weaponType);
            player.NetworkEvents.AddDamageFromDirection += e => ToOthers.UserAddDamageFromDirection(player.UserId, e.target, e.damage, e.arc);
            player.NetworkEvents.AddDamage += e => ToOthers.UserAddDamage(player.UserId, e.target, e.damage);

            //player.NetworkEvents.AddDamageFromDirection += e => Console.WriteLine(player.Username + " damaged " + e.target + " by " + e.damage);


            player.NetworkEvents.TeleportTo += e => ToOthers.UserTeleportTo(player.UserId, e.x, e.y);
            player.NetworkEvents.WalkTo += e => ToOthers.UserWalkTo(player.UserId, e.x, e.y);

            player.NetworkEvents.TakeBox += e => ToOthers.UserTakeBox(player.UserId, e.box);
            player.NetworkEvents.FiredWeapon += e => ToOthers.UserFiredWeapon(player.UserId, e.weapon);
            player.NetworkEvents.DeployExplosiveBarrel += e => ToOthers.UserDeployExplosiveBarrel(player.UserId, e.weapon, e.barrel, e.x, e.y);
            //player.NetworkEvents.DeployExplosiveBarrel += e => Console.WriteLine(player.Username + " deploy: " + e.barrel);
            player.NetworkEvents.UndeployExplosiveBarrel += e => ToOthers.UserUndeployExplosiveBarrel(player.UserId, e.barrel);
            //player.NetworkEvents.UndeployExplosiveBarrel += e => Console.WriteLine(player.Username + " undeploy: " + e.barrel);


            player.NetworkEvents.PlayerAdvertise += e => ToOthers.ServerPlayerAdvertise(player.UserId, player.Username, e.ego);
            player.NetworkEvents.PlayerResurrect += e => ToOthers.UserPlayerResurrect(player.UserId);
            //player.NetworkEvents.PlayerResurrect += e => Console.WriteLine("resurrect: " + player.Username);


            player.NetworkEvents.ReadyForServerRandomNumbers += e => player.GameEventStatus = Player.GameEventStatusEnum.Ready;
            player.NetworkEvents.CancelServerRandomNumbers += e => player.GameEventStatus = Player.GameEventStatusEnum.Cancelled;


            //player.Send("welcometogame", Users.Length); // send a message with the amount users in the game

            player.NetworkMessages.ServerPlayerHello(player.UserId, player.Username);

            ToOthers.ServerPlayerJoined(
               player.UserId, player.Username
            );
            
            // reset waves
            
            ScheduleCallback(
                delegate
                {
                


                    if (this.PlayersWithActiveWarzone == null)
                        player.NetworkMessages.ServerMessage("Game will start shortly!");
                    else
                        player.NetworkMessages.ServerMessage("Wait for the next day!");
                },
                25
            );


            // we need to resync the players now
        }

        /// <summary>When a user leaves the game instance</summary>
        public override void UserLeft(Player player)
        {
            if (PlayersWithActiveWarzone != null)
                PlayersWithActiveWarzone.Remove(player);

            var ToOthers =
                new SharedClass1.RemoteMessages
                {
                    Send = q => this.SendOthers(player.UserId, q.i, q.args)
                };

            ToOthers.ServerPlayerLeft(player.UserId, player.Username);
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


        public void SendOthers(int id, Shared.SharedClass1.Messages type, params object[] e)
        {
            foreach (var v in Users)
            {
                if (v.UserId != id)
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

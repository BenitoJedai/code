using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using FlashMinesweeper.ActionScript;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;
using FlashMinesweeper.ActionScript.Client.Assets;
using ScriptCoreLib.ActionScript.Nonoba.api;
using FlashMinesweeper.ActionScript.Shared;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashMinesweeper.ActionScript.Client
{
    [Script]
    [SWF(backgroundColor = 0xc0c0c0,
        width = DefaultControlWidth + NonobaChatWidth,
        height = DefaultControlHeight
        )]
    public partial class TeamPlay : Sprite
    {
        /* Let's make another game for Nonoba Multplayer API. We already
         * have a working minesweeper base. Lets extend it as less as we
         * can for the moment to get the multiplayer version online as soon
         * as possible. It will be a non-stop deathmatch game :). The
         * achievement and score API should also be used.
         * 
         * We will need to extract the message interface from flashtowerdefense.
         * 
         * TeamPlay means you get one field to play at the same time.
         * 
         * After we get join, left messages to work, we need to add cursors.
         * The map also needs to be synced.
        */

        private const int FieldXCount = 22;
        private const int FieldYCount = 22;

        public const int DefaultControlWidth = FieldXCount * FlashMinesweeper.MineButton.Width;
        public const int DefaultControlHeight = FieldYCount * FlashMinesweeper.MineButton.Height;

        public const int NonobaChatWidth = 200;

        public const uint ColorGreen = 0x00ff00;
        public const uint ColorRed = 0xff0000;
        public const uint ColorBlack = 0x000000;
        public const uint ColorWhite = 0xffffff;
        public const uint ColorBlue = 0x0000ff;
        public const uint ColorBlueDark = 0x000080;
        public const uint ColorBlueLight = 0x9090ff;

        public Action<string> ShowMessage;

        public MineField Field;

        public TeamPlay()
        {
            this.InvokeWhenStageIsReady(Initialize);


        }

        SharedClass1.IEvents Events;
        SharedClass1.IMessages Messages;

        public static void SendMessage(Connection c, SharedClass1.Messages m, params object[] e)
        {
            var i = new Message(((int)m).ToString());

            foreach (var z in e)
            {
                i.Add(z);
            }

            c.Send(i);
        }

        private void Initialize()
        {
            var c = NonobaAPI.MakeMultiplayer(stage
                //, "192.168.3.102"
                //, "192.168.1.119"
                );


            var MyEvents = new SharedClass1.RemoteEvents();
            var MyMessages = new SharedClass1.RemoteMessages
            {
                Send = e => SendMessage(c, e.i, e.args)
            };



            Events = MyEvents;
            Messages = MyMessages;

            this.InitializeEvents();

            #region Dispatch
            Func<Message, bool> Dispatch =
               e =>
               {
                   var type = (SharedClass1.Messages)int.Parse(e.Type);

                   if (MyEvents.Dispatch(type,
                         new SharedClass1.RemoteEvents.DispatchHelper
                         {
                             GetLength = i => e.length,
                             GetInt32 = e.GetInt,
                             GetDouble = e.GetNumber,
                             GetString = e.GetString,
                         }
                     ))
                       return true;

                   return false;
               };
            #endregion


            #region message
            c.Message +=
                e =>
                {
                    InitializeMap();


                    var Dispatched = false;

                    try
                    {
                        Dispatched = Dispatch(e.message);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("error at dispatch " + e.message.Type);

                        throw ex;
                    }

                    if (Dispatched)
                        return;

                    System.Console.WriteLine("not on dispatch: " + e.message.Type);

                };
            #endregion

            c.Disconnect +=
                 delegate
                 {
                 };

            c.Init +=
                delegate
                {
                    InitializeMap();
                };


        }


        private void SendMap()
        {
            var a = new int[FieldXCount * FieldYCount];

            for (int i = 0; i < Field.Buttons.Length; i++)
            {
                var v = Field.Buttons[i];

                var f = new BitField();

                f[1] = v.IsMined;
                f[2] = v.IsFlag;
                f[3] = v.Enabled;

                a[i] = f.Value;
            }

            GameIsLocked.Value = false;

            Messages.UnlockGame();
            Messages.SendMap(a);
        }

    }

}

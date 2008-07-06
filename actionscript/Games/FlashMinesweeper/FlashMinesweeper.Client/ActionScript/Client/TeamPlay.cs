﻿using System;
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

namespace FlashMinesweeper.ActionScript.Client
{
    [Script]
    [SWF(backgroundColor = 0xc0c0c0,
        width = DefaultControlWidth + NonobaChatWidth,
        height = DefaultControlHeight
        )]
    public class TeamPlay : Sprite
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

            if (stage == null)
                this.addedToStage +=
                    delegate
                    {
                        Initialize();
                    };
            else
                Initialize();


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

        private void InitializeEvents()
        {

            var MyIdentity = default(SharedClass1.RemoteEvents.ServerPlayerHelloArguments);

            // events after init
            Events.ServerPlayerHello +=
                e =>
                {
                    MyIdentity = e;

                    ShowMessage("Howdy, " + e.name);
                };

            Events.ServerPlayerJoined +=
              e =>
              {
                  ShowMessage("Player joined - " + e.name);


                  Messages.PlayerAdvertise(e.name);
              };

            Events.ServerPlayerLeft +=
              e =>
              {
                  ShowMessage("Player left - " + e.name);
              };

            Events.UserPlayerAdvertise +=
                e =>
                {
                    ShowMessage("Player already here - " + e.name);
                };

            var Cursors = new Dictionary<int, Shape>();


            Events.UserMouseMove +=
                e =>
                {
                    var s = default(Shape);

                    if (Cursors.ContainsKey(e.color))
                        s = Cursors[e.color];
                    else
                    {
                        s = new Shape
                            {
                                filters = new[] { new DropShadowFilter() }
                            };


                        var g = s.graphics;

                        g.beginFill((uint)e.color);
                        g.moveTo(0, 0);
                        g.lineTo(14, 14);
                        g.lineTo(0, 20);
                        g.lineTo(0, 0);
                        g.endFill();

                        Cursors[e.color] = s;
                    };

                    s.AttachTo(this).MoveTo(e.x, e.y);
                };

            Events.UserMouseOut +=
                e =>
                {
                    if (Cursors.ContainsKey(e.color))
                    {
                        Cursors[e.color].Orphanize();
                    }
                };

            Events.ServerSendMap +=
                e =>
                {
                    SendMap();
                };

            Events.UserSendMap +=
                e =>
                {
                    for (int i = 0; i < Field.Buttons.Length; i++)
                    {
                        var v = Field.Buttons[i];

                        var x = new BitField { Value = e.buttons[i] };


                        v.IsMined = x[1];
                        v.IsFlag = x[2];
                        v.Enabled = x[3];

                    }

                    foreach (var v in Field.Buttons)
                    {
                        if (v.Enabled)
                            v.Update();
                        else
                            v.RevealLocal();
                    }
                };

            Events.UserSetFlag +=
                e =>
                {
                    Field.Buttons[e.button].IsFlag = e.value == 1;
                    Field.Buttons[e.button].Update();
                    Field.Buttons[e.button].snd_flag.play();
                    Field.Buttons[e.button].CheckComplete();
                };

            Events.UserReveal +=
                e =>
                {
                    Field.Buttons[e.button].RevealOrExplode();
                    Field.Buttons[e.button].CheckComplete();
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

            Messages.SendMap(a);
        }

        #region InitializeMap
        bool InitializeMapDone;

        private void InitializeMap()
        {
            if (InitializeMapDone)
                return;

            InitializeMapDone = true;

            stage.scaleMode = StageScaleMode.NO_SCALE;

            Field = new MineField(FieldXCount, FieldYCount, 0.15);


            #region Messages
            var ActiveMessages = new List<TextField>();
            var ShowMessageNow = default(Action<string, Action>);

            ShowMessageNow =
                (MessageText, Done) =>
                {

                    var p = new TextField
                    {
                        textColor = ColorWhite,
                        background = true,
                        backgroundColor = ColorBlack,
                        filters = new[] { new GlowFilter(ColorBlack) },
                        autoSize = TextFieldAutoSize.LEFT,
                        text = MessageText,
                        mouseEnabled = false
                    };

                    var y = DefaultControlHeight - p.height - 32;

                    p.AddTo(ActiveMessages).AttachTo(this).MoveTo((DefaultControlWidth - p.width) / 2, DefaultControlHeight);

                    Sounds.snd_message.ToSoundAsset().play();

                    var MessagesToBeMoved = (from TheMessage in ActiveMessages select new { TheMessage, y = TheMessage.y - TheMessage.height }).ToArray();



                    (1000 / 24).AtInterval(
                        t =>
                        {
                            foreach (var i in MessagesToBeMoved)
                            {
                                if (i.TheMessage.y > i.y)
                                    i.TheMessage.y -= 4;

                            }

                            p.y -= 4;

                            if (p.y < y)
                            {
                                t.stop();

                                if (Done != null)
                                    Done();

                                9000.AtDelayDo(
                                    () => p.RemoveFrom(ActiveMessages).FadeOutAndOrphanize(1000 / 24, 0.21)
                                );
                            }
                        }
                    );
                };


            var QueuedMessages = new Queue<string>();

            this.ShowMessage =
                Text =>
                {
                    if (QueuedMessages.Count > 0)
                    {
                        QueuedMessages.Enqueue(Text);
                        return;
                    }

                    // not busy
                    QueuedMessages.Enqueue(Text);

                    var NextQueuedMessages = default(Action);

                    NextQueuedMessages =
                        () => ShowMessageNow(QueuedMessages.Peek(),
                            delegate
                            {
                                QueuedMessages.Dequeue();

                                if (QueuedMessages.Count > 0)
                                    NextQueuedMessages();
                            }
                        );

                    NextQueuedMessages();
                };
            #endregion

            ShowMessage("Ctrl-click for flag!");

            Field.OnBang +=
                LocalPlayer =>
                {
                    if (LocalPlayer)
                        ShowMessage("Booom! -20");
                    else
                        ShowMessage("Boom! -10");
                };

            Field.OnComplete +=
                   LocalPlayer =>
                   {
                       if (LocalPlayer)
                           ShowMessage("Yay! +150");
                       else
                           ShowMessage("Yay! +100");
                   };

            Field.IsFlagChanged +=
                (button, value) =>
                {
                    Messages.SetFlag(button, value.ToInt32());
                };

            Field.OnReveal +=
                (button) =>
                {
                    Messages.Reveal(button);
                };

            Field.GameResetByLocalPlayer +=
                delegate
                {
                    SendMap();
                    ShowMessage("Try not to blow up, okay?");
                };

            Field.AttachTo(this);


            var MyColor = 0xffffff.Random().ToInt32();

            Field.mouseMove +=
                e =>
                {

                    Messages.MouseMove(e.stageX.ToInt32(), e.stageY.ToInt32(), MyColor);
                };

            Field.mouseOut +=
                e =>
                {
                    if (e.relatedObject == Field)
                        Messages.MouseOut(MyColor);
                };
        }

        #endregion

    }

    [Script]
    class BitField
    {
        public int Value;

        public bool this[int index]
        {
            get
            {
                return ((Value >> index) & 1) == 1;
            }
            set
            {
                Value |= 1 << index;

                if (!value)
                    Value ^= 1 << index;
            }
        }
    }
}

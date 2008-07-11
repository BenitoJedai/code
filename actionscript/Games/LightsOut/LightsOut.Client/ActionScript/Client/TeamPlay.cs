using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LightsOut.ActionScript.Client.Assets;
using LightsOut.ActionScript.Shared;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Nonoba.api;

namespace LightsOut.ActionScript.Client
{
    [Script, ScriptApplicationEntryPoint(Width = DefaultControlWidth + NonobaChatWidth, Height = DefaultControlHeight)]
    [SWF(width = DefaultControlWidth + NonobaChatWidth, height = DefaultControlHeight, backgroundColor = 0)]
    public partial class TeamPlay : Sprite
    {
        public const int DefaultControlWidth = LightsOut.ControlWidth;
        public const int DefaultControlHeight = LightsOut.ControlWidth;

        public const uint ColorGreen = 0x00ff00;
        public const uint ColorRed = 0xff0000;
        public const uint ColorBlack = 0x000000;
        public const uint ColorWhite = 0xffffff;
        public const uint ColorBlue = 0x0000ff;
        public const uint ColorBlueDark = 0x000080;
        public const uint ColorBlueLight = 0x9090ff;


        public const int NonobaChatWidth = 200;


        public TeamPlay()
        {
            this.InvokeWhenStageIsReady(Initialize);
        }

        internal void Initialize()
        {
            var c = NonobaAPI.MakeMultiplayer(stage
                //, "192.168.3.102"
                //, "192.168.1.119"
              );

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

                                500.AtDelayDo(
                                    delegate
                                    {
                                        p.alpha = 0.5;


                                    }
                                );
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


            var MyEvents = new SharedClass1.RemoteEvents();
            Events = MyEvents;
            Messages = new SharedClass1.RemoteMessages
            {
                Send = e => SendMessage(c, e.i, e.args)
            };

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
                     ShowMessage("Disconnected!");
                 };

            c.Init +=
                delegate
                {
                    InitializeMap();
                };
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
    }
}

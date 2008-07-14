using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlashMinesweeper.ActionScript;
using FlashMinesweeper.ActionScript.Client.Assets;
using FlashMinesweeper.ActionScript.Shared;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.Nonoba.api;

namespace FlashMinesweeper.ActionScript.Client
{

    partial class TeamPlay
    {

        #region InitializeMap
        bool InitializeMapDone;

        private void InitializeMap()
        {
            if (InitializeMapDone)
                return;

            InitializeMapDone = true;

            stage.scaleMode = StageScaleMode.NO_SCALE;

            var MapContainer = new Sprite().AttachTo(this);

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

            ShowMessage("Ctrl-click for flag!");

            Action<int> AddScore =
                e =>
                {
                    if (e > 0)
                    {
                        if (e < 5)
                        {
                            //   ShowMessage("+" + e);
                        }
                        else
                            ShowMessage("Yay! +" + e);
                    }
                    else
                        ShowMessage("Booom! -" + e);

                    Messages.AddScore(e);

                };

            var DisallowClicks = default(Timer);
            var DisallowClicksMultiplierMin = 2;
            var DisallowClicksMultiplier = DisallowClicksMultiplierMin;


            Field.OnBang +=
                LocalPlayer =>
                {
                    ServerSendMapEnabled = false;

                    if (LocalPlayer)
                    {
                        DisallowClicksMultiplier++;


                        if (this.CoPlayerNames.Count > 0)
                        {
                            var timeout = (DisallowClicksMultiplier * 2 * (this.CoPlayerNames.Count + 1));

                            Field.DisallowClicks = true;

                            if (DisallowClicks != null && DisallowClicks.running)
                            {
                                DisallowClicks.stop();
                            }

                            DisallowClicks = (timeout * 1000).AtDelayDo(
                                () =>
                                {
                                    Field.DisallowClicks = false;

                                    ShowMessage("Your penalty multiplier is now " + DisallowClicksMultiplier);
                                }
                            );

                            ShowMessage("You must wait " + timeout + " seconds to resume!");
                        }

                        AddScore(-8);
                    }
                    else
                        AddScore(-4);
                };

            var LocalPlayerFieldsOpened = 0;

            Field.OnComplete +=
               LocalPlayer =>
               {
                   DisallowClicksMultiplier = DisallowClicksMultiplierMin;

                   ServerSendMapEnabled = false;

                   if (LocalPlayer)
                   {
                       if (LocalPlayerFieldsOpened < 10)
                       {
                           AddScore(100);
                       }
                       else
                       {
                           AddScore(150);
                           Messages.AwardAchievementFirstMinefieldComplete();
                       }

                   }
                   else
                   {
                       // give only half of points if a coplayer was not active
                       if (LocalPlayerFieldsOpened < 10)
                           AddScore(50);
                       else
                           AddScore(100);
                   }
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

            Field.GameReset +=
                IsLocalPlayer =>
                {
                    LocalPlayerFieldsOpened = 0;

                    if (!IsLocalPlayer)
                    {
                        // start a timer to generate a map on our own
                        CrudeMapReset = (4000 + 4000.Random()).ToInt32().AtDelayDo(
                            delegate
                            {
                                ShowMessage("Resetting map!");
                                Field.Reset();

                                SendMap();



                                StopCrudeMapReset();
                            }
                        );
                    }
                };

            Field.GameResetByLocalPlayer +=
                delegate
                {
                    SendMap();
                    ShowMessage("Try not to blow up, okay?");
                    StopCrudeMapReset();
                };

            Field.OneStepClosedToTheEnd +=
                LocalPlayer =>
                {
                    if (LocalPlayer)
                    {
                        LocalPlayerFieldsOpened++;

                        // every 8 clicks gets your penalty down by one
                        if (LocalPlayerFieldsOpened % 8 == 0)
                        {
                            if (DisallowClicksMultiplier > DisallowClicksMultiplierMin)
                            {
                                DisallowClicksMultiplier = (DisallowClicksMultiplier - 1).Max(DisallowClicksMultiplierMin);

                                ShowMessage("Your penalty multiplier is " + DisallowClicksMultiplier);
                            }
                        }

                        if (LocalPlayerFieldsOpened < 10)
                            AddScore(1);
                        else
                            AddScore(2);
                    }
                };

            Field.AttachTo(MapContainer);


            var MyColor = 0xffffff.Random().ToInt32();

            Field.mouseMove +=
                e =>
                {

                    Messages.MouseMove(e.stageX.ToInt32(), e.stageY.ToInt32(), MyColor);
                };


            //// menu for private game

            //MapContainer.filters = new[] { new BlurFilter() };
            //MapContainer.mouseChildren = false;

            //var LobbyMeny = new Sprite().AttachTo(this);

            //// http://snipplr.com/view/7050/as3-creating-a-gradient-rectangle/


            //var colors = new uint[2];
            //colors[0] = 1;
            //colors[1] = 0xffffff;

            //var alphas = new double[2];

            //alphas[0] = 0.5;
            //alphas[1] = 0.2;

            //var ratios = new int[2];

            //alphas[0] = 1;
            //alphas[1] = 255;


            //LobbyMeny.graphics.beginGradientFill("linear", colors, alphas, ratios);
            //LobbyMeny.graphics.drawRect(0, 0, stage.width, 200);


            //var Password = new TextField
            //{
            //    defaultTextFormat = new TextFormat
            //    {
            //        size = 24
            //    },
            //    text = "For private game\nenter your password:",
            //    filters = new[] { new GlowFilter(0xffffff) },
            //    autoSize = TextFieldAutoSize.LEFT,
            //    selectable = false,
            //}.MoveTo(8, 8).AttachTo(this);

            //var PasswordInput = new TextField
            //{
            //    defaultTextFormat = new TextFormat
            //    {
            //        size = 24
            //    },

            //    autoSize = TextFieldAutoSize.LEFT,
            //    // predefined passwords
            //    // http://www.modernlifeisrubbish.co.uk/article/top-10-most-common-passwords
            //    text = new[] { "thomas", "arsenal", "monkey", "charlie", "letmein", "123" }.Random(),
            //    type = TextFieldType.INPUT,
            //    background = true,
            //    backgroundColor = 0xffffff,
            //    border = true,
            //    borderColor = 0x909090,
            //};

            //var h = PasswordInput.height;

            //PasswordInput.autoSize = TextFieldAutoSize.NONE;
            //PasswordInput.width = Password.width;
            //PasswordInput.height = h;
            //PasswordInput.MoveTo(Password.x, Password.y + 8 + Password.height).AttachTo(LobbyMeny);

        }

        #endregion

        public Timer CrudeMapReset;
    }

}

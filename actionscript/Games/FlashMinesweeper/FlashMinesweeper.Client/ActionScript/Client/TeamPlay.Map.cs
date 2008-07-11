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
                            ShowMessage("+" + e);
                        else
                            ShowMessage("Yay! +" + e);
                    }
                    else
                        ShowMessage("Booom! -" + e);

                    Messages.AddScore(e);

                };

            Field.OnBang +=
                LocalPlayer =>
                {
                    if (LocalPlayer)
                        AddScore(-8);
                    else
                        AddScore(-4);
                };

            var LocalPlayerFieldsOpened = 0;

            Field.OnComplete +=
                   LocalPlayer =>
                   {
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
                delegate
                {
                    LocalPlayerFieldsOpened = 0;

                    // start a timer to generate a map on our own
                };

            Field.GameResetByLocalPlayer +=
                delegate
                {
                    SendMap();
                    ShowMessage("Try not to blow up, okay?");
                };

            Field.OneStepClosedToTheEnd +=
                LocalPlayer =>
                {
                    if (LocalPlayer)
                    {
                        LocalPlayerFieldsOpened++;

                        if (LocalPlayerFieldsOpened < 10)
                            AddScore(1);
                        else
                            AddScore(2);
                    }
                };

            Field.AttachTo(this);


            var MyColor = 0xffffff.Random().ToInt32();

            Field.mouseMove +=
                e =>
                {

                    Messages.MouseMove(e.stageX.ToInt32(), e.stageY.ToInt32(), MyColor);
                };

            //Field.mouseOut +=
            //    e =>
            //    {
            //        if (e.relatedObject == Field)
            //            Messages.MouseOut(MyColor);
            //    };
        }

        #endregion


    }

}

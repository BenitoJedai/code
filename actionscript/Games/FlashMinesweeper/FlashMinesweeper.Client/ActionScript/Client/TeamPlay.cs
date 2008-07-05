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

namespace FlashMinesweeper.ActionScript.Client
{
    [Script]
    [SWF(backgroundColor = 0xc0c0c0,
        width = DefaultControlWidth,
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
        */

        private const int FieldXCount = 22;
        private const int FieldYCount = 22;

        public const int DefaultControlWidth = FieldXCount * FlashMinesweeper.MineButton.Width;
        public const int DefaultControlHeight = FieldYCount * FlashMinesweeper.MineButton.Height;


        public const uint ColorGreen = 0x00ff00;
        public const uint ColorRed = 0xff0000;
        public const uint ColorBlack = 0x000000;
        public const uint ColorWhite = 0xffffff;
        public const uint ColorBlue = 0x0000ff;
        public const uint ColorBlueDark = 0x000080;
        public const uint ColorBlueLight = 0x9090ff;

        public readonly Action<string> ShowMessage;

        public readonly MineField Field;

        public TeamPlay()
        {
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
                () => ShowMessage("Booom!");

            Field.AttachTo(this);

        }
    }
}

using come2play_as3.api.auto_generated;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using System;

namespace come2play
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {

            // see also: http://www.come2play.com/API_inner.asp?f=1&newsid=357

            //AssetsLibraryInfo.ActionScriptOutput_contains_110456_bytes = 0;

            var log = new TextField
            {
                text = "loading...",
                multiline = true,
                autoSize = TextFieldAutoSize.LEFT
            }.AttachTo(this).MoveTo(8, 64);

            var ButtonPlayTheGame = new Sprite
            {
                useHandCursor = true,
                buttonMode = true,
                alpha = 0.6
            }.AttachTo(this);

            ButtonPlayTheGame.graphics.beginFill(0x00ff00);
            ButtonPlayTheGame.graphics.drawRect(0, 0, 96, 48);

            var ButtonSurrender = new Sprite
            {
                useHandCursor = true,
                buttonMode = true,
                alpha = 0.6
            }.AttachTo(this);

            ButtonSurrender.graphics.beginFill(0xff0000);
            ButtonSurrender.graphics.drawRect(96, 0, 96, 48);

            var ButtonFullscreen = new Sprite
            {
                useHandCursor = true,
                buttonMode = true,
                alpha = 0.6
            }.AttachTo(this);

            ButtonFullscreen.graphics.beginFill(0xffff00);
            ButtonFullscreen.graphics.drawRect(96 * 2, 0, 96, 48);

            var MessageSurrender = "z.surrender";

            this.InvokeWhenStageIsReady(
                delegate
                {
                    log.appendText("\n stage ready");

                    var x = new XClientGameAPI(this);

                    x.AtMatchStarted +=
                        id =>
                        {
                            log.appendText("\n AtMatchStarted id " + id);

                            ButtonFullscreen.click +=
                                delegate
                                {
                                    log.appendText("\n to fullscreen");

                                    this.stage.SetFullscreen(true);
                                };

                            ButtonPlayTheGame.click +=
                                (e) =>
                                {
                                    var z = "click " + e.localX + ";" + e.localY;

                                    log.appendText("\n " + z);

                                    var u = UserEntry.create(
                                        key: "z.click",
                                        value: z
                                    );

                                    x.doStoreState(
                                        new UserEntry[] { u }
                                    );
                                };


                            ButtonSurrender.click +=
                                (e) =>
                                {
                                    var z = "surrender " + e.localX + ";" + e.localY;

                                    log.appendText("\n " + z);

                                    var u = UserEntry.create(
                                        key: MessageSurrender,
                                        value: z
                                    );

                                    x.doStoreState(
                                        new[] { u }
                                    );
                                };

                            x.AtMatchEnded +=
                                (e) =>
                                {
                                    log.appendText("\n AtMatchEnded e length " + e.Length);

                                };

                            x.AtStateChanged +=
                                (e) =>
                                {
                                    log.appendText("\n AtStateChanged e length " + e.Length);

                                    e.WithEach(
                                        k =>
                                        {
                                            var key = (string)k.key;

                                            log.appendText("\n AtStateChanged: " + new { key, k.value, k.storedByUserId });

                                            if (key == MessageSurrender)
                                            {
                                                log.appendText("\n surrender: " + k.storedByUserId);

                                                var u = PlayerMatchOver.create(k.storedByUserId, 0, 0);

                                                x.doAllEndMatch(
                                                    new[] { u }
                                                );
                                            }
                                        }
                                    );
                                };
                        };

                    log.appendText("\n doRegisterOnServer");

                    x.doRegisterOnServer();


                }
            );

        }

    }



    class XClientGameAPI : ClientGameAPI
    {
        public XClientGameAPI(DisplayObjectContainer c)
            : base(c)
        {

        }

        public event Action<int> AtMatchStarted;
        public event Action<ServerEntry[]> AtStateChanged;
        public event Action<int[]> AtMatchEnded;

        public override void gotMatchEnded(object[] finishedPlayerIds)
        {
            AtMatchEnded(
                (int[])(object)finishedPlayerIds
            );
        }

        public override void gotStateChanged(object[] serverEntries)
        {
            AtStateChanged((ServerEntry[])serverEntries);
        }

        public override void gotMatchStarted(object[] allPlayerIds, object[] finishedPlayerIds, object[] serverEntries)
        {
            var myUserId = (int)come2play_as3.api.auto_copied.T.custom(CUSTOM_INFO_KEY_myUserId, -1);

            AtMatchStarted(myUserId);
        }


    }
}

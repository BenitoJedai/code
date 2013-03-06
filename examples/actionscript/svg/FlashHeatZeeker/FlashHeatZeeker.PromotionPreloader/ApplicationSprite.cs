using FlashHeatZeeker.CoreAudio.Library;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared;
using System;

namespace FlashHeatZeeker.PromotionPreloader
{

    [Frame(typeof(ApplicationSpritePreloader))]
    [SWF(backgroundColor = 0x9A6C46, width = 800, height = 600, frameRate = 60)]
    public sealed class ApplicationSprite : Sprite, IAlternator
    {
        public string Alternate { get; set; }

        public ApplicationSprite()
        {

            this.InvokeWhenPromotionIsReady();
        }

    }

    public static class PromotionPreloaderService
    {
        public static void InvokeWhenPromotionIsReady<T>(this T context, Action yield = null)
            where T : DisplayObjectContainer, IAlternator
        {
            var sb = new Soundboard();


            sb.loopheartbeat3.LeftVolume = 0.1;
            sb.loopheartbeat3.RightVolume = 0.1;
            var heart = sb.loopheartbeat3.Sound.play();



            context.InvokeWhenStageIsReady(
                delegate
                {
                    Bitmap i = null;

                    if (context.Alternate == "green")
                    {
                        sb.snd_nightvision.play();

                        i = new ActionScript.Images.Promotion3D_iso1_tiltshift_720p_green();


                    }
                    else
                    {
                        sb.snd_SelectWeapon.play();

                        i = new ActionScript.Images.Promotion3D_iso1_tiltshift_720p();


                    }

                    i.AttachTo(context);
                    i.x = (i.stage.stageWidth - 1280) / 2;
                    i.y = (i.stage.stageHeight - 720) / 2;

                    context.stage.resize += delegate
                    {
                        if (i == null)
                            return;


                        i.x = (i.stage.stageWidth - 1280) / 2;
                        i.y = (i.stage.stageHeight - 720) / 2;
                    };




                    {
                        var tt = new ScriptCoreLib.ActionScript.flash.utils.Timer(1000 / 15);


                        tt.timer +=
                            delegate
                            {
                                if (i.alpha > 0.07)
                                {
                                    i.alpha -= 0.07;
                                }
                                else
                                {
                                    i.Orphanize();
                                    i = null;
                                    tt.stop();

                                    if (yield != null)
                                        yield();
                                }
                            };

                        var t = new ScriptCoreLib.ActionScript.flash.utils.Timer(4000, 1);

                        t.timerComplete +=
                             delegate
                             {
                                 tt.start();
                             };

                        t.start();

                    }

                    #region heart.stop
                    {
                        var tt = new ScriptCoreLib.ActionScript.flash.utils.Timer(1000 / 5);


                        tt.timer +=
                            delegate
                            {
                                if (sb.loopheartbeat3.MasterVolume > 0.07)
                                {
                                    sb.loopheartbeat3.MasterVolume -= 0.07;
                                }
                                else
                                {
                                    heart.stop();
                                    tt.stop();
                                }
                            };

                        var t = new ScriptCoreLib.ActionScript.flash.utils.Timer(1500, 1);

                        t.timerComplete +=
                             delegate
                             {
                                 sb.snd_letsgo.play();
                                 tt.start();
                             };

                        t.start();
                    }
                    #endregion

                }
            );
        }
    }


    public interface IAlternator
    {
        string Alternate { get; set; }
    }

    // X:\jsc.svn\core\ScriptCoreLib.Mochi\ScriptCoreLib.Mochi\ActionScript\MochiLibrary\MochiAdPreloader.cs
    // X:\jsc.svn\examples\actionscript\FlashPreloader\FlashPreloader\ActionScript\FlashPreloader.cs
    public class ApplicationSpritePreloader : PreloaderSprite, IAlternator
    {
        public string Alternate { get; set; }

        [TypeOfByNameOverride]
        public virtual Type GetTargetType()
        {
            return typeof(ApplicationSprite);
        }

        public override DisplayObject CreateInstance()
        {
            return Activator.CreateInstance(GetTargetType()) as DisplayObject;
        }

        public override bool AutoCreateInstance()
        {
            return false;
        }

        // http://www.onflex.org/flexapps/components/CustomPreloader/srcview/index.html
        // http://www.onflex.org/ted/2006/07/flex-2-custom-preloaders.php
        // http://www.onflex.org/flexapps/components/CustomPreloader/PNG/srcview/
        // http://www.flexer.info/2008/02/07/very-first-flex-preloader-customization/
        // http://www.nulldesign.de/2007/11/30/as3-preloading-continued/
        // http://adventuresinactionscript.com/blog/03-04-2008/as3-preloader-with-mochiad-mochibot-simple-domain-locking-and-glossy-vista-style-pro
        // http://blog.jerrydon.com/index.php/2008/07/fullscreen-flex-preloader/
        // http://www.docsultant.com/site2/articles/flex_cmd.html
        // http://jerrydon.com/flex/fullscreenpreloader/srcview/index.html
        // http://www.ghost23.de/blogarchive/2008/04/as3-application-1.html
        // http://livedocs.adobe.com/flex/3/langref/mx/preloaders/DownloadProgressBar.html
        // http://livedocs.adobe.com/flex/3/html/help.html?content=app_container_4.html
        // http://www.bit-101.com/blog/?p=946


        public ApplicationSpritePreloader()
        {
            this.stage.align = StageAlign.TOP_LEFT;
            this.stage.scaleMode = StageScaleMode.NO_SCALE;

            if (((int)new Date().seconds % 2) == 0)
                Alternate = "green";

            var scale = 720.0 / 225.0;

            // this.stage.color 
            Bitmap i = new ActionScript.Images.Promotion3D_iso1_tiltshift_400x225();

            if (Alternate == "green")
            {
                i = new ActionScript.Images.Promotion3D_iso1_tiltshift_400x225_green();
                this.stage.color = 0x006A00;
            }
            else
            {
                this.stage.color = 0x9A6C46;

            }

            i.AttachTo(this);

            i.scaleX = scale;

            i.scaleY = scale;

            i.x = (i.stage.stageWidth - 1280) / 2;
            i.y = (i.stage.stageHeight - 720) / 2;

            this.stage.resize += delegate
            {
                if (i == null)
                    return;

                i.x = (i.stage.stageWidth - 1280) / 2;
                i.y = (i.stage.stageHeight - 720) / 2;
            };

            var te = new TextField { textColor = 0xffffff }.AttachTo(this).MoveTo(8, 8);

            this.LoadingInProgress += delegate
            {
                var per = (int)Math.Floor(100.0 * root.loaderInfo.bytesLoaded / root.loaderInfo.bytesTotal);

                //te.text = new { root.loaderInfo.bytesLoaded, root.loaderInfo.bytesTotal /*, framesLoaded, totalFrames */}.ToString();
                te.text = per + "%";
            };

            this.LoadingComplete +=
                delegate
                {
                    te.Orphanize();


                    var x = CreateInstance();
                    var a = x as IAlternator;

                    a.Alternate = Alternate;

                    var t = new ScriptCoreLib.ActionScript.flash.utils.Timer(700, 1);

                    t.timerComplete +=
                         delegate
                         {
                             x.AttachTo(this);
                             i.Orphanize();
                             i = null;
                         };

                    t.start();
                };
        }
    }
}

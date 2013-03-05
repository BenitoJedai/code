using FlashHeatZeeker.PromotionPreloader;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.TestGamePad.Library;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.sensors;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared;
using starling.core;
using System;
using System.Windows;
using System.Windows.Forms;

namespace FlashHeatZeeker.TestGamePad
{
    public class XApplicationSpritePreloader : ApplicationSpritePreloader
    {
        [TypeOfByNameOverride]
        public override Type GetTargetType()
        {
            return typeof(ApplicationSprite);
        }
    }

    [Frame(typeof(XApplicationSpritePreloader))]

    [SWF(width = 800, height = 600)]
    public sealed class ApplicationSprite : Sprite, IAlternator
    {
        public string Alternate { get; set; }

        public readonly ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSprite()
        {
            this.InvokeWhenPromotionIsReady(
                () =>
                {
                    this.stage.color = 0;
                    this.stage.frameRate = 30;
                    this.stage.frameRate = 60;

                    // http://gamua.com/starling/first-steps/
                    // http://forum.starling-framework.org/topic/starling-air-desktop-extendeddesktop-fullscreen-issue
                    Starling.handleLostContext = true;

                    var s = new Starling(
                        typeof(StarlingGameSpriteWithTestGamePad).ToClassToken(),
                        this.stage,

                        // http://forum.starling-framework.org/topic/air-34
                        profile: "baseline"
                    );


                    //Starling.current.showStats

                    s.showStats = true;

                    #region atresize
                    Action atresize = delegate
                    {
                        // http://forum.starling-framework.org/topic/starling-stage-resizing

                        s.viewPort = new ScriptCoreLib.ActionScript.flash.geom.Rectangle(
                            0, 0, this.stage.stageWidth, this.stage.stageHeight
                        );

                        s.stage.stageWidth = this.stage.stageWidth;
                        s.stage.stageHeight = this.stage.stageHeight;


                        //b2stage_centerize();
                    };

                    atresize();
                    #endregion

                    StarlingGameSpriteBase.onresize =
                        yield =>
                        {
                            this.stage.resize += delegate
                            {
                                atresize();

                                yield(this.stage.stageWidth, this.stage.stageHeight);
                            };

                            yield(this.stage.stageWidth, this.stage.stageHeight);
                        };




                    this.stage.enterFrame +=
                        delegate
                        {




                            StarlingGameSpriteBase.onframe(this.stage, s);
                        };

                    s.start();

                    #region FULL_SCREEN_INTERACTIVE
                    this.stage.keyUp +=
                         e =>
                         {
                             if (e.keyCode == (uint)System.Windows.Forms.Keys.F11)
                             {
                                 this.stage.displayState = ScriptCoreLib.ActionScript.flash.display.StageDisplayState.FULL_SCREEN_INTERACTIVE;
                             }

                             if (e.keyCode == (uint)System.Windows.Forms.Keys.F)
                             {
                                 this.stage.displayState = ScriptCoreLib.ActionScript.flash.display.StageDisplayState.FULL_SCREEN_INTERACTIVE;
                             }
                         };
                    #endregion

                    content.r.Opacity = 0;
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);



                    var InactiveOpaciy = 0.07;
                    var ActiveOpaciy = 0.5;


                    #region bind
                    Action<UIElement, Keys> bind =
                        (ui, key) =>
                        {
                            //                    Implementation not found for type import :
                            //type: System.Windows.UIElement
                            //method: Void add_MouseDown(System.Windows.Input.MouseButtonEventHandler)

                            //ui.MouseDown +=
                            ui.Opacity = InactiveOpaciy;

                            ui.MouseLeftButtonDown +=
                                (sender, e) =>
                                {
                                    ui.Opacity = ActiveOpaciy;
                                    e.Handled = true;
                                    StarlingGameSpriteWithTestGamePad.__keyDown[key] = true;
                                };

                            //ui.MouseUp +=
                            ui.MouseLeftButtonUp +=
                               (sender, e) =>
                               {
                                   ui.Opacity = InactiveOpaciy;
                                   e.Handled = true;
                                   StarlingGameSpriteWithTestGamePad.__keyDown[key] = false;
                               };

                            ui.TouchDown +=
                              (sender, e) =>
                              {
                                  ui.Opacity = ActiveOpaciy;
                                  e.Handled = true;
                                  StarlingGameSpriteWithTestGamePad.__keyDown[key] = true;
                              };

                            ui.TouchUp +=
                               (sender, e) =>
                               {
                                   ui.Opacity = InactiveOpaciy;
                                   e.Handled = true;
                                   StarlingGameSpriteWithTestGamePad.__keyDown[key] = false;
                               };
                        };
                    #endregion
                    //bind(content.up, Keys.Up);
                    //bind(content.down, Keys.Down);
                    //bind(content.left, Keys.Left);
                    //bind(content.right, Keys.Right);
                    bind(content.enter, Keys.Enter);
                    bind(content.space, Keys.Space);
                    bind(content.control, Keys.ControlKey);
                    bind(content.alt, Keys.Alt);


                    var text = new TextField().AttachTo(this);
                    text.width = 800;
                    text.y = 72;
                    text.textColor = 0xffffff;
                    text.multiline = true;

                    if (Accelerometer.isSupported)
                    {
                        StarlingGameSpriteWithTestGamePad.__keyDown.InitializeConnection(
                            text: text,


                            yield_Notify:
                                xml =>
                                {

                                    if (xml.Name.LocalName == "switchto")
                                    {
                                        var type = xml.Attribute("type").Value;

                                        StarlingGameSpriteWithTestGamePad.__switchto(type);
                                    }


                                }
                        );

                        //var a = new Accelerometer();
                        a = new Accelerometer();

                        a.update +=
                          e =>
                          {

                              //text.text = new
                              //{
                              //    x = e.accelerationX,
                              //    y = e.accelerationY,
                              //    z = e.accelerationZ
                              //}.ToString();

                              content.tiltx = 1 - (e.accelerationX + 0.5);
                              content.tilty = e.accelerationY;
                              content.tilt_update();

                              StarlingGameSpriteWithTestGamePad.__keyDown.forcex = ((Math.Abs(content.tiltx - 0.5) / 0.5) - 0.05) * 0.3;
                              StarlingGameSpriteWithTestGamePad.__keyDown.forcey = Math.Abs(content.tilty - 0.5) / 0.5;

                              content.rtiltx.Opacity = StarlingGameSpriteWithTestGamePad.__keyDown.forcex;
                              content.rtilty.Opacity = StarlingGameSpriteWithTestGamePad.__keyDown.forcey;

                              // we did do this for TestDrive example!
                              #region StarlingGameSpriteWithTestGamePad
                              if (e.accelerationX > 0.05)
                              {
                                  StarlingGameSpriteWithTestGamePad.__keyDown[Keys.Left] = true;
                                  StarlingGameSpriteWithTestGamePad.__keyDown[Keys.Right] = false;

                              }
                              else if (e.accelerationX < -0.05)
                              {
                                  StarlingGameSpriteWithTestGamePad.__keyDown[Keys.Left] = false;
                                  StarlingGameSpriteWithTestGamePad.__keyDown[Keys.Right] = true;

                              }
                              else
                              {
                                  StarlingGameSpriteWithTestGamePad.__keyDown[Keys.Left] = false;
                                  StarlingGameSpriteWithTestGamePad.__keyDown[Keys.Right] = false;
                              }
                              #endregion

                              #region accelerationY
                              if (e.accelerationY < 0.1)
                              {
                                  StarlingGameSpriteWithTestGamePad.__keyDown[Keys.Up] = false;
                                  StarlingGameSpriteWithTestGamePad.__keyDown[Keys.Down] = false;
                              }
                              else if (e.accelerationY < 0.4)
                              {
                                  StarlingGameSpriteWithTestGamePad.__keyDown[Keys.Up] = true;
                                  StarlingGameSpriteWithTestGamePad.__keyDown[Keys.Down] = false;

                              }
                              else if (e.accelerationY > 0.6)
                              {
                                  StarlingGameSpriteWithTestGamePad.__keyDown[Keys.Up] = false;
                                  StarlingGameSpriteWithTestGamePad.__keyDown[Keys.Down] = true;

                              }
                              else
                              {
                                  StarlingGameSpriteWithTestGamePad.__keyDown[Keys.Up] = false;
                                  StarlingGameSpriteWithTestGamePad.__keyDown[Keys.Down] = false;
                              }
                              #endregion



                          };

                        a.setRequestedUpdateInterval(1000 / 60);
                    }
                    else
                    {

                        StarlingGameSpriteWithTestGamePad.__keyDown.InitializeConnection(WriteMode: false, ReadMode: true, text: text);

                        //text.text = "no Accelerometer";
                    }
                }
            );
        }
        Accelerometer a;

    }
}

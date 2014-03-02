using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript.flash.events;
using System;
using ScriptCoreLib.ActionScript.flash.sensors;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;

namespace IntegrationToSkyIsland.Components
{
    internal sealed class MySprite1 : Sprite
    {
        public const int WidescreenWidth = 800;

        public const int ContentWidth = 640;
        public const int DefaultHeight = 480;

        public const int DefaultWidth = WidescreenWidth;

        public MySprite1()
        {
            // http://forums.adobe.com/thread/986019
            // wont run on AIR anymore?
            //TypeError: Error #1009: Cannot access a property or method of a null object reference.
            //    at Main/frame1()


            var q = (WidescreenWidth - ContentWidth) / 2;
            var c = new Sprite();

            c.graphics.beginFill(0);
            c.graphics.drawRect(0, 0, DefaultWidth, DefaultHeight);

            c.AttachTo(this);

            //        TypeError: Error #1009: Cannot access a property or method of a null object reference.
            //at Main/frame1()

            //V:\web\IntegrationToSkyIsland\Assets\sky_island_10899.as(10): col: 9: Error: unable to resolve '/assets/IntegrationToSkyIsland/sky-island-10899.swf' for transcoding

            //        [Embed(source = "/assets/IntegrationToSkyIsland/sky-island-10899.swf", mimeType = "application/x-shockwave-flash")]
            //        ^

            //V:\web\IntegrationToSkyIsland\Assets\sky_island_10899.as(10): col: 9: Error: Unable to transcode /assets/IntegrationToSkyIsland/sky-island-10899.swf.


            Sprite x = null;


            try
            {
                x = Assets.sky_island_10899.Source;
                x.AttachTo(this).MoveTo(q, 0);
            }
            catch
            {
                // can we skip?
            }



            #region buttons
            Action<Sprite, uint> bind = (s, keyCodeValue) =>
            {
                s.click +=
                    e =>
                    {
                        e.stopImmediatePropagation();


                        // a glitch?
                        //this.stage.fullScreenSourceRect = new ScriptCoreLib.ActionScript.flash.geom.Rectangle(
                        //     0, 0, DefaultWidth, DefaultHeight
                        // );

                        //this.stage.SetFullscreen(true);
                    };

                s.mouseDown +=
                  e =>
                  {
                      e.stopImmediatePropagation();
                      x.dispatchEvent(
                          new KeyboardEvent(KeyboardEvent.KEY_DOWN,
                              keyCodeValue: keyCodeValue /* left */
                          )
                      );
                  };

                s.mouseUp +=
                      e =>
                      {
                          e.stopImmediatePropagation();

                          x.dispatchEvent(
                             new KeyboardEvent(KeyboardEvent.KEY_UP,
                                 keyCodeValue: keyCodeValue /* left */
                             )
                         );
                      };

            };

            new Sprite().With(
                GoLeft =>
                {

                    GoLeft.graphics.beginFill(0);
                    GoLeft.graphics.drawRect(0, 0, q, DefaultHeight);
                    GoLeft.alpha = 0.9;

                    bind(GoLeft, 37);


                    GoLeft.AttachTo(this);
                }
          );

            new Sprite().With(
                GoRight =>
                {

                    GoRight.graphics.beginFill(0);
                    GoRight.graphics.drawRect(WidescreenWidth - q, 0, q, DefaultHeight);
                    GoRight.alpha = 0.9;

                    bind(GoRight, 39);

                    GoRight.AttachTo(this);
                }
            );

            new Sprite().With(
                GoUp =>
                {

                    GoUp.graphics.beginFill(0);
                    GoUp.graphics.drawRect(0, DefaultHeight - q, WidescreenWidth, q);
                    GoUp.alpha = 0.05;

                    bind(GoUp, 38);

                    GoUp.AttachTo(this);
                }
            );
            #endregion


            //var t = new TextField().AttachTo(this);
            //t.autoSize = TextFieldAutoSize.LEFT;

            #region Accelerometer - AIR?
            if (Accelerometer.isSupported)
            {
                a = new Accelerometer();

                var ax = 0.0;
                var az = 0.0;

                a.update +=
                  e =>
                  {
                      var wx = 0.15;
                      var wz = 0.5;

                      if (e.accelerationZ > wz)
                      {
                          if (az > wz)
                          {
                              // nop
                          }
                          else
                          {
                              x.dispatchEvent(
                                  new KeyboardEvent(KeyboardEvent.KEY_DOWN,
                                      keyCodeValue: 38
                                  )
                              );
                          }
                      }
                      else
                      {
                          if (az > wz)
                          {
                              x.dispatchEvent(
                                 new KeyboardEvent(KeyboardEvent.KEY_UP,
                                     keyCodeValue: 38
                                 )
                             );
                          }
                      }

                      if (e.accelerationX < -wx)
                      {
                          if (ax < -wx)
                          {
                              // nop
                          }
                          else
                          {
                              x.dispatchEvent(
                                  new KeyboardEvent(KeyboardEvent.KEY_DOWN,
                                      keyCodeValue: 39
                                  )
                              );
                          }
                      }
                      else
                      {
                          if (ax < -wx)
                          {
                              x.dispatchEvent(
                                 new KeyboardEvent(KeyboardEvent.KEY_UP,
                                     keyCodeValue: 39
                                 )
                             );
                          }
                      }


                      if (e.accelerationX > wx)
                      {
                          if (ax > wx)
                          {
                              // nop
                          }
                          else
                          {
                              x.dispatchEvent(
                                  new KeyboardEvent(KeyboardEvent.KEY_DOWN,
                                      keyCodeValue: 37
                                  )
                              );
                          }
                      }
                      else
                      {
                          if (ax > wx)
                          {
                              x.dispatchEvent(
                                 new KeyboardEvent(KeyboardEvent.KEY_UP,
                                     keyCodeValue: 37
                                 )
                             );
                          }
                      }

                      ax = e.accelerationX;
                      az = e.accelerationZ;

                      //t.text = "" + new
                      //{
                      //    //Multitouch.maxTouchPoints,
                      //    ax,
                      //    e.accelerationY,
                      //    e.accelerationZ
                      //};
                  };

                a.setRequestedUpdateInterval(1000 / 60);
            }
            #endregion


        }

        Accelerometer a;

    }
}

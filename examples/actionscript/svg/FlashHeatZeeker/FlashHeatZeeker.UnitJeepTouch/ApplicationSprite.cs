using FlashHeatZeeker.UnitJeepControl.Library;
using net.hires.debug;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using System;
using System.Windows;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitJeepTouch
{
    [SWF(backgroundColor = 0xA26D41, width = 800, height = 600)]
    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content1 = new ApplicationCanvas();

        // 30 fps in chrome?
        // 60 fps in air tester?

        public ApplicationSprite()
        {
            // http://away3d.com/forum/viewthread/1976/
            // http://forum.starling-framework.org/topic/works-on-web-browser-but-not-on-ios

            //Error: Error #3709: The depthAndStencil flag in the application descriptor must match the enableDepthAndStencil Boolean passed to configureBackBuffer on the Context3D object.
            //    at flash.display3D::Context3D/configureBackBuffer()
            //    at Function/http://adobe.com/AS3/2006/builtin::apply()
            //    at starling.core::Starling/configureBackBuffer()
            //    at starling.core::Starling/updateViewPort()
            //    at starling.core::Starling/initializeGraphicsAPI()
            //    at starling.core::Starling/initialize()
            //    at starling.core::Starling/onContextCreated()



            var content0 = new FlashHeatZeeker.UnitJeepControl.ApplicationSprite();

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150110/hz
            content0.AttachToSprite();

            content1.r.Opacity = 0;

            #region bind
            Action<UIElement, Keys> bind =
                (ui, key) =>
                {
                    //                    Implementation not found for type import :
                    //type: System.Windows.UIElement
                    //method: Void add_MouseDown(System.Windows.Input.MouseButtonEventHandler)

                    //ui.MouseDown +=
                    ui.MouseLeftButtonDown +=
                        (sender, e) =>
                        {
                            ui.Opacity = 1;
                            e.Handled = true;
                            StarlingGameSpriteWithJeepControl.__keyDown[key] = true;
                        };

                    //ui.MouseUp +=
                    ui.MouseLeftButtonUp +=
                       (sender, e) =>
                       {
                           ui.Opacity = 0.5;
                           e.Handled = true;
                           StarlingGameSpriteWithJeepControl.__keyDown[key] = false;
                       };

                    ui.TouchDown +=
                      (sender, e) =>
                      {
                          ui.Opacity = 1;
                          e.Handled = true;
                          StarlingGameSpriteWithJeepControl.__keyDown[key] = true;
                      };

                    ui.TouchUp +=
                       (sender, e) =>
                       {
                           ui.Opacity = 0.5;
                           e.Handled = true;
                           StarlingGameSpriteWithJeepControl.__keyDown[key] = false;
                       };
                };
            #endregion

            bind(content1.up, Keys.Up);
            bind(content1.down, Keys.Down);
            bind(content1.left, Keys.Left);
            bind(content1.right, Keys.Right);


            this.InvokeWhenStageIsReady(
                () =>
                {
                    content1.AttachToContainer(this);
                    content1.AutoSizeTo(this.stage);


                    // http://www.flare3d.com/support/index.php?topic=1101.0
                    //this.addChild(new Stats());
                    // public static T AttachToSprite<T>(this T e) where T : DisplayObject;
                    // Error	1	The type 'net.hires.debug.Stats' cannot be used as type parameter 'T'
                    // in the generic type or method 'System.Windows.Forms.ControlExtensions.MoveTo<T>(T, int, int)'.
                    //There is no implicit reference conversion from 'net.hires.debug.Stats' to 'System.Windows.Forms.Control'.	X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.UnitJeepTouch\ApplicationSprite.cs	101	21	FlashHeatZeeker.UnitJeepTouch
                    //new net.hires.debug.Stats().AttachToSprite().MoveTo(0, 48);
                    new net.hires.debug.Stats().AttachToSprite().y = 48;
                }
            );
        }

    }
}

using FlashHeatZeeker.TestDrivers.Library;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using System;
using System.Windows;
using System.Windows.Forms;

namespace FlashHeatZeeker.TestDriversTouch
{
    [SWF(backgroundColor = 0xA26D41)]
    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content1 = new ApplicationCanvas();

        public ApplicationSprite()
        {
            var content0 = new FlashHeatZeeker.TestDrivers.ApplicationSprite();

            content0.AttachTo(this);

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
                            StarlingGameSpriteWithTestDrivers.__keyDown[(int)key] = new object();
                        };

                    //ui.MouseUp +=
                    ui.MouseLeftButtonUp +=
                       (sender, e) =>
                       {
                           ui.Opacity = 0.5;
                           e.Handled = true;
                           StarlingGameSpriteWithTestDrivers.__keyDown[(int)key] = null;
                       };

                    ui.TouchDown +=
                      (sender, e) =>
                      {
                          ui.Opacity = 1;
                          e.Handled = true;
                          StarlingGameSpriteWithTestDrivers.__keyDown[(int)key] = new object();
                      };

                    ui.TouchUp +=
                       (sender, e) =>
                       {
                           ui.Opacity = 0.5;
                           e.Handled = true;
                           StarlingGameSpriteWithTestDrivers.__keyDown[(int)key] = null;
                       };
                };
            #endregion

            bind(content1.up, Keys.Up);
            bind(content1.down, Keys.Down);
            bind(content1.left, Keys.Left);
            bind(content1.right, Keys.Right);
            bind(content1.space, Keys.Space);
            bind(content1.control, Keys.ControlKey);
            bind(content1.enter, Keys.Enter);


            this.InvokeWhenStageIsReady(
                () =>
                {
                    content1.AttachToContainer(this);
                    content1.AutoSizeTo(this.stage);
                }
            );
        }

    }
}

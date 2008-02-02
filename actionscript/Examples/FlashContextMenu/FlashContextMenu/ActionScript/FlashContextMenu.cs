using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;
using System;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.Extensions;

namespace FlashContextMenu.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashContextMenu : Sprite
    {
        public FlashContextMenu()
        {
            var ctx = new ContextMenu();

            ctx.hideBuiltInItems();

            var a = new ContextMenuItem("zproxy.zapto.org");



            ctx.customItems.push(a);

            this.contextMenu = ctx;




            var f1 = new TextField
                            {
                                text = "powered by jsc",
                                x = 20,
                                y = 40,
                                selectable = false,
                                multiline = true,
                                autoSize = TextFieldAutoSize.LEFT,
                                sharpness = -400,
                                textColor = 0xffffff
                            };
            stage.mouseMove +=
                ev =>
                {
                    f1.x = ev.stageX + 32;
                    f1.y = ev.stageY + 4;
                };


            addChild(f1);

            Action Toggle =
                delegate
                {
                    if (f1.parent == null)
                        addChild(f1);
                    else
                        removeChild(f1);
                };

            a.menuItemSelect +=
                delegate
                {
                    Toggle();
                };



            var circle1 = new Sprite();
            circle1.graphics.beginFill(0xFFCC00);
            circle1.graphics.drawCircle(40, 40, 20);
            circle1.graphics.endFill();

            circle1.buttonMode = true;
            circle1.click += delegate { Toggle(); };

            
            circle1.filters = new []
                {
                    new GlowFilter
                    {
                        color = 0x00ff00,
                        blurX = 10,
                        blurY = 10
                    }
                };

            circle1.mouseOver +=
                delegate
                {
                    circle1.alpha = 1;
                };

            circle1.mouseOut +=
                delegate
                {
                    circle1.alpha = 0.3;
                };

            circle1.alpha = 0.1;
            var circle2 = new Sprite();
            circle2.graphics.beginFill(0xFFCC00);
            circle2.graphics.drawCircle(120, 40, 20);
            circle1.graphics.endFill();

            circle2.buttonMode = false;
            circle2.click += delegate { Toggle(); };

            addChild(circle1);
            addChild(circle2);

            new TextField
            {
                text = "Clicking on the orange buttons or on the context menu item will toggle the visibility of the white text near the cursor.",
                selectable = false,
                mouseEnabled = false,
                x = 2,
                y = 2,
                autoSize = TextFieldAutoSize.LEFT
            }.AttachTo(this);

            var input = new TextField
            {
                text = f1.text,
                type = TextFieldType.INPUT,
                width = 200,
                height = 60,
                multiline = true,
                background = true,
                backgroundColor = 0xffffff,
                
                y = 80,
                x = 20,
                border = true
            }.AttachTo(this);

            input.change +=
                delegate
                {
                    f1.text = input.text;
                };
        }
    }

}

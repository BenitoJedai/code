using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.Extensions;


namespace FlashTowerDefense.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashTowerDefense : Sprite
    {
        public FlashTowerDefense()
        {

            var bg = new Sprite { x = 0, y = 0 };

            bg.graphics.beginFill(0xffffff);
            bg.graphics.drawRect(0, 0, stage.stageWidth, stage.stageHeight);

            bg.AttachTo(this);

            var t = new TextField
            {
                x = 4,
                y = 4,
                width = 200,
                height = 20,
                mouseEnabled = false
            };

            t.AttachTo(this);



            this.mouseMove +=
                e =>
                {
                    t.text = "{x = " + e.stageX + ", y = " + e.stageY + " }";

                };
        }
    }

}

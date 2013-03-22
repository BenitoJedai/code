using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.Extensions;
using System;

namespace com.abstractatech.gamification.fth
{
    public sealed class ApplicationSprite :
        Sprite
    {
        public const int DefaultWidth = FlashTreasureHunt.ActionScript.FlashTreasureHunt.DefaultControlWidth;
        public const int DefaultHeight = FlashTreasureHunt.ActionScript.FlashTreasureHunt.DefaultControlHeight;

        Sprite InternalContent;

        public void WhenReady(Action yield)
        {
            yield();
        }

        public ApplicationSprite()
        {



            InternalContent = new global::FlashTreasureHunt.ActionScript.FlashTreasureHunt();
            InternalContent.AttachTo(this);


            //this.click +=
            //  e =>
            //  {
            //      e.stopImmediatePropagation();


            //      //this.stage.fullScreenSourceRect = new ScriptCoreLib.ActionScript.flash.geom.Rectangle(
            //      //     0, 0, DefaultWidth, DefaultHeight
            //      // );



            //      this.stage.SetFullscreen(true);


            //  };

        }


        public void __keydown(string __keyCode)
        {
            var keyCode = int.Parse(__keyCode);

            InternalContent.dispatchEvent(
                new KeyboardEvent(KeyboardEvent.KEY_DOWN,
                    keyCodeValue: (uint)keyCode
                )
            );
        }

        public void __keyup(string __keyCode)
        {
            var keyCode = int.Parse(__keyCode);

            InternalContent.dispatchEvent(
                new KeyboardEvent(KeyboardEvent.KEY_UP,
                    keyCodeValue: (uint)keyCode
                )
            );
        }

    }
}

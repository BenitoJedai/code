using System;
using System.Collections.Generic;
using System.Linq;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.RayCaster;


namespace RayCaster6.ActionScript
{

    [ScriptImportsType("flash.utils.getTimer")]
    [Script]
    public sealed partial class RayCaster4base : ViewEngineBase
    {
        // http://livedocs.adobe.com/flex/2/langref/flash/utils/package.html#getTimer()
        [Script(OptimizedCode = "return flash.utils.getTimer();")]
        internal static int getTimer()
        {
            return default(int);
        }

        public TextField txtMain;


        

        protected int time;
        protected int counter;

        public RayCaster4base(int w, int h)
            : base(w, h)
        {
            //moveSpeed = 0.2;




            counter = 0;

           
            txtMain = new TextField
            {
                defaultTextFormat = new TextFormat
                {
                    font = "Verdana",
                    align = TextFormatAlign.LEFT,
                    size = 10,
                    color = 0xffffff
                },
                autoSize = TextFieldAutoSize.LEFT,
                text = "0"
            };


        }








        public SpriteInfo CreateDummy(Texture64 Stand)
        {
            return CreateWalkingDummy(new[] { Stand });

        }

        public SpriteInfo CreateWalkingDummy(Texture64[] Stand, params Texture64[][] Walk)
        {
            var s = new SpriteInfo
            {
                Position = new Point { x = posX + dirX * 2, y = posY + dirY * 2 },
                Frames = Stand,
                Direction = dir
            }.AddTo(Sprites);

            if (Walk.Length > 0)
                (200).AtInterval(
                    t =>
                    {
                        s.Frames = Walk[t.currentCount % Walk.Length];
                    }
                );

            return s;
        }









    }

}

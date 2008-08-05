using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using System;
using ScriptCoreLib.ActionScript.mx.core;



namespace FlashSpaceInvaders.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(backgroundColor = Colors.Black, width = DefaultWidth, height = DefaultHeight)]
    public class FlashSpaceInvaders : Sprite
    {
        public const int DefaultWidth = 480;
        public const int DefaultHeight = 480;

 
        public const int KeyLeft = 37;
        public const int KeyRight = 39;

	
   
        
        public FlashSpaceInvaders()
        {
			this.graphics.lineStyle(1, Colors.Green, 1);
			this.graphics.drawRect(0, 0, DefaultWidth - 1, DefaultHeight - 1);


			var m = new MenuSprite(DefaultWidth).AttachTo(this);
		}
    }

}

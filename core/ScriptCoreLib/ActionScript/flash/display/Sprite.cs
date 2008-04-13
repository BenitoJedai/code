using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.media;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/display/Sprite.html
    [Script(IsNative = true)]
    public class Sprite : DisplayObjectContainer
    {
        #region Properties
        /// <summary>
        /// Specifies the button mode of this sprite.
        /// </summary>
        public bool buttonMode { get; set; }

        /// <summary>
        /// [read-only] Specifies the display object over which the sprite is being dragged, or on which the sprite was dropped.
        /// </summary>
        public DisplayObject dropTarget { get; private set; }

        /// <summary>
        /// [read-only] Specifies the Graphics object that belongs to this sprite where vector drawing commands can occur.
        /// </summary>
        public Graphics graphics { get; private set; }

        /// <summary>
        /// Designates another sprite to serve as the hit area for a sprite.
        /// </summary>
        public Sprite hitArea { get; set; }

        /// <summary>
        /// Controls sound within this sprite.
        /// </summary>
        public SoundTransform soundTransform { get; set; }

        /// <summary>
        /// A Boolean value that indicates whether the pointing hand (hand cursor) appears when the mouse rolls over a sprite in which the buttonMode property is set to true.
        /// </summary>
        public bool useHandCursor { get; set; }

        #endregion

    }
}

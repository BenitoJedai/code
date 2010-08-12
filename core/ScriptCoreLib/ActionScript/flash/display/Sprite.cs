using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.geom;

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

        #region Methods
        /// <summary>
        /// Lets the user drag the specified sprite.
        /// </summary>
        public void startDrag(bool lockCenter, Rectangle bounds)
        {
        }

        /// <summary>
        /// Lets the user drag the specified sprite.
        /// </summary>
        public void startDrag(bool lockCenter)
        {
        }

        /// <summary>
        /// Lets the user drag the specified sprite.
        /// </summary>
        public void startDrag()
        {
        }

        /// <summary>
        /// Lets the user drag the specified sprite on a touch-enabled device.
        /// </summary>
        public void startTouchDrag(int touchPointID, bool lockCenter, Rectangle bounds)
        {
        }

        /// <summary>
        /// Lets the user drag the specified sprite on a touch-enabled device.
        /// </summary>
        public void startTouchDrag(int touchPointID, bool lockCenter)
        {
        }

        /// <summary>
        /// Lets the user drag the specified sprite on a touch-enabled device.
        /// </summary>
        public void startTouchDrag(int touchPointID)
        {
        }

        /// <summary>
        /// Ends the startDrag() method.
        /// </summary>
        public void stopDrag()
        {
        }

        /// <summary>
        /// Ends the startTouchDrag() method, for use with touch-enabled devices.
        /// </summary>
        public void stopTouchDrag(int touchPointID)
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new Sprite instance.
        /// </summary>
        public Sprite()
            : base()
        {
        }

        #endregion

    }
}

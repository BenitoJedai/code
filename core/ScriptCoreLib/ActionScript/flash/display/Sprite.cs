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
        // ! this class has economic metrics to it.

        //We believe that the Runtime remains a great choice for developers who want a multi platform solution.  
        // From games, education, enterprise and a myriad of other categories, developers have had great success 
        // using Flash Player and AIR.   Flash Player remains ubiquitous, installed on easily over a billion computers.  
        // It comes bundled with all new Windows 8.x systems and every Chrome/Chrome OS installation.  It's found on 
        // nearly every computer with a connection to the internet.  AIR is also a huge success, with over 2.1 billion 
        // user installations and over a hundred thousand distinct applications created.
        // https://forums.adobe.com/thread/1365685?start=280&tstart=0


        // October 22, 2014 — This is a beta release of AIR 15, code named Market.
        // Last Updated: October 23, 2014 

        // http://labsdownload.adobe.com/pub/labs/flashruntimes/shared/air15_flashplayer15_releasenotes.pdf
        // http://labs.adobe.com/technologies/flashruntimes/air/
        // https://forums.adobe.com/community/labs/flashruntimes/air/

        // https://forums.adobe.com/thread/1541258
        // https://forums.adobe.com/thread/1493673

        // jsc likes starling
        // http://gamua.com/blog/2014/05/starling-15/?scid=social24460774

        // http://blogs.adobe.com/flashplayer/

        // "X:\jsc.internal.git\core\ScriptCoreLib\References\AdobeAIRSDK\frameworks\libs\player\14.0\playerglobal.swc"
        // when will we do LINQ to SQL for flash, async/await threading?

        // http://blogs.adobe.com/flashplayer/2014/03/latest-updates-on-the-flash-runtime.html
        // http://www.adobe.com/devnet/flashplatform/whitepapers/roadmap.html
        // Flash Player plug-in for Android is not supported or available for the Google Chrome browser on Android.

        // the Web Browser on my Galaxy S does support flash plugin.

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

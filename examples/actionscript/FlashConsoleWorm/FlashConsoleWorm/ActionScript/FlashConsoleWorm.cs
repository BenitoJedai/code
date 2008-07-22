using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace FlashConsoleWorm.ActionScript
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(width = DefaultWidth, height = DefaultHeight, backgroundColor = ColorBlack)]
    public class FlashConsoleWorm : Sprite
    {
        // vNext should be semi 3D - http://www.freeworldgroup.com/games/3dworm/index.html


        public const uint ColorBlack = 0;
        public const uint ColorGreen = 0x00ff00;

        public const int DefaultWidth = GridX * DefaultZoom;
        public const int DefaultHeight = GridY * DefaultZoom;

        public const int DefaultZoom = 24;

        public const int GridX = 32;
        public const int GridY = 32;

        /// <summary>
        /// Default constructor
        /// </summary>
        public FlashConsoleWorm()
        {
            var s = new Bitmap(new BitmapData(GridX, GridY, false, ColorBlack));

            s.bitmapData.setPixel(1, 1, ColorGreen);
            s.scaleX = DefaultZoom;
            s.scaleY = DefaultZoom;

            s.AttachTo(this);

            // add scull ani here
            // att status text here

        }
    }
}
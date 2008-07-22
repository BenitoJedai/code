using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using ScriptCoreLib.ActionScript;

namespace FlashConsoleWorm.ActionScript
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(width = DefaultWidth, height = DefaultHeight, backgroundColor = ColorBlack)]
    public class FlashConsoleWorm : Sprite
    {
        public const uint ColorBlack = 0;

        public const int DefaultWidth = 320;
        public const int DefaultHeight = 320;
        /// <summary>
        /// Default constructor
        /// </summary>
        public FlashConsoleWorm()
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/2/langref/flash/display/Sprite.html
    [Script(IsNative = true)]
    public class Sprite : DisplayObjectContainer
    {
        /// <summary>
        /// Specifies the Graphics object that belongs to this sprite where vector drawing commands can occur.
        /// </summary>
        public Graphics graphics { get; private set; }
    }
}

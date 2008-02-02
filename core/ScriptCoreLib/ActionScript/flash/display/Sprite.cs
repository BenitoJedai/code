using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/display/Sprite.html
    [Script(IsNative = true)]
    public class Sprite : DisplayObjectContainer
    {



        /// <summary>
        /// Specifies the Graphics object that belongs to this sprite where vector drawing commands can occur.
        /// </summary>
        public Graphics graphics { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.net;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/display/Loader.html
    [Script(IsNative = true)]
    public class Loader : DisplayObjectContainer
    {
        /// <summary>
        /// Loads a SWF, JPEG, progressive JPEG, unanimated GIF, or PNG file into an object that is a child of this Loader object.
        /// </summary>
        /// <param name="request"></param>
        public void load(URLRequest request)
        {
        }

    }
}

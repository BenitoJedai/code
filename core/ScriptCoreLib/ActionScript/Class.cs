using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.media;

namespace ScriptCoreLib.ActionScript
{
    // http://livedocs.adobe.com/flex/3/langref/Class.html
    [Script(IsNative = true)]
    public class Class
    {
        [Script(NotImplementedHere = true)]
        public static implicit operator SoundAsset(Class e)
        {
            return default(SoundAsset);
        }

        [Script(NotImplementedHere = true)]
        public static implicit operator Sound(Class e)
        {
            return default(Sound);
        }

        [Script(NotImplementedHere = true)]
        public static implicit operator BitmapAsset(Class e)
        {
            return default(BitmapAsset);
        }

        [Script(NotImplementedHere = true)]
        public static implicit operator Sprite(Class e)
        {
            return default(Sprite);
        } 


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.display;


namespace ScriptCoreLib.ActionScript.Extensions
{
    [Script(Implements = typeof(Class))]
    internal class __Class
    {
        
        public static implicit operator Class(__Class e)
        {
            return (Class)(object)e;
        }

        #region Implementation for methods marked with [Script(NotImplementedHere = true)]

        public static implicit operator SoundAsset(__Class e)
        {
            return ((Class)e).ToSoundAsset();
        }

        public static implicit operator Sound(__Class e)
        {
            return ((Class)e).ToSoundAsset();
        }

        public static implicit operator BitmapAsset(__Class e)
        {
            return ((Class)e).ToBitmapAsset();
        }

        public static implicit operator Bitmap(__Class e)
        {
            return ((Class)e).ToBitmapAsset();
        }

        #endregion




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.media;

namespace ScriptCoreLib.ActionScript.Extensions.flash.media
{
    [Script(Implements = typeof(SoundChannel))]
    internal static class __SoundChannel
    {

        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region soundComplete
        public static void add_soundComplete(SoundChannel that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.SOUND_COMPLETE);
        }

        public static void remove_soundComplete(SoundChannel that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.SOUND_COMPLETE);
        }
        #endregion

        #endregion

    }
}

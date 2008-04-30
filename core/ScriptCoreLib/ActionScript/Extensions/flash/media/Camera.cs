using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.media;

namespace ScriptCoreLib.ActionScript.Extensions.flash.media
{
    // if a type implements a type that is set to be native, then only implementation
    // which is marked with NotImplementedHere applies

    [Script(Implements = typeof(Camera))]
    public static class __Camera
    {
        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region activity
        public static void add_activity(Camera that, Action<ActivityEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, ActivityEvent.ACTIVITY);
        }

        public static void remove_activity(Camera that, Action<ActivityEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, ActivityEvent.ACTIVITY);
        }
        #endregion

        #region status
        public static void add_status(Camera that, Action<StatusEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, StatusEvent.STATUS);
        }

        public static void remove_status(Camera that, Action<StatusEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, StatusEvent.STATUS);
        }
        #endregion

        #endregion


    
    }
}

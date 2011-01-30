using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.sensors;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Extensions.flash.sensors
{
    // if a type implements a type that is set to be native, then only implementation
    // which is marked with NotImplementedHere applies

    [Script(Implements = typeof(Accelerometer))]
    public static class __Accelerometer
    {
        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region status
        public static void add_status(Accelerometer that, Action<StatusEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, StatusEvent.STATUS);
        }

        public static void remove_status(Accelerometer that, Action<StatusEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, StatusEvent.STATUS);
        }
        #endregion

        #region update
        public static void add_update(Accelerometer that, Action<AccelerometerEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, AccelerometerEvent.UPDATE);
        }

        public static void remove_update(Accelerometer that, Action<AccelerometerEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, AccelerometerEvent.UPDATE);
        }
        #endregion

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.com.google.maps;

namespace ScriptCoreLib.ActionScript.Extensions.com.google.maps
{
    [Script(Implements = typeof(Map))]
    internal static class __Map
    {
        #region added
        public static void add_MapReady(Map that, Action<MapEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapEvent.MAP_READY);
        }

        public static void remove_MapReady(Map that, Action<MapEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapEvent.MAP_READY);
        }
        #endregion
    }
}

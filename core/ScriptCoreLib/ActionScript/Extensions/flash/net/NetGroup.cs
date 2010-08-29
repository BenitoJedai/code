using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.net;

namespace ScriptCoreLib.ActionScript.Extensions.flash.net
{
    [Script(Implements = typeof(NetGroup))]
    internal static class __NetGroup
    {

        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region netStatus
        public static void add_netStatus(NetGroup that, Action<NetStatusEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, NetStatusEvent.NET_STATUS);
        }

        public static void remove_netStatus(NetGroup that, Action<NetStatusEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, NetStatusEvent.NET_STATUS);
        }
        #endregion

        #endregion
    }
}

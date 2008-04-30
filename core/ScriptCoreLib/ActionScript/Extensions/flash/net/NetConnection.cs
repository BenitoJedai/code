
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Extensions.flash.net
{
    [Script(Implements = typeof(NetConnection))]
    internal static class __NetConnection
    {
        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region asyncError
        public static void add_asyncError(NetConnection that, Action<AsyncErrorEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, AsyncErrorEvent.ASYNC_ERROR);
        }

        public static void remove_asyncError(NetConnection that, Action<AsyncErrorEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, AsyncErrorEvent.ASYNC_ERROR);
        }
        #endregion

        #region ioError
        public static void add_ioError(NetConnection that, Action<IOErrorEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, IOErrorEvent.IO_ERROR);
        }

        public static void remove_ioError(NetConnection that, Action<IOErrorEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, IOErrorEvent.IO_ERROR);
        }
        #endregion

        #region netStatus
        public static void add_netStatus(NetConnection that, Action<NetStatusEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, NetStatusEvent.NET_STATUS);
        }

        public static void remove_netStatus(NetConnection that, Action<NetStatusEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, NetStatusEvent.NET_STATUS);
        }
        #endregion

        #region securityError
        public static void add_securityError(NetConnection that, Action<SecurityErrorEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, SecurityErrorEvent.SECURITY_ERROR);
        }

        public static void remove_securityError(NetConnection that, Action<SecurityErrorEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, SecurityErrorEvent.SECURITY_ERROR);
        }
        #endregion

        #endregion

    }
}

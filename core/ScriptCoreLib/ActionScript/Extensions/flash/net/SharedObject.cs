
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Extensions.flash.net
{
    [Script(Implements = typeof(SharedObject))]
	internal static class __SharedObject
    {
		#region Implementation for methods marked with [Script(NotImplementedHere = true)]
		#region asyncError
		public static void add_asyncError(SharedObject that, Action<AsyncErrorEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, AsyncErrorEvent.ASYNC_ERROR);
		}

		public static void remove_asyncError(SharedObject that, Action<AsyncErrorEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, AsyncErrorEvent.ASYNC_ERROR);
		}
		#endregion

		#region netStatus
		public static void add_netStatus(SharedObject that, Action<NetStatusEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, NetStatusEvent.NET_STATUS);
		}

		public static void remove_netStatus(SharedObject that, Action<NetStatusEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, NetStatusEvent.NET_STATUS);
		}
		#endregion

		#region sync
		public static void add_sync(SharedObject that, Action<SyncEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, SyncEvent.SYNC);
		}

		public static void remove_sync(SharedObject that, Action<SyncEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, SyncEvent.SYNC);
		}
		#endregion

		#endregion

    }
}

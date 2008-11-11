using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Extensions.flash.net
{
    // if a type implements a type that is set to be native, then only implementation
    // which is marked with NotImplementedHere applies

    [Script(Implements = typeof(LocalConnection))]
	public static class __LocalConnection
    {

		#region Implementation for methods marked with [Script(NotImplementedHere = true)]
		#region asyncError
		public static void add_asyncError(LocalConnection that, Action<AsyncErrorEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, AsyncErrorEvent.ASYNC_ERROR);
		}

		public static void remove_asyncError(LocalConnection that, Action<AsyncErrorEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, AsyncErrorEvent.ASYNC_ERROR);
		}
		#endregion

		#region securityError
		public static void add_securityError(LocalConnection that, Action<SecurityErrorEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, SecurityErrorEvent.SECURITY_ERROR);
		}

		public static void remove_securityError(LocalConnection that, Action<SecurityErrorEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, SecurityErrorEvent.SECURITY_ERROR);
		}
		#endregion

		#region status
		public static void add_status(LocalConnection that, Action<StatusEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, StatusEvent.STATUS);
		}

		public static void remove_status(LocalConnection that, Action<StatusEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, StatusEvent.STATUS);
		}
		#endregion

		#endregion

    
    }
}

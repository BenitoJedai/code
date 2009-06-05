using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.net;

namespace ScriptCoreLib.ActionScript.Extensions.flash.net
{
	[Script(Implements = typeof(FileReference))]
	internal static class __FileReference
	{
		#region Implementation for methods marked with [Script(NotImplementedHere = true)]
		#region cancel
		public static void add_cancelled(FileReference that, Action<Event> value)
		{
			CommonExtensions.CombineDelegate(that, value, Event.CANCEL);
		}

		public static void remove_cancelled(FileReference that, Action<Event> value)
		{
			CommonExtensions.RemoveDelegate(that, value, Event.CANCEL);
		}
		#endregion

		#region complete
		public static void add_complete(FileReference that, Action<Event> value)
		{
			CommonExtensions.CombineDelegate(that, value, Event.COMPLETE);
		}

		public static void remove_complete(FileReference that, Action<Event> value)
		{
			CommonExtensions.RemoveDelegate(that, value, Event.COMPLETE);
		}
		#endregion

		#region httpStatus
		public static void add_httpStatus(FileReference that, Action<HTTPStatusEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, HTTPStatusEvent.HTTP_STATUS);
		}

		public static void remove_httpStatus(FileReference that, Action<HTTPStatusEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, HTTPStatusEvent.HTTP_STATUS);
		}
		#endregion

		#region ioError
		public static void add_ioError(FileReference that, Action<IOErrorEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, IOErrorEvent.IO_ERROR);
		}

		public static void remove_ioError(FileReference that, Action<IOErrorEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, IOErrorEvent.IO_ERROR);
		}
		#endregion

		#region open
		public static void add_open(FileReference that, Action<Event> value)
		{
			CommonExtensions.CombineDelegate(that, value, Event.OPEN);
		}

		public static void remove_open(FileReference that, Action<Event> value)
		{
			CommonExtensions.RemoveDelegate(that, value, Event.OPEN);
		}
		#endregion

		#region progress
		public static void add_progress(FileReference that, Action<ProgressEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, ProgressEvent.PROGRESS);
		}

		public static void remove_progress(FileReference that, Action<ProgressEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, ProgressEvent.PROGRESS);
		}
		#endregion

		#region securityError
		public static void add_securityError(FileReference that, Action<SecurityErrorEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, SecurityErrorEvent.SECURITY_ERROR);
		}

		public static void remove_securityError(FileReference that, Action<SecurityErrorEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, SecurityErrorEvent.SECURITY_ERROR);
		}
		#endregion

		#region select
		public static void add_select(FileReference that, Action<Event> value)
		{
			CommonExtensions.CombineDelegate(that, value, Event.SELECT);
		}

		public static void remove_select(FileReference that, Action<Event> value)
		{
			CommonExtensions.RemoveDelegate(that, value, Event.SELECT);
		}
		#endregion

		#region uploadCompleteData
		public static void add_uploadCompleteData(FileReference that, Action<DataEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, DataEvent.UPLOAD_COMPLETE_DATA);
		}

		public static void remove_uploadCompleteData(FileReference that, Action<DataEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, DataEvent.UPLOAD_COMPLETE_DATA);
		}
		#endregion

		#endregion

	}
}

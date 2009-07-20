using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.media;

namespace ScriptCoreLib.ActionScript.Extensions.flash.media
{
    [Script(Implements = typeof(Sound))]
    internal static class __Sound
    {
		#region Implementation for methods marked with [Script(NotImplementedHere = true)]
		#region complete
		public static void add_complete(Sound that, Action<Event> value)
		{
			CommonExtensions.CombineDelegate(that, value, Event.COMPLETE);
		}

		public static void remove_complete(Sound that, Action<Event> value)
		{
			CommonExtensions.RemoveDelegate(that, value, Event.COMPLETE);
		}
		#endregion

		#region id3
		public static void add_onid3(Sound that, Action<Event> value)
		{
			CommonExtensions.CombineDelegate(that, value, Event.ID3);
		}

		public static void remove_onid3(Sound that, Action<Event> value)
		{
			CommonExtensions.RemoveDelegate(that, value, Event.ID3);
		}
		#endregion

		#region ioError
		public static void add_ioError(Sound that, Action<IOErrorEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, IOErrorEvent.IO_ERROR);
		}

		public static void remove_ioError(Sound that, Action<IOErrorEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, IOErrorEvent.IO_ERROR);
		}
		#endregion

		#region open
		public static void add_open(Sound that, Action<Event> value)
		{
			CommonExtensions.CombineDelegate(that, value, Event.OPEN);
		}

		public static void remove_open(Sound that, Action<Event> value)
		{
			CommonExtensions.RemoveDelegate(that, value, Event.OPEN);
		}
		#endregion

		#region progress
		public static void add_progress(Sound that, Action<ProgressEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, ProgressEvent.PROGRESS);
		}

		public static void remove_progress(Sound that, Action<ProgressEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, ProgressEvent.PROGRESS);
		}
		#endregion

		#region sampleData
		public static void add_sampleData(Sound that, Action<SampleDataEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, SampleDataEvent.SAMPLE_DATA);
		}

		public static void remove_sampleData(Sound that, Action<SampleDataEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, SampleDataEvent.SAMPLE_DATA);
		}
		#endregion

		#endregion

    }
}

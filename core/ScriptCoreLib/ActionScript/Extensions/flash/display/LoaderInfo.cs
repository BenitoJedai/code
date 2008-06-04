using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Extensions.flash.display
{
    // if a type implements a type that is set to be native, then only implementation
    // which is marked with NotImplementedHere applies

    
    [Script(Implements = typeof(LoaderInfo))]
    internal static class __LoaderInfo
    {
        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region complete
        public static void add_complete(LoaderInfo that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.COMPLETE);
        }

        public static void remove_complete(LoaderInfo that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.COMPLETE);
        }
        #endregion

        #region httpStatus
        public static void add_httpStatus(LoaderInfo that, Action<HTTPStatusEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, HTTPStatusEvent.HTTP_STATUS);
        }

        public static void remove_httpStatus(LoaderInfo that, Action<HTTPStatusEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, HTTPStatusEvent.HTTP_STATUS);
        }
        #endregion

        #region init
        public static void add_init(LoaderInfo that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.INIT);
        }

        public static void remove_init(LoaderInfo that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.INIT);
        }
        #endregion

        #region ioError
        public static void add_ioError(LoaderInfo that, Action<IOErrorEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, IOErrorEvent.IO_ERROR);
        }

        public static void remove_ioError(LoaderInfo that, Action<IOErrorEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, IOErrorEvent.IO_ERROR);
        }
        #endregion

        #region open
        public static void add_open(LoaderInfo that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.OPEN);
        }

        public static void remove_open(LoaderInfo that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.OPEN);
        }
        #endregion

        #region progress
        public static void add_progress(LoaderInfo that, Action<ProgressEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, ProgressEvent.PROGRESS);
        }

        public static void remove_progress(LoaderInfo that, Action<ProgressEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, ProgressEvent.PROGRESS);
        }
        #endregion

        #region unload
        public static void add_unload(LoaderInfo that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.UNLOAD);
        }

        public static void remove_unload(LoaderInfo that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.UNLOAD);
        }
        #endregion

        #endregion

        


    }
}

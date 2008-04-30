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

    [Script(Implements = typeof(URLLoader))]
    public static class __URLLoader
    {
        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region complete
        public static void add_complete(URLLoader that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.COMPLETE);
        }

        public static void remove_complete(URLLoader that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.COMPLETE);
        }
        #endregion

        #region httpStatus
        public static void add_httpStatus(URLLoader that, Action<HTTPStatusEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, HTTPStatusEvent.HTTP_STATUS);
        }

        public static void remove_httpStatus(URLLoader that, Action<HTTPStatusEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, HTTPStatusEvent.HTTP_STATUS);
        }
        #endregion

        #region ioError
        public static void add_ioError(URLLoader that, Action<IOErrorEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, IOErrorEvent.IO_ERROR);
        }

        public static void remove_ioError(URLLoader that, Action<IOErrorEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, IOErrorEvent.IO_ERROR);
        }
        #endregion

        #region open
        public static void add_open(URLLoader that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.OPEN);
        }

        public static void remove_open(URLLoader that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.OPEN);
        }
        #endregion

        #region progress
        public static void add_progress(URLLoader that, Action<ProgressEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, ProgressEvent.PROGRESS);
        }

        public static void remove_progress(URLLoader that, Action<ProgressEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, ProgressEvent.PROGRESS);
        }
        #endregion

        #region securityError
        public static void add_securityError(URLLoader that, Action<SecurityErrorEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, SecurityErrorEvent.SECURITY_ERROR);
        }

        public static void remove_securityError(URLLoader that, Action<SecurityErrorEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, SecurityErrorEvent.SECURITY_ERROR);
        }
        #endregion

        #endregion

    
    }
}

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
        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region ControlAdded
        public static void add_ControlAdded(Map that, Action<MapEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapEvent.CONTROL_ADDED);
        }

        public static void remove_ControlAdded(Map that, Action<MapEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapEvent.CONTROL_ADDED);
        }
        #endregion

        #region ControlRemoved
        public static void add_ControlRemoved(Map that, Action<MapEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapEvent.CONTROL_REMOVED);
        }

        public static void remove_ControlRemoved(Map that, Action<MapEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapEvent.CONTROL_REMOVED);
        }
        #endregion

        #region CopyrightsUpdated
        public static void add_CopyrightsUpdated(Map that, Action<MapEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapEvent.COPYRIGHTS_UPDATED);
        }

        public static void remove_CopyrightsUpdated(Map that, Action<MapEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapEvent.COPYRIGHTS_UPDATED);
        }
        #endregion

        #region InfowindowClosed
        public static void add_InfowindowClosed(Map that, Action<MapEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapEvent.INFOWINDOW_CLOSED);
        }

        public static void remove_InfowindowClosed(Map that, Action<MapEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapEvent.INFOWINDOW_CLOSED);
        }
        #endregion

        #region InfowindowClosing
        public static void add_InfowindowClosing(Map that, Action<MapEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapEvent.INFOWINDOW_CLOSING);
        }

        public static void remove_InfowindowClosing(Map that, Action<MapEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapEvent.INFOWINDOW_CLOSING);
        }
        #endregion

        #region InfowindowOpened
        public static void add_InfowindowOpened(Map that, Action<MapEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapEvent.INFOWINDOW_OPENED);
        }

        public static void remove_InfowindowOpened(Map that, Action<MapEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapEvent.INFOWINDOW_OPENED);
        }
        #endregion

        #region MapReady
        public static void add_MapReady(Map that, Action<MapEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapEvent.MAP_READY);
        }

        public static void remove_MapReady(Map that, Action<MapEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapEvent.MAP_READY);
        }
        #endregion

        #region MaptypeAdded
        public static void add_MaptypeAdded(Map that, Action<MapEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapEvent.MAPTYPE_ADDED);
        }

        public static void remove_MaptypeAdded(Map that, Action<MapEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapEvent.MAPTYPE_ADDED);
        }
        #endregion

        #region MaptypeChanged
        public static void add_MaptypeChanged(Map that, Action<MapEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapEvent.MAPTYPE_CHANGED);
        }

        public static void remove_MaptypeChanged(Map that, Action<MapEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapEvent.MAPTYPE_CHANGED);
        }
        #endregion

        #region MaptypeRemoved
        public static void add_MaptypeRemoved(Map that, Action<MapEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapEvent.MAPTYPE_REMOVED);
        }

        public static void remove_MaptypeRemoved(Map that, Action<MapEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapEvent.MAPTYPE_REMOVED);
        }
        #endregion

        #region SizeChanged
        public static void add_SizeChanged(Map that, Action<MapEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapEvent.SIZE_CHANGED);
        }

        public static void remove_SizeChanged(Map that, Action<MapEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapEvent.SIZE_CHANGED);
        }
        #endregion

        #region VisibilityChanged
        public static void add_VisibilityChanged(Map that, Action<MapEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapEvent.VISIBILITY_CHANGED);
        }

        public static void remove_VisibilityChanged(Map that, Action<MapEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapEvent.VISIBILITY_CHANGED);
        }
        #endregion

        #endregion


        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region MoveEnd
        public static void add_MoveEnd(Map that, Action<MapMoveEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMoveEvent.MOVE_END);
        }

        public static void remove_MoveEnd(Map that, Action<MapMoveEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMoveEvent.MOVE_END);
        }
        #endregion

        #region MoveStart
        public static void add_MoveStart(Map that, Action<MapMoveEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMoveEvent.MOVE_START);
        }

        public static void remove_MoveStart(Map that, Action<MapMoveEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMoveEvent.MOVE_START);
        }
        #endregion

        #region MoveStep
        public static void add_MoveStep(Map that, Action<MapMoveEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMoveEvent.MOVE_STEP);
        }

        public static void remove_MoveStep(Map that, Action<MapMoveEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMoveEvent.MOVE_STEP);
        }
        #endregion

        #endregion


        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region ZoomEnd
        public static void add_ZoomEnd(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapZoomEvent.ZOOM_END);
        }

        public static void remove_ZoomEnd(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapZoomEvent.ZOOM_END);
        }
        #endregion

        #region ZoomRangeChanged
        public static void add_ZoomRangeChanged(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapZoomEvent.ZOOM_RANGE_CHANGED);
        }

        public static void remove_ZoomRangeChanged(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapZoomEvent.ZOOM_RANGE_CHANGED);
        }
        #endregion

        #region ZoomStart
        public static void add_ZoomStart(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapZoomEvent.ZOOM_START);
        }

        public static void remove_ZoomStart(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapZoomEvent.ZOOM_START);
        }
        #endregion

        #region ZoomStep
        public static void add_ZoomStep(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapZoomEvent.ZOOM_STEP);
        }

        public static void remove_ZoomStep(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapZoomEvent.ZOOM_STEP);
        }
        #endregion

        #endregion

    }
}

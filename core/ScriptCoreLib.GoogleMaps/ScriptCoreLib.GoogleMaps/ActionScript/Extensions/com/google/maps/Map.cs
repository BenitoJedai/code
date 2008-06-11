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
        #region MapMoveEnd
        public static void add_MapMoveEnd(Map that, Action<MapMoveEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMoveEvent.MOVE_END);
        }

        public static void remove_MapMoveEnd(Map that, Action<MapMoveEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMoveEvent.MOVE_END);
        }
        #endregion

        #region MapMoveStart
        public static void add_MapMoveStart(Map that, Action<MapMoveEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMoveEvent.MOVE_START);
        }

        public static void remove_MapMoveStart(Map that, Action<MapMoveEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMoveEvent.MOVE_START);
        }
        #endregion

        #region MapMoveStep
        public static void add_MapMoveStep(Map that, Action<MapMoveEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMoveEvent.MOVE_STEP);
        }

        public static void remove_MapMoveStep(Map that, Action<MapMoveEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMoveEvent.MOVE_STEP);
        }
        #endregion

        #endregion


        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region MapZoomEnd
        public static void add_MapZoomEnd(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapZoomEvent.ZOOM_END);
        }

        public static void remove_MapZoomEnd(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapZoomEvent.ZOOM_END);
        }
        #endregion

        #region MapZoomRangeChanged
        public static void add_MapZoomRangeChanged(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapZoomEvent.ZOOM_RANGE_CHANGED);
        }

        public static void remove_MapZoomRangeChanged(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapZoomEvent.ZOOM_RANGE_CHANGED);
        }
        #endregion

        #region MapZoomStart
        public static void add_MapZoomStart(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapZoomEvent.ZOOM_START);
        }

        public static void remove_MapZoomStart(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapZoomEvent.ZOOM_START);
        }
        #endregion

        #region MapZoomStep
        public static void add_MapZoomStep(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapZoomEvent.ZOOM_STEP);
        }

        public static void remove_MapZoomStep(Map that, Action<MapZoomEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapZoomEvent.ZOOM_STEP);
        }
        #endregion

        #endregion


        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region MapClick
        public static void add_MapClick(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMouseEvent.CLICK);
        }

        public static void remove_MapClick(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMouseEvent.CLICK);
        }
        #endregion

        #region MapDoubleClick
        public static void add_MapDoubleClick(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMouseEvent.DOUBLE_CLICK);
        }

        public static void remove_MapDoubleClick(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMouseEvent.DOUBLE_CLICK);
        }
        #endregion

        #region MapDragEnd
        public static void add_MapDragEnd(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMouseEvent.DRAG_END);
        }

        public static void remove_MapDragEnd(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMouseEvent.DRAG_END);
        }
        #endregion

        #region MapDragStart
        public static void add_MapDragStart(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMouseEvent.DRAG_START);
        }

        public static void remove_MapDragStart(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMouseEvent.DRAG_START);
        }
        #endregion

        #region MapDragStep
        public static void add_MapDragStep(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMouseEvent.DRAG_STEP);
        }

        public static void remove_MapDragStep(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMouseEvent.DRAG_STEP);
        }
        #endregion

        #region MapMouseDown
        public static void add_MapMouseDown(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMouseEvent.MOUSE_DOWN);
        }

        public static void remove_MapMouseDown(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMouseEvent.MOUSE_DOWN);
        }
        #endregion

        #region MapMouseMove
        public static void add_MapMouseMove(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMouseEvent.MOUSE_MOVE);
        }

        public static void remove_MapMouseMove(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMouseEvent.MOUSE_MOVE);
        }
        #endregion

        #region MapMouseUp
        public static void add_MapMouseUp(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMouseEvent.MOUSE_UP);
        }

        public static void remove_MapMouseUp(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMouseEvent.MOUSE_UP);
        }
        #endregion

        #region MapRollOut
        public static void add_MapRollOut(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMouseEvent.ROLL_OUT);
        }

        public static void remove_MapRollOut(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMouseEvent.ROLL_OUT);
        }
        #endregion

        #region MapRollOver
        public static void add_MapRollOver(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MapMouseEvent.ROLL_OVER);
        }

        public static void remove_MapRollOver(Map that, Action<MapMouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MapMouseEvent.ROLL_OVER);
        }
        #endregion

        #endregion


    }
}

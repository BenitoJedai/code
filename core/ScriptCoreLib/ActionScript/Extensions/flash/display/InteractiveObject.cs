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

    [Script(Implements = typeof(InteractiveObject))]
    internal static class __InteractiveObject
    {
        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region clear
        public static void add_clear(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.CLEAR);
        }

        public static void remove_clear(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.CLEAR);
        }
        #endregion

        #region click
        public static void add_click(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.CLICK);
        }

        public static void remove_click(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.CLICK);
        }
        #endregion

        #region copy
        public static void add_copy(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.COPY);
        }

        public static void remove_copy(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.COPY);
        }
        #endregion

        #region cut
        public static void add_cut(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.CUT);
        }

        public static void remove_cut(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.CUT);
        }
        #endregion

        #region doubleClick
        public static void add_doubleClick(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.DOUBLE_CLICK);
        }

        public static void remove_doubleClick(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.DOUBLE_CLICK);
        }
        #endregion

        #region focusIn
        public static void add_focusIn(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, FocusEvent.FOCUS_IN);
        }

        public static void remove_focusIn(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, FocusEvent.FOCUS_IN);
        }
        #endregion

        #region focusOut
        public static void add_focusOut(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, FocusEvent.FOCUS_OUT);
        }

        public static void remove_focusOut(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, FocusEvent.FOCUS_OUT);
        }
        #endregion

        #region gesturePan
        public static void add_gesturePan(InteractiveObject that, Action<TransformGestureEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TransformGestureEvent.GESTURE_PAN);
        }

        public static void remove_gesturePan(InteractiveObject that, Action<TransformGestureEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TransformGestureEvent.GESTURE_PAN);
        }
        #endregion

        #region gesturePressAndTap
        public static void add_gesturePressAndTap(InteractiveObject that, Action<PressAndTapGestureEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, PressAndTapGestureEvent.GESTURE_PRESS_AND_TAP);
        }

        public static void remove_gesturePressAndTap(InteractiveObject that, Action<PressAndTapGestureEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, PressAndTapGestureEvent.GESTURE_PRESS_AND_TAP);
        }
        #endregion

        #region gestureRotate
        public static void add_gestureRotate(InteractiveObject that, Action<TransformGestureEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TransformGestureEvent.GESTURE_ROTATE);
        }

        public static void remove_gestureRotate(InteractiveObject that, Action<TransformGestureEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TransformGestureEvent.GESTURE_ROTATE);
        }
        #endregion

        #region gestureSwipe
        public static void add_gestureSwipe(InteractiveObject that, Action<TransformGestureEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TransformGestureEvent.GESTURE_SWIPE);
        }

        public static void remove_gestureSwipe(InteractiveObject that, Action<TransformGestureEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TransformGestureEvent.GESTURE_SWIPE);
        }
        #endregion

        #region gestureTwoFingerTap
        public static void add_gestureTwoFingerTap(InteractiveObject that, Action<GestureEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, GestureEvent.GESTURE_TWO_FINGER_TAP);
        }

        public static void remove_gestureTwoFingerTap(InteractiveObject that, Action<GestureEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, GestureEvent.GESTURE_TWO_FINGER_TAP);
        }
        #endregion

        #region gestureZoom
        public static void add_gestureZoom(InteractiveObject that, Action<TransformGestureEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TransformGestureEvent.GESTURE_ZOOM);
        }

        public static void remove_gestureZoom(InteractiveObject that, Action<TransformGestureEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TransformGestureEvent.GESTURE_ZOOM);
        }
        #endregion

        #region imeStartComposition
        public static void add_imeStartComposition(InteractiveObject that, Action<IMEEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, IMEEvent.IME_START_COMPOSITION);
        }

        public static void remove_imeStartComposition(InteractiveObject that, Action<IMEEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, IMEEvent.IME_START_COMPOSITION);
        }
        #endregion

        #region keyDown
        public static void add_keyDown(InteractiveObject that, Action<KeyboardEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, KeyboardEvent.KEY_DOWN);
        }

        public static void remove_keyDown(InteractiveObject that, Action<KeyboardEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, KeyboardEvent.KEY_DOWN);
        }
        #endregion

        #region keyFocusChange
        public static void add_keyFocusChange(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, FocusEvent.KEY_FOCUS_CHANGE);
        }

        public static void remove_keyFocusChange(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, FocusEvent.KEY_FOCUS_CHANGE);
        }
        #endregion

        #region keyUp
        public static void add_keyUp(InteractiveObject that, Action<KeyboardEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, KeyboardEvent.KEY_UP);
        }

        public static void remove_keyUp(InteractiveObject that, Action<KeyboardEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, KeyboardEvent.KEY_UP);
        }
        #endregion

        #region mouseDown
        public static void add_mouseDown(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.MOUSE_DOWN);
        }

        public static void remove_mouseDown(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.MOUSE_DOWN);
        }
        #endregion

        #region mouseFocusChange
        public static void add_mouseFocusChange(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, FocusEvent.MOUSE_FOCUS_CHANGE);
        }

        public static void remove_mouseFocusChange(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, FocusEvent.MOUSE_FOCUS_CHANGE);
        }
        #endregion

        #region mouseMove
        public static void add_mouseMove(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.MOUSE_MOVE);
        }

        public static void remove_mouseMove(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.MOUSE_MOVE);
        }
        #endregion

        #region mouseOut
        public static void add_mouseOut(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.MOUSE_OUT);
        }

        public static void remove_mouseOut(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.MOUSE_OUT);
        }
        #endregion

        #region mouseOver
        public static void add_mouseOver(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.MOUSE_OVER);
        }

        public static void remove_mouseOver(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.MOUSE_OVER);
        }
        #endregion

        #region mouseUp
        public static void add_mouseUp(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.MOUSE_UP);
        }

        public static void remove_mouseUp(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.MOUSE_UP);
        }
        #endregion

        #region mouseWheel
        public static void add_mouseWheel(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.MOUSE_WHEEL);
        }

        public static void remove_mouseWheel(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.MOUSE_WHEEL);
        }
        #endregion

        #region paste
        public static void add_paste(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.PASTE);
        }

        public static void remove_paste(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.PASTE);
        }
        #endregion

        #region rollOut
        public static void add_rollOut(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.ROLL_OUT);
        }

        public static void remove_rollOut(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.ROLL_OUT);
        }
        #endregion

        #region rollOver
        public static void add_rollOver(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.ROLL_OVER);
        }

        public static void remove_rollOver(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.ROLL_OVER);
        }
        #endregion

        #region selectAll
        public static void add_selectAll(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.SELECT_ALL);
        }

        public static void remove_selectAll(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.SELECT_ALL);
        }
        #endregion

        #region tabChildrenChange
        public static void add_tabChildrenChange(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.TAB_CHILDREN_CHANGE);
        }

        public static void remove_tabChildrenChange(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.TAB_CHILDREN_CHANGE);
        }
        #endregion

        #region tabEnabledChange
        public static void add_tabEnabledChange(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.TAB_ENABLED_CHANGE);
        }

        public static void remove_tabEnabledChange(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.TAB_ENABLED_CHANGE);
        }
        #endregion

        #region tabIndexChange
        public static void add_tabIndexChange(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.TAB_INDEX_CHANGE);
        }

        public static void remove_tabIndexChange(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.TAB_INDEX_CHANGE);
        }
        #endregion

        #region textInput
        public static void add_textInput(InteractiveObject that, Action<TextEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TextEvent.TEXT_INPUT);
        }

        public static void remove_textInput(InteractiveObject that, Action<TextEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TextEvent.TEXT_INPUT);
        }
        #endregion

        #region touchBegin
        public static void add_touchBegin(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TouchEvent.TOUCH_BEGIN);
        }

        public static void remove_touchBegin(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TouchEvent.TOUCH_BEGIN);
        }
        #endregion

        #region touchEnd
        public static void add_touchEnd(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TouchEvent.TOUCH_END);
        }

        public static void remove_touchEnd(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TouchEvent.TOUCH_END);
        }
        #endregion

        #region touchMove
        public static void add_touchMove(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TouchEvent.TOUCH_MOVE);
        }

        public static void remove_touchMove(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TouchEvent.TOUCH_MOVE);
        }
        #endregion

        #region touchOut
        public static void add_touchOut(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TouchEvent.TOUCH_OUT);
        }

        public static void remove_touchOut(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TouchEvent.TOUCH_OUT);
        }
        #endregion

        #region touchOver
        public static void add_touchOver(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TouchEvent.TOUCH_OVER);
        }

        public static void remove_touchOver(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TouchEvent.TOUCH_OVER);
        }
        #endregion

        #region touchRollOut
        public static void add_touchRollOut(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TouchEvent.TOUCH_ROLL_OUT);
        }

        public static void remove_touchRollOut(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TouchEvent.TOUCH_ROLL_OUT);
        }
        #endregion

        #region touchRollOver
        public static void add_touchRollOver(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TouchEvent.TOUCH_ROLL_OVER);
        }

        public static void remove_touchRollOver(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TouchEvent.TOUCH_ROLL_OVER);
        }
        #endregion

        #region touchTap
        public static void add_touchTap(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TouchEvent.TOUCH_TAP);
        }

        public static void remove_touchTap(InteractiveObject that, Action<TouchEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TouchEvent.TOUCH_TAP);
        }
        #endregion

        #endregion







    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.display
{

    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/display/InteractiveObject.html
    // http://livedocs.adobe.com/flex/3/langref/flash/display/InteractiveObject.html#contextMenu
    // http://help.adobe.com/en_US/FlashPlatform/beta/reference/actionscript/3/flash/display/InteractiveObject.html
    [Script(IsNative = true)]
    public class InteractiveObject : DisplayObject
    {
        #region Events
        /// <summary>
        /// Dispatched when the user selects 'Clear' (or 'Delete') from the text context menu.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> clear;

        /// <summary>
        /// Dispatched when a user presses and releases the main button of the user's pointing device over the same InteractiveObject.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> click;

        /// <summary>
        /// Dispatched when the user activates the platform specific accelerator key combination for a copy operation or selects 'Copy' from the text context menu.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> copy;

        /// <summary>
        /// Dispatched when the user activates the platform specific accelerator key combination for a cut operation or selects 'Cut' from the text context menu.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> cut;

        /// <summary>
        /// Dispatched when a user presses and releases the main button of a pointing device twice in rapid succession over the same InteractiveObject when that object's doubleClickEnabled flag is set to true.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> doubleClick;

        /// <summary>
        /// Dispatched after a display object gains focus.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<FocusEvent> focusIn;

        /// <summary>
        /// Dispatched after a display object loses focus.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<FocusEvent> focusOut;

        /// <summary>
        /// Dispatched when the user moves a point of contact over the InteractiveObject instance on a touch-enabled device (such as moving a fingers from left to right over a display object on a mobile phone or tablet with a touch screen).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TransformGestureEvent> gesturePan;

        /// <summary>
        /// Dispatched when the user creates a point of contact with an InteractiveObject instance, then taps on a touch-enabled device (such as placing several fingers over a display object to open a menu and then taps one finger to select a menu item on a mobile phone or tablet with a touch screen).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<PressAndTapGestureEvent> gesturePressAndTap;

        /// <summary>
        /// Dispatched when the user performs a rotation gesture at a point of contact with an InteractiveObject instance (such as touching two fingers and rotating them over a display object on a mobile phone or tablet with a touch screen).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TransformGestureEvent> gestureRotate;

        /// <summary>
        /// Dispatched when the user performs a swipe gesture at a point of contact with an InteractiveObject instance (such as touching three fingers to a screen and then moving them in parallel over a display object on a mobile phone or tablet with a touch screen).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TransformGestureEvent> gestureSwipe;

        /// <summary>
        /// Dispatched when the user presses two points of contact over the same InteractiveObject instance on a touch-enabled device (such as presses and releases two fingers over a display object on a mobile phone or tablet with a touch screen).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<GestureEvent> gestureTwoFingerTap;

        /// <summary>
        /// Dispatched when the user performs a zoom gesture at a point of contact with an InteractiveObject instance (such as touching two fingers to a screen and then quickly spreading the fingers apart over a display object on a mobile phone or tablet with a touch screen).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TransformGestureEvent> gestureZoom;

        /// <summary>
        /// This event is dispatched to any client app that supports inline input with an IME
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<IMEEvent> imeStartComposition;

        /// <summary>
        /// Dispatched when the user presses a key.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<KeyboardEvent> keyDown;

        /// <summary>
        /// Dispatched when the user attempts to change focus by using keyboard navigation.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<FocusEvent> keyFocusChange;

        /// <summary>
        /// Dispatched when the user releases a key.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<KeyboardEvent> keyUp;

        /// <summary>
        /// Dispatched when a user presses the pointing device button over an InteractiveObject instance.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> mouseDown;

        /// <summary>
        /// Dispatched when the user attempts to change focus by using a pointer device.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<FocusEvent> mouseFocusChange;

        /// <summary>
        /// Dispatched when a user moves the pointing device while it is over an InteractiveObject.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> mouseMove;

        /// <summary>
        /// Dispatched when the user moves a pointing device away from an InteractiveObject instance.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> mouseOut;

        /// <summary>
        /// Dispatched when the user moves a pointing device over an InteractiveObject instance.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> mouseOver;

        /// <summary>
        /// Dispatched when a user releases the pointing device button over an InteractiveObject instance.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> mouseUp;

        /// <summary>
        /// Dispatched when a mouse wheel is spun over an InteractiveObject instance.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> mouseWheel;

        /// <summary>
        /// Dispatched when the user activates the platform specific accelerator key combination for a paste operation or selects 'Paste' from the text context menu.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> paste;

        /// <summary>
        /// Dispatched when the user moves a pointing device away from an InteractiveObject instance.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> rollOut;

        /// <summary>
        /// Dispatched when the user moves a pointing device over an InteractiveObject instance.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> rollOver;

        /// <summary>
        /// Dispatched when the user activates the platform specific accelerator key combination for a select all operation or selects 'Select All' from the text context menu.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> selectAll;

        /// <summary>
        /// Dispatched when the value of the object's tabChildren flag changes.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> tabChildrenChange;

        /// <summary>
        /// Dispatched when the object's tabEnabled flag changes.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> tabEnabledChange;

        /// <summary>
        /// Dispatched when the value of the object's tabIndex property changes.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> tabIndexChange;

        /// <summary>
        /// Dispatched when a user enters one or more characters of text.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TextEvent> textInput;

        /// <summary>
        /// Dispatched when the user first contacts a touch-enabled device (such as touches a finger to a mobile phone or tablet with a touch screen).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TouchEvent> touchBegin;

        /// <summary>
        /// Dispatched when the user removes contact with a touch-enabled device (such as lifts a finger off a mobile phone or tablet with a touch screen).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TouchEvent> touchEnd;

        /// <summary>
        /// Dispatched when the user moves the point of contact with a touch-enabled device (such as drags a finger across a mobile phone or tablet with a touch screen).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TouchEvent> touchMove;

        /// <summary>
        /// Dispatched when the user moves the point of contact away from InteractiveObject instance on a touch-enabled device (such as drags a finger from one display object to another on a mobile phone or tablet with a touch screen).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TouchEvent> touchOut;

        /// <summary>
        /// Dispatched when the user moves the point of contact over an InteractiveObject instance on a touch-enabled device (such as drags a finger from a point outside a display object to a point over a display object on a mobile phone or tablet with a touch screen).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TouchEvent> touchOver;

        /// <summary>
        /// Dispatched when the user moves the point of contact away from an InteractiveObject instance on a touch-enabled device (such as drags a finger from over a display object to a point outisde the display object on a mobile phone or tablet with a touch screen).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TouchEvent> touchRollOut;

        /// <summary>
        /// Dispatched when the user moves the point of contact over an InteractiveObject instance on a touch-enabled device (such as drags a finger from a point outside a display object to a point over a display object on a mobile phone or tablet with a touch screen).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TouchEvent> touchRollOver;

        /// <summary>
        /// Dispatched when the user lifts the point of contact over the same InteractiveObject instance on which the contact was initiated on a touch-enabled device (such as presses and releases a finger from a single point over a display object on a mobile phone or tablet with a touch screen).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TouchEvent> touchTap;

        #endregion



        #region Properties
        /// <summary>
        /// Specifies the context menu associated with this object.
        /// </summary>
        public ContextMenu contextMenu { get; set; }

        /// <summary>
        /// Specifies whether the object receives doubleClick events.
        /// </summary>
        public bool doubleClickEnabled { get; set; }

        /// <summary>
        /// Specifies whether this object displays a focus rectangle.
        /// </summary>
        public object focusRect { get; set; }

        /// <summary>
        /// Specifies whether this object receives mouse messages.
        /// </summary>
        public bool mouseEnabled { get; set; }

        /// <summary>
        /// Specifies whether this object is in the tab order.
        /// </summary>
        public bool tabEnabled { get; set; }

        /// <summary>
        /// Specifies the tab ordering of objects in a SWF file.
        /// </summary>
        public int tabIndex { get; set; }

        #endregion




    }
}

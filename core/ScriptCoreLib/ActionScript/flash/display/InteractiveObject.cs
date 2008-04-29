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
    [Script(IsNative = true)]
    public class InteractiveObject : DisplayObject
    {
        #region Events
        /// <summary>
        /// Dispatched when a user presses and releases the main button of the user's pointing device over the same InteractiveObject.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> click;

        
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

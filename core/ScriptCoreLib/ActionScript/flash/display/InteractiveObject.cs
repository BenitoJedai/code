using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/display/InteractiveObject.html
    [Script(IsNative = true)]
    public class InteractiveObject : DisplayObject
    {
        /// <summary>
        /// Dispatched when a user presses and releases the main button of a pointing device twice in rapid succession over the same InteractiveObject when that object's doubleClickEnabled flag is set to true.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> dblClick;

        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> click;

        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> mouseDown;

        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> mouseUp;


        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> mouseMove;

        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> mouseWheel;

        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> mouseOver;

        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> mouseOut;


        [method: Script(NotImplementedHere = true)]
        public event Action<KeyboardEvent> keyDown;

        [method: Script(NotImplementedHere = true)]
        public event Action<KeyboardEvent> keyUp;


        [method: Script(NotImplementedHere = true)]
        public event Action<FocusEvent> focusIn;

        [method: Script(NotImplementedHere = true)]
        public event Action<FocusEvent> focusOut;

        /// <summary>
        /// Specifies the context menu associated with this object.
        /// </summary>
        public ContextMenu contextMenu { get; set; }

        /// <summary>
        /// Specifies whether the object receives doubleClick events.
        /// </summary>
        public Boolean doubleClickEnabled { get; set; }

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



    }
}

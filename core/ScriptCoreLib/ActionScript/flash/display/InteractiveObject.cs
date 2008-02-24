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
        /// Specifies whether this object receives mouse messages.
        /// </summary>
        public bool mouseEnabled { get; set; }

        /// <summary>
        /// Specifies the context menu associated with this object. 
        /// </summary>
        public ContextMenu contextMenu { get; set; }


        /// <summary>
        /// Specifies the button mode of this sprite.
        /// </summary>
        public bool buttonMode { get; set; }




    }
}

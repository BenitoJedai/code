using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/events/TouchEvent.idl

    [Script(HasNoPrototype = true)]
    public class TouchEvent : IEvent
    {
        // Windows 7 Firefox touchscreen

        public int streamId;

        // http://www.w3.org/TR/touch-events/

        public readonly TouchList touches;
        public readonly TouchList changedTouches;
    }


    // http://src.chromium.org/viewvc/blink/trunk/Source/core/dom/TouchList.idl
    [Script(HasNoPrototype = true)]
    public class TouchList
    {
        public readonly uint length;


        public Touch this[uint index]
        {
            get
            {
                return default(Touch);
            }
        }

    }



    // http://src.chromium.org/viewvc/blink/trunk/Source/core/dom/Touch.idl
    [Script(HasNoPrototype = true)]
    public class Touch
    {
        public readonly int screenX;
        public readonly int screenY;
        public readonly int clientX;
        public readonly int clientY;
        public readonly int identifier;
        public readonly int pageX;
        public readonly int pageY;

    }
}

using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true)]
    public class TouchEvent : IEvent
    {
        // Windows 7 Firefox touchscreen

        public int streamId;

        // http://www.w3.org/TR/touch-events/


        [Script(DefineAsStatic = true)]
        public void CaptureTouch()
        {
            var Element = ((IHTMLElement)this.Element);

            Action StopCapture = null;
            Action<IEvent> __ontouchend = null;

            __ontouchend = delegate
            {
                StopCapture();

                Element.ontouchend -= __ontouchend;
            };

            Element.ontouchend += __ontouchend;

            // no reason to keep default behaviour to select text
            this.PreventDefault();

            StopCapture = Element.CaptureMouse();

        }
    }
}

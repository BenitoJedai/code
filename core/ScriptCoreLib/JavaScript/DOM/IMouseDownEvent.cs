using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib;
using System;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true)]
    public class IMouseDownEvent : IEvent
    {
        [Script(DefineAsStatic = true)]
        public void CaptureMouse()
        {
            var Element = ((IHTMLElement)this.Element);

            Action StopCapture = null;
            Action<IEvent> __mouseup = null;

            __mouseup = delegate
            {
                StopCapture();

                Element.onmouseup -= __mouseup;
            };

            Element.onmouseup += __mouseup;

            // no reason to keep default behaviour to select text
            this.PreventDefault();

            StopCapture = Element.CaptureMouse();

        }
    }

}

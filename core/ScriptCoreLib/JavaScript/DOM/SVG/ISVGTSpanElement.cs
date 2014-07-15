using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/svg/SVGTSpanElement.idl

    [Script(InternalConstructor = true)]
    public class ISVGTSpanElement : ISVGElementBase
    {
        internal static ISVGTSpanElement InternalConstructor()
        {
            return (ISVGTSpanElement)new ISVGElementBase(SVGElementNames.tspan);
        }
    }
}

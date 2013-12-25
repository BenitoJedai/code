using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    [Script(InternalConstructor = true)]
    public class ISVGTSpanElement : ISVGElementBase
    {
        internal static ISVGTSpanElement InternalConstructor()
        {
            return (ISVGTSpanElement)new ISVGElementBase(SVGElementNames.tspan);
        }
    }
}

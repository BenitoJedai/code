using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    [Script(InternalConstructor = true)]
    public class ISVGRectElement : ISVGElement
    {
        public ISVGRectElement()
        {
        }

        internal static ISVGRectElement InternalConstructor()
        {
            return (ISVGRectElement)new ISVGElement("rect");
        }
    }
}

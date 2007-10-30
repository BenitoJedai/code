using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    [Script(InternalConstructor = true)]
    public class ISVGRectElement : ISVGElementBase
    {
        public ISVGRectElement()
        {
        }

        internal static ISVGRectElement InternalConstructor()
        {
            return (ISVGRectElement)new ISVGElementBase("rect");
        }
    }
}

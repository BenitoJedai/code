using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    [Script(InternalConstructor = true)]
    public class ISVGGElement : ISVGElement
    {

        public ISVGGElement()
        {
        }

        internal static ISVGGElement InternalConstructor()
        {
            return (ISVGGElement)new ISVGElement("g");
        }
    }
}

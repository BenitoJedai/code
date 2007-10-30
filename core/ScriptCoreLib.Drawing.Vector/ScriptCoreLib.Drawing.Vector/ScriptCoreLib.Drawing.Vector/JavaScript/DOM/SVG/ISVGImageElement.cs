using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    // http://www.xulplanet.com/references/objref/SVGImageElement.html
    [Script(InternalConstructor = true)]
    public class ISVGImageElement : ISVGElement
    {

        public ISVGImageElement()
        {
        }

        public string href
        {
            [Script(DefineAsStatic=true)]
            set
            {
                // http://www.thescripts.com/forum/thread152404.html

                var x = (IElementNS)(object)this;

                x.setAttributeNS("http://www.w3.org/1999/xlink", "href", value);

            }
        }

        internal static ISVGImageElement InternalConstructor()
        {
            return (ISVGImageElement)new ISVGElement("image");
        }
    }
}

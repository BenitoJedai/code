using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    // http://www.xulplanet.com/references/objref/SVGSVGElement.html
    [Script(InternalConstructor = true)]
    public class ISVGSVGElement : ISVGElement
    {
        public ISVGSVGElement()
        {
        }

        internal static ISVGSVGElement InternalConstructor()
        {
            var c = (ISVGSVGElement)new ISVGElement("svg") { type = "image/svg+xml" };

            //c.setAttribute("xmlns", "http://www.w3.org/2000/svg");
            //c.setAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink");

            var x = (IElementNS)(object)c;

            //x.setAttributeNS("http://www.w3.org/2000/xmlns/", "xmlns", "http://www.w3.org/2000/svg");
            x.setAttributeNS("http://www.w3.org/2000/xmlns/", "xmlns:xlink", "http://www.w3.org/1999/xlink");

            return c;
        }
    }
}

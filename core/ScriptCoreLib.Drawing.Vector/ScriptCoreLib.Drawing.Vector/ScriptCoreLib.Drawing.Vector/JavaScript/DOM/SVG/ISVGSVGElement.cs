using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    // http://www.xulplanet.com/references/objref/SVGSVGElement.html
    [Script(InternalConstructor = true)]
    public class ISVGSVGElement : ISVGElementBase
    {
        public ISVGSVGElement()
        {
        }

        internal static ISVGSVGElement InternalConstructor()
        {
            var c = (ISVGSVGElement)new ISVGElementBase("svg") { type = ISVGElementBase.Settings.MimeType };

            //c.setAttribute("xmlns", "http://www.w3.org/2000/svg");
            //c.setAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink");


            //x.setAttributeNS("http://www.w3.org/2000/xmlns/", "xmlns", "http://www.w3.org/2000/svg");
            c.setAttributeNS("http://www.w3.org/2000/xmlns/", "xmlns:xlink", "http://www.w3.org/1999/xlink");

            return c;
        }
    }
}

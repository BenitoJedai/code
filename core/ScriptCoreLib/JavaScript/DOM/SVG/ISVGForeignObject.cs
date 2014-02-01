using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    [Script(InternalConstructor = true)]
    public class ISVGForeignObject : ISVGElementBase
    {
        // http://www.w3.org/TR/SVG11/extend.html#ForeignObjectElement

        internal static ISVGForeignObject InternalConstructor()
        {
            var f = (ISVGForeignObject)new ISVGElementBase(SVGElementNames.foreignObject);

            f.setAttribute("width", "100%");
            f.setAttribute("height", "100%");

            return f;
        }
    }
}

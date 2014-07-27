using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/SVGForeignObjectElement.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/svg/SVGForeignObjectElement.idl

    [Script(InternalConstructor = true)]
    public class ISVGForeignObject : ISVGElementBase
    {
        // tested by?
        // X:\jsc.svn\examples\javascript\svg\SVGHTMLElement\SVGHTMLElement\Application.cs
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\IHTMLDiv.cs
        // X:\jsc.svn\examples\javascript\svg\SVGCSSContent\SVGCSSContent\Application.cs

        // what about shadow dom inside svg inside webgl?
        // X:\jsc.svn\examples\javascript\svg\SVGForShadowDOM\SVGForShadowDOM\Application.cs

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

using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            var c = (ISVGSVGElement)new ISVGElementBase(SVGElementNames.svg)
            {
                type = ISVGElementBase.Settings.MimeType
            };


            //c.setAttribute("xmlns", "http://www.w3.org/2000/svg");
            //c.setAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink");

            // xmlns='http://www.w3.org/2000/svg'

            // X:\jsc.svn\examples\javascript\svg\SVGCSSContent\SVGCSSContent\Application.cs

            c.setAttributeNS("http://www.w3.org/2000/xmlns/", "xmlns", "http://www.w3.org/2000/svg");
            c.setAttributeNS("http://www.w3.org/2000/xmlns/", "xmlns:xlink", "http://www.w3.org/1999/xlink");

            // https://developer.mozilla.org/en-US/docs/Web/CSS/@namespace
            //@namespace url(http://www.w3.org/1999/xhtml);
            //@namespace svg url(http://www.w3.org/2000/svg);

            return c;
        }


    }
}

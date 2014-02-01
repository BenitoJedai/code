using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            return c;
        }

        public static implicit operator IHTMLImage(ISVGSVGElement s)
        {
            // https://bugzilla.mozilla.org/show_bug.cgi?id=368437
            var xml = new XElement("xml", s.AsXElement()).Elements().First();
            var xmlstring = xml.ToString().Replace("<IMG", "<img");

            Console.WriteLine(new { xmlstring });

            var img = new IHTMLImage();
            var url = "data:image/svg+xml;base64," + Convert.ToBase64String(Encoding.UTF8.GetBytes(xmlstring));
            img.src = url;

            //img.InvokeOnComplete(
            //    delegate
            //    {
            //        page.body.css.after.contentImage = img;
            //    }
            //);

            return img;
        }
    }
}

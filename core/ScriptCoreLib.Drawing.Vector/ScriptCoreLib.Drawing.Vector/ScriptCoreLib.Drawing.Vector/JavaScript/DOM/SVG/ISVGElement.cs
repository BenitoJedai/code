using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    [Script(InternalConstructor = true)]
    public class ISVGElement : IHTMLElement
    {
        public static readonly string NS = "http://www.w3.org/2000/svg";

        public string type;
        public ISVGElement ownerSVGElement;
        public ISVGElement viewportSVGElement;

        protected ISVGElement()
        {
        }

        protected static ISVGElement InternalConstructor()
        {
            throw new Exception();
        }

        public ISVGElement(string tag)
        {
        }

        internal static ISVGElement InternalConstructor(string tag)
        {
            return (ISVGElement)(object)
                ((IHTMLDocumentNS)Native.Document).createElementNS(NS, tag);




        }

    }

}

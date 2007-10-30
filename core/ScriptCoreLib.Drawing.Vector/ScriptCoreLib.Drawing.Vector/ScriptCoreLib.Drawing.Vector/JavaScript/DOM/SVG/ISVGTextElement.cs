using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    [Script(InternalConstructor = true)]
    public class ISVGTextElement : ISVGElement
    {
        public string textContent;
        public string x;
        public string y;
        public string fill;

        public ISVGTextElement()
        {
        }

        internal static ISVGTextElement InternalConstructor()
        {
            return (ISVGTextElement)new ISVGElement("text");
        }
    }
}

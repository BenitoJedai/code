using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    // http://www.w3.org/TR/SVG/idl.html
    [Script(InternalConstructor = true)]
    public class ISVGPathElement : ISVGElementBase
    {
        #region InternalConstructor
        public ISVGPathElement()
        {
        }

        internal static ISVGPathElement InternalConstructor()
        {
            return (ISVGPathElement)new ISVGElementBase(SVGElementNames.path);
        }
        #endregion

        public string d
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return (string)this.getAttribute("d");
            }

            [Script(DefineAsStatic = true)]
            set
            {
                this.setAttribute("d", value);

            }
        }
    }
}

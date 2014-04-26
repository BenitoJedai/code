using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    [Script(InternalConstructor = true)]
    public class ISVGPolygonElement : ISVGElementBase
    {
        // X:\jsc.svn\examples\javascript\svg\SVGChartExperiment\SVGChartExperiment\Application.cs

        public string points
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return (string)this.getAttribute("points");
            }

            [Script(DefineAsStatic = true)]
            set
            {
                this.setAttribute("points", value);

            }
        }

        public ISVGPolygonElement()
        {
        }

        internal static ISVGPolygonElement InternalConstructor()
        {
            return (ISVGPolygonElement)new ISVGElementBase(SVGElementNames.polygon);
        }
    }
}

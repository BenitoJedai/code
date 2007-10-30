using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.VML
{
    [Script(InternalConstructor = true)]
    public class IVMLPolyline : IVMLElementBase
    {
        // http://msdn2.microsoft.com/en-us/library/bb229518.aspx
        public string points;

        public IVMLPolyline()
        {
        }

        public static IVMLPolyline InternalConstructor()
        {
            return (IVMLPolyline) new IVMLElementBase("polyline");
        }
    }
}

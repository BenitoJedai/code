using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Drawing;

namespace ScriptCoreLib.JavaScript.Controls.NatureBoy
{
    [Script]
    public class CoordTranslatorBase
    {
        public Func<Point<double>, Point<double>> ConvertMapToCanvas;
        public Func<Point<double>, Point<double>> ConvertCanvasToMap;
    }

    [Script]
    public class CoordTranslator : CoordTranslatorBase
    {
        public CoordTranslator()
        {

        }

        public CoordTranslator(CoordTranslatorBase e)
        {
            this.ConvertCanvasToMap = e.ConvertCanvasToMap;
            this.ConvertMapToCanvas = e.ConvertMapToCanvas;
        }

        Point<double> _Map;
        public Point<double> OnMap
        {
            get
            {
                if (_Map == null)
                    _Map = ConvertCanvasToMap(_Canvas);

                return _Map;
            }
            set { _Map = value; _Canvas = null; }
        }


        Point<double> _Canvas;
        public Point<double> OnCanvas
        {
            get
            {
                if (_Canvas == null)
                    _Canvas = ConvertMapToCanvas(_Map);

                return _Canvas;
            }
            set { _Canvas = value; _Map = null; }
        }

    }
}

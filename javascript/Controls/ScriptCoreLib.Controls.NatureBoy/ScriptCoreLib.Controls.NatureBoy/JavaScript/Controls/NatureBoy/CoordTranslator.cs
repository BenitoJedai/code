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
    public class CoordTranslatorDual
    {
        public readonly CoordTranslator From;
        public readonly CoordTranslator To;

        public CoordTranslatorDual(CoordTranslatorBase _TranslatorBase)
        {
            From = new CoordTranslator(_TranslatorBase);
            To = new CoordTranslator(_TranslatorBase);
        }

        
    }

    [Script]
    public class CoordTranslator
    {
        public CoordTranslator()
        {

        }

        public CoordTranslatorBase TranslatorBase;

        public CoordTranslator(CoordTranslatorBase _TranslatorBase)
        {
            this.TranslatorBase = _TranslatorBase;   
        }

        Point<double> _Map;
        public Point<double> OnMap
        {
            get
            {
                if (_Map == null)
                    _Map = TranslatorBase.ConvertCanvasToMap(_Canvas);

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
                    _Canvas = TranslatorBase.ConvertMapToCanvas(_Map);

                return _Canvas;
            }
            set { _Canvas = value; _Map = null; }
        }

    }
}

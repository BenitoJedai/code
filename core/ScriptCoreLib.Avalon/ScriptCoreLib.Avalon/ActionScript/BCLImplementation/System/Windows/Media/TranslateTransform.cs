using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media
{
    [Script(Implements = typeof(global::System.Windows.Media.TranslateTransform))]
    internal class __TranslateTransform : __Transform
    {
        public event Action InternalValueChanged;

        double InternalX;
        public double X
        {
            get
            {
                return InternalX;
            }
            set
            {
                InternalX = value;
                if (InternalValueChanged != null)
                    InternalValueChanged();

            }
        }
        double InternalY;
        public double Y
        {
            get
            {
                return InternalY;
            }
            set
            {
                InternalY = value;
                if (InternalValueChanged != null)
                    InternalValueChanged();

            }
        }

        public __TranslateTransform(double offsetX, double offsetY)
        {
            this.X = offsetX;
            this.Y = offsetY;
        }
    }
}

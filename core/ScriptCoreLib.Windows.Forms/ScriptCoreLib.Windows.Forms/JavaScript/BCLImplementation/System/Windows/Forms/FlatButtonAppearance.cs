using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.FlatButtonAppearance))]
    internal class __FlatButtonAppearance
    {
        public event Action InternalBorderColorChanged;

        Color InternalBorderColor;
        public Color BorderColor
        {
            get { return InternalBorderColor; }
            set
            {
                InternalBorderColor = value; if (InternalBorderColorChanged != null) InternalBorderColorChanged();
            }
        }
    }
}

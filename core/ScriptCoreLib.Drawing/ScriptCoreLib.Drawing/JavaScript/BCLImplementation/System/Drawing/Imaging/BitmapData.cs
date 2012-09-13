using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Imaging.BitmapData))]
    internal sealed class __BitmapData
    {
        public IntPtr Scan0 { get; set; }

        public ImageData InternalImageData;   
    }
}

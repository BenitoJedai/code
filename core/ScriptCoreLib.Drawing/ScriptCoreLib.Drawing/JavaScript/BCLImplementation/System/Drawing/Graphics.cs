using ScriptCoreLib.Shared.BCLImplementation.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Graphics))]
    internal class __Graphics : __MarshalByRefObject
    {
        public Action<__Bitmap, __Rectangle> AfterDrawImage;

        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle)
        {
            // ?

        }

        public void DrawImage(__Image image, __Rectangle rect)
        {
            var b = image as __Bitmap;
            if (b != null)
            {
                b.InternalContext.putImageData(
                    b.InternalBitmapData.InternalImageData,
                    0,
                    0,
                    0,
                    0,
                    image.Width,
                    image.Height
                );

                if (AfterDrawImage != null)
                    AfterDrawImage(b, rect);
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Bitmap))]
    internal class __Bitmap : __Image
    {
        public IHTMLCanvas InternalCanvas;
        public CanvasRenderingContext2D InternalContext;

        public __Bitmap(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.InternalCanvas = new IHTMLCanvas
            {
                width = width,
                height = height
            };

            this.InternalContext = (CanvasRenderingContext2D)this.InternalCanvas.getContext("2d");
        }

        public __BitmapData InternalBitmapData;

        public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format)
        {
            if (this.InternalBitmapData == null)
            {
                var x = this.InternalContext.getImageData(0, 0, this.Width, this.Height);

                var p = new __IntPtr
                {
                    PointerToUInt8 = x.data
                };

                this.InternalBitmapData = new __BitmapData
                {
                    Scan0 = (IntPtr)(object)p,
                    InternalImageData = x
                };
            }

            return (BitmapData)(object)this.InternalBitmapData;
        }

        public void UnlockBits(BitmapData bitmapdata)
        {
            // nop?
        }
    }
}

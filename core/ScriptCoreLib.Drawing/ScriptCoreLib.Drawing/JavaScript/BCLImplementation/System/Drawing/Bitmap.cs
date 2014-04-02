using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Bitmap))]
    public class __Bitmap : __Image
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140402
        // X:\jsc.svn\examples\javascript\canvas\CanvasFromBytes\CanvasFromBytes\Application.cs
        // http://dotnetframework.org/default.aspx/4@0/4@0/DEVDIV_TFS/Dev10/Releases/RTMRel/ndp/fx/src/CommonUI/System/Drawing/Bitmap@cs/1305376/Bitmap@cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131217-picturebox

        // just a snapshot
        public IHTMLImage InternalImage;

        public static implicit operator __Bitmap(IHTMLImage x)
        {
            // Error	5	The type 'System.Xml.Linq.XElement' is defined in an assembly that is not referenced. You must add a reference to assembly 'System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.	X:\jsc.svn\core\ScriptCoreLib.Drawing\ScriptCoreLib.Drawing\JavaScript\BCLImplementation\System\Drawing\Bitmap.cs	21	13	ScriptCoreLib.Drawing
            if (x == null)
                return null;

            return new __Bitmap { InternalImage = x };
        }

        // the editable
        public IHTMLCanvas InternalCanvas;

        public CanvasRenderingContext2D InternalContext;

        public __Bitmap()
        {

        }

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

        internal __BitmapData InternalBitmapData;

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

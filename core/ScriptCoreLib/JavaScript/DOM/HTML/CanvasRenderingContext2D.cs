using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // is the namespace correct?
    // updated by IDL
    [Script(HasNoPrototype = true, InternalConstructor = true)]
    public class CanvasRenderingContext2D
    {
        // see also 
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\WebGL\WebGLRenderingContext.cs

        public IHTMLCanvas canvas;


        #region Constructor

        public CanvasRenderingContext2D()
        {
            // InternalConstructor
        }

        static CanvasRenderingContext2D InternalConstructor()
        {
            // tested by X:\jsc.svn\examples\javascript\ImageCachedIntoLocalStorageExperiment\ImageCachedIntoLocalStorageExperiment\Application.cs

            var canvas = new IHTMLCanvas();
            var context = (CanvasRenderingContext2D)canvas.getContext("2d");

            return context;
        }

        #endregion


        public void putImageData(ImageData imagedata, float dx, float dy, float dirtyX, float dirtyY, float dirtyWidth, float dirtyHeight)
        {

        }

        public ImageData getImageData(float sx, float sy, float sw, float sh)
        {
            return default(ImageData);
        }

        public void drawImage(IHTMLImage image, float dx, float dy, float dw, float dh)
        {
        }

    }
}

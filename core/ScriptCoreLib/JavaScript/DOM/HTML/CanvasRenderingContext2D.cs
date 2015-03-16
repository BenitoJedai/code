using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/CanvasRenderingContext2D.webidl
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/canvas/nsIDOMCanvasRenderingContext2D.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/CanvasRenderingContext2D.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/CanvasRenderingContext2D.cpp
    // http://msdn.microsoft.com/en-us/library/ie/ff975057(v=vs.85).aspx

    // is the namespace correct?
    // updated by IDL
    [Script(HasNoPrototype = true, InternalConstructor = true)]
    public class CanvasRenderingContext2D
    {
        // X:\jsc.svn\examples\javascript\CanvasMarchinAntsExperiment\CanvasMarchinAntsExperiment\Application.cs

        // see also 
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\WebGL\WebGLRenderingContext.cs

        public IHTMLCanvas canvas;

        // X:\jsc.svn\examples\javascript\synergy\webgl\WebGLCity\WebGLCity\Application.cs

        public string fillStyle;
        public void fillRect(int x, int y, int cx, int cy) { }
        public void clearRect( float x,  float y,  float w,  float h) {}

        // dashed lines
        public void setLineDash(double[] dash) { }
        public double[] getLineDash() { return default(double[]); }
        public double lineDashOffset;



        #region Constructor

        public CanvasRenderingContext2D(int w, int h)
        {
            // InternalConstructor
        }

        static CanvasRenderingContext2D InternalConstructor(int w, int h)
        {
            // tested by X:\jsc.svn\examples\javascript\ImageCachedIntoLocalStorageExperiment\ImageCachedIntoLocalStorageExperiment\Application.cs

            var canvas = new IHTMLCanvas { width = w, height = h };
            var context = (CanvasRenderingContext2D)canvas.getContext("2d");




            return context;
        }

        #endregion



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



        [Script(DefineAsStatic = true)]
        public ImageData getImageData()
        {
            return this.getImageData(0, 0, this.canvas.width, this.canvas.height);
        }

        public ImageData getImageData(float sx, float sy, float sw, float sh)
        {
            return default(ImageData);
        }

        public void drawImage(IHTMLCanvas image, float dx, float dy, float dw, float dh)
        {
        }

        public void drawImage(IHTMLImage image, float dx, float dy, float dw, float dh)
        {
        }

        public void drawImage(IHTMLVideo image, float dx, float dy, float dw, float dh)
        {
        }


        public bool ImageSmoothingEnabled
        {

            [Script(DefineAsStatic = true)]
            set
            {
                // https://github.com/LearnBoost/node-canvas/issues/211
                // update scriptcorelib? why isnt this defined in idl?
                // what about patternQuality ?

                // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
                dynamic context = this;

                context.imageSmoothingEnabled = value;
                context.webkitImageSmoothingEnabled = value;
                context.mozImageSmoothingEnabled = value;
            }
        }



        public static implicit operator IHTMLImage(CanvasRenderingContext2D c)
        {
            var i = new IHTMLImage { src = c.canvas.toDataURL() };

            return i;
        }


		// X:\jsc.svn\examples\javascript\synergy\webgl\WebGLEarthByBjorn\WebGLEarthByBjorn\Application.cs
		// X:\jsc.svn\examples\javascript\chrome\apps\ChromeHTMLTextToGLSLBytes\ChromeHTMLTextToGLSLBytes\Application.cs
		public byte[] bytes
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var i = this.getImageData();

                return i.data;
            }

            [Script(DefineAsStatic = true)]
            set
            {
                if (value != null)
                    if (value.Length == canvas.width * canvas.height * 4)
                    {
                        // tested by 
                        // X:\jsc.svn\examples\javascript\canvas\CanvasFromBytes\CanvasFromBytes\Application.cs

                        var i = this.getImageData();
                        i.data.set(value, 0);
                        this.putImageData(i, 0, 0, 0, 0, canvas.width, canvas.height);
                    }
            }
        }

        [Obsolete]
        public static implicit operator Uint8ClampedArray(CanvasRenderingContext2D c)
        {
            // tested by X:\jsc.svn\examples\javascript\android\CameraPreviewExperiment\CameraPreviewExperiment\Application.cs

            var x = c.getImageData(
                0, 0, c.canvas.width, c.canvas.height
            );

            return x.data;
        }
    }
}

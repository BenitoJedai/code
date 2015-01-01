using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/ImageData.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/ImageData.idl
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/ImageDocument.webidl

    // updated by IDL
    [Script(HasNoPrototype = true)]
    public class ImageData
    {
        // available for service workers?



        // http://www.khronos.org/registry/typedarray/specs/latest/
        //public readonly CanvasPixelArray data;

        // namespace issues. typed array to be moved to where?
        // is jsc using byte[] now as an alias?
        public readonly Uint8ClampedArray data;


        public readonly uint height;
        public readonly uint width;

        public ImageData()
        {

        }
    }
}

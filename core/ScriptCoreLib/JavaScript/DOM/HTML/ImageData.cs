using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // updated by IDL
    [Script(HasNoPrototype = true)]
    public class ImageData
    {
        // http://www.khronos.org/registry/typedarray/specs/latest/
        //public readonly CanvasPixelArray data;

        // namespace issues. typed array to be moved to where?
        public readonly Uint8ClampedArray data;


        public readonly uint height;
        public readonly uint width;

        public ImageData()
        {

        }
    }
}

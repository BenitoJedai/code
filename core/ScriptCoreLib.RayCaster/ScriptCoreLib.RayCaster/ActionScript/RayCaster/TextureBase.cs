using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.RayCaster
{
    using T = UInt32;

    [Script]
    public abstract class TextureBase
    {
        public abstract int Size
        {
            get;
        }

        public Bitmap Bitmap;

        public abstract T this[int x, int y] { get; set; }
    }
}

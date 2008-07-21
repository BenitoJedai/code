using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace RayCaster6.ActionScript
{
    using T = UInt32;

    [Script]
    public abstract class TextureBase
    {
        public abstract int Size
        {
            get;
        }

        public abstract T this[int x, int y] { get; set; }
    }
}

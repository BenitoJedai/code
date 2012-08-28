using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.WebGL;

namespace WebGLPuls.Shaders
{
    using gl = WebGLRenderingContext;


    public class PulsSurface
    {
        public PulsSurface(ISurface s)
        {
            s.onsurface +=
                gl =>
                {

                };
        }
    }
}

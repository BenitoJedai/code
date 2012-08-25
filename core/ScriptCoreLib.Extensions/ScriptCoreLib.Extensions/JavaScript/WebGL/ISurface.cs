using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    /// <summary>
    /// With this interface application can be implemented for 
    /// WebGL and Android OpenGL ES.
    /// </summary>
    public interface ISurface
    {
        event Action<WebGLRenderingContext> onsurface;
        event Action onframe;
        event Action<int, int> onresize;
    }
}

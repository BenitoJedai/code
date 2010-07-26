using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    /// <summary>
    /// The discard keyword is only allowed within fragment shaders. It can be used within a fragment shader to
    /// abandon the operation on the current fragment. This keyword causes the fragment to be discarded and no
    /// updates to any buffers will occur.
    /// 
    /// jsc shall translate <code>throw new discard()</code> to <code>discard</code>
    /// </summary>
    public class discard : Exception
    {

    }
}

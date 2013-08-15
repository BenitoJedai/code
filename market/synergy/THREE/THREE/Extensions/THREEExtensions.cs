using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class THREEExtensions
    {
        public static void setSize(this THREE.WebGLRenderer r, IWindow w = null)
        {
            if (w == null)
                w = Native.window;

            r.setSize(w.Width, w.Height);

        }
    }
}

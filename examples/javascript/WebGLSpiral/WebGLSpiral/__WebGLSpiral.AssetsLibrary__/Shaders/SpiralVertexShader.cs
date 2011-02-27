using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;

namespace WebGLSpiral.Shaders
{
    public class SpiralVertexShader : VertexShader
    {
        public override string ToString()
        {
            return @"
			    attribute vec3 position;
 
			    void main() {
 
				    gl_Position = vec4( position, 1.0 );
 
			    }
            ";
        }
    }
}

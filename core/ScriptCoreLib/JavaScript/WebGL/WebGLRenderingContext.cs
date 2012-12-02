using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    [Script(HasNoPrototype = true)]
    public class WebGLRenderingContext
    {
        public const uint FRAGMENT_SHADER = 0x8B30;
        public const uint VERTEX_SHADER = 0x8B31;

        public WebGLProgram createProgram()
        {
            return default(WebGLProgram);
        }

        public void attachShader(WebGLProgram p, WebGLShader s)
        {
        }

        public void deleteShader(WebGLShader s)
        {
        }

        public void compileShader(WebGLShader s)
        {
        }

        public void shaderSource(WebGLShader s, string e)
        {
        }

        public WebGLShader createShader(uint s)
        {
            return default(WebGLShader);
        }

        public void bufferData(uint target, ArrayBufferView data, uint usage)
        {

        }

        public WebGLUniformLocation getUniformLocation(WebGLProgram p, string name)
        {
            return null;
        }


        public void uniform1f(WebGLUniformLocation location, float x)
        {
        }

        public void uniform2f(WebGLUniformLocation location, float x, float y)
        {
        }

        public void uniform3f(WebGLUniformLocation location, float x, float y, float z)
        {
        }
    }
}

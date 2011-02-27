using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.GLSL;

namespace ScriptCoreLib.JavaScript.Extensions
{
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    [Script]
    public static class WebGLExtensions
    {
        // we are defining extensions for a class generated from IDL

        public static WebGLShader createShader(this WebGLRenderingContext gl, Shader source)
        {
            var type = gl.VERTEX_SHADER;

            if (source is FragmentShader)
                type = gl.FRAGMENT_SHADER;

            var shader = gl.createShader(type);

            gl.shaderSource(shader, source);
            gl.compileShader(shader);

            return shader;
}

        public static void shaderSource(this WebGLRenderingContext gl, WebGLShader shader, Shader source)
        {
            // we could just use object.ToString

            // later we might want to update jsc to create a special interface method for that..

            var code = source.ToString();

            // or jsc could replace a method to "compile" a type into actual source code :)
            // ahead of time or on runtime? (.net 5 code expressions?)

            // how do WebGLShader and Shader relate?

            // will actionscript require the same extensions?

            gl.shaderSource(shader, code);
        }
    }
}

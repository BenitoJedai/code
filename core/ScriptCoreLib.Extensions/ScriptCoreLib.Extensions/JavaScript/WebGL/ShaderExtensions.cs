using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using ScriptCoreLib.JavaScript.WebGL;

// move to ScriptCoreLib.JavaScript.WebGL ?
namespace ScriptCoreLib.JavaScript.WebGL
{
    using gl = WebGLRenderingContext;
    using ScriptCoreLib.Shared.BCLImplementation.GLSL;

    public static class ShaderExtensions
    {
        // we are defining extensions for a class generated from IDL

        public static WebGLProgram createProgram(this WebGLRenderingContext gl, VertexShader v, FragmentShader f)
        {
            var programHandle = gl.createProgram();

            var vs = gl.createShader(v);
            var fs = gl.createShader(f);

            gl.attachShader(programHandle, vs);
            gl.attachShader(programHandle, fs);

            gl.deleteShader(vs);
            gl.deleteShader(fs);
            // http://www.seas.upenn.edu/~pcozzi/OpenGLInsights/OpenGLInsights-ANGLE.pdf
            // are implicitly linked when the shaders are made active.

            return programHandle;
        }

        public static WebGLShader createShader(this WebGLRenderingContext gl, Shader source)
        {
            // jsc/java should pick this up!
            FragmentShader refhack;

            var type = gl.VERTEX_SHADER;

            //if (source is FragmentShader)
            var IsFragmentShader = source is FragmentShader;

            if (IsFragmentShader)
                type = gl.FRAGMENT_SHADER;

            var shader = gl.createShader(type);

       
        
            // we could just use object.ToString

            // later we might want to update jsc to create a special interface method for that..

            var code = source.ToString();

            // or jsc could replace a method to "compile" a type into actual source code :)
            // ahead of time or on runtime? (.net 5 code expressions?)

            // how do WebGLShader and Shader relate?

            // will actionscript require the same extensions?

            gl.shaderSource(shader, code);

            gl.compileShader(shader);

            return shader;
        }

        public static void bufferData(this WebGLRenderingContext gl, uint target, float[] vertices, uint usage)
        {
            gl.bufferData(target, new Float32Array(vertices), usage);
        }

        public static void bufferData(this gl gl, uint target, ushort[] i, uint usage)
        {
            gl.bufferData(target, new Uint16Array(i), usage);
        }

        [Obsolete("Shall revert to vec2 as soon as possible.")]
        public static void uniform2f(this gl gl, WebGLUniformLocation location, __vec2 xy)
        {
            gl.uniform2f(location, xy.x, xy.y);
        }

        [Obsolete("Shall revert to vec2 as soon as possible.")]
        public static void uniform3f(this gl gl, WebGLUniformLocation location, __vec3 xyz)
        {
            gl.uniform3f(location, xyz.x, xyz.y, xyz.z);
        }
    }
}

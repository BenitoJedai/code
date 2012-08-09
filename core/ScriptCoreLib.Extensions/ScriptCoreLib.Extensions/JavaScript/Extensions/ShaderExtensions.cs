﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using ScriptCoreLib.JavaScript.WebGL;

namespace ScriptCoreLib.JavaScript.Extensions
{
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    public static class ShaderExtensions
    {
        // we are defining extensions for a class generated from IDL

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
    }
}

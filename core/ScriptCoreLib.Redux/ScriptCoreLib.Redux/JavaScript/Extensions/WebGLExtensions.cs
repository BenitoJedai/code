extern alias assets;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.GLSL;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.WebGL
{
    //using gl = global::ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    [Script]
    public static class WebGLExtensions
    {
        // X:\jsc.svn\examples\java\webgl\Test\TestInstancedANGLE\TestInstancedANGLE\Application.cs

        // http://blog.tojicode.com/2013/09/whats-coming-in-webgl-20.html

        // or should this live in ScriptCoreLib.Extensions merge assembly?

        // promoted extensions. will this conflict with WebGL2 RenderingContext ?
        public static void drawArraysInstanced(this assets::ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext gl, uint mode, int first, int count, int primcount)
        {
            var ANGLEInstancedArrays = (ANGLE_instanced_arrays)gl.getExtension("ANGLE_instanced_arrays");
            // 0:63ms {{ ANGLEInstancedArrays = [object ANGLEInstancedArrays] }} 

            // tail call inline?
            // can jsc meta inline such extension methods, and cache the first statement to the call site?


            ANGLEInstancedArrays.drawArraysInstancedANGLE(
                mode, first, count, primcount
            );

        }


        public static void vertexAttribDivisor(this assets::ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext gl, uint index, uint divisor)
        {
            var ANGLEInstancedArrays = (ANGLE_instanced_arrays)gl.getExtension("ANGLE_instanced_arrays");
            // 0:63ms {{ ANGLEInstancedArrays = [object ANGLEInstancedArrays] }} 

            // tail call inline?
            // can jsc meta inline such extension methods, and cache the first statement to the call site?


            ANGLEInstancedArrays.vertexAttribDivisorANGLE(
                index, divisor
            );

        }
    }
}

namespace ScriptCoreLib.JavaScript.DOM
{

    [Script]
    public static class LocalMediaStreamExtensions
    {
        // used by?

        // .src instead?
        public static string ToObjectURL(this MediaStream e)
        {
            var src = (string)new IFunction("return window.URL.createObjectURL(this);").apply(e);

            return src;
        }
    }
}

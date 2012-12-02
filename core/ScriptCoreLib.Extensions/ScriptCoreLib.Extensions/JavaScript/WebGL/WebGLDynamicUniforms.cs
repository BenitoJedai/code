using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    using System.ComponentModel;
    using gl = WebGLRenderingContext;

    [Description("Access GLSL uniforms via dynamic dispatch.")]
    public class WebGLDynamicUniforms : DynamicObject
    {
        public WebGLProgram program;
        public gl gl;

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            // cache location

            var isvec2 = value is __vec2;
            if (isvec2)
            {
                var value_vec2 = (__vec2)value;

                gl.uniform2f(
                    gl.getUniformLocation(program, binder.Name),
                    value_vec2
                );

                return true;
            }

            var isvec3 = value is __vec3;
            if (isvec3)
            {
                var value_vec3 = (__vec3)value;

                gl.uniform3f(
                    gl.getUniformLocation(program, binder.Name),
                    value_vec3
                );

                return true;
            }

            gl.uniform1f(gl.getUniformLocation(program, binder.Name), (float)value);
            return true;
        }
    }
}

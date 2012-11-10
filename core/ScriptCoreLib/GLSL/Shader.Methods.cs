using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    partial class Shader
    {
        // see the "new" keyword :)

        // X:\jsc.svn\examples\javascript\CSSShaderGrayScale\CSSShaderGrayScale\web\assets\CSSShaderGrayScale\grayscaleFragmentShader.cs
        static protected mat4 mat4(float f)
        {
            throw new NotImplementedException();
        }

        // "X:\jsc.svn\examples\javascript\CSSShaderSphereSimple\CSSShaderSphereSimple.sln"
        static protected mat4 mat4(
            float x00, float x01, float x02, float x03,
            float x10, float x11, float x12, float x13,
            float x20, float x21, float x22, float x23,
            float x30, float x31, float x32, float x33
            )
        {
            throw new NotImplementedException();
        }

        static protected mat3 mat3(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        static protected vec2 vec2(float x, float y)
        {
            throw new NotImplementedException();
        }


        #region vec3
        static protected vec3 vec3(float x)
        {
            throw new NotImplementedException();
        }

        static protected vec3 vec3(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        static protected vec3 vec3(vec3 v)
        {
            throw new NotImplementedException();
        }

        static protected vec3 vec3(vec4 v)
        {
            throw new NotImplementedException();
        }

        static protected vec3 vec3(vec2 v, float z)
        {
            throw new NotImplementedException();
        }

        static protected vec3 vec3(float z, vec2 v)
        {
            throw new NotImplementedException();
        }
        #endregion



        static protected vec4 vec4(vec2 x, vec2 y)
        {
            throw new NotImplementedException();
        }

        static protected vec4 vec4(vec2 x, float y, float z)
        {
            throw new NotImplementedException();
        }

        static protected vec4 vec4(vec3 x, float y)
        {
            throw new NotImplementedException();
        }

        static protected vec4 vec4(float x, float y, float z, float w)
        {
            throw new NotImplementedException();
        }

        static protected vec4 vec4(float x)
        {
            throw new NotImplementedException();
        }

        // are those valid?

        protected float length(float x) { throw new NotImplementedException(); }

    }
}

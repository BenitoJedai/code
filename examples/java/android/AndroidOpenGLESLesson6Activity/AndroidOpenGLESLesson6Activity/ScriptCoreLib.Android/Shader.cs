﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.opengl;
using ScriptCoreLib;

namespace ScriptCoreLib.Android
{
    [Script(Implements = typeof(ScriptCoreLib.GLSL.Shader))]
    internal class __Shader
    {
        public override string ToString()
        {
            return "/* GLSL shader source */";
        }
    }

    [Script(Implements = typeof(ScriptCoreLib.GLSL.FragmentShader))]
    internal class __FragmentShader : __Shader
    {

    }

    [Script(Implements = typeof(ScriptCoreLib.GLSL.VertexShader))]
    internal class __VertexShader : __Shader
    {

    }




    class WebGLRenderingContext
    {
        // let's try to mimic WebGL api and see how far we get
        // why is Android ES a static reference?

        // whats the most popular api looking at Y:\jsc.svn\examples\javascript\WebGLLesson16\WebGLLesson16\Application.cs ?
        // jsc analyzer shows how many distinct methods reference the api but not yet how many times what is referenced
        // as such some manual guessing eeds to be done.

        public WebGLUniformLocation getUniformLocation(WebGLProgram program, string name)
        {
            return new WebGLUniformLocation { value = GLES20.glGetUniformLocation(program.value, name) };
        }

        public void useProgram(WebGLProgram program)
        {
            GLES20.glUseProgram(program.value);
        }

        public void uniform1i(WebGLUniformLocation u, int p)
        {
            // webgl is using int64 instead of in32. why? is the idl being used correctly?

            GLES20.glUniform1i(u.value, p);
        }

        public void uniform3f(WebGLUniformLocation u, float p1, float p2, float p3)
        {
            GLES20.glUniform3f(u.value, p1, p2, p3);
        }

        public void uniformMatrix4fv(WebGLUniformLocation u, int p1, bool p2, float[] mMVPMatrix, int p3)
        {
            // see also: http://www.opengl.org/sdk/docs/man/xhtml/glUniform.xml
            // see also: http://developer.android.com/reference/android/opengl/GLES20.html#glUniformMatrix4fv(int, int, boolean, float[], int)
            
        //void glUniformMatrix4fv(	GLint  	location,
        //    GLsizei  	count,
        //    GLboolean  	transpose,
        //    const GLfloat * 	value);


            GLES20.glUniformMatrix4fv(u.value, p1, p2, mMVPMatrix, p3);
        }

        public void vertexAttribPointer(WebGLUniformLocation u, int size, int type, bool normalized, int stride, java.nio.FloatBuffer ptr)
        {
            // http://developer.android.com/reference/android/opengl/GLES20.html#glVertexAttribPointer(int, int, int, boolean, int, java.nio.Buffer)

            GLES20.glVertexAttribPointer(u.value, size, type, normalized, stride, ptr);
        }

        // shall we also redefine the constants?

        public void enableVertexAttribArray(WebGLUniformLocation u)
        {
            GLES20.glEnableVertexAttribArray(u.value);
        }
    }

    class WebGLUniformLocation
    {
        public int value;
    }

    class WebGLProgram
    {
        public int value;
    }
}

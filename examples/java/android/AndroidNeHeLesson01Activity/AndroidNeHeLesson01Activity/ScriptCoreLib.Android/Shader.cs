using System;
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




    class __WebGLRenderingContext
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


        // shall we also redefine the constants?



        public int getAttribLocation(WebGLProgram program, string name)
        {
            return GLES20.glGetAttribLocation(program.value, name);
        }

        public void clearColor(float p1, float p2, float p3, float p4)
        {
            GLES20.glClearColor(p1, p2, p3, p4);
        }

        public void viewport(int p1, int p2, int width, int height)
        {
            GLES20.glViewport(p1, p2, width, height);
        }

        internal WebGLProgram createProgram()
        {
            return new WebGLProgram { value = GLES20.glCreateProgram() };
        }

        internal void deleteProgram(WebGLProgram programObject)
        {
            GLES20.glDeleteProgram(programObject.value);
        }

        internal void linkProgram(WebGLProgram programObject)
        {
            GLES20.glLinkProgram(programObject.value);
        }

        internal void clear(int p)
        {
            GLES20.glClear(p);
        }

        internal void vertexAttribPointer(int p1, int p2, int p3, bool p4, int p5, java.nio.FloatBuffer vertices)
        {
            GLES20.glVertexAttribPointer(p1, p2, p3, p4, p5, vertices);
        }

        internal void enableVertexAttribArray(int p)
        {
            GLES20.glEnableVertexAttribArray(p);
        }

        internal void drawArrays(int p1, int p2, int p3)
        {
            GLES20.glDrawArrays(p1, p2, p3);
        }

        internal WebGLShader createShader(int shaderType)
        {
            return new WebGLShader { value = GLES20.glCreateShader(shaderType) };
        }

        internal  void shaderSource(WebGLShader shaderHandle, string shaderSource)
        {
            GLES20.glShaderSource(shaderHandle.value, shaderSource);
        }

        internal void compileShader(WebGLShader shaderHandle)
        {
            GLES20.glCompileShader(shaderHandle.value);
        }

        internal void deleteShader(WebGLShader shaderHandle)
        {
            GLES20.glDeleteShader(shaderHandle.value);
        }

        internal void attachShader(WebGLProgram program, WebGLShader vertexShader)
        {
            GLES20.glAttachShader(program.value, vertexShader.value);
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

    class WebGLShader
    {
        public int value;
    }
}

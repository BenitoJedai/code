using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using android.content;
using android.opengl;
using android.util;
using android.view;
using android.widget;
using java.lang;
using ScriptCoreLib;
using android.app;
using java.nio;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLibJava.Extensions;

namespace ScriptCoreLib.Android.WebGL
{
    // Android L will allow <webview webgl!

    using gl = __WebGLRenderingContext;
    using opengl = GLES20;


    #region ScriptCoreLib.GLSL.Shader
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
    #endregion



    #region __WebGLRenderingContext

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext))]
    public class __WebGLRenderingContext
    {
        // let's try to mimic WebGL api and see how far we get
        // why is Android ES a static reference?

        // whats the most popular api looking at Y:\jsc.svn\examples\javascript\WebGLLesson16\WebGLLesson16\Application.cs ?
        // jsc analyzer shows how many distinct methods reference the api but not yet how many times what is referenced
        // as such some manual guessing eeds to be done.

        public __WebGLUniformLocation getUniformLocation(__WebGLProgram program, string name)
        {
            return new __WebGLUniformLocation { value = GLES20.glGetUniformLocation(program.value, name) };
        }

        public void useProgram(__WebGLProgram program)
        {
            GLES20.glUseProgram(program.value);
        }

        public void uniform1i(__WebGLUniformLocation u, int p)
        {
            GLES20.glUniform1i(u, p);
        }

        public void uniform1f(__WebGLUniformLocation u, float x)
        {
            GLES20.glUniform1f(u, x);
        }

        public void uniform2f(__WebGLUniformLocation u, float x, float y)
        {
            GLES20.glUniform2f(u, x, y);
        }

        public void uniform3f(__WebGLUniformLocation u, float p1, float p2, float p3)
        {
            GLES20.glUniform3f(u, p1, p2, p3);
        }

        public void uniform3fv(__WebGLUniformLocation u, float[] p1)
        {
            GLES20.glUniform3fv(u, p1.Length * 4, p1, 0);
        }


        public void uniformMatrix4fv(__WebGLUniformLocation location, bool transpose, float[] value)
        {
            // see also: http://www.opengl.org/sdk/docs/man/xhtml/glUniform.xml
            // see also: http://developer.android.com/reference/android/opengl/GLES20.html#glUniformMatrix4fv(int, int, boolean, float[], int)

            //void glUniformMatrix4fv(	GLint  	location,
            //    GLsizei  	count,
            //    GLboolean  	transpose,
            //    const GLfloat * 	value);


            GLES20.glUniformMatrix4fv(location.value, /* count */ 1, transpose, value, /* offset */ 0);
        }


        // shall we also redefine the constants?



        public int getAttribLocation(__WebGLProgram program, string name)
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

        internal __WebGLProgram createProgram()
        {
            return new __WebGLProgram { value = GLES20.glCreateProgram() };
        }

        internal void deleteProgram(__WebGLProgram programObject)
        {
            GLES20.glDeleteProgram(programObject);
        }

        internal void linkProgram(__WebGLProgram programObject)
        {
            GLES20.glLinkProgram(programObject);
        }

        internal void clear(uint mask)
        {

            GLES20.glClear((int)mask);
        }

        internal void clearDepth(float f)
        {

            GLES20.glClearDepthf(f);
        }

        internal void enableVertexAttribArray(uint index)
        {
            GLES20.glEnableVertexAttribArray((int)index);
        }

        internal void drawArrays(uint mode, int first, int count)
        {
            GLES20.glDrawArrays((int)mode, first, count);
        }

        internal void drawElements(uint mode, int count, uint type, int offset)
        {
            GLES20.glDrawElements((int)mode, count, (int)type, offset);
        }

        internal __WebGLShader createShader(int shaderType)
        {
            return new __WebGLShader { value = GLES20.glCreateShader(shaderType) };
        }

        internal void shaderSource(__WebGLShader shaderHandle, string shaderSource)
        {
            GLES20.glShaderSource(shaderHandle, shaderSource);
        }

        internal void compileShader(__WebGLShader shaderHandle)
        {
            GLES20.glCompileShader(shaderHandle);
        }

        internal void deleteShader(__WebGLShader shaderHandle)
        {
            GLES20.glDeleteShader(shaderHandle);
        }

        internal void attachShader(__WebGLProgram program, __WebGLShader vertexShader)
        {
            GLES20.glAttachShader(program, vertexShader);
        }


        internal void flush()
        {
            GLES20.glFlush();
        }

        internal void enable(uint cap)
        {
            GLES20.glEnable((int)cap);
        }

        internal void disable(uint cap)
        {
            GLES20.glDisable((int)cap);
        }

        internal void disableVertexAttribArray(int pointPositionHandle)
        {
            GLES20.glDisableVertexAttribArray(pointPositionHandle);
        }

        internal void vertexAttrib3f(int pointPositionHandle, float p1, float p2, float p3)
        {
            GLES20.glVertexAttrib3f(pointPositionHandle, p1, p2, p3);
        }

        internal void texParameteri(uint target, uint pname, int param)
        {
            GLES20.glTexParameteri((int)target, (int)pname, param);
        }

        internal __WebGLTexture createTexture()
        {
            int[] textureHandle = new int[1];

            GLES20.glGenTextures(1, textureHandle, 0);

            return new __WebGLTexture { value = textureHandle[0] };
        }

        internal void bindTexture(uint target, __WebGLTexture textureHandle)
        {
            GLES20.glBindTexture((int)target, textureHandle);
        }

        internal void activeTexture(uint texture)
        {
            GLES20.glActiveTexture((int)texture);
        }



        internal void blendFunc(uint sfactor, uint dfactor)
        {
            GLES20.glBlendFunc((int)sfactor, (int)dfactor);
        }

        internal void generateMipmap(uint target)
        {
            GLES20.glGenerateMipmap((int)target);
        }

        internal void bindAttribLocation(__WebGLProgram programHandle, int i, string p)
        {
            GLES20.glBindAttribLocation(programHandle, i, p);
        }

        internal __WebGLBuffer createBuffer()
        {
            return new __WebGLBuffer();
        }

        internal __WebGLBuffer CurrentBuffer;

        internal void bindBuffer(uint p, __WebGLBuffer buffer)
        {
            CurrentBuffer = buffer;

            if (CurrentBuffer.value > 0)
                opengl.glBindBuffer((int)p, buffer.value);
        }

        internal void bufferData(uint p, __ArrayBufferView v, uint p_2)
        {
            #region f32
            var f32 = v as __Float32Array;
            if (f32 != null)
            {
                if (CurrentBuffer.value == 0)
                {
                    CurrentBuffer.value = f32.InternalFloatArray.Length * 4;

                    bindBuffer(p, CurrentBuffer);
                }

                opengl.glBufferData((int)p, CurrentBuffer.value, f32.InternalFloatBuffer, (int)p_2);
            }
            #endregion

            #region u16
            var u16 = v as __Uint16Array;
            if (u16 != null)
            {
                if (CurrentBuffer.value == 0)
                {
                    CurrentBuffer.value = u16.InternalFloatArray.Length * 2;

                    bindBuffer(p, CurrentBuffer);
                }

                opengl.glBufferData((int)p, CurrentBuffer.value, u16.InternalBuffer, (int)p_2);
            }
            #endregion
        }

        internal void vertexAttribPointer(uint p, int p_2, uint p_3, bool p_4, int p_5, int p_6)
        {
            opengl.glVertexAttribPointer((int)p, p_2, (int)p_3, p_4, p_5, p_6);
        }


    }

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.ArrayBufferView))]
    public class __ArrayBufferView
    {
    }

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.Float32Array))]
    public class __Float32Array : __ArrayBufferView
    {
        public float[] InternalFloatArray;
        public FloatBuffer InternalFloatBuffer;

        public __Float32Array(params float[] array)
        {
            InternalFloatArray = array;
            InternalFloatBuffer = FloatBuffer.wrap(
                array
            );
        }
    }

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.Uint16Array))]
    public class __Uint16Array : __ArrayBufferView
    {
        public ushort[] InternalFloatArray;
        public ShortBuffer InternalBuffer;

        public __Uint16Array(params ushort[] array)
        {
            InternalFloatArray = array;
            InternalBuffer = ShortBuffer.wrap(
                (short[])(object)array
            );
        }
    }


    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLBuffer))]
    public class __WebGLBuffer : __WebGLObject
    {

    }

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLUniformLocation))]
    public class __WebGLUniformLocation : __WebGLObject
    {
        public static implicit operator __WebGLUniformLocation(ScriptCoreLib.JavaScript.WebGL.WebGLUniformLocation e)
        {
            return (__WebGLUniformLocation)(object)e;
        }
    }

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLTexture))]
    public class __WebGLTexture : __WebGLObject
    {
    }


    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLProgram))]
    public class __WebGLProgram : __WebGLObject
    {
        public static implicit operator ScriptCoreLib.JavaScript.WebGL.WebGLProgram(__WebGLProgram e)
        {
            return (ScriptCoreLib.JavaScript.WebGL.WebGLProgram)(object)e;
        }
    }

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLShader))]
    public class __WebGLShader : __WebGLObject
    {
    }

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLObject))]
    public class __WebGLObject
    {
        public int value;

        public static implicit operator int(__WebGLObject e)
        {
            return e.value;
        }
    }

    #endregion


}

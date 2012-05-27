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

namespace ScriptCoreLib.Android
{
    using android.app;
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
            // webgl is using int64 instead of in32. why? is the idl being used correctly?

            GLES20.glUniform1i(u.value, p);
        }

        public void uniform3f(__WebGLUniformLocation u, float p1, float p2, float p3)
        {
            GLES20.glUniform3f(u.value, p1, p2, p3);
        }

        public void uniformMatrix4fv(__WebGLUniformLocation u, int p1, bool p2, float[] mMVPMatrix, int p3)
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
            GLES20.glDeleteProgram(programObject.value);
        }

        internal void linkProgram(__WebGLProgram programObject)
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

        internal __WebGLShader createShader(int shaderType)
        {
            return new __WebGLShader { value = GLES20.glCreateShader(shaderType) };
        }

        internal void shaderSource(__WebGLShader shaderHandle, string shaderSource)
        {
            GLES20.glShaderSource(shaderHandle.value, shaderSource);
        }

        internal void compileShader(__WebGLShader shaderHandle)
        {
            GLES20.glCompileShader(shaderHandle.value);
        }

        internal void deleteShader(__WebGLShader shaderHandle)
        {
            GLES20.glDeleteShader(shaderHandle.value);
        }

        internal void attachShader(__WebGLProgram program, __WebGLShader vertexShader)
        {
            GLES20.glAttachShader(program.value, vertexShader.value);
        }

        internal void enable(int p)
        {
            GLES20.glEnable(p);
        }

        internal void disableVertexAttribArray(__WebGLUniformLocation pointPositionHandle)
        {
            GLES20.glDisableVertexAttribArray(pointPositionHandle.value);
        }

        internal void vertexAttrib3f(__WebGLUniformLocation pointPositionHandle, float p1, float p2, float p3)
        {
            GLES20.glVertexAttrib3f(pointPositionHandle.value, p1, p2, p3);
        }
    }

    public class __WebGLUniformLocation : __WebGLObject
    {
    }

    public class __WebGLProgram : __WebGLObject
    {
    }

    public class __WebGLShader : __WebGLObject
    {
    }

    public class __WebGLObject
    {
        public int value;
    }

    #endregion



    [Description("ScriptCoreLib.Extensions")]
    public static class MyExtensions
    {

        #region gl
        public static __WebGLShader createShader(this gl gl, ScriptCoreLib.GLSL.FragmentShader fragmentShader)
        {
            return gl.createShader(GLES20.GL_FRAGMENT_SHADER, fragmentShader.ToString());
        }

        public static __WebGLShader createShader(this gl gl, ScriptCoreLib.GLSL.VertexShader fragmentShader)
        {
            return gl.createShader(GLES20.GL_VERTEX_SHADER, fragmentShader.ToString());
        }

        private static __WebGLShader createShader(this gl gl, int shaderType, string shaderSource)
        {
            var shaderHandle = gl.createShader(shaderType);


            // Pass in the shader source.
            gl.shaderSource(shaderHandle, shaderSource);

            // Compile the shader.
            gl.compileShader(shaderHandle);

            // Get the compilation status.
            int[] compileStatus = new int[1];
            GLES20.glGetShaderiv(shaderHandle.value, GLES20.GL_COMPILE_STATUS, compileStatus, 0);

            // If the compilation failed, delete the shader.
            if (compileStatus[0] == 0)
            {
                Log.e("createShader", GLES20.glGetShaderInfoLog(shaderHandle.value));
                gl.deleteShader(shaderHandle);
                shaderHandle = null;
            }

            if (shaderHandle == null)
            {
                //throw new RuntimeException("Error creating shader.");
                throw null;
            }

            return shaderHandle;
        }


        public static __WebGLProgram createAndLinkProgram(this gl gl, ScriptCoreLib.GLSL.VertexShader vertexShaderHandle, ScriptCoreLib.GLSL.FragmentShader fragmentShaderHandle, params string[] attributes)
        {
            return gl.createAndLinkProgram(
                gl.createShader(vertexShaderHandle),
                gl.createShader(fragmentShaderHandle),
                attributes
            );
        }

        public static __WebGLProgram createAndLinkProgram(this gl gl, __WebGLShader vertexShaderHandle, __WebGLShader fragmentShaderHandle, params string[] attributes)
        {
            var programHandle = gl.createProgram();


            // Bind the vertex shader to the program.
            gl.attachShader(programHandle, vertexShaderHandle);

            // Bind the fragment shader to the program.
            gl.attachShader(programHandle, fragmentShaderHandle);

            // Bind attributes
            if (attributes != null)
            {
                int size = attributes.Length;
                for (int i = 0; i < size; i++)
                {
                    GLES20.glBindAttribLocation(programHandle.value, i, attributes[i]);
                }
            }

            // Link the two shaders together into a program.
            gl.linkProgram(programHandle);

            // Get the link status.
            int[] linkStatus = new int[1];
            GLES20.glGetProgramiv(programHandle.value, GLES20.GL_LINK_STATUS, linkStatus, 0);

            // If the link failed, delete the program.
            if (linkStatus[0] == 0)
            {
                Log.e("createAndLinkProgram", GLES20.glGetProgramInfoLog(programHandle.value));
                gl.deleteProgram(programHandle);
                programHandle = null;
            }

            //if (programHandle == 0)
            //{
            //    throw null;
            //    //throw new RuntimeException("Error creating program.");
            //}

            return programHandle;
        }

        #endregion




        public static void setText(this TextView e, string value)
        {
            // this cast will work on JVM
            e.setText((java.lang.CharSequence)(object)value);
        }

        public static Context ShowToast(this Context c, string e)
        {
            Toast.makeText(
                  c,
                  (CharSequence)(object)e,
                  Toast.LENGTH_SHORT
              ).show();

            return c;
        }




        public static View AttachTo(this View v, ViewGroup g)
        {
            g.addView(v);

            return v;
        }

        public static Activity ToFullscreen(this Activity e)
        {
            e.requestWindowFeature(Window.FEATURE_NO_TITLE);
            e.getWindow().setFlags(WindowManager_LayoutParams.FLAG_FULLSCREEN, WindowManager_LayoutParams.FLAG_FULLSCREEN);

            return e;
        }
    }

    [Script(Implements = typeof(Attribute))]
    internal class __Attribute
    {

    }

    [Script(Implements = typeof(IDisposable))]
    internal interface __IDisposable
    {
        void Dispose();
    }
}



using android.opengl;
using ScriptCoreLib;
using ScriptCoreLib.Android;
namespace AndroidNeHeLesson01Activity.Library
{
    using opengl = GLES20;
    using gl = __WebGLRenderingContext;

    static class ShaderHelper
    {
        /** 
	 * Helper function to compile a shader.
	 * 
	 * @param shaderType The shader type.
	 * @param shaderSource The shader source code.
	 * @return An OpenGL handle to the shader.
	 */

        #region ScriptCoreLib.JavaScript.Extensions.WebGLExtensions.cs
        public static WebGLShader createShader(this gl gl, ScriptCoreLib.GLSL.FragmentShader fragmentShader)
        {
            return gl.compileShader(GLES20.GL_FRAGMENT_SHADER, fragmentShader.ToAndroidString());
        }

        public static WebGLShader createShader(this gl gl, ScriptCoreLib.GLSL.VertexShader fragmentShader)
        {
            return gl.compileShader(GLES20.GL_VERTEX_SHADER, fragmentShader.ToAndroidString());
        }
        #endregion

        public static WebGLShader compileShader(this gl gl, int shaderType, string shaderSource)
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
                //Log.e(TAG, "Error compiling shader: " + GLES20.glGetShaderInfoLog(shaderHandle));
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

        /**
         * Helper function to compile and link a program.
         * 
         * @param vertexShaderHandle An OpenGL handle to an already-compiled vertex shader.
         * @param fragmentShaderHandle An OpenGL handle to an already-compiled fragment shader.
         * @param attributes Attributes that need to be bound to the program.
         * @return An OpenGL handle to the program.
         */
        public static int createAndLinkProgram(int vertexShaderHandle, int fragmentShaderHandle, params string[] attributes)
        {
            int programHandle = GLES20.glCreateProgram();

            if (programHandle != 0)
            {
                // Bind the vertex shader to the program.
                GLES20.glAttachShader(programHandle, vertexShaderHandle);

                // Bind the fragment shader to the program.
                GLES20.glAttachShader(programHandle, fragmentShaderHandle);

                // Bind attributes
                if (attributes != null)
                {
                    int size = attributes.Length;
                    for (int i = 0; i < size; i++)
                    {
                        GLES20.glBindAttribLocation(programHandle, i, attributes[i]);
                    }
                }

                // Link the two shaders together into a program.
                GLES20.glLinkProgram(programHandle);

                // Get the link status.
                int[] linkStatus = new int[1];
                GLES20.glGetProgramiv(programHandle, GLES20.GL_LINK_STATUS, linkStatus, 0);

                // If the link failed, delete the program.
                if (linkStatus[0] == 0)
                {
                    //Log.e(TAG, "Error compiling program: " + GLES20.glGetProgramInfoLog(programHandle));
                    GLES20.glDeleteProgram(programHandle);
                    programHandle = 0;
                }
            }

            if (programHandle == 0)
            {
                throw null;
                //throw new RuntimeException("Error creating program.");
            }

            return programHandle;
        }
    }
}

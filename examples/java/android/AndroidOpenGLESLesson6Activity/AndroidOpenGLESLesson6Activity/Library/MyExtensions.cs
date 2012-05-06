using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.opengl;
using android.widget;
using java.lang;
using ScriptCoreLib;

namespace AndroidOpenGLESLesson6Activity.Library
{
    public static class MyExtensions
    {
        public static int compileShader(this ScriptCoreLib.GLSL.FragmentShader fragmentShader)
        {
            return ShaderHelper.compileShader(GLES20.GL_FRAGMENT_SHADER, fragmentShader.ToAndroidString());
        }

        public static int compileShader(this ScriptCoreLib.GLSL.VertexShader fragmentShader)
        {
            return ShaderHelper.compileShader(GLES20.GL_VERTEX_SHADER, fragmentShader.ToAndroidString());
        }


        [Obsolete("This is a workaround. object.ToString not yet supported")]
        [Script(OptimizedCode = "return \"\" + e;")]
        public static string ToAndroidString(this object e)
        {
            return "";
        }

        public static void setText(this TextView e, string value)
        {
            // http://stackoverflow.com/questions/1049228/charsequence-vs-string-in-java
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebGLLesson01.Library
{
    public static class ArrayCompatibility
    {
        public static float[] push(this float[] source, float value)
        {
            return source;
        }

        public static float[][] push(this float[][] source, params float[] value)
        {
            return source;
        }
    }
}

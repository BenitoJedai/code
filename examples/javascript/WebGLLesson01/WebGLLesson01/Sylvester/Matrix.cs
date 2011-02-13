using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebGLLesson01.Sylvester
{
    public class Matrix
    {
        public static Matrix I(int e)
        {
            return new Matrix();
        }

        public Matrix x(Matrix e)
        {
            return this;
        }

        public static Matrix Translation(object v)
        {
            return new Matrix();
        }

        public Matrix ensure4x4()
        {
            return this;
        }
    }
}

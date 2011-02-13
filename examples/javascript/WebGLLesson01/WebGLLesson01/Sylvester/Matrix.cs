using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebGLLesson01.Library;

namespace WebGLLesson01.Sylvester
{
    public class Matrix
    {
        public float[][] elements;

        public Matrix(params float[] e)
        {

        }

        //  Identity matrix of size n
        public static Matrix I(int n)
        {
            var r = new Matrix();

            if (n == 4)
                r.ensure4x4();
            else
                throw new NotImplementedException();


            return r;
        }

        public Matrix x(Matrix e)
        {
            return this.multiply(e);
        }

        private Matrix multiply(Matrix e)
        {
            throw new NotImplementedException();
        }



        public Matrix ensure4x4()
        {
            if (this.elements.Length == 4 &&
                this.elements[0].Length == 4)
                return this;

            if (this.elements.Length > 4 ||
                this.elements[0].Length > 4)
                return null;

            for (var i = 0; i < this.elements.Length; i++)
            {
                for (var j = this.elements[i].Length; j < 4; j++)
                {
                    if (i == j)
                        this.elements[i] = this.elements[i].push(1);
                    else
                        this.elements[i] = this.elements[i].push(0);
                }
            }

            for (var i = this.elements.Length; i < 4; i++)
            {
                if (i == 0)
                    this.elements = this.elements.push(1f, 0f, 0f, 0f);
                else if (i == 1)
                    this.elements = this.elements.push(0f, 1f, 0f, 0f);
                else if (i == 2)
                    this.elements = this.elements.push(0f, 0f, 1f, 0f);
                else if (i == 3)
                    this.elements = this.elements.push(0f, 0f, 0f, 1f);
            }

            return this;
        }


        public float[] flatten()
        {
            var result = new List<float>();
            if (this.elements.Length == 0)
                return result.ToArray();


            for (var j = 0; j < this.elements[0].Length; j++)
                for (var i = 0; i < this.elements.Length; i++)
                    result.Add(this.elements[i][j]);

            return result.ToArray();
        }

        public static Matrix Translation(Vector v)
        {
            if (v.elements.Length == 2)
            {
                var r = Matrix.I(3);
                r.elements[2][0] = v.elements[0];
                r.elements[2][1] = v.elements[1];
                return r;
            }

            if (v.elements.Length == 3)
            {
                var r = Matrix.I(4);
                r.elements[0][3] = v.elements[0];
                r.elements[1][3] = v.elements[1];
                r.elements[2][3] = v.elements[2];
                return r;
            }

            throw new InvalidOperationException();
        }
    }



}

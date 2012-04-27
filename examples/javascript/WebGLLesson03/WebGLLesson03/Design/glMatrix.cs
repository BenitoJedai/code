using ScriptCoreLib;
using System;
using ScriptCoreLib.JavaScript.WebGL;
using System.ComponentModel;

namespace WebGLLesson03.Design
{
    [Script]
    [Description("Future versions of JSC will enable seamless integration with JavaScript libraries")]
    internal class __glMatrix : glMatrix
    {
        // this should be generated via assets build :)

        [Script(ExternalTarget = "mat4")]
        static public mat4 mat4;
    }

    [Script(HasNoPrototype = true, ExternalTarget = "mat4")]
    internal class mat4
    {
        // ouch. we cannot use static like we want to :)

        // see also: http://code.google.com/p/glmatrix/wiki/Usage

        public Float32Array create()
        {
            throw new NotImplementedException();
        }

        public void translate(Float32Array mvMatrix, float[] p)
        {
            throw new NotImplementedException();
        }

        public void identity(Float32Array mvMatrix)
        {
            throw new NotImplementedException();
        }

        public void perspective(float p, float p_2, float p_3, float p_4, Float32Array pMatrix)
        {
            throw new NotImplementedException();
        }



        internal void set(Float32Array mvMatrix, Float32Array copy)
        {
            throw new NotImplementedException();
        }

        internal void rotate(Float32Array mvMatrix, float p, float[] p_2)
        {
            throw new NotImplementedException();
        }
    }

 

}
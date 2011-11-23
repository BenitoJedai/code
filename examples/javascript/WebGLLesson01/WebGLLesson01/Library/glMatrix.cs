using ScriptCoreLib;
using System;
using ScriptCoreLib.JavaScript.WebGL;

namespace WebGLLesson01.Library
{


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


    }

    [Script]
    internal class __glMatrix : glMatrix
    {
        // this should be generated via assets build :)

        [Script(ExternalTarget = "mat4")]
        static public mat4 mat4;
    }
    
}
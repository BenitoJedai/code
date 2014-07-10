using ScriptCoreLib.GLSL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CSSShaderGrayScale.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __grayscaleFragmentShader : ScriptCoreLib.GLSL.FragmentShader
    {

        // This uniform value is passed in using CSS.
        [uniform]
        public float amount;

        void main()
        {
            mat4 identityMatrix = mat4(1.0f);

            mat4 grayscaleMatrix = mat4(
                0.33f, 0.33f, 0.33f, 0.0f,
                0.33f, 0.33f, 0.33f, 0.0f,
                0.33f, 0.33f, 0.33f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f);

            css_ColorMatrix[0] = mix(identityMatrix[0], grayscaleMatrix[0], amount);
            css_ColorMatrix[1] = mix(identityMatrix[1], grayscaleMatrix[1], amount);
            css_ColorMatrix[2] = mix(identityMatrix[2], grayscaleMatrix[2], amount);
            css_ColorMatrix[3] = mix(identityMatrix[3], grayscaleMatrix[3], amount);
        }

        // https://dvcs.w3.org/hg/FXTF/raw-file/tip/filters/index.html#shader-processing-model
        /* inherited */
        mat4 css_ColorMatrix;



    }


}

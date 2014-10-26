using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace WebGLEscherDrosteEffect.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __EscherDorsteFragmentShader : ScriptCoreLib.GLSL.FragmentShader
    {
        //precision highp float;

        [uniform]
        sampler2D texture;
        [uniform]
        float t;
        [varying]
        vec2 tc;

        const float pi = 3.1415926535f;

        void main()
        {
            const float p1 = 1.0f;
            const float p2 = 1.0f;
            float u_corner = 2.0f * pi * p2;
            float v_corner = log(256.0f) * p1;
            float diag = sqrt(u_corner * u_corner + v_corner * v_corner);
            float sin_a = v_corner / diag;
            float cos_a = u_corner / diag;
            float scale = diag / 2.0f / pi;

            float a = atan(-tc.y, tc.x);
            float r = sqrt(tc.x * tc.x + tc.y * tc.y);

            float fu = a;
            float fv = log(r);

            float tmp = (fu * cos_a + fv * sin_a) * scale;
            fv = (-fu * sin_a + fv * cos_a) * scale;
            fu = tmp;

            fu /= 2.0f * pi;
            fv /= log(256.0f);

            gl_FragColor = texture2D(texture, vec2(fu, fv - 0.35f - t * 0.1f));
        }


    }
}

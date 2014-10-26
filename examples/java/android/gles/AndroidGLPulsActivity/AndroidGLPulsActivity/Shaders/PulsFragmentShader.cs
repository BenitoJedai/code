using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace AndroidGLPulsActivity.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __PulsFragmentShader : FragmentShader
    {
        [uniform]
        public float t;

        [varying]
        vec2 tc;

        const float BLOWUP = 66.0f; /* 86.0 */
        const float MAXSTEPSHIFT = 8.0f; /* 6.0 */
        const int MAXITERS = 34; /* 26 */

        const float pi = 3.1415926535f;

        float sum(vec3 v)
        {
            return v.x + v.y + v.z;
        }

        int func(vec3 pos, float stepshift)
        {
            vec3 v2 = abs(fract(pos) - vec3(0.5f, 0.5f, 0.5f)) / 2.0f;
            float r = 0.0769f * sin(t * -0.0708f);
            float blowup = BLOWUP / pow(2.0f, stepshift + 8.0f);

            if (sum(v2) - 0.1445 + r < blowup) return 1;
            v2 = vec3(0.25f, 0.25f, 0.25f) - v2;
            if (sum(v2) - 0.1445 - r < blowup) return 2;

            int hue;
            float width;
            if (abs(sum(v2) - 3.0f * r - 0.375f) < 0.03846f)
            {
                width = 0.1445f;
                hue = 4;
            }
            else
            {
                width = 0.0676f;
                hue = 3;
            }

            if (sum(abs(v2.zxy - v2.xyz)) - width < blowup) return hue;

            return 0;

        }

        void main()
        {
            float x = tc.x * 0.5f;
            float y = tc.y * 0.5f;

            float sin_a = sin(t * 0.00564f);
            float cos_a = cos(t * 0.00564f);

            vec3 dir = vec3(x, -y, 0.33594f - x * x - y * y);
            dir = vec3(dir.y, dir.z * cos_a - dir.x * sin_a, dir.x * cos_a + dir.z * sin_a);
            dir = vec3(dir.y, dir.z * cos_a - dir.x * sin_a, dir.x * cos_a + dir.z * sin_a);
            dir = vec3(dir.y, dir.z * cos_a - dir.x * sin_a, dir.x * cos_a + dir.z * sin_a);

            vec3 pos = vec3(0.5f, 1.1875f, 0.875f) + vec3(1.0f, 1.0f, 1.0f) * 0.0134f * t;

            float stepshift = MAXSTEPSHIFT;

            if (fract(pow(x, y) * t * 1000.0f) > 0.5f) pos += dir / pow(2.0f, stepshift);
            else pos -= dir / pow(2.0f, stepshift);

            int i = 0;
            int c = 0;
            for (int xxx = 0; xxx < 64; xxx++)
            {
                c = func(pos, stepshift);
                if (c > 0)
                {
                    stepshift += 1.0f;
                    pos -= dir / pow(2.0f, stepshift);
                }
                else
                {
                    if (stepshift > 0.0f) stepshift -= 1.0f;
                    pos += dir / pow(2.0f, stepshift);
                    i++;
                }

                if (!(stepshift < MAXSTEPSHIFT && i < MAXITERS))
                    break;
            }


            vec3 col = default(vec3);
            if (c == 0) col = vec3(0.0f, 0.0f, 0.0f);
            else if (c == 1) col = vec3(1.0f, 0.5f, 0.0f);
            else if (c == 2) col = vec3(0.0f, 1.0f, 0.0f);
            else if (c == 3) col = vec3(1.0f, 1.0f, 1.0f);
            else if (c == 4) col = vec3(0.5f, 0.5f, 0.5f);

            gl_FragColor = vec4(col * (1.0f - ((float)i - stepshift) / 32.0f), 1.0f);
        }
    }
}

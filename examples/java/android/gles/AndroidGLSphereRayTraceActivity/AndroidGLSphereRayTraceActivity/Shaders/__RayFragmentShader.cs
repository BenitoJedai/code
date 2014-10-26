using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;

namespace AndroidGLSphereRayTraceActivity.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __RayFragmentShader : ScriptCoreLib.GLSL.FragmentShader
    {
        //precision mediump float;

        /*const*/
        vec3 lightDir = vec3(0.577350269f, 0.577350269f, -0.577350269f);
        [varying]
        vec3 vPosition;
        [uniform]
        vec3 cameraPos;
        [uniform]
        vec3 sphere1Center;
        [uniform]
        vec3 sphere2Center;
        [uniform]
        vec3 sphere3Center;

        bool intersectSphere(vec3 center, vec3 lStart, vec3 lDir,
                             out float dist)
        {
            vec3 c = center - lStart;
            float b = dot(lDir, c);
            float d = b * b - dot(c, c) + 1.0f;
            if (d < 0.0)
            {
                dist = 10000.0f;
                return false;
            }

            dist = b - sqrt(d);
            if (dist < 0.0)
            {
                dist = 10000.0f;
                return false;
            }

            return true;
        }

        vec3 lightAt(vec3 N, vec3 V, vec3 color)
        {
            vec3 L = lightDir;
            vec3 R = reflect(-L, N);

            float c = 0.3f + 0.4f * pow(max(dot(R, V), 0.0f), 30.0f) + 0.7f * dot(L, N);

            if (c > 1.0)
            {
                return mix(color, vec3(1.6f, 1.6f, 1.6f), c - 1.0f);
            }

            return c * color;
        }

        bool intersectWorld(vec3 lStart, vec3 lDir, ref vec3 pos,
                            ref vec3 normal, ref vec3 color)
        {
            float d1, d2, d3;
            bool h1, h2, h3;

            h1 = intersectSphere(sphere1Center, lStart, lDir, out d1);
            h2 = intersectSphere(sphere2Center, lStart, lDir, out d2);
            h3 = intersectSphere(sphere3Center, lStart, lDir, out d3);

            if (h1 && d1 < d2 && d1 < d3)
            {
                pos = lStart + d1 * lDir;
                normal = pos - sphere1Center;
                color = vec3(0.0f, 0.0f, 0.9f);
            }
            else if (h2 && d2 < d3)
            {
                pos = lStart + d2 * lDir;
                normal = pos - sphere2Center;
                color = vec3(0.9f, 0.0f, 0.0f);
            }
            else if (h3)
            {
                pos = lStart + d3 * lDir;
                normal = pos - sphere3Center;
                color = vec3(0.0f, 0.9f, 0.0f);
            }
            else if (lDir.y < -0.01)
            {
                pos = lStart + ((lStart.y + 2.7f) / -lDir.y) * lDir;
                if (pos.x * pos.x + pos.z * pos.z > 30.0)
                {
                    return false;
                }
                normal = vec3(0.0f, 1.0f, 0.0f);
                if (fract(pos.x / 5.0f) > 0.5f == fract(pos.z / 5.0f) > 0.5f)
                {
                    color = vec3(1.0f);
                }
                else
                {
                    color = vec3(0.0f);
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        void main()
        {
            vec3 cameraDir = normalize(vPosition - cameraPos);

            vec3 p1 = default(vec3), norm = default(vec3), p2 = default(vec3);
            vec3 col, colT = default(vec3), colM, col3;
            if (intersectWorld(cameraPos, cameraDir, ref p1,
                               ref norm, ref colT))
            {
                col = lightAt(norm, -cameraDir, colT);
                colM = (colT + vec3(0.7f)) / 1.7f;
                cameraDir = reflect(cameraDir, norm);
                if (intersectWorld(p1, cameraDir, ref p2, ref norm, ref colT))
                {
                    col += lightAt(norm, -cameraDir, colT) * colM;
                    colM *= (colT + vec3(0.7f)) / 1.7f;
                    cameraDir = reflect(cameraDir, norm);
                    if (intersectWorld(p2, cameraDir, ref p1, ref norm, ref colT))
                    {
                        col += lightAt(norm, -cameraDir, colT) * colM;
                    }
                }

                gl_FragColor = vec4(col, 1.0f);
            }
            else
            {
                gl_FragColor = vec4(0.0f, 0.0f, 0.0f, 1.0f);
            }
        }
    }
}

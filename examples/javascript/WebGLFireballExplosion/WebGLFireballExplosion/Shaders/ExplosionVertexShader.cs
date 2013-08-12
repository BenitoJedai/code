using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;

namespace WebGLFireballExplosion.Shaders
{
    class __ExplosionVertexShader : VertexShader
    {



        //
        // GLSL textureless classic 3D noise "cnoise",
        // with an RSL-style periodic variant "pnoise".
        // Author:  Stefan Gustavson (stefan.gustavson@liu.se)
        // Version: 2011-10-11
        //
        // Many thanks to Ian McEwan of Ashima Arts for the
        // ideas for permutation and gradient selection.
        //
        // Copyright (c) 2011 Stefan Gustavson. All rights reserved.
        // Distributed under the MIT license. See LICENSE file.
        // https://github.com/ashima/webgl-noise
        //

        vec3 mod289(vec3 x)
        {
            return x - floor(x * (1.0f / 289.0f)) * 289.0f;
        }

        vec4 mod289(vec4 x)
        {
            return x - floor(x * (1.0f / 289.0f)) * 289.0f;
        }

        vec4 permute(vec4 x)
        {
            return mod289(((x * 34.0f) + 1.0f) * x);
        }

        vec4 taylorInvSqrt(vec4 r)
        {
            return 1.79284291400159f - 0.85373472095314f * r;
        }

        vec3 fade(vec3 t)
        {
            return t * t * t * (t * (t * 6.0f - 15.0f) + 10.0f);
        }

        // Classic Perlin noise
        float cnoise(vec3 P)
        {
            vec3 Pi0 = floor(P); // Integer part for indexing
            vec3 Pi1 = Pi0 + vec3(1.0f); // Integer part + 1
            Pi0 = mod289(Pi0);
            Pi1 = mod289(Pi1);
            vec3 Pf0 = fract(P); // Fractional part for interpolation
            vec3 Pf1 = Pf0 - vec3(1.0f); // Fractional part - 1.0
            vec4 ix = vec4(Pi0.x, Pi1.x, Pi0.x, Pi1.x);
            vec4 iy = vec4(Pi0.yy, Pi1.yy);
            vec4 iz0 = Pi0.zzzz;
            vec4 iz1 = Pi1.zzzz;

            vec4 ixy = permute(permute(ix) + iy);
            vec4 ixy0 = permute(ixy + iz0);
            vec4 ixy1 = permute(ixy + iz1);

            vec4 gx0 = ixy0 * (1.0f / 7.0f);
            vec4 gy0 = fract(floor(gx0) * (1.0f / 7.0f)) - 0.5f;
            gx0 = fract(gx0);
            vec4 gz0 = vec4(0.5f) - abs(gx0) - abs(gy0);
            vec4 sz0 = step(gz0, vec4(0.0f));
            gx0 -= sz0 * (step(0.0f, gx0) - 0.5f);
            gy0 -= sz0 * (step(0.0f, gy0) - 0.5f);

            vec4 gx1 = ixy1 * (1.0f / 7.0f);
            vec4 gy1 = fract(floor(gx1) * (1.0f / 7.0f)) - 0.5f;
            gx1 = fract(gx1);
            vec4 gz1 = vec4(0.5f) - abs(gx1) - abs(gy1);
            vec4 sz1 = step(gz1, vec4(0.0f));
            gx1 -= sz1 * (step(0.0f, gx1) - 0.5f);
            gy1 -= sz1 * (step(0.0f, gy1) - 0.5f);

            vec3 g000 = vec3(gx0.x, gy0.x, gz0.x);
            vec3 g100 = vec3(gx0.y, gy0.y, gz0.y);
            vec3 g010 = vec3(gx0.z, gy0.z, gz0.z);
            vec3 g110 = vec3(gx0.w, gy0.w, gz0.w);
            vec3 g001 = vec3(gx1.x, gy1.x, gz1.x);
            vec3 g101 = vec3(gx1.y, gy1.y, gz1.y);
            vec3 g011 = vec3(gx1.z, gy1.z, gz1.z);
            vec3 g111 = vec3(gx1.w, gy1.w, gz1.w);

            vec4 norm0 = taylorInvSqrt(vec4(dot(g000, g000), dot(g010, g010), dot(g100, g100), dot(g110, g110)));
            g000 *= norm0.x;
            g010 *= norm0.y;
            g100 *= norm0.z;
            g110 *= norm0.w;
            vec4 norm1 = taylorInvSqrt(vec4(dot(g001, g001), dot(g011, g011), dot(g101, g101), dot(g111, g111)));
            g001 *= norm1.x;
            g011 *= norm1.y;
            g101 *= norm1.z;
            g111 *= norm1.w;

            float n000 = dot(g000, Pf0);
            float n100 = dot(g100, vec3(Pf1.x, Pf0.yz));
            float n010 = dot(g010, vec3(Pf0.x, Pf1.y, Pf0.z));
            float n110 = dot(g110, vec3(Pf1.xy, Pf0.z));
            float n001 = dot(g001, vec3(Pf0.xy, Pf1.z));
            float n101 = dot(g101, vec3(Pf1.x, Pf0.y, Pf1.z));
            float n011 = dot(g011, vec3(Pf0.x, Pf1.yz));
            float n111 = dot(g111, Pf1);

            vec3 fade_xyz = fade(Pf0);
            vec4 n_z = mix(vec4(n000, n100, n010, n110), vec4(n001, n101, n011, n111), fade_xyz.z);
            vec2 n_yz = mix(n_z.xy, n_z.zw, fade_xyz.y);
            float n_xyz = mix(n_yz.x, n_yz.y, fade_xyz.x);
            return 2.2f * n_xyz;
        }

        // Classic Perlin noise, periodic variant
        float pnoise(vec3 P, vec3 rep)
        {
            vec3 Pi0 = mod(floor(P), rep); // Integer part, modulo period
            vec3 Pi1 = mod(Pi0 + vec3(1.0f), rep); // Integer part + 1, mod period
            Pi0 = mod289(Pi0);
            Pi1 = mod289(Pi1);
            vec3 Pf0 = fract(P); // Fractional part for interpolation
            vec3 Pf1 = Pf0 - vec3(1.0f); // Fractional part - 1.0
            vec4 ix = vec4(Pi0.x, Pi1.x, Pi0.x, Pi1.x);
            vec4 iy = vec4(Pi0.yy, Pi1.yy);
            vec4 iz0 = Pi0.zzzz;
            vec4 iz1 = Pi1.zzzz;

            vec4 ixy = permute(permute(ix) + iy);
            vec4 ixy0 = permute(ixy + iz0);
            vec4 ixy1 = permute(ixy + iz1);

            vec4 gx0 = ixy0 * (1.0f / 7.0f);
            vec4 gy0 = fract(floor(gx0) * (1.0f / 7.0f)) - 0.5f;
            gx0 = fract(gx0);
            vec4 gz0 = vec4(0.5f) - abs(gx0) - abs(gy0);
            vec4 sz0 = step(gz0, vec4(0.0f));
            gx0 -= sz0 * (step(0.0f, gx0) - 0.5f);
            gy0 -= sz0 * (step(0.0f, gy0) - 0.5f);

            vec4 gx1 = ixy1 * (1.0f / 7.0f);
            vec4 gy1 = fract(floor(gx1) * (1.0f / 7.0f)) - 0.5f;
            gx1 = fract(gx1);
            vec4 gz1 = vec4(0.5f) - abs(gx1) - abs(gy1);
            vec4 sz1 = step(gz1, vec4(0.0f));
            gx1 -= sz1 * (step(0.0f, gx1) - 0.5f);
            gy1 -= sz1 * (step(0.0f, gy1) - 0.5f);

            vec3 g000 = vec3(gx0.x, gy0.x, gz0.x);
            vec3 g100 = vec3(gx0.y, gy0.y, gz0.y);
            vec3 g010 = vec3(gx0.z, gy0.z, gz0.z);
            vec3 g110 = vec3(gx0.w, gy0.w, gz0.w);
            vec3 g001 = vec3(gx1.x, gy1.x, gz1.x);
            vec3 g101 = vec3(gx1.y, gy1.y, gz1.y);
            vec3 g011 = vec3(gx1.z, gy1.z, gz1.z);
            vec3 g111 = vec3(gx1.w, gy1.w, gz1.w);

            vec4 norm0 = taylorInvSqrt(vec4(dot(g000, g000), dot(g010, g010), dot(g100, g100), dot(g110, g110)));
            g000 *= norm0.x;
            g010 *= norm0.y;
            g100 *= norm0.z;
            g110 *= norm0.w;
            vec4 norm1 = taylorInvSqrt(vec4(dot(g001, g001), dot(g011, g011), dot(g101, g101), dot(g111, g111)));
            g001 *= norm1.x;
            g011 *= norm1.y;
            g101 *= norm1.z;
            g111 *= norm1.w;

            float n000 = dot(g000, Pf0);
            float n100 = dot(g100, vec3(Pf1.x, Pf0.yz));
            float n010 = dot(g010, vec3(Pf0.x, Pf1.y, Pf0.z));
            float n110 = dot(g110, vec3(Pf1.xy, Pf0.z));
            float n001 = dot(g001, vec3(Pf0.xy, Pf1.z));
            float n101 = dot(g101, vec3(Pf1.x, Pf0.y, Pf1.z));
            float n011 = dot(g011, vec3(Pf0.x, Pf1.yz));
            float n111 = dot(g111, Pf1);

            vec3 fade_xyz = fade(Pf0);
            vec4 n_z = mix(vec4(n000, n100, n010, n110), vec4(n001, n101, n011, n111), fade_xyz.z);
            vec2 n_yz = mix(n_z.xy, n_z.zw, fade_xyz.y);
            float n_xyz = mix(n_yz.x, n_yz.y, fade_xyz.x);
            return 2.2f * n_xyz;
        }

        [attribute]
        vec3 position;
        [attribute]
        vec3 normal;
        [attribute]
        vec2 uv;

        [varying]
        vec2 vUv;
        [varying]
        vec3 vReflect;
        [varying]
        vec3 pos;
        [varying]
        float ao;
        [uniform]
        float time;
        [uniform]
        float weight;
        [varying]
        float d;

        float stripes(float x, float f)
        {
            float PI = 3.14159265358979323846264f;
            float t = .5f + .5f * sin(f * 2.0f * PI * x);
            return t * t - .5f;
        }

        float turbulence(vec3 p)
        {
            float w = 100.0f;
            float t = -.5f;
            for (float f = 1.0f; f <= 10.0f; f++)
            {
                float power = pow(2.0f, f);
                t += abs(pnoise(vec3(power * p), vec3(10.0f, 10.0f, 10.0f)) / power);
            }
            return t;
        }

        [uniform]
        mat4 modelViewMatrix;
        [uniform]
        mat4 projectionMatrix;

        [uniform]
        mat4 modelMatrix;

        [uniform] vec3 cameraPosition;

        void main()
        {

            vUv = uv;

            vec4 mPosition = modelMatrix * vec4(position, 1.0f);
            vec3 nWorld = normalize(mat3(modelMatrix[0].xyz, modelMatrix[1].xyz, modelMatrix[2].xyz) * normal);
            vReflect = normalize(reflect(normalize(mPosition.xyz - cameraPosition), nWorld));

            pos = position;
            //float noise = .3 * pnoise( 8.0 * vec3( normal ) );
            float noise = 10.0f * -.10f * turbulence(.5f * normal + time);
            //float noise = - stripes( normal.x + 2.0 * turbulence( normal ), 1.6 );

            float displacement = -weight * noise;
            displacement += 5.0f * pnoise(0.05f * position + vec3(2.0f * time), vec3(100.0f));

            ao = noise;
            vec3 newPosition = position + normal * vec3(displacement);
            gl_Position = projectionMatrix * modelViewMatrix * vec4(newPosition, 1.0f);

        }




    }
}

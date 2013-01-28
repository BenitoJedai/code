using ScriptCoreLib.GLSL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CSSShaderSphereSimple.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __sphere_simpleVertexShader : ScriptCoreLib.GLSL.VertexShader
    {


        // Built-in attributes

        [attribute]
        vec4 a_position;
        [attribute]
        vec2 a_texCoord;

        // Built-in uniforms

        [uniform]
        mat4 u_projectionMatrix;

        // Uniforms passed in from CSS

        [uniform]
        public float amount;
        [uniform]
        public float sphereRadius;
        [uniform]
        public vec3 lightPosition;

        // Varyings

        [varying]
        float v_light;

        // Constants

        const float PI = 3.1415f;

        // Construct perspective matrix

        vec3 computeSpherePosition(vec2 uv, float r)
        {

            var p = default(vec3);

            float fi = uv.x * PI * 2.0f;
            float th = uv.y * PI;

            p.x = r * sin(th) * cos(fi);
            p.y = r * sin(th) * sin(fi);
            p.z = r * cos(th);

            return p;

        }

        // Main

        void main()
        {

            vec4 position = a_position;

            // Map plane to sphere using UV coordinates

            vec3 sphere = computeSpherePosition(a_texCoord, sphereRadius);

            // Blend plane and sphere

            position.xyz = mix(position.xyz, sphere, amount);

            // Set vertex position

            gl_Position = u_projectionMatrix * position;

            // Compute lighting

            vec3 lightPositionNormalized = normalize(lightPosition);

            vec3 planeNormal = lightPositionNormalized;
            vec3 sphereNormal = normalize(position.xyz);

            vec3 normal = normalize(mix(planeNormal, sphereNormal, amount));

            float light = max(dot(normal, lightPositionNormalized), 0.0f);

            // Pass in varyings

            v_light = light;

        }

    }
}

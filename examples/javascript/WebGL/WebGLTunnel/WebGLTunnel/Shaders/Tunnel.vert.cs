using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLTunnel.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __TunnelVertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        // -- "attribute": read-only per-vertex data, available only within vertex shaders.
        // -- the vertex position (x, y, z)
        [attribute]
        vec3 aVertexPosition;
        // -- the vertex color (r, g, b, a)
        [attribute]
        vec4 aVertexColor;
        // -- the texture coordinate for this vertex (u, v)
        [attribute]
        vec2 aTextureCoord;

        // -- "uniform": remains constant during each shader execution.
        // -- model-view matrix
        [uniform]
        mat4 uMVMatrix;
        // -- projection matrix
        [uniform]
        mat4 uPMatrix;
        // -- the time value (changes every frame)
        [uniform]
        float fTime;

        // -- "varying": output of the vertex shader that corresponds to read-only interpolated input
        //    of the fragment shader
        // -- the color
        [varying]
        vec4 vColor;
        // -- the texture coordinates
        [varying]
        vec2 vTextureCoord;

        void main()
        {
            vec3 pos = aVertexPosition;
            // -- displace the x coordinate based on the time and the z position 
            pos.x += cos(fTime + (aVertexPosition.z / 4.0f));
            // -- displace the y coordinate based on the time and the z position 
            pos.y += sin(fTime + (aVertexPosition.z / 4.0f));
            // -- transform the vertex 
            gl_Position = uPMatrix * uMVMatrix * vec4(pos, 1.0f);
            // -- copy the vertex color
            vColor = aVertexColor;
            // -- displace the texture's y (v) coordinate. This gives the illusion of movement.
            vec2 texcoord = aTextureCoord;
            texcoord.y = texcoord.y + (fTime);
            // -- copy the texture coordinate 
            vTextureCoord = texcoord;
        }
    }
}

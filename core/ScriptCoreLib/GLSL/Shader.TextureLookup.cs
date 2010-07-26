using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    partial class Shader
    {
        protected vec4 texture2D(sampler2D sampler, vec2 coord) { throw new NotImplementedException(); }
        protected vec4 texture2D(sampler2D sampler, vec2 coord, float bias) { throw new NotImplementedException(); }
        protected vec4 texture2DProj(sampler2D sampler, vec3 coord) { throw new NotImplementedException(); }
        protected vec4 texture2DProj(sampler2D sampler, vec3 coord, float bias) { throw new NotImplementedException(); }
        protected vec4 texture2DProj(sampler2D sampler, vec4 coord) { throw new NotImplementedException(); }
        protected vec4 texture2DProj(sampler2D sampler, vec4 coord, float bias) { throw new NotImplementedException(); }
        protected vec4 texture2DLod(sampler2D sampler, vec2 coord, float lod) { throw new NotImplementedException(); }
        protected vec4 texture2DProjLod(sampler2D sampler, vec3 coord, float lod) { throw new NotImplementedException(); }
        protected vec4 texture2DProjLod(sampler2D sampler, vec4 coord, float lod) { throw new NotImplementedException(); }

        protected vec4 textureCube(samplerCube sampler, vec3 coord) { throw new NotImplementedException(); }
        protected vec4 textureCube(samplerCube sampler, vec3 coord, float bias) { throw new NotImplementedException(); }
        protected vec4 textureCubeLod(samplerCube sampler, vec3 coord, float lod) { throw new NotImplementedException(); }
    }
}

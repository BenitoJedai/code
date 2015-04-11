using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
	[Script(Implements = typeof(ScriptCoreLib.GLSL.FragmentShader))]
	internal class __FragmentShader : __Shader
	{
		// http://stackoverflow.com/questions/26440996/is-it-possible-to-raytrace-with-glsl-while-using-opengl-in-a-normal-way
		// Raytracing is a golbal illumination model. This means that access to the whole scene with all objects and light sources is required, for obvious reasons. The rendering pipleine OpenGL implements never sees more than a single primitive at a time. While one can do raytricing on the GPU, this is completely different from just writing some shader that can work as a drop-in replacement while keeping the rest of the rendering algorithm the same. You need to completely reogranize the data, and the GL drawing functions are of no good use at all anymore.
		// http://www.openglsuperbible.com/example-code/

		// tested by?

		// https://github.com/jteeuwen/spirv
		// https://www.khronos.org/registry/spir-v/specs/1.0/SPIRV.pdf
		// http://www.g-truc.net/post-0714.html
		// https://www.khronos.org/vulkan
		// https://github.com/jteeuwen/spirv
		// https://www.khronos.org/assets/uploads/developers/library/2014-siggraph-bof/OpenGL-Ecosystem-BOF_Aug14.pdf
		// https://www.khronos.org/spir
		// https://www.khronos.org/registry/spir-v/papers/WhitePaper.pdf
		// https://www.khronos.org/registry/spir-v/specs/1.0/GLSL.std.450.pdf
		// https://www.khronos.org/registry/spir-v/specs/1.0/OpenCL.std.21.pdf


		// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyColumns\ChromeShaderToyColumns\Shaders\ProgramFragmentShader.cs

		// https://www.khronos.org/opengles/sdk/tools/Reference-Compiler/
		//.vert - a vertex shader
		//.tesc - a tessellation control shader
		//.tese - a tessellation evaluation shader
		//.geom - a geometry shader
		//.frag - a fragment shader
		//.comp - a compute shader
	}
}

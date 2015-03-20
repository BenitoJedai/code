using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.GLSL
{
    // http://www.khronos.org/registry/gles/specs/2.0/GLSL_ES_Specification_1.0.17.pdf



    /// <summary>
    /// Uniforms are used to pass information to your shaders other than vertices. 
    /// http://codeflow.org/entries/2012/apr/25/webgl-statistics-and-the-state-of-webgl-html5/
    /// A uniform is a global GLSL variable declared with the "uniform" storage qualifier. These act as parameters that the user of a shader program can pass to that program. They are stored in a program object.
    /// http://www.opengl.org/wiki/GLSL_Uniforms
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    [Description("Vertex Shader max: 254 vectors, 1016 floats, Fragment Shader max: 29 vectors, 116 floats")]
    public sealed class uniform : Attribute
    {
		// 4.3.9 Interface Blocks

		// http://webglreport.com/?v=2
		// notice webgl has renamed the overloads

		//'uniform1ui' : 'uniform',
		//'uniform2ui' : 'uniform',
		//'uniform3ui' : 'uniform',
		//'uniform4ui' : 'uniform',
		//'uniform1uiv' : 'uniform',
		//'uniform2uiv' : 'uniform',
		//'uniform3uiv' : 'uniform',
		//'uniform4uiv' : 'uniform',
		//'uniformMatrix2x3fv' : 'uniform',
		//'uniformMatrix3x2fv' : 'uniform',
		//'uniformMatrix2x4fv' : 'uniform',
		//'uniformMatrix4x2fv' : 'uniform',
		//'uniformMatrix3x4fv' : 'uniform',
		//'uniformMatrix4x3fv' : 'uniform',
	}
}

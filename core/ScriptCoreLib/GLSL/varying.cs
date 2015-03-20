using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.GLSL
{
	// http://www.khronos.org/registry/gles/specs/2.0/GLSL_ES_Specification_1.0.17.pdf



	/// <summary>
	/// Varyings are used to pass values from the vertex shader to the fragment shader. 
	/// http://codeflow.org/entries/2012/apr/25/webgl-statistics-and-the-state-of-webgl-html5/
	/// Varying variables provide the interface between the vertex shader, the fragment shader, and the fixed
	/// functionality between them.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	[Description("Recommended max: 8 varyings, 32 floats")]
	[Obsolete]
	public sealed class varying : Attribute
	{
		// 4.3 Storage Qualifiers
		// compatibility profile only and vertex and fragment languages only; same
		//as out when in a vertex shader and same as in when in a fragment shader
	}

}

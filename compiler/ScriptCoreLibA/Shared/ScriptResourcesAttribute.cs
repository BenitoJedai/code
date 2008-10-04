using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared
{
	/// <summary>
	/// allows a project to have embedded resources, which are to be extracted on the compile time when jsc compiler is finished.
	/// Current implementation has a special folder called 'web' into which all output generated to. This means that the embedded resources should be within a folder called \web\ in your solution.
	/// </summary>
	[global::System.AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class ScriptResourcesAttribute : Attribute
	{
		public string Value;

		public ScriptResourcesAttribute(string e)
		{
			this.Value = e;
		}

		public ScriptResourcesAttribute()
		{
			// when used on a class we should include
			// all string constants as paths to resources
		}

	}
}

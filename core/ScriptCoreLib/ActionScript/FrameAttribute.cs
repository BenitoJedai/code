using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
	// http://nondocs.blogspot.com/2007/04/metadataframe_22.html
	// http://blogs.adobe.com/rgonzalez/2006/06/modular_applications_part_2.html

	[Script(IsNative = true)]
	public sealed class FrameAttribute : Attribute
	{
		public FrameAttribute()
		{
				
		}

		public FrameAttribute(Type e)
		{
			// note that this does not support namespace renaming

			this.factoryClass = e.FullName;
		}

		public string factoryClass;
		public string extraClass;
	}
}

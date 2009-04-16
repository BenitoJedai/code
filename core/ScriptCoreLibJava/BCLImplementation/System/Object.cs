using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using javax.common.runtime;

namespace ScriptCoreLibJava.BCLImplementation.System
{

	[Script(Implements = typeof(global::System.Object),
		ImplementationType = typeof(object))]
	internal class __Object
	{
		[Script(ExternalTarget = "toString")]
		public new string ToString()
		{
			return default(string);
		}
	}
}

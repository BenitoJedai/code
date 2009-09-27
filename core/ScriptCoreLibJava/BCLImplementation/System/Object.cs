using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using javax.common.runtime;
using System;

namespace ScriptCoreLibJava.BCLImplementation.System
{

	[Script(

		Implements = typeof(global::System.Object),
		ImplementationType = typeof(global::java.lang.Object)

		//Implements = typeof(global::System.Object),
		//ImplementationType = typeof(object)
		
		)]
	internal class __Object
	{
		[Script(ExternalTarget = "toString")]
		public new string ToString()
		{
			return default(string);
		}

		[Script(DefineAsStatic = true)]
		new public Type GetType()
		{
			return __Type.GetTypeFromValue(this);
		}

	
	}


}

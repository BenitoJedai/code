using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Activator))]
	internal class __Activator
	{
		public static object CreateInstance(Type type)
		{
			return type.ToClassToken().CreateType();
		}
	}
}

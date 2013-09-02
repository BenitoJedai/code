using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices
{
	[Script(Implements = typeof(global::System.Runtime.CompilerServices.RuntimeHelpers))]
	internal class __RuntimeHelpers
	{
        public static object GetObjectValue(object obj)
        {
            return obj;
        }
	}
}

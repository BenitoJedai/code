using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Diagnostics
{
	// http://referencesource.microsoft.com/#mscorlib/system/diagnostics/debugger.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Diagnostics/Debugger.cs
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Diagnostics/Debugger.cs

	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Diagnostics/Debug.cs

	[Script(Implements = typeof(global::System.Diagnostics.Debugger))]
	internal class __Debugger
	{
		public static bool IsAttached
		{
			get
			{
				// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131224

				// would we know if the inspector is opened yet?

				return true;
			}
		}


		// or would it mean, switch to debugger context, and the break?
		// would async need to scan and do an implict context switch for us?
		// would jsc rewriter want to replace Break into .break opcode?

		[Script(OptimizedCode = "debugger;")]
		public static void Break()
		{

		}


	}
}

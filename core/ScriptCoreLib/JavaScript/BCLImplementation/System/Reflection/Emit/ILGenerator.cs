using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.Emit
{
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Reflection/Emit/ILGenerator.cs

	[Script(Implements = typeof(global::System.Reflection.Emit.ILGenerator))]
	public sealed class __ILGenerator
	{
		// would we need it for Edit And Continue scenarius, where trivial methods get regenerated in the client, not on the server?

		// the other option is for the server to prepare the patch as javascript and send it over at sync?
	}
}

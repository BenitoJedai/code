using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Runtime.Serialization
{
	// http://referencesource.microsoft.com/#mscorlib/system/runtime/serialization/streamingcontext.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Runtime/Serialization/StreamingContext.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Runtime/Serialization/StreamingContext.cs

	[Script(Implements = typeof(global::System.Runtime.Serialization.StreamingContext))]
    public class StreamingContext
	{
		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150401
		// is this an important type to understand cross machine sync?

	}
}

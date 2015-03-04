using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/marshalbyrefobject.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/MarshalByRefObject.cs

    [Script(Implements = typeof(global::System.MarshalByRefObject))]
	public class __MarshalByRefObject
	{
		// https://github.com/dotnet/coreclr/blob/master/src/vm/remoting.h
		// https://github.com/dotnet/coreclr/blob/master/src/vm/remoting.cpp

	}
}

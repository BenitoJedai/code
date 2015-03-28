using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\DateTime.cs

	[Script(Implements = typeof(IComparable))]
    internal interface __IComparable
    {
        // Methods
        int CompareTo(object obj);
    }


    [Script(Implements = typeof(IComparable<>))]
    internal interface __IComparable<T>
    {
        // Methods
        int CompareTo(T obj);
    }



}

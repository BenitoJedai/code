using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Collections
{
	[Script(Implements = typeof(ICollection))]
	internal interface __ICollection : __IEnumerable
	{
		
		int Count { get; }
		
	}
}

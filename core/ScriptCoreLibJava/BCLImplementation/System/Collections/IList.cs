using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections
{
	[Script(Implements = typeof(global::System.Collections.IList))]
	internal interface __IList : __ICollection, __IEnumerable
	{
	}
}

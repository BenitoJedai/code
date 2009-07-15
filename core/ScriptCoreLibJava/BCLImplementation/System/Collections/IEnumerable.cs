using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections
{
	[Script(Implements = typeof(global::System.Collections.IEnumerable))]
	internal interface __IEnumerable
	{
		IEnumerator GetEnumerator();
	}
}

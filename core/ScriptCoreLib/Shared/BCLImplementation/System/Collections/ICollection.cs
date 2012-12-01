using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections
{
	[Script(Implements = typeof(ICollection))]
	public interface __ICollection : __IEnumerable
	{

		int Count { get; }
		//object SyncRoot { get; }

		//void CopyTo(global::System.Array array, int index);
	}
}

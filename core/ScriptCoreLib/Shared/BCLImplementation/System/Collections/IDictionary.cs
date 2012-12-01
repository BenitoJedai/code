using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(IDictionary))]
	public interface __IDictionary : __ICollection, __IEnumerable
    {
        object this[object key] { get; set; }
    }
}

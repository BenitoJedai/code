using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(global::System.Collections.IDictionary))]
    internal interface __IDictionary : __ICollection, __IEnumerable
    {
        object this[object key] { get; set; }
    }
}

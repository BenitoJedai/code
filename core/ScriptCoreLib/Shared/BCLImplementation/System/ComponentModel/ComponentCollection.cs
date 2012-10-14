using ScriptCoreLib.Shared.BCLImplementation.System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.ComponentCollection))]
    internal class __ComponentCollection : __ReadOnlyCollectionBase
    {
        public readonly ArrayList InternalElements = new ArrayList();
    }
}

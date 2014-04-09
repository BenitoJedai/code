using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.IListSource))]
    internal interface __IListSource
    {
        bool ContainsListCollection { get; }

        IList GetList();
    }
}

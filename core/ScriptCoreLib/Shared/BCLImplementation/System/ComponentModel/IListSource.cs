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
        // http://msdn.microsoft.com/en-us/library/system.componentmodel.ilistsource(v=vs.110).aspx

        bool ContainsListCollection { get; }

        IList GetList();
    }
}

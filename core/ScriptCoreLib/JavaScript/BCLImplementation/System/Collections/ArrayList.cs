using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections
{
    using ScriptCoreLib.JavaScript.DOM;

    [Script(Implements = typeof(global::System.Collections.ArrayList))]
    internal class __ArrayList
    {
        readonly IArray<object> items = new IArray<object>();

        public void Add(object e)
        {
            items.push(e);
        }
    }
}

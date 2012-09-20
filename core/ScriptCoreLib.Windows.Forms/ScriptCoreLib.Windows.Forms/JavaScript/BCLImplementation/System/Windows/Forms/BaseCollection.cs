using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.BaseCollection))]
    internal class __BaseCollection
    {
        public virtual int Count { get; set; }
    }
}

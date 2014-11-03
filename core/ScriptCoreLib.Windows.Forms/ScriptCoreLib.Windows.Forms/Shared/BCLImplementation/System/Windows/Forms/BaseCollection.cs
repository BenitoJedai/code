using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms
{
    // http://referencesource.microsoft.com/#System.Windows.Forms/ndp/fx/src/winforms/Managed/System/WinForms/BaseCollection.cs

    [Script(Implements = typeof(global::System.Windows.Forms.BaseCollection))]
    public class __BaseCollection
    {
        public virtual int Count { get; set; }
    }
}

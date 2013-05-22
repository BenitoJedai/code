using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.IDataObject))]
    internal interface __IDataObject
    {
        object GetData(string format);

        string[] GetFormats();
    }
}

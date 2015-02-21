using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.IDataObject))]
    internal interface __IDataObject : global::System.Windows.Forms.IDataObject
    {
        // used where?
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\Control\Control.cs

        // x:\jsc.svn\examples\javascript\forms\formstreeviewdrag\formstreeviewdrag\applicationcontrol.cs
        object GetData(Type format);
        object GetData(string format);

        string[] GetFormats();
    }
}

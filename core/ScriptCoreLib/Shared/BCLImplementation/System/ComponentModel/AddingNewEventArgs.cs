using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.AddingNewEventArgs))]
    internal class __AddingNewEventArgs : __EventArgs
    {
        // script: error JSC1000: No implementation found for this native method, please implement [System.ComponentModel.AddingNewEventHandler.Invoke(System.Object, System.ComponentModel.AddingNewEventArgs)]

        public __AddingNewEventArgs(object newObject)
        {
            this.NewObject = newObject;
        }

        public object NewObject { get; set; }
    }
}
